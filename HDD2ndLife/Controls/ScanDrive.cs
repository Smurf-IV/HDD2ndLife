#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="ScanDrive.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 Simon Coghlan (Aka Smurf-IV)
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
using System.Threading;
using System.Threading.Tasks;
using Clifton.Collections.Generic;
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

    internal class ScanDrive
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static int AVERAGE_BUFFER_SIZE = 64;
        private static int DISK_BUFFER_SIZE = 1024 * 1024;  // Seems to be "Optimum" for Sata II HDD and PCIE cards, giving > 90% read utilisation

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
        public TimeSpan TimeRemaining => TimeSpan.FromTicks((long) lastTimeRemaining.Average());

        public int CurrentSector;

        public void Cancel()
        {
            Log.Info(@"Cancelling");
            cancelTokenSrc.Cancel();
        }

        public void Start()
        {
            try
            {

                Log.Debug(@"Creating Disk interface");
                RawDisk disk = new RawDisk(deviceId, FileAccess.ReadWrite);
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
            try
            {
                int multiplier = DISK_BUFFER_SIZE / disk.SectorSize;
                byte[] buffer = new byte[disk.SectorSize * multiplier];

                Stopwatch sw = new Stopwatch();
                long sectorCount = disk.SectorCount;    // prevent this from being calculated each time
                int bufferLength = buffer.Length - 1;
                CurrentSector = 0;
                for (; CurrentSector < sectorCount && !cancelTokenSrc.IsCancellationRequested; CurrentSector+= multiplier)
                {
                    ReadGroup(disk, sw, buffer, multiplier, sectorCount, bufferLength);
                }
                // Do the last few sectors of the drive
                multiplier = 1;
                buffer = new byte[disk.SectorSize * multiplier];
                bufferLength = buffer.Length - 1;
                for (; CurrentSector < sectorCount && !cancelTokenSrc.IsCancellationRequested; CurrentSector += multiplier)
                {
                    ReadGroup(disk, sw, buffer, multiplier, sectorCount, bufferLength);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            cancelTokenSrc.Cancel();
        }

        private void ReadGroup(RawDisk disk, Stopwatch sw, byte[] buffer, int multiplier, long sectorCount, int bufferLength)
        {
            try
            {
                sw.Restart();
                double read = disk.ReadSectors(buffer, 0, CurrentSector, multiplier);
                sw.Stop();
                long elapsedTicks = sw.Elapsed.Ticks;
                lastSpeedInMBytesPerSec.Value = (read / elapsedTicks) * 10; // Sw.Elapsed.Ticks are always the same distance
                lastSpeedInMBytesPerSec.Next();
                lastTimeRemaining.Value = (sectorCount - CurrentSector) * elapsedTicks / multiplier;
                lastTimeRemaining.Next();
                if (read < bufferLength)
                {
                    throw new FileLoadException($@"read != buffer.Length. [{read}] != [{buffer.Length}]");
                }
            }
            catch (IOException ex1)
            {
                int errorCode1 = GetWin32ErrorCode(ex1);
                Log.Error(ex1, new Win32Exception(errorCode1).Message);
                // TODO: Add to the list of failed sectors
            }
            catch (Exception ex2)
            {
                Log.Error(ex2);
                // TODO: Add to the list of failed sectors
            }
        }

        public static ushort GetWin32ErrorCode(IOException ex)
        {
            int hResult = GetExceptionHResult(ex);
            // The Win32 error code is stored in the 16 first bits of the value
            return (ushort)(hResult & 0x0000FFFF);
        }

        public static int GetExceptionHResult(IOException ex)
        {
            PropertyInfo hResult = ex.GetType().GetProperty("HResult", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return (int)hResult.GetValue(ex, null);
        }

    }
}