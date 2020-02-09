using System;
using System.Runtime.InteropServices;
using System.Text;

using DeviceIOControlLib.Objects.Enums;
using DeviceIOControlLib.Objects.Storage;
using DeviceIOControlLib.Utilities;
using DeviceIOControlLib.Wrapper;

using Microsoft.Win32.SafeHandles;
// ReSharper disable InconsistentNaming

namespace HDD2ndLife.WMI
{
    public struct STORAGE_DEVICE_DESCRIPTOR_PARSED_EX
    {
        public uint Version;
        public uint Size;
        public byte DeviceType;
        public byte DeviceTypeModifier;
        [MarshalAs(UnmanagedType.U1)]
        public bool RemovableMedia;
        [MarshalAs(UnmanagedType.U1)]
        public bool CommandQueueing;
        public uint VendorIdOffset;
        public uint ProductIdOffset;
        public uint ProductRevisionOffset;
        public uint SerialNumberOffset;
        public STORAGE_BUS_TYPE BusType;
        public uint RawPropertiesLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public byte[] RawDeviceProperties;
        public string SerialNumber;
        public string VendorId;
        public string ProductId;
        public string ProductRevision;
    }

    /// <summary>
    /// Stolen from DeviceIOLibrary
    /// and then modified to return the strings
    /// </summary>
    public class StorageDeviceWrapperEx : DeviceIoWrapperBase
    {
        public StorageDeviceWrapperEx(SafeFileHandle handle, bool ownsHandle = false)
            : base(handle, ownsHandle)
        {

        }

        public static T ToStructure<T>(IntPtr ptr)
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }

        public static T ByteArrayToStruct<T>(byte[] data, int index) where T : struct
        {
            using (UnmanagedMemory mem = new UnmanagedMemory(data.Length - index))
            {
                Marshal.Copy(data, index, mem.Handle, data.Length - index);
                return ToStructure<T>(mem.Handle);
            }
        }

        public static string ReadNullTerminatedAsciiString(byte[] br, int index)
        {
            byte[] nameBytes = br;
            for (int i = index; i < nameBytes.Length; i++)
            {
                if (nameBytes[i] == 0) // \0
                {
                    return Encoding.ASCII.GetString(nameBytes, index, i - index);
                }
            }

            return string.Empty;
        }

        public STORAGE_DEVICE_DESCRIPTOR_PARSED_EX StorageGetDeviceProperty()
        {
            STORAGE_PROPERTY_QUERY query = new STORAGE_PROPERTY_QUERY
            {
                QueryType = STORAGE_QUERY_TYPE.PropertyStandardQuery,
                PropertyId = STORAGE_PROPERTY_ID.StorageDeviceProperty
            };

            byte[] res = DeviceIoControlHelper.InvokeIoControlUnknownSize(Handle, IOControlCode.StorageQueryProperty, query);
            STORAGE_DEVICE_DESCRIPTOR descriptor = /*Utils.*/ByteArrayToStruct<STORAGE_DEVICE_DESCRIPTOR>(res, 0);

            STORAGE_DEVICE_DESCRIPTOR_PARSED_EX returnValue = new STORAGE_DEVICE_DESCRIPTOR_PARSED_EX
            {
                Version = descriptor.Version,
                Size = descriptor.Size,
                DeviceType = descriptor.DeviceType,
                DeviceTypeModifier = descriptor.DeviceTypeModifier,
                RemovableMedia = descriptor.RemovableMedia,
                CommandQueueing = descriptor.CommandQueueing,
                VendorIdOffset = descriptor.VendorIdOffset,
                ProductIdOffset = descriptor.ProductIdOffset,
                ProductRevisionOffset = descriptor.ProductRevisionOffset,
                SerialNumberOffset = descriptor.SerialNumberOffset,
                BusType = descriptor.BusType,
                RawPropertiesLength = descriptor.RawPropertiesLength,
                RawDeviceProperties = descriptor.RawDeviceProperties
            };
            if (returnValue.SerialNumberOffset != 0)
                returnValue.SerialNumber = ReadNullTerminatedAsciiString(res, (int)returnValue.SerialNumberOffset);
            if (returnValue.VendorIdOffset != 0)
                returnValue.VendorId = ReadNullTerminatedAsciiString(res, (int)returnValue.VendorIdOffset);
            if (returnValue.ProductIdOffset != 0)
                returnValue.ProductId = ReadNullTerminatedAsciiString(res, (int)returnValue.ProductIdOffset);
            if (returnValue.ProductRevisionOffset != 0)
                returnValue.ProductRevision = ReadNullTerminatedAsciiString(res, (int)returnValue.ProductRevisionOffset);


            return returnValue;
        }

    }
}
