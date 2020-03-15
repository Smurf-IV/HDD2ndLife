#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskSectors.cs" company="Smurf-IV">
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
// Icon Source : <a title = "Recycle PNG" href="http://pluspng.com/recycle-png-522.html">Recycle PNG</a>
#endregion

using System;

using ComponentFactory.Krypton.Toolkit;

namespace HDD2ndLife.Controls
{
    public partial class DiskSectors : KryptonDataGridView
    {
        private const int BlockSize = 4;
        public DiskSectors()
        {
        }

        /// <summary>
        /// How Many blocks does this drive have
        /// </summary>
        public ulong NumberOfDriveBlocks { get; set; }

        private int horizontalDisplay;
        private int verticalDisplay;
        private BlockStatus[] blocks;

        private void DiskSectors_Resize(object sender, EventArgs e)
        {
            horizontalDisplay = Width / BlockSize;
            verticalDisplay = Height / BlockSize;
            blocks  = new BlockStatus[horizontalDisplay * verticalDisplay];
            RowCount = verticalDisplay;
            ColumnCount = horizontalDisplay;
        }
    }

    internal class BlockStatus
    {
    }
}
