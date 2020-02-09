#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="HDD2ndLife.cs" company="Smurf-IV">
// 
//  Copyright (C) 2019-2020 Simon Coghlan (Aka Smurf-IV)
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
// Icon Source : <a title = "Recycle PNG" href="http://pluspng.com/recycle-png-522.html">Recycle PNG</a>
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;


using ComponentFactory.Krypton.Toolkit;

using DeviceIOControlLib.Objects.Disk;
using DeviceIOControlLib.Objects.Enums;
using DeviceIOControlLib.Objects.Storage;
using DeviceIOControlLib.Wrapper;
using HDD2ndLife.WMI;
using Microsoft.Win32.SafeHandles;

using NLog;

using RawDiskLib;

namespace HDD2ndLife
{
    public partial class Form1 : KryptonForm
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Form1()
        {
            InitializeComponent();

            if (Properties.Settings.Default.UpdateRequired)
            {
                // Thanks go to http://cs.rthand.com/blogs/blog_with_righthand/archive/2005/12/09/246.aspx
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpdateRequired = false;
                Properties.Settings.Default.Save();
            }

            if (Enum.TryParse(Properties.Settings.Default.Theme, out PaletteModeManager value))
            {
                kryptonManager1.GlobalPaletteMode = value;
            }

            // Hook into changes in the global palette
            KryptonManager.GlobalPaletteChanged += OnPaletteChanged;
            ThemeManager.PropagateThemeSelector(themeComboBox);
            themeComboBox.Text =
                ThemeManager.ReturnPaletteModeManagerAsString(PaletteModeManager.Office2007Blue, kryptonManager1);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            TextExtra = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                UseWaitCursor = true;
                StartTree();
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
            }
        }

        private void themeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThemeManager.SetTheme(themeComboBox.Text, kryptonManager1);
        }

        private void OnPaletteChanged(object sender, EventArgs e)
        {
            // persist our geometry string.
            RecalcNonClient();
            Properties.Settings.Default.Theme = kryptonManager1.GlobalPaletteMode.ToString();
            Properties.Settings.Default.Save();
        }

        private void changeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://github.com/Smurf-IV/HDD2ndLife/commits");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"https://github.com/Smurf-IV/HDD2ndLife/blob/master/ReadMe.md");
        }


        private void StartTree()
        {
            try
            {
                driveTree.BeginUpdate();
                driveTree.Nodes.Clear();

                driveImageList.Images.Clear();

                // Set a default icon for the file.
                Icon iconForFile = SystemIcons.WinLogo;
                driveImageList.Images.Add(iconForFile);

                Log.Debug("Create the root node.");
                TreeNode tvwRoot = new TreeNode {Text = Environment.MachineName, ImageIndex = 0};
                tvwRoot.SelectedImageIndex = tvwRoot.ImageIndex;
                driveTree.Nodes.Add(tvwRoot);

                Log.Debug("Now we need to add any children to the root node.");
                foreach (Win32DiskDrive deviceInfo in Win32DiskDrive.Retrieve())
                {
                    try
                    {
                        FillInStorageDeviceDirectoryType(tvwRoot, deviceInfo);
                    }
                    catch (Exception ex)
                    {
                        Log.Warn(ex, "A storage device failed to enumerate.");
                    }
                }

                tvwRoot.Expand();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, @"StartTree Threw:");
            }
            finally
            {
                Enabled = true;
                UseWaitCursor = false;
                driveTree.EndUpdate();
            }
        }

        private void FillInStorageDeviceDirectoryType(TreeNode parentNode, Win32DiskDrive win32Disk)
        {

            TreeNode thisNode = new TreeNode
            {
                Text = win32Disk.Caption,
                Tag = win32Disk
            };
            parentNode.Nodes.Add(thisNode);

            using RawDisk disk = new RawDisk(win32Disk.DeviceId);
            FillInStorageDeviceDirectoryType(thisNode, disk);

            var partitionQueryText =
                $"associators of {{{win32Disk.RelativePath}}} where AssocClass = Win32_DiskDriveToDiskPartition";

            var partitionQuery = new ManagementObjectSearcher(partitionQueryText);
            foreach (ManagementObject p in partitionQuery.Get())
            {
                var logicalDriveQueryText =
                    $"associators of {{{p.Path.RelativePath}}} where AssocClass = Win32_LogicalDiskToPartition";
                var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
                foreach (ManagementObject ld in logicalDriveQuery.Get())
                {
                    Win32LogicalDisk ldp = new Win32LogicalDisk(ld);
                    TreeNode part = new TreeNode
                    {
                        Text = string.Concat(ldp.VolumeName, @" -> ", ldp.Caption),
                        Tag = ldp
                    };

                    thisNode.Nodes.Add(part);

                }
            }
        }


        private void FillInStorageDeviceDirectoryType(TreeNode parentNode, RawDisk disk)
        {
            if (disk.DiskInfo.MediaType != MEDIA_TYPE.FixedMedia) return;

            TreeNode thisNode = new TreeNode
            {
                Text = disk.DosDeviceName,
                Tag = new DiskEX(disk.DiskHandle)
            };

            parentNode.Nodes.Add(thisNode);
        }


        private void driveTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

            switch (e.Node.Tag)
            {
                case Win32DiskDrive device:
                    lblDetails.Text = device.ToString();
                    diskStatsView1.DriveSize = device.Size;
                    break;
                case Win32LogicalDisk ld:
                    lblDetails.Text = ld.ToString();
                    diskStatsView1.DriveSize = ld.Size;
                    break;
                case DiskEX diskEx:
                    lblDetails.Text = diskEx.ToString();
                    diskStatsView1.DriveSize =
                        diskEx.dge.HasValue ? (ulong) diskEx.dge.Value.Geometry.DiskSize : (ulong) 0;

                    break;
            }

        }


    }
}
