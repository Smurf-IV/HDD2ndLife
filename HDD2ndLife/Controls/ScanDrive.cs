﻿#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="ScanDrive.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 - 2025 Simon Coghlan (Aka Smurf-IV)
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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using AverageBuddy;

using HDD2ndLife.Annotations;
using HDD2ndLife.WMI;

using NLog;

using RawDiskLib;

namespace HDD2ndLife.Controls;

internal enum ScanType
{
    Unknown,
    Read,
    Write,
    Verify,
    Pass2
}


internal class ScanDrive : INotifyPropertyChanged
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private static int AVERAGE_BUFFER_SIZE = 500;
    internal static long DISK_BUFFER_SIZE = 1024 * 1024;  // Seems to be "Optimum" for Sata II HDD and PCIE cards, giving > 95% read utilisation

    private readonly ScanType scanType;
    private readonly bool failFirst;
    private readonly int speedOption;
    private readonly string deviceId;
    private readonly CancellationTokenSource cancelTokenSrc;

    private Averager<long> lastTimeRemaining;
    private Averager<double> lastSpeedInBytesPerSec;

    public ScanDrive(ScanType scanType, Action completionAction, bool failFirst, int speedOption, string deviceId)
    {
        this.scanType = scanType;
        this.failFirst = failFirst;
        this.speedOption = speedOption;
        this.deviceId = deviceId;
        cancelTokenSrc = new CancellationTokenSource();
        cancelTokenSrc.Token.Register(completionAction);
        lastTimeRemaining = new Averager<long>(AVERAGE_BUFFER_SIZE, 0L);
        lastSpeedInBytesPerSec = new Averager<double>(AVERAGE_BUFFER_SIZE, 0.0);
    }

    public double SpeedInBytesPerSec => lastSpeedInBytesPerSec.Average();
    public TimeSpan TimeRemaining => TimeSpan.FromTicks((long)lastTimeRemaining.Average());

    private long currentCluster;
    private string phase;

    public void Cancel()
    {
        Log.Info(@"Cancelling");
        cancelTokenSrc.Cancel();
        Phase = @"Errored";
    }

    public Action<long, BlockStatus> SetScaledClusterStatus { get; set; }

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
    public double Percent { get; private set; }

    private void SetCurrentProgress(string curPhase, long diskClusterSize)
    {
        Percent = currentCluster * 1.0 / diskClusterSize;
        Phase = $@"{curPhase} [{Percent:P}]";
    }

    // 0xAA = 1010 1010 pattern
    private bool PerformWrite(RawDisk disk, byte pattern = 0xAA)
    {
        var phaseWrite = $@"Writing 0x{pattern:X}";
        var multiplier = DISK_BUFFER_SIZE / disk.ClusterSize;
        try
        {
            var buffer = InitByteArray(pattern, disk.ClusterSize * multiplier);
            var sw = new Stopwatch();
            var clusterCount = disk.ClusterCount;    // prevent this from being calculated each time
            currentCluster = 0;
            for (; currentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; currentCluster += multiplier)
            {
                var currentScaledCluster = currentCluster / multiplier;
                SetScaledClusterStatus(currentScaledCluster, BlockStatus.Writing);
                SetCurrentProgress(phaseWrite, clusterCount);
                if (!WriteGroup(disk, sw, buffer, multiplier, currentCluster, clusterCount))
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Failed);
                    if (failFirst)
                    {
                        // TODO: Add to the list of failed sectors
                        return false;
                    }
                }
                else
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.WriteDone);
                }
            }
            // Do the last few sectors of the drive
            multiplier = 1;
            buffer = new byte[disk.ClusterSize /* * multiplier*/];
            for (; currentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; currentCluster += multiplier)
            {
                var currentScaledCluster = currentCluster / multiplier;
                SetScaledClusterStatus(currentScaledCluster, BlockStatus.Writing);
                SetCurrentProgress(phaseWrite, clusterCount);
                if (!WriteGroup(disk, sw, buffer, multiplier, currentCluster, clusterCount))
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Failed);
                    if (failFirst)
                    {
                        // TODO: Add to the list of failed sectors
                        return false;
                    }
                }
                else
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.WriteDone);
                }
            }
        }
        catch (Exception e)
        {
            Log.Error(e, @"[{2}]: CurrentCluster [{0}] attempting to write to scaled [{1}]",
                currentCluster, currentCluster / multiplier, phaseWrite);
            // TODO: Add to the list of failed sectors
        }
        return true;
    }

    private bool WriteGroup(RawDisk disk, Stopwatch sw, byte[] buffer, long multiplier, long currentCluster, long clusterCount)
    {
        try
        {
            GC.TryStartNoGCRegion(buffer.Length);
            sw.Restart();
            disk.WriteClusters(buffer, currentCluster);
            sw.Stop();
            GC.EndNoGCRegion();
            var timeSpan = sw.Elapsed;
            lastSpeedInBytesPerSec.Add((buffer.Length / timeSpan.TotalSeconds));
            lastTimeRemaining.Add((clusterCount - currentCluster) * timeSpan.Ticks / multiplier);
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
        var phaseRead = @"Reading";
        var multiplier = DISK_BUFFER_SIZE / disk.ClusterSize;
        try
        {
            var buffer = new byte[disk.ClusterSize * multiplier];
            var bufferLength = buffer.Length;
            ReadOnlySpan<byte> checkPattern = null;
            if (pattern.HasValue)
            {
                phaseRead = $@"Reading [0x{pattern:X}]";
                checkPattern = InitByteArray(pattern.Value, bufferLength);
            }

            var sw = new Stopwatch();
            var clusterCount = disk.ClusterCount;    // prevent this from being calculated each time
            currentCluster = 0;
            for (; currentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; currentCluster += multiplier)
            {
                var currentScaledCluster = currentCluster / multiplier;
                SetScaledClusterStatus(currentScaledCluster, BlockStatus.Reading);
                SetCurrentProgress(phaseRead, clusterCount);
                if (!ReadGroup(disk, sw, buffer, multiplier, clusterCount, bufferLength))
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Failed);
                    if (failFirst)
                    {
                        return false;
                    }
                }
                // TODO: When doing a verify the disk read utilisation drops from 98% down to 85%
                else if (checkPattern != null!)
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Validating);
                    if (!checkPattern.SequenceEqual(buffer))
                    {
                        SetScaledClusterStatus(currentScaledCluster, BlockStatus.Failed);
                        if (failFirst)
                        {
                            return false;
                        }
                        // TODO: Add to the list of failed sectors
                    }
                    else
                    {
                        SetScaledClusterStatus(currentScaledCluster, BlockStatus.Passed);
                    }
                }
                else
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Passed);
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
            for (; currentCluster < clusterCount && !cancelTokenSrc.IsCancellationRequested; currentCluster += multiplier)
            {
                var currentScaledCluster = currentCluster / multiplier;
                SetScaledClusterStatus(currentScaledCluster, BlockStatus.Reading);
                SetCurrentProgress(phaseRead, clusterCount);
                if (!ReadGroup(disk, sw, buffer, multiplier, clusterCount, bufferLength))
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Failed);
                    if (failFirst)
                    {
                        return false;
                    }
                }
                else if (checkPattern != null!)
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Validating);
                    if (!checkPattern.SequenceEqual(buffer))
                    {
                        SetScaledClusterStatus(currentScaledCluster, BlockStatus.Failed);
                        if (failFirst)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        SetScaledClusterStatus(currentScaledCluster, BlockStatus.Passed);
                    }
                }
                else
                {
                    SetScaledClusterStatus(currentScaledCluster, BlockStatus.Passed);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, @"[{2}]: CurrentCluster [{0}] attempting to read to scaled [{1}]",
                currentCluster, currentCluster / multiplier, phaseRead);
            // TODO: Add to the list of failed sectors
        }

        return false;
    }

    private bool ReadGroup(RawDisk disk, Stopwatch sw, byte[] buffer, long multiplier, long clusterCount, int bufferLength)
    {
        try
        {
            GC.TryStartNoGCRegion(bufferLength);
            sw.Restart();
            double read = disk.ReadClusters(buffer, 0, currentCluster, (int)multiplier);
            sw.Stop();
            GC.EndNoGCRegion();
            var timeSpan = sw.Elapsed;
            lastSpeedInBytesPerSec.Add((buffer.Length / timeSpan.TotalSeconds));
            lastTimeRemaining.Add((clusterCount - currentCluster) * timeSpan.Ticks / multiplier);
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
        PropertyInfo hResult = ex.GetType().GetProperty("HResult", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        return (int)hResult.GetValue(ex, null);
    }

    [DllImport(@"msvcrt.dll", EntryPoint = @"memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
    private static extern IntPtr MemSet(IntPtr dest, int c, int count);

    //If you need super speed, calling out to M$ memset optimized method using P/invoke
    private static byte[] InitByteArray(byte fillWith, long size)
    {
        var arrayBytes = new byte[size];
        GCHandle gch = GCHandle.Alloc(arrayBytes, GCHandleType.Pinned);
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