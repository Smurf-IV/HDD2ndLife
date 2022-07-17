#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="AssemblyInfo.cs" company="Smurf-IV">
// 
//  Copyright (C) 2019-2021 Simon Coghlan (Aka Smurf-IV)
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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("HDD2ndLife")]
[assembly: AssemblyDescription("'Remove Bad Sector Degredation Areas' via 'Marked Unusable Partitioning Scheme'")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Smurf-IV")]
[assembly: AssemblyProduct("HDD2ndLife")]
[assembly: AssemblyCopyright("Copyright © Simon Coghlan (Aka Smurf-IV) 2019-2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ecfac773-f16e-4413-ace4-0ed0d3e93aca")]

// Version information for an assembly consists of the following four values:
//
//      Major Version -> Year
//      Minor Version -> Month
//      Build Number
//      Revision    -> Day
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("22.7.246.17")]
[assembly: AssemblyFileVersion("22.7.247.17")]   // used by the installer

// TODO: Add more relevant hints here
[assembly: Dependency("System", LoadHint.Always)]
[assembly: Dependency("System.Drawing", LoadHint.Always)]
[assembly: Dependency("System.IO", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency("System.Xml", LoadHint.Always)]

[assembly: Dependency("Krypton.Toolkit", LoadHint.Always)]
[assembly: Dependency("Exceptionless", LoadHint.Always)]
[assembly: Dependency("Exceptionless.NLog", LoadHint.Always)]
[assembly: Dependency("Exceptionless.Windows", LoadHint.Always)]
[assembly: Dependency("NLog", LoadHint.Always)]
[assembly: Dependency("RawDiskLib", LoadHint.Always)]

[assembly: Dependency("System.Buffers", LoadHint.Always)]
[assembly: Dependency("System.Memory", LoadHint.Always)]
[assembly: Dependency("System.Numerics.Vectors", LoadHint.Always)]
[assembly: Dependency("System.Runtime.CompilerServices.Unsafe", LoadHint.Always)]
