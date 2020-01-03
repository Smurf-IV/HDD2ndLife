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

        struct DiskEX
        {
            internal DISK_GEOMETRY_EX? dge;
            internal PARTITION_INFORMATION_EX? pie;

            internal DRIVE_LAYOUT_INFORMATION_EX? dlie;

            //internal GETVERSIONINPARAMS ? vip;
            //internal DISK_CACHE_INFORMATION ? dci;
            internal GET_DISK_ATTRIBUTES? da;

            internal STORAGE_DEVICE_DESCRIPTOR_PARSED? sddp;

            public DiskEX(SafeFileHandle diskHandle)
            {
                dge = null;
                pie = null;
                dlie = null;
                da = null;
                sddp = null;
                try
                {
                    DiskDeviceWrapper diskDeviceWrapper = new DiskDeviceWrapper(diskHandle, false);
                    dge = diskDeviceWrapper.DiskGetDriveGeometryEx();
                    pie = diskDeviceWrapper.DiskGetPartitionInfoEx();
                    dlie = diskDeviceWrapper.DiskGetDriveLayoutEx();
                    //GETVERSIONINPARAMS vip = diskDeviceWrapper.DiskGetSmartVersion();
                    //DISK_CACHE_INFORMATION dci = diskDeviceWrapper.DiskGetCacheInformation();
                    da = diskDeviceWrapper.DiskGetDiskAttributes();

                    StorageDeviceWrapper sdw = new StorageDeviceWrapper(diskHandle, false);
                    sddp = sdw.StorageGetDeviceProperty();

                    //MountManagerWrapper mmw = new MountManagerWrapper(disk.DiskHandle, false);
                    //List<MountPoint> mps = mmw.MountQueryPoints();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        };

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
            StringBuilder details = new StringBuilder();

            switch (e.Node.Tag)
            {
                case Win32DiskDrive device:
                    details.Append(device);
                    break;
                case Win32LogicalDisk ld:
                    details.Append(ld);
                    break;
                case DiskEX diskEx:
                    details.AppendLine(e.Node.Parent.Text);
                    if (diskEx.dge.HasValue)
                    {
                        DISK_GEOMETRY_EX dge = diskEx.dge.Value;
                        details.Append('\t').AppendLine("DISK_GEOMETRY:");
                        details.Append("\t\t").Append("Cylinders: ").AppendLine(dge.Geometry.Cylinders.ToString());
                        //public MEDIA_TYPE MediaType;
                        details.Append("\t\t").Append("TracksPerCylinder: ")
                            .AppendLine(dge.Geometry.TracksPerCylinder.ToString());
                        details.Append("\t\t").Append("SectorsPerTrack: ")
                            .AppendLine(dge.Geometry.SectorsPerTrack.ToString());
                        details.Append("\t\t").Append("BytesPerSector: ")
                            .AppendLine(dge.Geometry.BytesPerSector.ToString());
                        details.Append("\t\t").Append("DiskSize: ").AppendLine(dge.Geometry.DiskSize.ToString());

                        details.Append('\t').AppendLine("DISK_PARTITION_INFO:");
                        //details.Append("\t\t").Append("SizeOfPartitionInfo: ").AppendLine(dge.PartitionInformation.SizeOfPartitionInfo.ToString());
                        details.Append("\t\t").Append("PartitionStyle: ")
                            .AppendLine(dge.PartitionInformation.PartitionStyle.ToString());
                        switch (dge.PartitionInformation.PartitionStyle)
                        {
                            case PartitionStyle.PARTITION_STYLE_MBR:
                                details.Append("\t\t").Append("MbrSignature: ")
                                    .AppendLine(dge.PartitionInformation.MbrSignature.ToString());
                                break;
                            case PartitionStyle.PARTITION_STYLE_GPT:
                                details.Append("\t\t").Append("GptGuidId: ")
                                    .AppendLine(dge.PartitionInformation.GptGuidId.ToString());
                                break;
                            case PartitionStyle.PARTITION_STYLE_RAW:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        details.Append('\t').AppendLine("DISK_EX_INT13_INFO:");
                        details.Append("\t\t").Append("ExBufferSize: ")
                            .AppendLine(dge.DiskInt13Info.ExBufferSize.ToString());
                        details.Append("\t\t").Append("ExFlags: ").AppendLine(dge.DiskInt13Info.ExFlags.ToString());
                        details.Append("\t\t").Append("ExCylinders: ")
                            .AppendLine(dge.DiskInt13Info.ExCylinders.ToString());
                        //public uint ExHeads;
                        //public uint ExSectorsPerTrack;
                        //public ulong ExSectorsPerDrive;
                        //public ushort ExSectorSize;
                        //public ushort ExReserved;
                    }

                    if (diskEx.pie.HasValue)
                    {
                        PARTITION_INFORMATION_EX pie = diskEx.pie.Value;
                        details.Append('\t').AppendLine("PARTITION_INFORMATION_EX:");
                        details.Append("\t\t").Append("PartitionStyle: ").AppendLine(pie.PartitionStyle.ToString());
                        details.Append("\t\t").Append("StartingOffset: ").AppendLine(pie.StartingOffset.ToString());
                        details.Append("\t\t").Append("PartitionLength: ").AppendLine(pie.PartitionLength.ToString());
                        details.Append("\t\t").Append("PartitionNumber: ").AppendLine(pie.PartitionNumber.ToString());
                        details.Append("\t\t").Append("RewritePartition: ").AppendLine(pie.RewritePartition.ToString());
                        switch (pie.PartitionStyle)
                        {
                            case PartitionStyle.PARTITION_STYLE_MBR:
                                details.Append("\t\t").AppendLine("PARTITION_INFORMATION_MBR: ");
                                details.Append("\t\t\t").Append("PartitionType: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Mbr.PartitionType.ToString());
                                details.Append("\t\t\t").Append("BootIndicator: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Mbr.BootIndicator.ToString());
                                details.Append("\t\t\t").Append("RecognizedPartition: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Mbr.RecognizedPartition.ToString());
                                details.Append("\t\t\t").Append("HiddenSectors: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Mbr.HiddenSectors.ToString());
                                break;
                            case PartitionStyle.PARTITION_STYLE_GPT:
                                details.Append("\t\t").AppendLine("PARTITION_INFORMATION_GPT: ");
                                details.Append("\t\t\t").Append("PartitionType: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Gpt.PartitionType.ToString());
                                details.Append("\t\t\t").Append("PartitionId: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Gpt.PartitionId.ToString());
                                details.Append("\t\t\t").Append("EFIPartitionAttributes: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Gpt.Attributes.ToString());
                                details.Append("\t\t\t").Append("Name: ")
                                    .AppendLine(pie.DriveLayoutInformaiton.Gpt.Name.ToString());
                                break;
                            case PartitionStyle.PARTITION_STYLE_RAW:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    if (diskEx.dlie.HasValue)
                    {
                        DRIVE_LAYOUT_INFORMATION_EX dlie = diskEx.dlie.Value;
                        details.Append('\t').AppendLine("DRIVE_LAYOUT_INFORMATION_EX:");
                        details.Append("\t\t").Append("PartitionStyle: ").AppendLine(dlie.PartitionStyle.ToString());
                        details.Append("\t\t").Append("PartitionCount: ").AppendLine(dlie.PartitionCount.ToString());
                        switch (dlie.PartitionStyle)
                        {
                            case PartitionStyle.PARTITION_STYLE_MBR:
                                details.Append("\t\t").AppendLine("DRIVE_LAYOUT_INFORMATION_MBR: ");
                                details.Append("\t\t\t").Append("Signature: ")
                                    .AppendLine(dlie.DriveLayoutInformaiton.Mbr.Signature.ToString());
                                break;
                            case PartitionStyle.PARTITION_STYLE_GPT:
                                details.Append("\t\t").AppendLine("DRIVE_LAYOUT_INFORMATION_GPT: ");
                                details.Append("\t\t\t").Append("DiskId: ")
                                    .AppendLine(dlie.DriveLayoutInformaiton.Gpt.DiskId.ToString());
                                details.Append("\t\t\t").Append("StartingUsableOffset: ")
                                    .AppendLine(dlie.DriveLayoutInformaiton.Gpt.StartingUsableOffset.ToString());
                                details.Append("\t\t\t").Append("UsableLength: ")
                                    .AppendLine(dlie.DriveLayoutInformaiton.Gpt.UsableLength.ToString());
                                details.Append("\t\t\t").Append("MaxPartitionCount: ")
                                    .AppendLine(dlie.DriveLayoutInformaiton.Gpt.MaxPartitionCount.ToString());
                                break;
                            case PartitionStyle.PARTITION_STYLE_RAW:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        for (int entry = 0; entry < dlie.PartitionCount; entry++)
                        {
                            PARTITION_INFORMATION_EX pie = dlie.PartitionEntry[entry];
                            details.Append("\t\t").AppendLine("PARTITION_INFORMATION_EX:");
                            details.Append("\t\t\t").Append("PartitionStyle: ")
                                .AppendLine(pie.PartitionStyle.ToString());
                            details.Append("\t\t\t").Append("StartingOffset: ")
                                .AppendLine(pie.StartingOffset.ToString());
                            details.Append("\t\t\t").Append("PartitionLength: ")
                                .AppendLine(pie.PartitionLength.ToString());
                            details.Append("\t\t\t").Append("PartitionNumber: ")
                                .AppendLine(pie.PartitionNumber.ToString());
                            details.Append("\t\t\t").Append("RewritePartition: ")
                                .AppendLine(pie.RewritePartition.ToString());
                            switch (pie.PartitionStyle)
                            {
                                case PartitionStyle.PARTITION_STYLE_MBR:
                                    details.Append("\t\t\t").AppendLine("PARTITION_INFORMATION_MBR: ");
                                    details.Append("\t\t\t\t").Append("PartitionType: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Mbr.PartitionType.ToString());
                                    details.Append("\t\t\t\t").Append("BootIndicator: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Mbr.BootIndicator.ToString());
                                    details.Append("\t\t\t\t").Append("RecognizedPartition: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Mbr.RecognizedPartition.ToString());
                                    details.Append("\t\t\t\t").Append("HiddenSectors: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Mbr.HiddenSectors.ToString());
                                    break;
                                case PartitionStyle.PARTITION_STYLE_GPT:
                                    details.Append("\t\t\t").AppendLine("PARTITION_INFORMATION_GPT: ");
                                    details.Append("\t\t\t\t").Append("PartitionType: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Gpt.PartitionType.ToString());
                                    details.Append("\t\t\t\t").Append("PartitionId: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Gpt.PartitionId.ToString());
                                    details.Append("\t\t\t\t").Append("EFIPartitionAttributes: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Gpt.Attributes.ToString());
                                    details.Append("\t\t\t\t").Append("Name: ")
                                        .AppendLine(pie.DriveLayoutInformaiton.Gpt.Name.ToString());
                                    break;
                                case PartitionStyle.PARTITION_STYLE_RAW:
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }

                    if (diskEx.da.HasValue)
                    {
                        GET_DISK_ATTRIBUTES da = diskEx.da.Value;
                        details.Append('\t').AppendLine("DISK_ATTRIBUTES:");
                        details.Append("\t\t").Append("Version: ").AppendLine(da.Version.ToString());
                        //public int Reserved1;
                        details.Append("\t\t").Append("Attributes: ").AppendLine(da.Attributes.ToString());
                    }

                    if (diskEx.sddp.HasValue)
                    {
                        STORAGE_DEVICE_DESCRIPTOR_PARSED sddp = diskEx.sddp.Value;
                        details.Append('\t').AppendLine("STORAGE_DEVICE_DESCRIPTOR_PARSED:");
                        details.Append("\t\t").Append("Size: ").AppendLine(sddp.Size.ToString());
                        details.Append("\t\t").Append("DeviceType: ").AppendLine(sddp.DeviceType.ToString());
                        details.Append("\t\t").Append("DeviceTypeModifier: ")
                            .AppendLine(sddp.DeviceTypeModifier.ToString());
                        details.Append("\t\t").Append("RemovableMedia: ").AppendLine(sddp.RemovableMedia.ToString());
                        details.Append("\t\t").Append("CommandQueueing: ").AppendLine(sddp.CommandQueueing.ToString());
                        details.Append("\t\t").Append("VendorIdOffset: ").AppendLine(sddp.VendorIdOffset.ToString());
                        details.Append("\t\t").Append("ProductIdOffset: ").AppendLine(sddp.ProductIdOffset.ToString());
                        details.Append("\t\t").Append("ProductRevisionOffset: ")
                            .AppendLine(sddp.ProductRevisionOffset.ToString());
                        details.Append("\t\t").Append("SerialNumberOffset: ")
                            .AppendLine(sddp.SerialNumberOffset.ToString());
                        details.Append("\t\t").Append("BusType: ").AppendLine(sddp.BusType.ToString());
                        details.Append("\t\t").Append("RawPropertiesLength: ")
                            .AppendLine(sddp.RawPropertiesLength.ToString());
                        details.Append("\t\t").Append("RawDeviceProperties: ")
                            .AppendLine(sddp.RawDeviceProperties.ToString());
                        //public byte[] RawDeviceProperties;
                        details.Append("\t\t").Append("SerialNumber: ").AppendLine(sddp.SerialNumbe);
                    }

                    break;
            }

            lblDetails.Text = details.ToString();
        }


    }
}
