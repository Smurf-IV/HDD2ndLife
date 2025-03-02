#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="Win32DiskPartition.cs" company="Smurf-IV">
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
using System.Management;
using System.Text;

// ReSharper disable InconsistentNaming
#pragma warning disable 649

namespace HDD2ndLife.WMI;

internal class Win32DiskPartition
{
    // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-diskpartition
#pragma warning disable IDE0044 // Add readonly modifier
    private ushort AdditionalAvailability;
    private ushort Availability;
    private ushort[] PowerManagementCapabilities;
    private string[] IdentifyingDescriptions;
    private ulong MaxQuiesceTime;
    private ulong OtherIdentifyingInfo;
    private ushort StatusInfo;
    private ulong PowerOnHours;
    private ulong TotalPowerOnHours;
    private ushort Access;
    private ulong BlockSize;
    private bool Bootable;
    private bool BootPartition;
    private string Caption;
    private uint ConfigManagerErrorCode;
    private bool ConfigManagerUserConfig;
    private string CreationClassName;
    private string Description;
    private string DeviceID;
    private uint DiskIndex;
    private bool ErrorCleared;
    private string ErrorDescription;
    private string ErrorMethodology;
    private uint HiddenSectors;
    private uint Index;
    private DateTime InstallDate;
    private uint LastErrorCode;
    private string Name;
    private ulong NumberOfBlocks;
    private string PNPDeviceID;
    private bool PowerManagementSupported;
    private bool PrimaryPartition;
    private string Purpose;
    private bool RewritePartition;
    private ulong Size;
    private ulong StartingOffset;
    private string Status;
    private string SystemCreationClassName;
    private string SystemName;
    private string Type;
#pragma warning restore IDE0044 // Add readonly modifier


    public Win32DiskPartition(ManagementObject mo)
    {
        var count = mo.Properties.Count;
        var propertyArray = new PropertyData[count];
        mo.Properties.CopyTo(propertyArray, 0);
        //AdditionalAvailability = (ushort)(mo.Properties["AdditionalAvailability"]?.Value ?? default(ushort));
        Availability = (ushort)(mo.Properties["Availability"]?.Value ?? default(ushort));
        PowerManagementCapabilities = (ushort[])(mo.Properties["PowerManagementCapabilities"]?.Value ?? new ushort[0]);
        IdentifyingDescriptions = (string[])(mo.Properties["IdentifyingDescriptions"]?.Value ?? new string[0]);
        MaxQuiesceTime = (ulong)(mo.Properties["MaxQuiesceTime"]?.Value ?? default(ulong));
        OtherIdentifyingInfo = (ulong)(mo.Properties["OtherIdentifyingInfo"]?.Value ?? default(ulong));
        StatusInfo = (ushort)(mo.Properties["StatusInfo"]?.Value ?? default(ushort));
        PowerOnHours = (ulong)(mo.Properties["PowerOnHours"]?.Value ?? default(ulong));
        TotalPowerOnHours = (ulong)(mo.Properties["TotalPowerOnHours"]?.Value ?? default(ulong));
        Access = (ushort)(mo.Properties["Access"]?.Value ?? default(ushort));
        BlockSize = (ulong)(mo.Properties["BlockSize"]?.Value ?? default(ulong));
        Bootable = (bool)(mo.Properties["Bootable"]?.Value ?? default(bool));
        BootPartition = (bool)(mo.Properties["BootPartition"]?.Value ?? default(bool));
        Caption = (string)(mo.Properties["Caption"]?.Value);
        ConfigManagerErrorCode = (uint)(mo.Properties["ConfigManagerErrorCode"]?.Value ?? default(uint));
        ConfigManagerUserConfig = (bool)(mo.Properties["ConfigManagerUserConfig"]?.Value ?? default(bool));
        CreationClassName = (string)(mo.Properties["CreationClassName"]?.Value);
        Description = (string)(mo.Properties["Description"]?.Value);
        DeviceID = (string)(mo.Properties["DeviceID"]?.Value);
        DiskIndex = (uint)(mo.Properties["DiskIndex"]?.Value ?? default(uint));
        ErrorCleared = (bool)(mo.Properties["ErrorCleared"]?.Value ?? default(bool));
        ErrorDescription = (string)(mo.Properties["ErrorDescription"]?.Value);
        ErrorMethodology = (string)(mo.Properties["ErrorMethodology"]?.Value);
        HiddenSectors = (uint)(mo.Properties["HiddenSectors"]?.Value ?? default(uint));
        Index = (uint)(mo.Properties["Index"]?.Value ?? default(uint));
        InstallDate = ManagementDateTimeConverter.ToDateTime(mo.Properties["InstallDate"]?.Value as string ?? "00010102000000.000000+060");
        LastErrorCode = (uint)(mo.Properties["LastErrorCode"]?.Value ?? default(uint));
        Name = (string)(mo.Properties["Name"]?.Value);
        NumberOfBlocks = (ulong)(mo.Properties["NumberOfBlocks"]?.Value ?? default(ulong));
        PNPDeviceID = (string)(mo.Properties["PNPDeviceID"]?.Value);
        PowerManagementSupported = (bool)(mo.Properties["PowerManagementSupported"]?.Value ?? default(bool));
        PrimaryPartition = (bool)(mo.Properties["PrimaryPartition"]?.Value ?? default(bool));
        Purpose = (string)(mo.Properties["Purpose"]?.Value);
        RewritePartition = (bool)(mo.Properties["RewritePartition"]?.Value ?? default(bool));
        Size = (ulong)(mo.Properties["Size"]?.Value ?? default(ulong));
        StartingOffset = (ulong)(mo.Properties["StartingOffset"]?.Value ?? default(ulong));
        Status = (string)(mo.Properties["StatusInfo"]?.Value);
        SystemCreationClassName = (string)(mo.Properties["SystemCreationClassName"]?.Value);
        SystemName = (string)(mo.Properties["SystemName"]?.Value);
        Type = (string)(mo.Properties["Type"]?.Value);
    }

    public override string ToString()
    {
        var str = new StringBuilder();
        str.AppendLine(Name);

        str.Append('\t').Append("AdditionalAvailability: ").Append(Decode.AdditionalAvailability(AdditionalAvailability)).AppendLine();
        str.Append('\t').Append("Availability: ").Append(Decode.Availability(Availability)).AppendLine();
        str.Append('\t').Append("PowerManagementCapabilities: ").Append(Decode.PowerManagementCapabilities(PowerManagementCapabilities)).AppendLine();
        str.Append('\t').Append("IdentifyingDescriptions: ").Append(string.Join(", ", IdentifyingDescriptions)).AppendLine();
        str.Append('\t').Append("MaxQuiesceTime: ").Append(MaxQuiesceTime).AppendLine();
        str.Append('\t').Append("OtherIdentifyingInfo: ").Append(OtherIdentifyingInfo).AppendLine();
        str.Append('\t').Append("StatusInfo: ").Append(Decode.StatusInfo(StatusInfo)).AppendLine();
        str.Append('\t').Append("PowerOnHours: ").Append(PowerOnHours).AppendLine();
        str.Append('\t').Append("TotalPowerOnHours: ").Append(TotalPowerOnHours).AppendLine();
        str.Append('\t').Append("Access: ").Append(Decode.Access(Access)).AppendLine();
        str.Append('\t').Append("BlockSize: ").Append(BlockSize).AppendLine();
        str.Append('\t').Append("Bootable: ").Append(Bootable).AppendLine();
        str.Append('\t').Append("BootPartition: ").Append(BootPartition).AppendLine();
        str.Append('\t').Append("Caption: ").Append(Caption).AppendLine();
        str.Append('\t').Append("ConfigManagerErrorCode: ").Append(Decode.ConfigManagerErrorCode(ConfigManagerErrorCode)).AppendLine();
        str.Append('\t').Append("ConfigManagerUserConfig: ").Append(ConfigManagerUserConfig).AppendLine();
        str.Append('\t').Append("CreationClassName: ").Append(CreationClassName).AppendLine();
        str.Append('\t').Append("Description: ").Append(Description).AppendLine();
        str.Append('\t').Append("DeviceID: ").Append(DeviceID).AppendLine();
        str.Append('\t').Append("DiskIndex: ").Append(DiskIndex).AppendLine();
        str.Append('\t').Append("ErrorCleared: ").Append(ErrorCleared).AppendLine();
        str.Append('\t').Append("ErrorDescription: ").Append(ErrorDescription).AppendLine();
        str.Append('\t').Append("ErrorMethodology: ").Append(ErrorMethodology).AppendLine();
        str.Append('\t').Append("HiddenSectors: ").Append(HiddenSectors).AppendLine();
        str.Append('\t').Append("Index: ").Append(Index).AppendLine();
        str.Append('\t').Append("InstallDate: ").Append(InstallDate.ToString(@"u")).AppendLine();
        str.Append('\t').Append("LastErrorCode: ").Append(LastErrorCode).AppendLine();
        str.Append('\t').Append("NumberOfBlocks: ").Append(NumberOfBlocks).AppendLine();
        str.Append('\t').Append("PNPDeviceID: ").Append(PNPDeviceID).AppendLine();
        str.Append('\t').Append("PowerManagementSupported: ").Append(PowerManagementSupported).AppendLine();
        str.Append('\t').Append("PrimaryPartition: ").Append(PrimaryPartition).AppendLine();
        str.Append('\t').Append("Purpose: ").Append(Purpose).AppendLine();
        str.Append('\t').Append("RewritePartition: ").Append(RewritePartition).AppendLine();
        str.Append('\t').Append("Size: ").Append(Size).AppendLine();
        str.Append('\t').Append("StartingOffset: ").Append(StartingOffset).AppendLine();
        str.Append('\t').Append("Status: ").Append(Status).AppendLine();
        str.Append('\t').Append("SystemCreationClassName: ").Append(SystemCreationClassName).AppendLine();
        str.Append('\t').Append("SystemName: ").Append(SystemName).AppendLine();
        str.Append('\t').Append("Type: ").Append(Type).AppendLine();

        return str.ToString();
    }
}