#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="StorageDeviceWrapperEx.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 - 2020 Simon Coghlan (Aka Smurf-IV)
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
