#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="HDD2ndLife.cs" company="Smurf-IV">
// 
//  Copyright (C) 2019-2019 Simon Coghlan (Aka Smurf-IV)
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

using ComponentFactory.Krypton.Toolkit;

using NLog;

namespace HDD2ndLife
{
    public partial class Form1 : KryptonForm
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Form1()
        {
            InitializeComponent();
        }
    }
}
