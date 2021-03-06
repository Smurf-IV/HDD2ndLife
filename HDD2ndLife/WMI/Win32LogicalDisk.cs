#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="Win32LogicalDisk.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 - 2021 Simon Coghlan (Aka Smurf-IV)
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
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HDD2ndLife.WMI
{
    internal class Win32LogicalDisk
    {
        // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-logicaldisk
        private ushort Access;
        private ushort Availability;
        private ulong BlockSize;
        public string Caption;
        private bool Compressed;
        private uint ConfigManagerErrorCode;
        private bool ConfigManagerUserConfig;
        private string CreationClassName;
        private string Description;
        private string DeviceID;
        private uint DriveType;
        private bool ErrorCleared;
        private string ErrorDescription;
        private string ErrorMethodology;
        private string FileSystem;
        private ulong FreeSpace;
        private DateTime InstallDate;
        private uint LastErrorCode;
        private uint MaximumComponentLength;
        private uint MediaType;
        private string Name;
        private ulong NumberOfBlocks;
        private string PNPDeviceID;
        private ushort[] PowerManagementCapabilities;
        private bool PowerManagementSupported;
        private string ProviderName;
        private string Purpose;
        private bool QuotasDisabled;
        private bool QuotasIncomplete;
        private bool QuotasRebuilding;
        public ulong Size;
        private string Status;
        private ushort StatusInfo;
        private bool SupportsDiskQuotas;
        private bool SupportsFileBasedCompression;
        private string SystemCreationClassName;
        private string SystemName;
        private bool VolumeDirty;
        public string VolumeName;
        private string VolumeSerialNumber;


        public Win32LogicalDisk(ManagementObject mo)
        {
            Access = (ushort)(mo.Properties["Access"]?.Value ?? default(ushort));
            Availability = (ushort)(mo.Properties["Availability"]?.Value ?? default(ushort));
            BlockSize = (ulong)(mo.Properties["BlockSize"]?.Value ?? default(ulong));
            Caption = (string)(mo.Properties["Caption"]?.Value);
            Compressed = (bool) (mo.Properties["Compressed"]?.Value ?? default(bool));
            ConfigManagerErrorCode = (uint) (mo.Properties["ConfigManagerErrorCode"]?.Value ?? default(uint));
            ConfigManagerUserConfig = (bool)(mo.Properties["ConfigManagerUserConfig"]?.Value ?? default(bool));
            CreationClassName = (string)(mo.Properties["CreationClassName"]?.Value);
            Description = (string)(mo.Properties["Description"]?.Value);
            DeviceID = (string)(mo.Properties["DeviceID"]?.Value);
            DriveType = (uint)(mo.Properties["DriveType"]?.Value ?? default(uint));
            ErrorCleared = (bool)(mo.Properties["ErrorCleared"]?.Value ?? default(bool));
            ErrorDescription = (string)(mo.Properties["ErrorDescription"]?.Value);
            ErrorMethodology = (string)(mo.Properties["ErrorMethodology"]?.Value);
            FileSystem = (string)(mo.Properties["FileSystem"]?.Value);
            FreeSpace = (ulong)(mo.Properties["FreeSpace"]?.Value ?? default(ulong));
            InstallDate = ManagementDateTimeConverter.ToDateTime(mo.Properties["InstallDate"]?.Value as string ?? "00010102000000.000000+060");
            LastErrorCode = (uint)(mo.Properties["LastErrorCode"]?.Value ?? default(uint));
            MaximumComponentLength = (uint)(mo.Properties["MaximumComponentLength"]?.Value ?? default(uint));
            MediaType = (uint)(mo.Properties["MediaType"]?.Value ?? default(uint));
            Name = (string)(mo.Properties["Name"]?.Value);
            NumberOfBlocks = (ulong)(mo.Properties["NumberOfBlocks"]?.Value ?? default(ulong));
            PNPDeviceID = (string)(mo.Properties["PNPDeviceID"]?.Value);
            PowerManagementCapabilities = (ushort[])(mo.Properties["PowerManagementCapabilities"]?.Value ?? new ushort[0]);
            PowerManagementSupported = (bool)(mo.Properties["PowerManagementSupported"]?.Value ?? default(bool));
            ProviderName = (string)(mo.Properties["ProviderName"]?.Value);
            Purpose = (string)(mo.Properties["Purpose"]?.Value);
            QuotasDisabled = (bool)(mo.Properties["QuotasDisabled"]?.Value ?? default(bool));
            QuotasIncomplete = (bool)(mo.Properties["QuotasIncomplete"]?.Value ?? default(bool));
            QuotasRebuilding = (bool)(mo.Properties["QuotasRebuilding"]?.Value ?? default(bool));
            Size = (ulong)(mo.Properties["Size"]?.Value ?? default(ulong));
            Status = (string)(mo.Properties["Status"]?.Value);
            StatusInfo = (ushort)(mo.Properties["StatusInfo"]?.Value ?? default(ushort));
            SupportsDiskQuotas = (bool)(mo.Properties["SupportsDiskQuotas"]?.Value ?? default(bool));
            SupportsFileBasedCompression = (bool)(mo.Properties["SupportsFileBasedCompression"]?.Value ?? default(bool));
            SystemCreationClassName = (string)(mo.Properties["SystemCreationClassName"]?.Value);
            SystemName = (string)(mo.Properties["SystemName"]?.Value);
            VolumeDirty = (bool)(mo.Properties["VolumeDirty"]?.Value ?? default(bool));
            VolumeName = (string)(mo.Properties["VolumeName"]?.Value);
            VolumeSerialNumber = (string)(mo.Properties["VolumeSerialNumber"]?.Value);
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(Name);

            str.Append('\t').Append("Availability: ").Append(Decode.Availability(Availability)).AppendLine();
            str.Append('\t').Append("Access: ").Append(Decode.Access(Access)).AppendLine();
            str.Append('\t').Append("BlockSize: ").Append(BlockSize).AppendLine();
            str.Append('\t').Append("Caption: ").Append(Caption).AppendLine();
            str.Append('\t').Append("Compressed: ").Append(Compressed).AppendLine();
            str.Append('\t').Append("ConfigManagerErrorCode: ").Append(Decode.ConfigManagerErrorCode(ConfigManagerErrorCode)).AppendLine();
            str.Append('\t').Append("ConfigManagerUserConfig: ").Append(ConfigManagerUserConfig).AppendLine();
            str.Append('\t').Append("CreationClassName: ").Append(CreationClassName).AppendLine();
            str.Append('\t').Append("Description: ").Append(Description).AppendLine();
            str.Append('\t').Append("DeviceID: ").Append(DeviceID).AppendLine();
            str.Append('\t').Append("DriveType: ").Append(Decode.DriveType(DriveType)).AppendLine();
            str.Append('\t').Append("ErrorCleared: ").Append(ErrorCleared).AppendLine();
            str.Append('\t').Append("ErrorDescription: ").Append(ErrorDescription).AppendLine();
            str.Append('\t').Append("ErrorMethodology: ").Append(ErrorMethodology).AppendLine();
            str.Append('\t').Append("FileSystem: ").Append(FileSystem).AppendLine();
            str.Append('\t').Append("FreeSpace: ").Append(FreeSpace).AppendLine();
            str.Append('\t').Append("InstallDate: ").Append(InstallDate.ToString(@"u")).AppendLine();
            str.Append('\t').Append("LastErrorCode: ").Append(LastErrorCode).AppendLine();
            str.Append('\t').Append("MaximumComponentLength: ").Append(MaximumComponentLength).AppendLine();
            str.Append('\t').Append("MediaType: ").Append(Decode.MediaType(MediaType)).AppendLine();
            str.Append('\t').Append("NumberOfBlocks: ").Append(NumberOfBlocks).AppendLine();
            str.Append('\t').Append("PNPDeviceID: ").Append(PNPDeviceID).AppendLine();
            str.Append('\t').Append("PowerManagementCapabilities: ").Append(Decode.PowerManagementCapabilities(PowerManagementCapabilities)).AppendLine();
            str.Append('\t').Append("PowerManagementSupported: ").Append(PowerManagementSupported).AppendLine();
            str.Append('\t').Append("ProviderName: ").Append(ProviderName).AppendLine();
            str.Append('\t').Append("Purpose: ").Append(Purpose).AppendLine();
            str.Append('\t').Append("QuotasDisabled: ").Append(QuotasDisabled).AppendLine();
            str.Append('\t').Append("QuotasIncomplete: ").Append(QuotasIncomplete).AppendLine();
            str.Append('\t').Append("QuotasRebuilding: ").Append(QuotasRebuilding).AppendLine();
            str.Append('\t').Append("Size: ").Append(Size).AppendLine();
            str.Append('\t').Append("Status: ").Append(Status).AppendLine();
            str.Append('\t').Append("StatusInfo: ").Append(Decode.StatusInfo(StatusInfo)).AppendLine();
            str.Append('\t').Append("SupportsDiskQuotas: ").Append(SupportsDiskQuotas).AppendLine();
            str.Append('\t').Append("SupportsFileBasedCompression: ").Append(SupportsFileBasedCompression).AppendLine();
            str.Append('\t').Append("SystemCreationClassName: ").Append(SystemCreationClassName).AppendLine();
            str.Append('\t').Append("SystemName: ").Append(SystemName).AppendLine();
            str.Append('\t').Append("VolumeDirty: ").Append(VolumeDirty).AppendLine();
            str.Append('\t').Append("VolumeName: ").Append(VolumeName).AppendLine();
            str.Append('\t').Append("VolumeSerialNumber: ").Append(VolumeSerialNumber).AppendLine();
            
            return str.ToString();
        }
    }
}
