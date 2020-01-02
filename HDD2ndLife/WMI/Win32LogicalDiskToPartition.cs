using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HDD2ndLife.WMI
{
    public class Win32LogicalDiskToPartition
    {
        public string VolumeSerial { get; set; }
        public string VolumeName { get; set; }
        public uint DriveMediaType { get; set; }
        public ulong TotalSpace { get; set; }
        public ulong FreeSpace { get; set; }
        public string FileSystem { get; set; }
        public uint DriveType { get; set; }
        public bool DriveCompressed { get; set; }
        public string DriveId { get; set; }
        public string DriveName { get; set; }
        /*
         https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-diskpartition
         * [Dynamic, Provider("CIMWin32"), UUID("{8502C4B8-5FBB-11D2-AAC1-006008C78BC7}"), AMENDMENT]
class Win32_DiskPartition : CIM_DiskPartition
{
  unit16   AdditionalAvailability;
  uint16   Availability;
  uint16   PowerManagementCapabilities[];
  string   IdentifyingDescriptions[1];
  uint64   MaxQuiesceTime;
  uint64   OtherIdentifyingInfo;
  uint16   StatusInfo;
  uint64   PowerOnHours;
  uint64   TotalPowerOnHours;
  uint16   Access;
  uint64   BlockSize;
  boolean  Bootable;
  boolean  BootPartition;
  string.  Caption;
  uint32   ConfigManagerErrorCode;
  boolean  ConfigManagerUserConfig;
  string.  CreationClassName;
  string   Description;
  string   DeviceID;
  uint32   DiskIndex;
  boolean  ErrorCleared;
  string   ErrorDescription;
  string   ErrorMethodology;
  uint32   HiddenSectors;
  uint32   Index;
  datetime InstallDate;
  uint32   LastErrorCode;
  string   Name;
  uint64   NumberOfBlocks;
  string   PNPDeviceID;
  boolean  PowerManagementSupported;
  boolean  PrimaryPartition;
  string   Purpose;
  boolean  RewritePartition;
  uint64   Size;
  uint64   StartingOffset;
  string   Status;
  string   SystemCreationClassName;
  string   SystemName;
  string   Type;
};
         */

        /*
         https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-logicaldisk
         * [Dynamic, Provider("CIMWin32"), SupportsUpdate, UUID("{8502C4B7-5FBB-11D2-AAC1-006008C78BC7}"), AMENDMENT]
class Win32_LogicalDisk : CIM_LogicalDisk
{
  uint16   Access;
  uint16   Availability;
  uint64   BlockSize;
  string   Caption;
  boolean  Compressed;
  uint32   ConfigManagerErrorCode;
  boolean  ConfigManagerUserConfig;
  string   CreationClassName;
  string   Description;
  string   DeviceID;
  uint32   DriveType;
  boolean  ErrorCleared;
  string   ErrorDescription;
  string   ErrorMethodology;
  string   FileSystem;
  uint64   FreeSpace;
  datetime InstallDate;
  uint32   LastErrorCode;
  uint32   MaximumComponentLength;
  uint32   MediaType;
  string   Name;
  uint64   NumberOfBlocks;
  string   PNPDeviceID;
  uint16   PowerManagementCapabilities[];
  boolean  PowerManagementSupported;
  string   ProviderName;
  string   Purpose;
  boolean  QuotasDisabled;
  boolean  QuotasIncomplete;
  boolean  QuotasRebuilding;
  uint64   Size;
  string   Status;
  uint16   StatusInfo;
  boolean  SupportsDiskQuotas;
  boolean  SupportsFileBasedCompression;
  string   SystemCreationClassName;
  string   SystemName;
  boolean  VolumeDirty;
  string   VolumeName;
  string   VolumeSerialNumber;
};
         */
        internal Win32LogicalDiskToPartition(ManagementObject ld)
        {
            DriveName = Convert.ToString(ld.Properties["Name"].Value);
            DriveId = Convert.ToString(ld.Properties["DeviceId"].Value);
            DriveCompressed = Convert.ToBoolean(ld.Properties["Compressed"].Value);
            DriveType = Convert.ToUInt32(ld.Properties["DriveType"].Value);
            FileSystem = Convert.ToString(ld.Properties["FileSystem"].Value);
            FreeSpace = Convert.ToUInt64(ld.Properties["FreeSpace"].Value);
            TotalSpace = Convert.ToUInt64(ld.Properties["Size"].Value);
            DriveMediaType = Convert.ToUInt32(ld.Properties["MediaType"].Value);
            VolumeName = Convert.ToString(ld.Properties["VolumeName"].Value);
            VolumeSerial = Convert.ToString(ld.Properties["VolumeSerialNumber"].Value);

        }
    }
}
