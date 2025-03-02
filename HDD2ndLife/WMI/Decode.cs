#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="Decode.cs" company="Smurf-IV">
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

using System.Text;

namespace HDD2ndLife.WMI;

internal static class Decode
{
    public static string StatusInfo(ushort statusInfo)
    {
        return statusInfo switch
        {
            1 => @"Other(1)",
            2 => @"Unknown(2)",
            3 => @"Enabled(3)",
            4 => @"Disabled(4)",
            5 => @"Not Applicable(5)",
            _ => $@"Decode Unknown ({statusInfo})"
        };
    }

    public static string Access(ushort access)
    {
        return access switch
        {
            0 => @"Unknown(0)",
            1 => @"Readable (1)",
            2 => @"Writeable(2)",
            3 => @"Read / Write Supported(3)",
            4 => @"Write Once(4)",
            _ => $@"Decode Unknown ({access})"
        };
    }

    public static string DriveType(uint driveType)
    {
        return driveType switch
        {
            0 => @"Unknown(0)",
            1 => @"No Root Directory (1)",
            2 => @"Removable Disk (2)",
            3 => @"Local Disk (3)",
            4 => @"Network Drive (4)",
            5 => @"Compact Disc (5)",
            6 => @"RAM Disk (6)",
            _ => $@"Decode Unknown ({driveType})"
        };
    }

    public static string MediaType(uint mediaType)
    {
        return mediaType switch
        {
            0 => @"Format is unknown (0)",
            1 => @"5 1/4-Inch Floppy Disk - 1.2 MB - 512 bytes/sector",
            2 => @"3 1/2-Inch Floppy Disk - 1.44 MB -512 bytes/sector",
            3 => @"3 1/2-Inch Floppy Disk - 2.88 MB - 512 bytes/sector",
            4 => @"3 1/2-Inch Floppy Disk - 20.8 MB - 512 bytes/sector",
            5 => @"3 1/2-Inch Floppy Disk - 720 KB - 512 bytes/sector",
            6 => @"5 1/4-Inch Floppy Disk - 360 KB - 512 bytes/sector",
            7 => @"5 1/4-Inch Floppy Disk - 320 KB - 512 bytes/sector",
            8 => @"5 1/4-Inch Floppy Disk - 320 KB - 1024 bytes/sector",
            9 => @"5 1/4-Inch Floppy Disk - 180 KB - 512 bytes/sector",
            10 => @"5 1/4-Inch Floppy Disk - 160 KB - 512 bytes/sector",
            11 => @"Removable media other than floppy (11)",
            12 => @"Fixed hard disk media (12)",
            13 => @"3 1/2-Inch Floppy Disk - 120 MB - 512 bytes/sector",
            14 => @"3 1/2-Inch Floppy Disk - 640 KB - 512 bytes/sector",
            15 => @"5 1/4-Inch Floppy Disk - 640 KB - 512 bytes/sector",
            16 => @"5 1/4-Inch Floppy Disk - 720 KB - 512 bytes/sector",
            17 => @"3 1/2-Inch Floppy Disk - 1.2 MB - 512 bytes/sector",
            18 => @"3 1/2-Inch Floppy Disk - 1.23 MB - 1024 bytes/sector",
            19 => @"5 1/4-Inch Floppy Disk - 1.23 MB - 1024 bytes/sector",
            20 => @"3 1/2-Inch Floppy Disk - 128 MB - 512 bytes/sector",
            21 => @"3 1/2-Inch Floppy Disk - 230 MB - 512 bytes/sector",
            22 => @"8-Inch Floppy Disk - 256 KB - 128 bytes/sector",

            _ => $@"Decode Unknown ({mediaType})"
        };
    }

    public static StringBuilder PowerManagementCapabilities(ushort[] powerManagementCapabilities)
    {
        var caps = new StringBuilder();

        static string Action(int x)
        {
            return x switch
            {
                0 => @"Unknown(0)",
                1 => @"Not Supported(1)",
                2 => @"Disabled(2)",
                3 => @"Enabled(3)",
                4 => @"Power Saving Modes Entered Automatically(4)",
                5 => @"Power State Settable(5)",
                6 => @"Power Cycling Supported(6)",
                7 => @"Timed Power On Supported(7)",
                _ => $@"Decode Unknown ({x})"
            };
        }

        foreach (var capability in powerManagementCapabilities)
        {
            caps.Append(Action(capability)).Append(", ");
        }

        return caps;
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-diskdrive#members
    /// </summary>
    /// <param name="configManagerErrorCode"></param>
    public static string ConfigManagerErrorCode(uint configManagerErrorCode)
    {
        switch (configManagerErrorCode)
        {
            case 0:
                return @"Device is working properly. (0)";
            case 1:
                return @"Device is not configured correctly. (1)";
            case 2:
                return @"Windows cannot load the driver for this device. (2)";
            case 3:
                return
                    @"The driver for this device might be corrupted, or your system may be running low on memory or other resources. (3)";
            case 4:
                return
                    @"Device is not working properly.One of its drivers or your registry might be corrupted. (4)";
            case 5:
                return @"The driver for this device needs a resource that Windows cannot manage. (5)";
            case 6:
                return @"The boot configuration for this device conflicts with other devices. (6)";
            case 7:
                return @"Cannot filter. (7)";
            case 8:
                return @"The driver loader for the device is missing. (8)";
            case 9:
                return
                    @"Device is not working properly because the controlling firmware is reporting the resources for the device incorrectly. (9)";
            case 10:
                return @"Device cannot start. (10)";
            case 11:
                return @"Device failed. (11)";
            case 12:
                return @"Device cannot find enough free resources that it can use. (12)";
            case 13:
                return @"Windows cannot verify this device's resources. (13)";
            case 14:
                return @"Device cannot work properly until you restart your computer. (14)";
            case 15:
                return @"Device is not working properly because there is probably a re - enumeration problem. (15)";
            case 16:
                return @"Windows cannot identify all the resources this device uses. (16)";
            case 17:
                return @"Device is asking for an unknown resource type. (17)";
            case 18:
                return @"Reinstall the drivers for this device. (18)";
            case 19:
                return @"Failure using the VxD loader. (19)";
            case 20:
                return @"Your registry might be corrupted. (20)";
            case 21:
                return
                    @"System failure: Try changing the driver for this device.If that does not work, see your hardware documentation.Windows is removing this device. (21)";
            case 22:
                return @"Device is disabled. (22)";
            case 23:
                return
                    @"System failure: Try changing the driver for this device.If that doesn't work, see your hardware documentation. (23)";
            case 24:
                return
                    @"Device is not present, is not working properly, or does not have all its drivers installed. (24)";
            case 25:
                return @"Windows is still setting up this device. (25)";
            case 26:
                return @"Windows is still setting up this device. (26)";
            case 27:
                return @"Device does not have valid log configuration. (27)";
            case 28:
                return @"The drivers for this device are not installed. (28)";
            case 29:
                return
                    @"Device is disabled because the firmware of the device did not give it the required resources. (29)";
            case 30:
                return @"Device is using an Interrupt Request(IRQ) resource that another device is using. (30)";
            case 31:
                return
                    @"Device is not working properly because Windows cannot load the drivers required for this device. (31)";
            default:
                return $@"Decode Unknown({configManagerErrorCode})";
        }
    }

    /// <summary>
    /// This property is inherited from CIM_LogicalDevice.
    /// </summary>
    /// <param name="availability"></param>
    public static string Availability(ushort availability)
    {
        switch (availability)
        {
            case 1:
                return @"Other(1)";
            case 2:
                return @"Unknown (2)";
            case 3:
                return @"Running / Full Power(3)";
            case 4:
                return @"Warning(4)";
            case 5:
                return @"In Test(5)";
            case 6:
                return @"Not Applicable(6)";
            case 7:
                return @"Power Off(7)";
            case 8:
                return @"Off Line(8)";
            case 9:
                return @"Off Duty(9)";
            case 10:
                return @"Degraded(10)";
            case 11:
                return @"Not Installed(11)";
            case 12:
                return @"Install Error(12)";
            case 13:
                return
                    @"Power Save -Unknown(13)"; // The device is known to be in a power save mode, but its exact status is unknown.
            case 14:
                return
                    @"Power Save - Low Power Mode(14)"; // device is in a power save state but still functioning, and may exhibit degraded performance.
            case 15:
                return
                    @"Power Save -Standby(15)"; // The device is not functioning, but could be brought to full power quickly.
            case 16:
                return @"Power Cycle(16)";
            case 17:
                return
                    @"Power Save -Warning(17)"; // The device is in a warning state, though also in a power save mode.
            case 18:
                return @"Paused(18)";
            case 19:
                return @"Not Ready(19)";
            case 20:
                return @"Not Configured(20)";
            case 21:
                return @"Quiesced(21)";
            default:
                return @$"Decode Unknown({availability})";

        }
    }

    public static string AdditionalAvailability(ushort availability)
    {
        return availability switch
        {
            0 => @"Unknown(0)",
            1 => @"Other(1)",
            2 => @"Unknown (2)",
            3 => @"Running / Full Power(3)",
            4 => @"Warning(4)",
            5 => @"In Test(5)",
            6 => @"Not Applicable (6)",
            7 => @"Power Off (7)",
            8 => @"Off Line (8)",
            9 => @"Off Duty (9)",
            10 => @"Degraded(10)",
            11 => @"Not Installed(11)",
            12 => @"Install Error (12)",
            13 => @"Power Save -Unknown(13)",
            14 => @"Power Save -Low Power Mode (14)",
            15 => @"Power Save -Standby(15)",
            16 => @"Power Cycle (16)",
            17 => @"Power Save -Warning(17)",
            18 => @"Paused(18)",
            19 => @"Not Ready(19)",
            20 => @"Not Configured (20)",
            21 => @"Quiesce(21)",
            _ => $@"Decode Unknown ({availability})"
        };
    }

}