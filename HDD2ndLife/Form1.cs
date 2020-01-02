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

            StartTree();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            TextExtra = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
                UseWaitCursor = true;

                driveTree.BeginUpdate();
                driveTree.Nodes.Clear();

                driveImageList.Images.Clear();

                // Set a default icon for the file.
                Icon iconForFile = SystemIcons.WinLogo;
                driveImageList.Images.Add(iconForFile);

                Log.Debug("Create the root node.");
                TreeNode tvwRoot = new TreeNode { Text = Environment.MachineName, ImageIndex = 0 };
                tvwRoot.SelectedImageIndex = tvwRoot.ImageIndex;
                driveTree.Nodes.Add(tvwRoot);
                Dictionary<DriveType, int> imageOffset = new Dictionary<DriveType, int>();

                Log.Debug("Now we need to add any children to the root node.");
                var driveQuery = new ManagementObjectSearcher(@"select * from Win32_DiskDrive");
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
                var logicalDriveQueryText = $"associators of {{{p.Path.RelativePath}}} where AssocClass = Win32_LogicalDiskToPartition";
                var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
                foreach (ManagementObject ld in logicalDriveQuery.Get())
                {
                    Win32LogicalDiskToPartition ldp = new Win32LogicalDiskToPartition(ld);
                    TreeNode part = new TreeNode
                    {
                        Text = ldp.DriveName,
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
                    details.Append(device.ToString());
                    break;
                case Win32LogicalDiskToPartition ldp:
                    details.AppendLine(ldp.VolumeName);
                    details.Append('\t').Append("VolumeSerial: ").AppendLine(ldp.VolumeSerial);
                    details.Append('\t').Append("DriveMediaType: ").AppendLine(ldp.DriveMediaType.ToString());
                    details.Append('\t').Append("TotalSpace: ").AppendLine(ldp.TotalSpace.ToString());
                    details.Append('\t').Append("FreeSpace: ").AppendLine(ldp.FreeSpace.ToString());
                    details.Append('\t').Append("FileSystem: ").AppendLine(ldp.FileSystem);
                    details.Append('\t').Append("DriveType: ").AppendLine(ldp.DriveType.ToString());
                    details.Append('\t').Append("DriveCompressed: ").AppendLine(ldp.DriveCompressed.ToString());
                    details.Append('\t').Append("DriveId: ").AppendLine(ldp.DriveId);
                    details.Append('\t').Append("DriveName: ").AppendLine(ldp.DriveName);
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
                        details.Append("\t\t").Append("SerialNumber: ").AppendLine(sddp.SerialNumber.ToString());
                    }

                    break;
            }

            lblDetails.Text = details.ToString();
        }

        public bool isDevice(HDD drive, string instanceName)
        {
            return instanceName.StartsWith(drive.PnpDeviceId, StringComparison.InvariantCultureIgnoreCase);
        }

        public string GetProperty(ManagementObject obj, string key)
        {
            PropertyData data = obj.Properties[key];
            if (data == null || data.Value == null)
            {
                return "N/A";
            }
            return data.Value.ToString();
        }

        public void GetSMARTInformation(HDD drive)
        {
            //create a management scope object
            ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\WMI");

            //create object query
            ObjectQuery query = new ObjectQuery(@"SELECT * FROM MSStorageDriver_FailurePredictStatus");// Where InstanceName=""{drive}_0""");

            //create object searcher
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            //get a collection of WMI objects
            ManagementObjectCollection queryCollection = searcher.Get();

            //enumerate the collection.
            foreach (ManagementObject m in queryCollection)
            {
                // access properties of the WMI object
                Console.WriteLine("Active : {0}", m["Active"]);

            }
        }


        // http://www.know24.net/blog/C+WMI+HDD+SMART+Information.aspx
        //public void GetSMARTInformation(HDD drive)
        //{
        //    // this is the actual physical drive number
        //    //_logger.Write($"Drive Number {drive.Index} Drive {drive.Id}, {drive.Model}, PNP {drive.PnpDeviceId}");

        //    // get wmi access to hdd 
        //    var searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
        //    searcher.Scope = new ManagementScope(@"\root\wmi");

        //    // check if SMART reports the drive is failing
        //    searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictStatus");
        //    foreach (ManagementObject thisDrive in searcher.Get())
        //    {
        //        //_logger.Write($"Instance Name 1 {GetProperty(thisDrive, "InstanceName")}");
        //        if (isDevice(drive, GetProperty(thisDrive, "InstanceName")))
        //        {
        //            drive.IsOK = (bool) thisDrive.Properties["PredictFailure"].Value == false;
        //        }
        //    }

        //    // retrieve attribute flags, value worst and vendor data information
        //    searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
        //    foreach (ManagementObject data in searcher.Get())
        //    {
        //        //_logger.Write($"Instance Name 2 {GetProperty(data, "InstanceName")}");
        //        if (isDevice(drive, GetProperty(data, "InstanceName")))
        //        {
        //            Byte[] bytes = (Byte[]) data.Properties["VendorSpecific"].Value;
        //            for (int i = 0; i < 30; ++i)
        //            {
        //                int id = 0;
        //                try
        //                {
        //                    id = bytes[i * 12 + 2];

        //                    int flags = bytes[
        //                        i * 12 +
        //                        4]; // least significant status byte, +3 most significant byte, but not used so ignored.
        //                    //bool advisory = (flags & 0x1) == 0x0;
        //                    bool failureImminent = (flags & 0x1) == 0x1;
        //                    //bool onlineDataCollection = (flags & 0x2) == 0x2;

        //                    int value = bytes[i * 12 + 5];
        //                    int worst = bytes[i * 12 + 6];
        //                    int vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);
        //                    if (id == 0) continue;

        //                    //_logger.Write($"SMART Data: {id} {vendordata}");

        //                    Smart attr = drive.Attributes[id];
        //                    attr.Current = value;
        //                    attr.Worst = worst;
        //                    attr.Data = vendordata;
        //                    attr.IsOK = failureImminent == false;
        //                    //attr.IsPopulatedFromWmi = true;
        //                }
        //                catch
        //                {
        //                    // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
        //                    //_logger.Write($"SMART Key Not found {id}");
        //                }
        //            }
        //        }
        //    }

        //    // retrieve threshold values foreach attribute
        //    searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
        //    foreach (ManagementObject data in searcher.Get())
        //    {
        //        //_logger.Write($"Instance Name 3 {GetProperty(data, "InstanceName")}");
        //        if (isDevice(drive, GetProperty(data, "InstanceName")))
        //        {
        //            Byte[] bytes = (Byte[]) data.Properties["VendorSpecific"].Value;
        //            for (int i = 0; i < 30; ++i)
        //            {
        //                try
        //                {

        //                    int id = bytes[i * 12 + 2];
        //                    int thresh = bytes[i * 12 + 3];
        //                    if (id == 0) continue;

        //                    var attr = drive.Attributes[id];
        //                    attr.Threshold = thresh;
        //                }
        //                catch
        //                {
        //                    // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public class HDD
    {

        public int Index { get; set; }
        public bool IsOK { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }
        public string PnpDeviceId { get; set; }

        public Dictionary<int, Smart> Attributes = new Dictionary<int, Smart>() {
                {0x00, new Smart("Invalid")},
                {0x01, new Smart("Raw read error rate")},
                {0x02, new Smart("Throughput performance")},
                {0x03, new Smart("Spinup time")},
                {0x04, new Smart("Start/Stop count")},
                {0x05, new Smart("Reallocated sector count")},
                {0x06, new Smart("Read channel margin")},
                {0x07, new Smart("Seek error rate")},
                {0x08, new Smart("Seek timer performance")},
                {0x09, new Smart("Power-on hours count")},
                {0x0A, new Smart("Spinup retry count")},
                {0x0B, new Smart("Calibration retry count")},
                {0x0C, new Smart("Power cycle count")},
                {0x0D, new Smart("Soft read error rate")},
                {0xB8, new Smart("End-to-End error")},
                {0xBE, new Smart("Airflow Temperature")},
                {0xBF, new Smart("G-sense error rate")},
                {0xC0, new Smart("Power-off retract count")},
                {0xC1, new Smart("Load/Unload cycle count")},
                {0xC2, new Smart("HDD temperature")},
                {0xC3, new Smart("Hardware ECC recovered")},
                {0xC4, new Smart("Reallocation count")},
                {0xC5, new Smart("Current pending sector count")},
                {0xC6, new Smart("Offline scan uncorrectable count")},
                {0xC7, new Smart("UDMA CRC error rate")},
                {0xC8, new Smart("Write error rate")},
                {0xC9, new Smart("Soft read error rate")},
                {0xCA, new Smart("Data Address Mark errors")},
                {0xCB, new Smart("Run out cancel")},
                {0xCC, new Smart("Soft ECC correction")},
                {0xCD, new Smart("Thermal asperity rate (TAR)")},
                {0xCE, new Smart("Flying height")},
                {0xCF, new Smart("Spin high current")},
                {0xD0, new Smart("Spin buzz")},
                {0xD1, new Smart("Offline seek performance")},
                {0xDC, new Smart("Disk shift")},
                {0xDD, new Smart("G-sense error rate")},
                {0xDE, new Smart("Loaded hours")},
                {0xDF, new Smart("Load/unload retry count")},
                {0xE0, new Smart("Load friction")},
                {0xE1, new Smart("Load/Unload cycle count")},
                {0xE2, new Smart("Load-in time")},
                {0xE3, new Smart("Torque amplification count")},
                {0xE4, new Smart("Power-off retract count")},
                {0xE6, new Smart("GMR head amplitude")},
                {0xE7, new Smart("Temperature")},
                {0xF0, new Smart("Head flying hours")},
                {0xFA, new Smart("Read error retry rate")},
                /* slot in any new codes you find in here */
            };

    }

    public class Smart
    {
        public bool HasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }
        public string Attribute { get; set; }
        public int Current { get; set; }
        public int Worst { get; set; }
        public int Threshold { get; set; }
        public int Data { get; set; }
        public bool IsOK { get; set; }

        public Smart()
        {

        }

        public Smart(string attributeName)
        {
            this.Attribute = attributeName;
        }
    }
}
