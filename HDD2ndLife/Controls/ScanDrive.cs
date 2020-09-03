#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="ScanDrive.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 - 2020 Simon Coghlan (Aka Smurf-IV)
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//   any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program. If not, see http://www.gnu.org/licenses/.
//  </copyright>
//  <summary>
//  Url: https://github.com/Smurf-IV/HDD2ndLife
//  Email: https://github.com/Smurf-IV
//  </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using Clifton.Collections.Generic;
using HDD2ndLife.Annotations;
using HDD2ndLife.WMI;

using NLog;

using RawDiskLib;

namespace HDD2ndLife.Controls
{
    internal enum ScanType
    {
        Unknown,
        Read,
        Write,
        Verify,
        Pass2
    };

    internal class ScanDrive : INotifyPropertyChanged
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static int AVERAGE_BUFFER_SIZE = 64;
        internal static int DISK_BUFFER_SIZE = 1024 * 1024;  // Seems to be "Optimum" for Sata II HDD and PCIE cards, giving > 95% read utilisation

        private readonly ScanType scanType;
        private readonly bool failFirst;
        private readonly int speedOption;
        private readonly string deviceId;
        private readonly CancellationTokenSource cancelTokenSrc;
        private CircularList<long> lastTimeRemaining;
        private CircularList<double> lastSpeedInMBytesPerSec;

        public ScanDrive(ScanType scanType, Action completionAction, bool failFirst, int speedOption, string deviceId)
        {
            this.scanType = scanType;
            this.failFirst = failFirst;
            this.speedOption = speedOption;
            this.deviceId = deviceId;
            cancelTokenSrc = new CancellationTokenSource();
            cancelTokenSrc.Token.Register(completionAction);
            lastTimeRemaining = new CircularList<long>(AVERAGE_BUFFER_SIZE);
            lastTimeRemaining.SetAll(0);
            lastSpeedInMBytesPerSec = new CircularList<double>(AVERAGE_BUFFER_SIZE);
            lastSpeedInMBytesPerSec.SetAll(0);
        }

        public double SpeedInMBytesPerSec => lastSpeedInMBytesPerSec.Average();
        public TimeSpan TimeRemaining => TimeSpan.FromTicks((long)lastTimeRemaining.Average());

        private int CurrentCluster;
        private string phase;

        public void Cancel()
        {
            Log.Info(@"Cancelling");
            cancelTokenSrc.Cancel();
            Phase = @"Errored";
        }

        public void Start()
        {
            try
            {
                Phase = @"Initialising";
                Log.Debug(@"Creating Disk interface");
                var disk = new RawDisk(deviceId, FileAccess.ReadWrite);
                Log.Info(@"Setting [{0}] offline", deviceId);
                if (!DiskExSet.SetOnline(disk.DiskHandle, false, false))
                {
                    Log.Warn(@"Unable to take disk offline");
                }
                Task.Run(() => ProcessDrive(disk));

            }
            catch (Exception e)
            {
                Log.Error(e);
                Cancel();
            }

        }

        private void ProcessDrive(RawDisk disk)
        {
            switch (scanType)
            {
                case ScanType.Unknown:
                    break;
                case ScanType.Read:
                    PerformRead(disk);
                    break;
                case ScanType.Write:
                    if (PerformWrite(disk))
                    {
                        PerformRead(disk);
                    }
                    break;
                case ScanType.Verify:
                    if (PerformWrite(disk, 0xAA))
                    {
                        PerformRead(disk, 0xAA);
                    }
                    break;
                case ScanType.Pass2:
                    if (PerformWrite(disk, 0xAA))
                    {
                        if (PerformRead(disk, 0xAA))
                        {
                            if (PerformWrite(disk, 0x55))
                            {
                                PerformRead(disk, 0x55);
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            cancelTokenSrc.Cancel();
        }

        public string Phase
        {
            get => phase;
            set
            {
                if (phase != value)
                {
                    phase = value;
                    OnPropertyChanged(nameof(Phase));
                }
            }
        }

        // 0xAA = 1010 1010 pattern
        private bool PerformWrite(RawDisk disk, byte pattern = 0xAA)
        {
            Phase = $@"Writing 0x{pattern:X}";
            var multiplier = DISK_BUFFER_SIZE / disk.ClusterSize;
            var buffer = InitByteArray(pattern, disk.ClusterSize * multiplier);
            var sw = new Stopwatch();
            var clusterCount = disk.ClusterCount;    // prevent this from being calculated each time
            CurrentCluster = 0;
            for (; CurrentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; CurrentCluster += multiplier)
            {
                SetScaledClusterStatus(CurrentCluster / multiplier, BlockStatus.Writing);
                if (!WriteGroup(disk, sw, buffer, multiplier, CurrentCluster, clusterCount))
                {
                    SetScaledClusterStatus(CurrentCluster / multiplier, BlockStatus.Failed);
                    if (failFirst)
                    {
                        // TODO: Add to the list of failed sectors
                        return false;
                    }
                }
            }
            // Do the last few sectors of the drive
            multiplier = 1;
            buffer = new byte[disk.ClusterSize /* * multiplier*/];
            for (; CurrentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; CurrentCluster += multiplier)
            {
                SetScaledClusterStatus(CurrentCluster / multiplier, BlockStatus.Writing);
                if (!WriteGroup(disk, sw, buffer, multiplier, CurrentCluster, clusterCount))
                {
                    SetScaledClusterStatus(CurrentCluster / multiplier, BlockStatus.Failed);
                    if (failFirst)
                    {
                        // TODO: Add to the list of failed sectors
                        return false;
                    }
                }
            }

            return true;
        }

        public Action<long, BlockStatus> SetScaledClusterStatus;

        private bool WriteGroup(RawDisk disk, Stopwatch sw, byte[] buffer, int multiplier, long currentCluster, long clusterCount)
        {
            try
            {
                sw.Restart();
                disk.WriteClusters(buffer, currentCluster);
                sw.Stop();
                var elapsedTicks = sw.Elapsed.Ticks;
                lastSpeedInMBytesPerSec.Value = (buffer.Length / elapsedTicks) * 10; // Sw.Elapsed.Ticks are always the same distance
                lastSpeedInMBytesPerSec.Next();
                lastTimeRemaining.Value = (clusterCount - currentCluster) * elapsedTicks / multiplier;
                lastTimeRemaining.Next();
                return true;
            }
            catch (IOException ex1)
            {
                int errorCode1 = GetWin32ErrorCode(ex1);
                Log.Error(ex1, new Win32Exception(errorCode1).Message);
            }
            catch (Exception ex2)
            {
                Log.Error(ex2);
            }

            return false;
        }

        private bool PerformRead(RawDisk disk, byte? pattern = null)
        {
            Phase = @"Reading";
            try
            {
                var multiplier = DISK_BUFFER_SIZE / disk.ClusterSize;
                var buffer = new byte[disk.ClusterSize * multiplier];
                var bufferLength = buffer.Length;
                ReadOnlySpan<byte> checkPattern = null;
                if (pattern.HasValue)
                {
                    Phase = $@"Reading [0x{pattern:X}]";
                    checkPattern = InitByteArray(pattern.Value, bufferLength);
                }

                var sw = new Stopwatch();
                var clusterCount = disk.ClusterCount;    // prevent this from being calculated each time
                CurrentCluster = 0;
                for (; CurrentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; CurrentCluster += multiplier)
                {
                    var currentCluster = CurrentCluster / multiplier;
                    SetScaledClusterStatus(currentCluster, BlockStatus.Reading);
                    if (!ReadGroup(disk, sw, buffer, multiplier, clusterCount, bufferLength))
                    {
                        SetScaledClusterStatus(currentCluster, BlockStatus.Failed);
                        if (failFirst)
                            return false;
                    }
                    // TODO: When doing a verify the disk read utilisation drops from 98% down to 85%
                    else if (checkPattern != null)
                    {
                        SetScaledClusterStatus(currentCluster, BlockStatus.Validating);
                        if (!checkPattern.SequenceEqual(buffer))
                        {
                            SetScaledClusterStatus(currentCluster, BlockStatus.Failed);
                            if (failFirst)
                                return false;
                            // TODO: Add to the list of failed sectors
                        }
                        else
                        {
                            SetScaledClusterStatus(currentCluster, BlockStatus.Passed);
                        }
                    }
                    else
                    {
                        SetScaledClusterStatus(currentCluster, BlockStatus.Passed);
                    }
                }
                // Do the last few sectors of the drive
                multiplier = 1;
                buffer = new byte[disk.ClusterSize /* * multiplier*/];
                bufferLength = buffer.Length;
                if (pattern.HasValue)
                {
                    checkPattern = InitByteArray(pattern.Value, bufferLength);
                }
                for (; CurrentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; CurrentCluster += multiplier)
                {
                    var currentCluster = CurrentCluster / multiplier;
                    SetScaledClusterStatus(currentCluster, BlockStatus.Reading);
                    if (!ReadGroup(disk, sw, buffer, multiplier, clusterCount, bufferLength))
                    {
                        SetScaledClusterStatus(currentCluster, BlockStatus.Failed);
                        if (failFirst)
                            return false;
                    }
                    else if (checkPattern != null)
                    {
                        SetScaledClusterStatus(currentCluster, BlockStatus.Validating);
                        if (!checkPattern.SequenceEqual(buffer))
                        {
                            SetScaledClusterStatus(currentCluster, BlockStatus.Failed);
                            if (failFirst)
                                return false;
                        }
                        else
                        {
                            SetScaledClusterStatus(currentCluster, BlockStatus.Passed);
                        }
                    }
                    else
                    {
                        SetScaledClusterStatus(currentCluster, BlockStatus.Passed);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                // TODO: Add to the list of failed sectors
            }

            return false;
        }

        private bool ReadGroup(RawDisk disk, Stopwatch sw, byte[] buffer, int multiplier, long clusterCount, int bufferLength)
        {
            try
            {
                sw.Restart();
                double read = disk.ReadClusters(buffer, 0, CurrentCluster, multiplier);
                sw.Stop();
                var elapsedTicks = sw.Elapsed.Ticks;
                lastSpeedInMBytesPerSec.Value = (read / elapsedTicks) * 10; // Sw.Elapsed.Ticks are always the same distance
                lastSpeedInMBytesPerSec.Next();
                lastTimeRemaining.Value = (clusterCount - CurrentCluster) * elapsedTicks / multiplier;
                lastTimeRemaining.Next();
                if (read < bufferLength)
                {
                    throw new FileLoadException($@"read != buffer.Length. [{read}] != [{buffer.Length}]");
                }

                return true;
            }
            catch (IOException ex1)
            {
                int errorCode1 = GetWin32ErrorCode(ex1);
                Log.Error(ex1, new Win32Exception(errorCode1).Message);
            }
            catch (Exception ex2)
            {
                Log.Error(ex2);
            }

            return false;
        }

        private static ushort GetWin32ErrorCode(IOException ex)
        {
            var hResult = GetExceptionHResult(ex);
            // The Win32 error code is stored in the 16 first bits of the value
            return (ushort)(hResult & 0x0000FFFF);
        }

        private static int GetExceptionHResult(IOException ex)
        {
            var hResult = ex.GetType().GetProperty("HResult", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return (int)hResult.GetValue(ex, null);
        }

        [DllImport(@"msvcrt.dll",EntryPoint = @"memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern IntPtr MemSet(IntPtr dest, int c, int count);

        //If you need super speed, calling out to M$ memset optimized method using P/invoke
        private static byte[] InitByteArray(byte fillWith, int size)
        {
            var arrayBytes = new byte[size];
            var gch = GCHandle.Alloc(arrayBytes, GCHandleType.Pinned);
            MemSet(gch.AddrOfPinnedObject(), fillWith, arrayBytes.Length);
            return arrayBytes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}