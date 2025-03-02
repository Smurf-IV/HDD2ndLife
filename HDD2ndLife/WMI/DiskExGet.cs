#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskExGet.cs" company="Smurf-IV">
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
// Icon Source : <a title = "Recycle PNG" href="http://pluspng.com/recycle-png-522.html">Recycle PNG</a>
#endregion

using System;
using System.Text;

using DeviceIOControlLib.Objects.Disk;
using DeviceIOControlLib.Objects.Enums;
using DeviceIOControlLib.Wrapper;

using Microsoft.Win32.SafeHandles;

using NLog;

namespace HDD2ndLife.WMI;

/// <summary>
/// Wrapper around the DeviceIOControlLib get functions into a single place
/// </summary>
internal class DiskExGet
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    internal DISK_GEOMETRY_EX? dge;
    private PARTITION_INFORMATION_EX? pie;

    private DRIVE_LAYOUT_INFORMATION_EX? dlie;

    private GETVERSIONINPARAMS? vip;
    //internal DISK_CACHE_INFORMATION ? dci;
    private GET_DISK_ATTRIBUTES? da;

    private STORAGE_DEVICE_DESCRIPTOR_PARSED_EX? sddp;

    public DiskExGet(SafeFileHandle diskHandle)
    {
        dge = null;
        pie = null;
        dlie = null;
        da = null;
        sddp = null;
        try
        {
            var diskDeviceWrapper = new DiskDeviceWrapper(diskHandle, false);
            try
            {
                dge = diskDeviceWrapper.DiskGetDriveGeometryEx();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            try
            {
                pie = diskDeviceWrapper.DiskGetPartitionInfoEx();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            try
            {
                dlie = diskDeviceWrapper.DiskGetDriveLayoutEx();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            try
            {
                vip = diskDeviceWrapper.DiskGetSmartVersion();     // <- PCIE SSD's and Raid controllers can have this disabled thus throwing an exception
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            try
            {
                //DISK_CACHE_INFORMATION dci = diskDeviceWrapper.DiskGetCacheInformation();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            try
            {
                da = diskDeviceWrapper.DiskGetDiskAttributes();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            try
            {
                // Some drives do not return data.
                var sdw = new StorageDeviceWrapperEx(diskHandle, false);
                sddp = sdw.StorageGetDeviceProperty();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
            //MountManagerWrapper mmw = new MountManagerWrapper(disk.DiskHandle, false);
            //List<MountPoint> mps = mmw.MountQueryPoints();
        }
        catch (Exception e)
        {
            Log.Warn(e);
        }
    }

    public override string ToString()
    {
        var details = new StringBuilder();
        if (dge.HasValue)
        {
            DISK_GEOMETRY_EX dge1 = dge.Value;
            details.Append('\t').AppendLine("DISK_GEOMETRY:");
            details.Append("\t\t").Append("Cylinders: ").AppendLine(dge1.Geometry.Cylinders.ToString());
            //public MEDIA_TYPE MediaType;
            details.Append("\t\t").Append("TracksPerCylinder: ")
                .AppendLine(dge1.Geometry.TracksPerCylinder.ToString());
            details.Append("\t\t").Append("SectorsPerTrack: ")
                .AppendLine(dge1.Geometry.SectorsPerTrack.ToString());
            details.Append("\t\t").Append("BytesPerSector: ")
                .AppendLine(dge1.Geometry.BytesPerSector.ToString());
            details.Append("\t\t").Append("DiskSize: ").AppendLine(dge1.Geometry.DiskSize.ToString());

            details.Append('\t').AppendLine("DISK_PARTITION_INFO:");
            //details.Append("\t\t").Append("SizeOfPartitionInfo: ").AppendLine(dge.PartitionInformation.SizeOfPartitionInfo.ToString());
            details.Append("\t\t").Append("PartitionStyle: ")
                .AppendLine(dge1.PartitionInformation.PartitionStyle.ToString());
            switch (dge1.PartitionInformation.PartitionStyle)
            {
                case PartitionStyle.PARTITION_STYLE_MBR:
                    details.Append("\t\t").Append("MbrSignature: ")
                        .AppendLine(dge1.PartitionInformation.MbrSignature.ToString());
                    break;
                case PartitionStyle.PARTITION_STYLE_GPT:
                    details.Append("\t\t").Append("GptGuidId: ")
                        .AppendLine(dge1.PartitionInformation.GptGuidId.ToString());
                    break;
                case PartitionStyle.PARTITION_STYLE_RAW:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            details.Append('\t').AppendLine("DISK_EX_INT13_INFO:");
            details.Append("\t\t").Append("ExBufferSize: ")
                .AppendLine(dge1.DiskInt13Info.ExBufferSize.ToString());
            details.Append("\t\t").Append("ExFlags: ").AppendLine(dge1.DiskInt13Info.ExFlags.ToString());
            details.Append("\t\t").Append("ExCylinders: ")
                .AppendLine(dge1.DiskInt13Info.ExCylinders.ToString());
            //public uint ExHeads;
            //public uint ExSectorsPerTrack;
            //public ulong ExSectorsPerDrive;
            //public ushort ExSectorSize;
            //public ushort ExReserved;
        }

        if (pie.HasValue)
        {
            PARTITION_INFORMATION_EX pie1 = pie.Value;
            details.Append('\t').AppendLine("PARTITION_INFORMATION_EX:");
            details.Append("\t\t").Append("PartitionStyle: ").AppendLine(pie1.PartitionStyle.ToString());
            details.Append("\t\t").Append("StartingOffset: ").AppendLine(pie1.StartingOffset.ToString());
            details.Append("\t\t").Append("PartitionLength: ").AppendLine(pie1.PartitionLength.ToString());
            details.Append("\t\t").Append("PartitionNumber: ").AppendLine(pie1.PartitionNumber.ToString());
            details.Append("\t\t").Append("RewritePartition: ").AppendLine(pie1.RewritePartition.ToString());
            switch (pie1.PartitionStyle)
            {
                case PartitionStyle.PARTITION_STYLE_MBR:
                    details.Append("\t\t").AppendLine("PARTITION_INFORMATION_MBR: ");
                    details.Append("\t\t\t").Append("PartitionType: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Mbr.PartitionType.ToString());
                    details.Append("\t\t\t").Append("BootIndicator: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Mbr.BootIndicator.ToString());
                    details.Append("\t\t\t").Append("RecognizedPartition: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Mbr.RecognizedPartition.ToString());
                    details.Append("\t\t\t").Append("HiddenSectors: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Mbr.HiddenSectors.ToString());
                    break;
                case PartitionStyle.PARTITION_STYLE_GPT:
                    details.Append("\t\t").AppendLine("PARTITION_INFORMATION_GPT: ");
                    details.Append("\t\t\t").Append("PartitionType: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Gpt.PartitionType.ToString());
                    details.Append("\t\t\t").Append("PartitionId: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Gpt.PartitionId.ToString());
                    details.Append("\t\t\t").Append("EFIPartitionAttributes: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Gpt.Attributes.ToString());
                    details.Append("\t\t\t").Append("Name: ")
                        .AppendLine(pie1.DriveLayoutInformaiton.Gpt.Name.ToString());
                    break;
                case PartitionStyle.PARTITION_STYLE_RAW:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (dlie.HasValue)
        {
            DRIVE_LAYOUT_INFORMATION_EX dlie1 = dlie.Value;
            details.Append('\t').AppendLine("DRIVE_LAYOUT_INFORMATION_EX:");
            details.Append("\t\t").Append("PartitionStyle: ").AppendLine(dlie1.PartitionStyle.ToString());
            details.Append("\t\t").Append("PartitionCount: ").AppendLine((dlie1.PartitionCount-1).ToString()); // Last one is nulls
            switch (dlie1.PartitionStyle)
            {
                case PartitionStyle.PARTITION_STYLE_MBR:
                    details.Append("\t\t").AppendLine("DRIVE_LAYOUT_INFORMATION_MBR: ");
                    details.Append("\t\t\t").Append("Signature: ")
                        .AppendLine(dlie1.DriveLayoutInformaiton.Mbr.Signature.ToString());
                    break;
                case PartitionStyle.PARTITION_STYLE_GPT:
                    details.Append("\t\t").AppendLine("DRIVE_LAYOUT_INFORMATION_GPT: ");
                    details.Append("\t\t\t").Append("DiskId: ")
                        .AppendLine(dlie1.DriveLayoutInformaiton.Gpt.DiskId.ToString());
                    details.Append("\t\t\t").Append("StartingUsableOffset: ")
                        .AppendLine(dlie1.DriveLayoutInformaiton.Gpt.StartingUsableOffset.ToString());
                    details.Append("\t\t\t").Append("UsableLength: ")
                        .AppendLine(dlie1.DriveLayoutInformaiton.Gpt.UsableLength.ToString());
                    details.Append("\t\t\t").Append("MaxPartitionCount: ")
                        .AppendLine(dlie1.DriveLayoutInformaiton.Gpt.MaxPartitionCount.ToString());
                    break;
                case PartitionStyle.PARTITION_STYLE_RAW:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (var entry = 0; entry < dlie1.PartitionCount-1; entry++) // Last one is nulls
            {
                PARTITION_INFORMATION_EX pie = dlie1.PartitionEntry[entry];
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

        if (da.HasValue)
        {
            GET_DISK_ATTRIBUTES da1 = da.Value;
            details.Append('\t').AppendLine("DISK_ATTRIBUTES:");
            details.Append("\t\t").Append("Version: ").AppendLine(da1.Version.ToString());
            //public int Reserved1;
            details.Append("\t\t").Append("Attributes: ").AppendLine(da1.Attributes.ToString());
            if ((da1.Attributes & 1) == 1)
                details.Append("\t\t").Append("Attributes: ").AppendLine(@"DISK_ATTRIBUTE_OFFLINE");
            if ((da1.Attributes & 2) == 2)
                details.Append("\t\t").Append("Attributes: ").AppendLine(@"DISK_ATTRIBUTE_READ_ONLY");
        }

        if (vip.HasValue)
        {
            GETVERSIONINPARAMS vip1 = vip.Value;
            details.Append('\t').AppendLine("GETVERSIONINPARAMS:");
            details.Append("\t\t").Append("Version: ").AppendLine(vip1.bVersion.ToString());
            details.Append("\t\t").Append("Revision: ").AppendLine(vip1.bRevision.ToString());
            details.Append("\t\t").Append("Reserved: ").AppendLine(vip1.bReserved.ToString());
            details.Append("\t\t").Append("IDEDeviceMap: ").AppendLine(vip1.bIDEDeviceMap.ToString());
            details.Append("\t\t").Append("Capabilities: ").AppendLine(vip1.fCapabilities.ToString());
            //details.Append("\t\t").Append("dwReserved: ").AppendLine(vip1.dwReserved.ToString());
        }

        if (sddp.HasValue)
        {
            STORAGE_DEVICE_DESCRIPTOR_PARSED_EX sddp1 = sddp.Value;
            details.Append('\t').AppendLine("STORAGE_DEVICE_DESCRIPTOR_PARSED:");
            //details.Append("\t\t").Append("Size: ").AppendLine(sddp1.Size.ToString());
            details.Append("\t\t").Append("SCSI DeviceType: ").AppendLine(sddp1.DeviceType.ToString());
            details.Append("\t\t").Append("SCSI DeviceTypeModifier: ")
                .AppendLine(sddp1.DeviceTypeModifier.ToString());
            details.Append("\t\t").Append("RemovableMedia: ").AppendLine(sddp1.RemovableMedia.ToString());
            details.Append("\t\t").Append("CommandQueueing: ").AppendLine(sddp1.CommandQueueing.ToString());
            //details.Append("\t\t").Append("VendorIdOffset: ").AppendLine(sddp1.VendorIdOffset.ToString());
            //details.Append("\t\t").Append("ProductIdOffset: ").AppendLine(sddp1.ProductIdOffset.ToString());
            //details.Append("\t\t").Append("ProductRevisionOffset: ").AppendLine(sddp1.ProductRevisionOffset.ToString());
            //details.Append("\t\t").Append("SerialNumberOffset: ").AppendLine(sddp1.SerialNumberOffset.ToString());
            details.Append("\t\t").Append("BusType: ").AppendLine(sddp1.BusType.ToString());
            details.Append("\t\t").Append("RawPropertiesLength: ").AppendLine(sddp1.RawPropertiesLength.ToString());
            details.Append("\t\t").Append("RawDeviceProperties: ").AppendLine(sddp1.RawDeviceProperties.ToString());
            //public byte[] RawDeviceProperties;
            details.Append("\t\t").Append("VendorId: ").AppendLine(sddp1.VendorId);
            details.Append("\t\t").Append("ProductId: ").AppendLine(sddp1.ProductId);
            details.Append("\t\t").Append("ProductRevision: ").AppendLine(sddp1.ProductRevision);
            details.Append("\t\t").Append("SerialNumber: ").AppendLine(sddp1.SerialNumber);
        }

        return details.ToString();

    }
};