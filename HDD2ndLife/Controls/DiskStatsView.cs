#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskStatsView.cs" company="Smurf-IV">
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

using ByteSizeLib;

using Elucidate.wyDay.Controls;

using Krypton.Toolkit;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HDD2ndLife.Controls;

public partial class DiskStatsView : UserControl
{
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Scanning { get; private set; }

    private readonly Dictionary<KryptonRadioButton, ScanType> mapOptions;
    private readonly Dictionary<KryptonRadioButton, int> speedOptions;
    private ScanDrive scanDrive;
    private string deviceId;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DeviceId
    {
        private get => deviceId;
        set
        {
            deviceId = value;
            // Make sure that user cannot cause program to crash due to not selecting a drive.
            btnStartStop.Enabled = !string.IsNullOrWhiteSpace(deviceId);
            btnPartitioning.Enabled = btnStartStop.Enabled;
            // Had to put this somewhere after the control had been created and hosted
            lblPhase.ContainerControl = ParentForm;
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
        lblPhase.DisplayText = @"Waiting for Start";
        //lblPhase.Value = 2;

        const int CLUSTERS_PER_VECTOR = 64 / 3; // 64 bits / (5 represented in binary bits)

        var nodeSize = 12 * DeviceDpi / 96;
        var node = new Bitmap(nodeSize, nodeSize);
        Graphics g = Graphics.FromImage(node);
        diskSectors1.DrawNodeStatus(g, BlockStatus.NoWork, 0, 0, nodeSize);
        lblNoWork.Values.Image = (Image)node.Clone();
        diskSectors1.DrawNodeStatus(g, BlockStatus.Reading, 0, 0, nodeSize);
        lblReading.Values.Image = (Image)node.Clone();
        diskSectors1.DrawNodeStatus(g, BlockStatus.Writing, 0, 0, nodeSize);
        lblWriting.Values.Image = (Image)node.Clone();
        diskSectors1.DrawNodeStatus(g, BlockStatus.WriteDone, 0, 0, nodeSize);
        lblWriteDone.Values.Image = (Image)node.Clone();
        diskSectors1.DrawNodeStatus(g, BlockStatus.Validating, 0, 0, nodeSize);
        lblValidating.Values.Image = (Image)node.Clone();
        diskSectors1.DrawNodeStatus(g, BlockStatus.Failed, 0, 0, nodeSize);
        lblFailed.Values.Image = (Image)node.Clone();
        diskSectors1.DrawNodeStatus(g, BlockStatus.Passed, 0, 0, nodeSize);
        lblPassed.Values.Image = node;
    }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ulong DriveSize
    {
        set
        {
            ByteSize byteSize = ByteSize.FromBytes(value);
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
            lblPhase.DisplayText = @"Stopping";
            lblPhase.State = ProgressBarState.Pause;
            lblPhase.Style = ProgressBarStyle.Marquee;

            scanDrive?.Cancel();
        }
        else
        {
            grpScanType.Enabled = false;
            kryptonGroupBox2.Enabled = false;
            btnPartitioning.Enabled = false;
            Scanning = true;
            btnStartStop.Text = @"&Stop";
            lblPhase.DisplayText = @"Starting";
            lblPhase.Value = 0;
            lblPhase.State = ProgressBarState.Normal;
            lblPhase.Style = ProgressBarStyle.Continuous;
            ScanType st = mapOptions[mapOptions.Keys.First(r => r.Checked)];
            var speedOption = 99;
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
            tmrUpdate.Enabled = true;
        }
    }

    private void ScanDrive_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        lblPhase.DisplayText = e.PropertyName switch
        {
            @"Phase" => scanDrive?.Phase,
            _ => lblPhase.DisplayText
        };
        lblPhase.Value = (int)Math.Ceiling(scanDrive?.Percent ?? 1);
    }

    private void Cancellation()
    {
        tmrUpdate.Enabled = false;
        Scanning = false;
        scanDrive = null;
        BeginInvoke((MethodInvoker)delegate
        {
            btnStartStop.Text = @"&Start";
            lblPhase.DisplayText = @"Stopped";
            lblTimeRemaining.Text = string.Empty;
            lblSpeed.Text = string.Empty;
            grpScanType.Enabled = true;
            kryptonGroupBox2.Enabled = true;
            btnPartitioning.Enabled = true;
            // Make sure on fast systems the redraw is done for the final "Squares"
            diskSectors1.ReCalcStatus();
        });
    }

    private void tmrUpdate_Tick(object sender, System.EventArgs e)
    {
        ScanDrive local = scanDrive;  // Observe "Window of opportunity"
        if (local == null) return;
        TimeSpan timeSpan = local.TimeRemaining;
        ByteSize speedBytes = ByteSize.FromMegaBytes(local.SpeedInMBytesPerSec);
        lblTimeRemaining.Text = timeSpan.ToString(@"dd\.hh\:mm\:ss");
        lblSpeed.Text =
            $@"{speedBytes.LargestWholeNumberDecimalValue:N2} {speedBytes.LargestWholeNumberDecimalSymbol}/s";
        diskSectors1.ReCalcStatus();
    }

    private void btnPartitioning_Click(object sender, System.EventArgs e)
    {
        new PartitionScheme(diskSectors1.Blocks, DeviceId).ShowDialog(this);
    }

}