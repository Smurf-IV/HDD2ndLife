#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskStatsView.cs" company="Smurf-IV">
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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ByteSizeLib;

using ComponentFactory.Krypton.Toolkit;

namespace HDD2ndLife.Controls
{
    public partial class DiskStatsView : UserControl
    {
        public bool Scanning { get; private set; }

        private readonly Dictionary<KryptonRadioButton, ScanType> mapOptions;
        private readonly Dictionary<KryptonRadioButton, int> speedOptions;
        private ScanDrive scanDrive;
        private string deviceId;

        public string DeviceId
        {
            private get => deviceId;
            set
            {
                deviceId = value;
                btnStartStop.Enabled = !string.IsNullOrWhiteSpace(deviceId);
            }
        }


        public DiskStatsView()
        {
            InitializeComponent();
            mapOptions = new Dictionary<KryptonRadioButton, ScanType>
            {
                {rbRead, ScanType.Read},
                {rbWrite, ScanType.Write},
                {rbVerify, ScanType.Verify},
                {rb2Pass, ScanType.Pass2}
            };
            speedOptions = new Dictionary<KryptonRadioButton, int>
            {
                {rb20, 20},
                {rb30, 30},
                {rb50, 50},
                {rb75, 75}
            };
        }

        public ulong DriveSize
        {
            set
            {
                var byteSize = ByteSize.FromBytes(value);
                lblDriveSize.Text =
                    $@"{byteSize.LargestWholeNumberBinaryValue:N2} {byteSize.LargestWholeNumberBinarySymbol}";

                if (value > 0)
                    diskSectors1.ScaledClusterCount = (value / (ulong)ScanDrive.DISK_BUFFER_SIZE);
            }
        }

        private void chkUseSpeed_CheckedChanged(object sender, System.EventArgs e)
        {
            pnlSpeed.Enabled = chkUseSpeed.Checked;
        }

        private void btnStartStop_Click(object sender, System.EventArgs e)
        {
            if (Scanning)
            {
                btnStartStop.Text = @"&Stopping";
                lblPhase.Text = @"Stopping";
                scanDrive?.Cancel();
            }
            else
            {
                grpScanType.Enabled = false;
                kryptonGroupBox2.Enabled = false;
                Scanning = true;
                btnStartStop.Text = @"&Stop";
                lblPhase.Text = @"Starting";
                ScanType st = mapOptions[mapOptions.Keys.First(r => r.Checked)];
                int speedOption = 99;
                if (chkUseSpeed.Checked)
                {
                    speedOption = speedOptions[speedOptions.Keys.First(r => r.Checked)];
                }

                scanDrive = new ScanDrive(st, Cancellation, chkFailFirst.Checked, speedOption, DeviceId)
                {
                    SetScaledClusterStatus = diskSectors1.SetScaledClusterStatus
                };
                scanDrive.PropertyChanged += ScanDrive_PropertyChanged;
                scanDrive.Start();
                lblPhase.Text = @"Scanning";
                tmrUpdate.Enabled = true;
            }
        }

        private void ScanDrive_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lblPhase.Text = e.PropertyName switch
            {
                @"Phase" => scanDrive?.Phase,
                _ => lblPhase.Text
            };
        }

        private void Cancellation()
        {
            tmrUpdate.Enabled = false;
            Scanning = false;
            scanDrive = null;
            BeginInvoke((MethodInvoker)delegate
          {
              btnStartStop.Text = @"&Start";
              lblPhase.Text = @"Stopped";
              lblTimeRemaining.Text = string.Empty;
              lblSpeed.Text = string.Empty;
              grpScanType.Enabled = true;
              kryptonGroupBox2.Enabled = true;
          });
        }

        private void tmrUpdate_Tick(object sender, System.EventArgs e)
        {
            var local = scanDrive;  // Observe "Window of opportunity"
            if (local == null) return;
            var timeSpan = local.TimeRemaining;
            var speedBytes = ByteSize.FromMegaBytes(local.SpeedInMBytesPerSec);
            lblTimeRemaining.Text = timeSpan.ToString(@"dd\.hh\:mm\:ss");
            lblSpeed.Text =
                $@"{speedBytes.LargestWholeNumberDecimalValue:N2} {speedBytes.LargestWholeNumberDecimalSymbol}/s";
            diskSectors1.RecalcStatus();
        }
    }
}
