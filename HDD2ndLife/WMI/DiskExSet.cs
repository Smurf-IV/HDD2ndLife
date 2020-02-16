#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskExSet.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 Simon Coghlan (Aka Smurf-IV)
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
#endregion

using System.ComponentModel;
using System.Runtime.InteropServices;

using DeviceIOControlLib.Objects.Enums;
using DeviceIOControlLib.Wrapper;

using Microsoft.Win32.SafeHandles;

namespace HDD2ndLife.WMI
{
    /// <summary>
    /// The DeviceIOControlLib does not "Do" many Set functions, but it could
    /// </summary>
    internal static class DiskExSet
    {
        public const uint DISK_ATTRIBUTE_OFFLINE = 0x1;
        public const uint DISK_ATTRIBUTE_READ_ONLY = 0x2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diskHandle"></param>
        /// <param name="online"></param>
        /// <param name="persist">If TRUE, these settings are persisted across reboots</param>
        /// <returns>success</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool SetOnline(SafeFileHandle diskHandle, bool online, bool persist)
        {
            SET_DISK_ATTRIBUTES attributes = new SET_DISK_ATTRIBUTES(online ? 0 : DISK_ATTRIBUTE_OFFLINE, persist);

            // https://docs.microsoft.com/en-us/windows/win32/api/winioctl/ni-winioctl-ioctl_disk_set_disk_attributes
            DeviceIoControlHelper.InvokeIoControl(diskHandle, IOControlCode.DiskSetDiskAttributes, attributes);
            //new Win32Exception(win32ErrorCode).Message
            return true;
        }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-set_disk_attributes
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SET_DISK_ATTRIBUTES
    {
        public uint Version;
        [MarshalAs(UnmanagedType.I1)]
        public bool Persist;
        [MarshalAs(UnmanagedType.I1)]
        public bool Reserved1A;
        [MarshalAs(UnmanagedType.I1)]
        public bool Reserved1B;
        [MarshalAs(UnmanagedType.I1)]
        public bool Reserved1C;
        public ulong Attributes;
        public ulong AttributesMask;
        public uint Reserved2A;
        public uint Reserved2B;
        public uint Reserved2C;
        public uint Reserved2D;

        public SET_DISK_ATTRIBUTES(uint diskAttributeOffline, bool persist)
        {
            Persist = persist;
            Attributes = diskAttributeOffline;
            AttributesMask = DiskExSet.DISK_ATTRIBUTE_OFFLINE;
            Version = (uint)Marshal.SizeOf(typeof(SET_DISK_ATTRIBUTES));
            Reserved1A = false;
            Reserved1B = false;
            Reserved1C = false;
            Reserved2A = 0;
            Reserved2B = 0;
            Reserved2C = 0;
            Reserved2D = 0;
        }

    }
}