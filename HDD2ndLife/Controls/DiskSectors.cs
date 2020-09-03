#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskSectors.cs" company="Smurf-IV">
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using EnumsNET;

using HDD2ndLife.Thirdparty;

namespace HDD2ndLife.Controls
{
    public class DiskSectors : Control  // Try and use the "Lowest" fastest UI access
    {
        private const int BLOCK_SIZE = 5; // 1 line | 4 spaces for colour  -> etc.
        private const int CLUSTERS_PER_VECTOR = 64 / 3; // 64 bits / (5 represented in binary bits)
        private readonly BitVector64.Section[] sections = new BitVector64.Section[CLUSTERS_PER_VECTOR];

        public DiskSectors()
        {
            DoubleBuffered = true;
            Margin = new Padding(0);
            Size = new Size(310, 280);
            Resize += DiskSectors_Resize;
            Paint += DiskSectors_Paint;

            // Force initial layout
            sections[0] = BitVector64.CreateSection((int)BlockStatus.Max);
            for (var i = 1; i < CLUSTERS_PER_VECTOR; i++)
            {
                sections[i] = BitVector64.CreateSection((int)BlockStatus.Max, sections[i - 1]);
            }

            ScaledClusterCount = 1;
        }

        /// <summary>
        /// How Many Read Cluster blocks does this drive have, after being scaled in to 
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ulong ScaledClusterCount
        {
            set
            {
                var length = Math.Max(1, value / CLUSTERS_PER_VECTOR);
                ClusterStatus = new BitVector64[length];
                for (long index = 0; index < ClusterStatus.Length; index++)
                {
                    foreach (var section in sections)
                    {
                        ClusterStatus[index][section] = (long)BlockStatus.NoWork;
                    }
                }

                DiskSectors_Resize(this, EventArgs.Empty);
            }
        }

        private BitVector64[] ClusterStatus { get; set; }

        public void SetScaledClusterStatus(long readOffset, BlockStatus status)
        {
            long row = Math.DivRem(readOffset, CLUSTERS_PER_VECTOR, out var offset);
            ClusterStatus[row][sections[offset]] = Math.Max(ClusterStatus[row][sections[offset]], (long)status);
        }

        private int columnCount;
        private int rowCount;
        private BlockStatus[,] blocks;

        private void DiskSectors_Resize(object sender, EventArgs e)
        {
            columnCount = (Width / BLOCK_SIZE) - 1;
            rowCount = (Height / BLOCK_SIZE) - 1;
            blocks = new BlockStatus[columnCount, rowCount];
            RecalcStatus();
        }

        public void RecalcStatus()
        {
            long scale = ClusterStatus.LongLength / (columnCount * rowCount);
            if (scale == 0) return;

            for (int row = 0; row < rowCount; row++)
            {
                long yOffset = row * columnCount;
                for (int column = 0; column < columnCount; column++)
                {
                    long statusOffset = (yOffset + column) * scale;
                    if (statusOffset > ClusterStatus.LongLength)
                        break;
                    int countPasses = 0;
                    long current = 0;
                    for (int count = 0;
                        statusOffset < ClusterStatus.LongLength && count < scale;
                        statusOffset++, count++)
                    {
                        for (int index = 0; index < CLUSTERS_PER_VECTOR; index++)
                        {
                            var status = ClusterStatus[statusOffset][sections[index]];
                            if (status == (long) BlockStatus.Passed)
                            {
                                countPasses++;
                            }
                            else
                                current = Math.Max(current, status);
                        }
                    }

                    if (countPasses == scale * CLUSTERS_PER_VECTOR)
                    {
                        current = (long) BlockStatus.Passed;
                    }

                    blocks[column, row] = (BlockStatus)current;
                }
            }
            Invalidate();
        }

        private void DiskSectors_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            #region Draw Grid

            var p = Pens.Black;

            for (var y = 0; y <= rowCount; ++y)
            {
                g.DrawLine(p, 0, y * BLOCK_SIZE, columnCount * BLOCK_SIZE, y * BLOCK_SIZE);
            }

            for (var x = 0; x <= columnCount; ++x)
            {
                g.DrawLine(p, x * BLOCK_SIZE, 0, x * BLOCK_SIZE, rowCount * BLOCK_SIZE);
            }
            #endregion Draw Grid

            #region Populate Grid

            for (var x = 0; x < columnCount; x++)
                for (var y = 0; y < rowCount; y++)
                {
                    (Pen outer, Brush inner) target = blocks[x, y] switch
                    {
                        BlockStatus.NoWork => (Pens.LightGray, Brushes.Gray),
                        BlockStatus.Reading => (Pens.LightYellow, Brushes.Yellow),
                        BlockStatus.Writing => (Pens.CornflowerBlue, Brushes.DodgerBlue),
                        BlockStatus.Validating => (Pens.Orange, Brushes.DarkOrange),
                        BlockStatus.Failed => (Pens.MistyRose, Brushes.Red),
                        BlockStatus.Passed => (Pens.LightGreen, Brushes.LimeGreen),
                        _ => (Pens.LightGray, Brushes.LightGray)
                    };

                    var xLoc = x * BLOCK_SIZE + 1;
                    var yLoc = y * BLOCK_SIZE + 1;
                    g.DrawRectangle(target.outer, xLoc, yLoc, BLOCK_SIZE - 2, BLOCK_SIZE - 2);
                    g.FillRectangle(target.inner, xLoc + 1, yLoc + 1, BLOCK_SIZE - 2, BLOCK_SIZE - 2);
                }

            #endregion
        }

    }

    public enum BlockStatus
    {
        Unused = 0,
        NoWork,
        Writing,
        Reading,
        Validating,
        Passed,
        Failed,
        Max = Failed
    }
}
