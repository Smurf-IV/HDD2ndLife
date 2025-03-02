#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="Win32DiskDrive.cs" company="Smurf-IV">
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
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace HDD2ndLife.WMI;

internal class Win32DiskDrive
{
    public ushort Availability { get; private set; }
    public uint BytesPerSector { get; private set; }
    public ushort[] Capabilities { get; private set; }
    public string[] CapabilityDescriptions { get; private set; }
    public string Caption { get; private set; }
    public string CompressionMethod { get; private set; }
    public uint ConfigManagerErrorCode { get; private set; }
    public bool ConfigManagerUserConfig { get; private set; }
    public string CreationClassName { get; private set; }
    public ulong DefaultBlockSize { get; private set; }
    public string Description { get; private set; }
    public string DeviceId { get; private set; }
    public bool ErrorCleared { get; private set; }
    public string ErrorDescription { get; private set; }
    public string ErrorMethodology { get; private set; }
    public string FirmwareRevision { get; private set; }
    public uint Index { get; private set; }
    public DateTime InstallDate { get; private set; }
    public string InterfaceType { get; private set; }
    public uint LastErrorCode { get; private set; }
    public string Manufacturer { get; private set; }
    public ulong MaxBlockSize { get; private set; }
    public ulong MaxMediaSize { get; private set; }
    public bool MediaLoaded { get; private set; }
    public string MediaType { get; private set; }
    public ulong MinBlockSize { get; private set; }
    public string Model { get; private set; }
    public string Name { get; private set; }
    public bool NeedsCleaning { get; private set; }
    public uint NumberOfMediaSupported { get; private set; }
    public uint Partitions { get; private set; }
    public string PnpDeviceId { get; private set; }
    public ushort[] PowerManagementCapabilities { get; private set; }
    public bool PowerManagementSupported { get; private set; }
    public string RelativePath;
    public uint ScsiBus { get; private set; }
    public ushort ScsiLogicalUnit { get; private set; }
    public ushort ScsiPort { get; private set; }
    public ushort ScsiTargetId { get; private set; }
    public uint SectorsPerTrack { get; private set; }
    public string SerialNumber { get; private set; }
    public uint Signature { get; private set; }
    public ulong Size { get; private set; }
    public string Status { get; private set; }
    public ushort StatusInfo { get; private set; }
    public string SystemCreationClassName { get; private set; }
    public string SystemName { get; private set; }
    public ulong TotalCylinders { get; private set; }
    public uint TotalHeads { get; private set; }
    public ulong TotalSectors { get; private set; }
    public ulong TotalTracks { get; private set; }
    public uint TracksPerCylinder { get; private set; }

    public static IEnumerable<Win32DiskDrive> Retrieve(string remote, string username, string password)
    {
        var options = new ConnectionOptions
        {
            Impersonation = ImpersonationLevel.Impersonate,
            Username = username,
            Password = password
        };

        var managementScope = new ManagementScope(new ManagementPath($"\\\\{remote}\\root\\cimv2"), options);
        managementScope.Connect();

        return Retrieve(managementScope);
    }

    public static IEnumerable<Win32DiskDrive> Retrieve()
    {
        var managementScope = new ManagementScope(new ManagementPath("root\\cimv2"));
        return Retrieve(managementScope);
    }

    public static IEnumerable<Win32DiskDrive> Retrieve(ManagementScope managementScope)
    {
        var objectQuery = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
        var objectSearcher = new ManagementObjectSearcher(managementScope, objectQuery);
        ManagementObjectCollection objectCollection = objectSearcher.Get();

        foreach (ManagementObject managementObject in objectCollection)
            yield return new Win32DiskDrive
            {
                Availability = (ushort)(managementObject.Properties["Availability"]?.Value ?? default(ushort)),
                BytesPerSector = (uint)(managementObject.Properties["BytesPerSector"]?.Value ?? default(uint)),
                Capabilities = (ushort[])(managementObject.Properties["Capabilities"]?.Value ?? new ushort[0]),
                CapabilityDescriptions = (string[])(managementObject.Properties["CapabilityDescriptions"]?.Value ?? new string[0]),
                Caption = (string)(managementObject.Properties["Caption"]?.Value),
                CompressionMethod = (string)(managementObject.Properties["CompressionMethod"]?.Value),
                ConfigManagerErrorCode = (uint)(managementObject.Properties["ConfigManagerErrorCode"]?.Value ?? default(uint)),
                ConfigManagerUserConfig = (bool)(managementObject.Properties["ConfigManagerUserConfig"]?.Value ?? default(bool)),
                CreationClassName = (string)(managementObject.Properties["CreationClassName"]?.Value),
                DefaultBlockSize = (ulong)(managementObject.Properties["DefaultBlockSize"]?.Value ?? default(ulong)),
                Description = (string)(managementObject.Properties["Description"]?.Value),
                DeviceId = (string)(managementObject.Properties["DeviceID"]?.Value),
                ErrorCleared = (bool)(managementObject.Properties["ErrorCleared"]?.Value ?? default(bool)),
                ErrorDescription = (string)(managementObject.Properties["ErrorDescription"]?.Value),
                ErrorMethodology = (string)(managementObject.Properties["ErrorMethodology"]?.Value),
                FirmwareRevision = (string)(managementObject.Properties["FirmwareRevision"]?.Value),
                Index = (uint)(managementObject.Properties["Index"]?.Value ?? default(uint)),
                InstallDate = ManagementDateTimeConverter.ToDateTime(managementObject.Properties["InstallDate"]?.Value as string ?? "00010102000000.000000+060"),
                InterfaceType = (string)(managementObject.Properties["InterfaceType"]?.Value),
                LastErrorCode = (uint)(managementObject.Properties["LastErrorCode"]?.Value ?? default(uint)),
                Manufacturer = (string)(managementObject.Properties["Manufacturer"]?.Value),
                MaxBlockSize = (ulong)(managementObject.Properties["MaxBlockSize"]?.Value ?? default(ulong)),
                MaxMediaSize = (ulong)(managementObject.Properties["MaxMediaSize"]?.Value ?? default(ulong)),
                MediaLoaded = (bool)(managementObject.Properties["MediaLoaded"]?.Value ?? default(bool)),
                MediaType = (string)(managementObject.Properties["MediaType"]?.Value),
                MinBlockSize = (ulong)(managementObject.Properties["MinBlockSize"]?.Value ?? default(ulong)),
                Model = (string)(managementObject.Properties["Model"]?.Value),
                Name = (string)(managementObject.Properties["Name"]?.Value),
                NeedsCleaning = (bool)(managementObject.Properties["NeedsCleaning"]?.Value ?? default(bool)),
                NumberOfMediaSupported = (uint)(managementObject.Properties["NumberOfMediaSupported"]?.Value ?? default(uint)),
                Partitions = (uint)(managementObject.Properties["Partitions"]?.Value ?? default(uint)),
                PnpDeviceId = (string)(managementObject.Properties["PNPDeviceID"]?.Value),
                PowerManagementCapabilities = (ushort[])(managementObject.Properties["PowerManagementCapabilities"]?.Value ?? new ushort[0]),
                PowerManagementSupported = (bool)(managementObject.Properties["PowerManagementSupported"]?.Value ?? default(bool)),
                RelativePath = managementObject.Path.RelativePath,
                ScsiBus = (uint)(managementObject.Properties["SCSIBus"]?.Value ?? default(uint)),
                ScsiLogicalUnit = (ushort)(managementObject.Properties["SCSILogicalUnit"]?.Value ?? default(ushort)),
                ScsiPort = (ushort)(managementObject.Properties["SCSIPort"]?.Value ?? default(ushort)),
                ScsiTargetId = (ushort)(managementObject.Properties["SCSITargetId"]?.Value ?? default(ushort)),
                SectorsPerTrack = (uint)(managementObject.Properties["SectorsPerTrack"]?.Value ?? default(uint)),
                SerialNumber = (string)(managementObject.Properties["SerialNumber"]?.Value),
                Signature = (uint)(managementObject.Properties["Signature"]?.Value ?? default(uint)),
                Size = (ulong)(managementObject.Properties["Size"]?.Value ?? default(ulong)),
                Status = (string)(managementObject.Properties["Status"]?.Value),
                StatusInfo = (ushort)(managementObject.Properties["StatusInfo"]?.Value ?? default(ushort)),
                SystemCreationClassName = (string)(managementObject.Properties["SystemCreationClassName"]?.Value),
                SystemName = (string)(managementObject.Properties["SystemName"]?.Value),
                TotalCylinders = (ulong)(managementObject.Properties["TotalCylinders"]?.Value ?? default(ulong)),
                TotalHeads = (uint)(managementObject.Properties["TotalHeads"]?.Value ?? default(uint)),
                TotalSectors = (ulong)(managementObject.Properties["TotalSectors"]?.Value ?? default(ulong)),
                TotalTracks = (ulong)(managementObject.Properties["TotalTracks"]?.Value ?? default(ulong)),
                TracksPerCylinder = (uint)(managementObject.Properties["TracksPerCylinder"]?.Value ?? default(uint))
            };
    }

    public override string ToString()
    {
        var details = new StringBuilder();
        details.AppendLine(Caption);
        details.Append('\t').Append("Availability: ").Append(Decode.Availability(Availability)).AppendLine();
        details.Append('\t').Append("BytesPerSector: ").Append(BytesPerSector).AppendLine();
        details.Append('\t').Append("Capabilities: ").Append(Capabilities).AppendLine();
        details.Append('\t').Append("CapabilityDescriptions: ").Append(string.Join(", ", CapabilityDescriptions)).AppendLine();
        details.Append('\t').Append("CompressionMethod: ").Append(CompressionMethod).AppendLine();
        details.Append('\t').Append("ConfigManagerErrorCode: ").Append(Decode.ConfigManagerErrorCode(ConfigManagerErrorCode)).AppendLine();
        details.Append('\t').Append("ConfigManagerUserConfig: ").Append(ConfigManagerUserConfig).AppendLine();
        details.Append('\t').Append("CreationClassName: ").Append(CreationClassName).AppendLine();
        details.Append('\t').Append("DefaultBlockSize: ").Append(DefaultBlockSize).AppendLine();
        details.Append('\t').Append("Description: ").Append(Description).AppendLine();
        details.Append('\t').Append("DeviceId: ").Append(DeviceId).AppendLine();
        details.Append('\t').Append("ErrorCleared: ").Append(ErrorCleared).AppendLine();
        details.Append('\t').Append("ErrorDescription: ").Append(ErrorDescription).AppendLine();
        details.Append('\t').Append("ErrorMethodology: ").Append(ErrorMethodology).AppendLine();
        details.Append('\t').Append("FirmwareRevision: ").Append(FirmwareRevision).AppendLine();
        details.Append('\t').Append("Index: ").Append(Index).AppendLine();
        details.Append('\t').Append("InterfaceType: ").Append(InterfaceType).AppendLine();
        details.Append('\t').Append("InstallDate: ").Append(InstallDate.ToString("u")).AppendLine();
        details.Append('\t').Append("LastErrorCode: ").Append(LastErrorCode).AppendLine();
        details.Append('\t').Append("Manufacturer: ").Append(Manufacturer).AppendLine();
        details.Append('\t').Append("MaxBlockSize: ").Append(MaxBlockSize).AppendLine();
        details.Append('\t').Append("MaxMediaSize: ").Append(MaxMediaSize).AppendLine();
        details.Append('\t').Append("MediaLoaded: ").Append(MediaLoaded).AppendLine();
        details.Append('\t').Append("MediaType: ").Append(MediaType).AppendLine();
        details.Append('\t').Append("MinBlockSize: ").Append(MinBlockSize).AppendLine();
        details.Append('\t').Append("Model: ").Append(Model).AppendLine();
        details.Append('\t').Append("Name: ").Append(Name).AppendLine();
        details.Append('\t').Append("NeedsCleaning: ").Append(NeedsCleaning).AppendLine();
        details.Append('\t').Append("NumberOfMediaSupported: ").Append(NumberOfMediaSupported).AppendLine();
        details.Append('\t').Append("Partitions: ").Append(Partitions).AppendLine();
        details.Append('\t').Append("PNPDeviceID: ").Append(PnpDeviceId).AppendLine();
        details.Append('\t').Append("PowerManagementCapabilities: ").Append(Decode.PowerManagementCapabilities(PowerManagementCapabilities)).AppendLine();
        details.Append('\t').Append("PowerManagementSupported: ").Append(PowerManagementSupported).AppendLine();
        details.Append('\t').Append("SCSIBus: ").Append(ScsiBus).AppendLine();
        details.Append('\t').Append("SCSILogicalUnit: ").Append(ScsiLogicalUnit).AppendLine();
        details.Append('\t').Append("SCSIPort: ").Append(ScsiPort).AppendLine();
        details.Append('\t').Append("SCSITargetId: ").Append(ScsiTargetId).AppendLine();
        details.Append('\t').Append("SectorsPerTrack: ").Append(SectorsPerTrack).AppendLine();
        details.Append('\t').Append("SerialNumber: ").Append(SerialNumber).AppendLine();
        details.Append('\t').Append("Signature: ").Append(Signature).AppendLine();
        details.Append('\t').Append("Size: ").Append(Size).AppendLine();
        details.Append('\t').Append("Status: ").Append(Status).AppendLine();
        details.Append('\t').Append("StatusInfo: ").Append(Decode.StatusInfo(StatusInfo)).AppendLine();
        details.Append('\t').Append("SystemCreationClassName: ").Append(SystemCreationClassName).AppendLine();
        details.Append('\t').Append("SystemName: ").Append(SystemName).AppendLine();
        details.Append('\t').Append("TotalCylinders: ").Append(TotalCylinders).AppendLine();
        details.Append('\t').Append("TotalHeads: ").Append(TotalHeads).AppendLine();
        details.Append('\t').Append("TotalSectors: ").Append(TotalSectors).AppendLine();
        details.Append('\t').Append("TotalTracks: ").Append(TotalTracks).AppendLine();
        details.Append('\t').Append("TracksPerCylinder: ").Append(TracksPerCylinder).AppendLine();

        return details.ToString();
    }


}