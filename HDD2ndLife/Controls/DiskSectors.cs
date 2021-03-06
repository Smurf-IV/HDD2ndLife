#region Copyright (C)
// ---------------------------------------------------------------------------------------------------------------
//  <copyright file="DiskSectors.cs" company="Smurf-IV">
// 
//  Copyright (C) 2020 - 2021 Simon Coghlan (Aka Smurf-IV)
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
using System.Windows.Forms;

using HDD2ndLife.Thirdparty;

using NLog;

namespace HDD2ndLife.Controls
{
    public class DiskSectors : Control  // Try and use the "Lowest" fastest UI access
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private const int BLOCK_SIZE = 6; // 1 line | 4 spaces for colour  -> etc.
        private const int CLUSTERS_PER_VECTOR = 64 / 3; // 64 bits / (5 represented in binary bits)
        private readonly BitVector64.Section[] sections = new BitVector64.Section[CLUSTERS_PER_VECTOR];
        private BitVector64[] clusterStatus;

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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public BlockStatus[,] Blocks { get; private set; }

        /// <summary>
        /// How Many Read Cluster blocks does this drive have, after being scaled in to 
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ulong ScaledClusterCount
        {
            set
            {
                var length = (int)Math.Max(1, Math.Ceiling(value * 1.0 / CLUSTERS_PER_VECTOR));
                Log.Debug(@"ScaledClusterCount with [{0}], resulted in length of [{1}]", value, length);
                clusterStatus = new BitVector64[length];
                for (long index = 0; index < clusterStatus.Length; index++)
                {
                    foreach (var section in sections)
                    {
                        clusterStatus[index][section] = (long)BlockStatus.NoWork;
                    }
                }

                DiskSectors_Resize(this, EventArgs.Empty);
            }
        }


        public void SetScaledClusterStatus(long readOffset, BlockStatus status)
        {
            long row = Math.DivRem(readOffset, CLUSTERS_PER_VECTOR, out var offset);
            clusterStatus[row][sections[offset]] = (long)status;
        }

        private int columnCount;
        private int rowCount;
        private bool mouseSelecting;
        private Point startCell;
        private Point lastCell;

        private void DiskSectors_Resize(object sender, EventArgs e)
        {
            columnCount = Math.Max((Width / BLOCK_SIZE) - 1, 1);
            rowCount = Math.Max((Height / BLOCK_SIZE) - 2, 1);
            #region Make sure that if the drive blocks out numbers the clusters it does not fill the scren with rubbish
            while (0 >= clusterStatus.LongLength / (columnCount * rowCount)
                && rowCount > 2)
            {
                rowCount--;
            }
            rowCount++;
            #endregion
            Blocks = new BlockStatus[columnCount, rowCount];
            RecalcStatus();
        }

        public void RecalcStatus()
        {
            long scale = Math.Max(1, clusterStatus.LongLength / (columnCount * rowCount));

            for (int row = 0; row < rowCount; row++)
            {
                long yOffset = row * columnCount;
                for (int column = 0; column < columnCount; column++)
                {
                    long statusOffset = (yOffset + column) * scale;
                    if (statusOffset > clusterStatus.LongLength)
                        break;
                    long current = 0;
                    for (int count = 0;
                        statusOffset < clusterStatus.LongLength && count < scale;
                        statusOffset++, count++)
                    {
                        for (int index = 0; index < CLUSTERS_PER_VECTOR; index++)
                        {
                            var status = clusterStatus[statusOffset][sections[index]];
                            current = Math.Max(current, status);
                        }
                    }

                    Blocks[column, row] = (BlockStatus)current;
                }
            }
            Invalidate();
        }

        private void DiskSectors_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            #region Populate Grid

            for (var x = 0; x < columnCount; x++)
                for (var y = 0; y < rowCount; y++)
                {
                    if (Blocks[x, y] == BlockStatus.Unused)
                        continue;
                    (Pen outer, Brush inner) = Blocks[x, y] switch
                    {
                        BlockStatus.NoWork => (Pens.DarkGray, Brushes.Transparent),
                        BlockStatus.Reading => (Pens.GreenYellow, Brushes.LightGoldenrodYellow),
                        BlockStatus.Writing => (Pens.DodgerBlue, Brushes.DeepSkyBlue),
                        BlockStatus.WriteDone => (Pens.DarkBlue, Brushes.SkyBlue),
                        BlockStatus.Validating => (Pens.DarkGoldenrod, Brushes.Orange),
                        BlockStatus.Failed => (Pens.DarkRed, Brushes.Red),
                        BlockStatus.Passed => (Pens.Green, Brushes.LimeGreen),
                        _ => (Pens.Transparent, Brushes.Transparent)
                    };

                    var xLoc = x * BLOCK_SIZE + 1;
                    var yLoc = y * BLOCK_SIZE + 1;
                    g.DrawRectangle(outer, xLoc, yLoc, BLOCK_SIZE - 2, BLOCK_SIZE - 2);
                    g.FillRectangle(inner, xLoc + 1, yLoc + 1, BLOCK_SIZE - 3, BLOCK_SIZE - 3);
                }

            #endregion
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;
            startCell = FindClosestCell(e.Location);
            lastCell = startCell;
            mouseSelecting = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!mouseSelecting)
                return;
            // Find the cell closest
            Point closestCell = FindClosestCell(e.Location);
            if (SetStatesFromLastToThis(closestCell))
                lastCell = closestCell;
        }

        private Point FindClosestCell(Point eLocation)
        {
            // Deal with moving outside bounds of control
            // And Outside the current "Used" region
            var column = Math.Max(0, Math.Min(columnCount - 1, eLocation.X / BLOCK_SIZE));
            var row = Math.Max(0, Math.Min(rowCount - 1, eLocation.Y / BLOCK_SIZE));
            return new Point(column, row);
        }

        private bool SetStatesFromLastToThis(Point closestCell)
        {
            // TODO: Deal with the closestCell being smaller than the Start !
            if (closestCell.Y < startCell.Y)
                return false;

            if ((closestCell.Y < lastCell.Y)
                || ((closestCell.Y == lastCell.Y)
                    && (closestCell.X < lastCell.X))
            )
            {
                // Do the magic of "Unsetting the selection"
                var rows = Math.Max(0, lastCell.Y - closestCell.Y);

                if (rows > 0)
                {
                    // Remove from end to last last to the end
                    for (var xSteps = lastCell.X; xSteps >= 0; xSteps--)
                        if (Blocks[xSteps, lastCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, lastCell.Y] = BlockStatus.Passed;
                    for (var xSteps = columnCount - 1; xSteps > lastCell.X; xSteps--)
                        if (Blocks[xSteps, lastCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, lastCell.Y] = BlockStatus.Passed;
                }
                else
                {
                    // Remove from last to here
                    for (var xSteps = lastCell.X; xSteps >= closestCell.X; xSteps--)
                        if (Blocks[xSteps, lastCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, lastCell.Y] = BlockStatus.Passed;
                }

                if (rows > 1)
                {
                    // Remove complete rows
                    for (var ySteps = lastCell.Y - 1; ySteps > closestCell.Y; ySteps--)
                    {
                        for (var xSteps = 0; xSteps < columnCount; xSteps++)
                            if (Blocks[xSteps, ySteps] != BlockStatus.Failed)
                                Blocks[xSteps, ySteps] = BlockStatus.Passed;
                    }
                }

                if (rows > 0)
                {
                    // Remove last row to the current X
                    for (var xSteps = columnCount - 1; xSteps > closestCell.X; xSteps--)
                        if (Blocks[xSteps, closestCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, closestCell.Y] = BlockStatus.Passed;

                }
            }

            if ((closestCell.Y > lastCell.Y)
                || ((closestCell.Y == lastCell.Y)
                    && (closestCell.X > lastCell.X)
                    )
                )
            {
                // Do the magic of "Setting the selection"
                // Use startCell because of the negative diagonal X above will sometimes unselect.
                var rows = Math.Max(0, closestCell.Y - startCell.Y);
                if (rows > 0)
                {
                    // Fill in from last to the end
                    for (var xSteps = startCell.X; xSteps < columnCount; xSteps++)
                        if (Blocks[xSteps, lastCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, lastCell.Y] = BlockStatus.Selected;
                }

                if (rows > 1)
                {
                    // Fill in all complete rows
                    for (var ySteps = startCell.Y + 1; ySteps < startCell.Y + rows; ySteps++)
                    {
                        for (var xSteps = 0; xSteps < columnCount; xSteps++)
                            if (Blocks[xSteps, ySteps] != BlockStatus.Failed)
                                Blocks[xSteps, ySteps] = BlockStatus.Selected;
                    }
                }

                if (rows > 0)
                {
                    // Fill in from last row to the current X
                    for (var xSteps = 0; xSteps <= closestCell.X; xSteps++)
                        if (Blocks[xSteps, closestCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, closestCell.Y] = BlockStatus.Selected;
                }
                else
                {
                    // Fill from last to here
                    for (var xSteps = startCell.X; xSteps <= closestCell.X; xSteps++)
                        if (Blocks[xSteps, closestCell.Y] != BlockStatus.Failed)
                            Blocks[xSteps, closestCell.Y] = BlockStatus.Selected;
                }
            }
            Invalidate();
            return true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseSelecting = false;
            base.OnMouseUp(e);
            Log.Debug(@"Selected from [{0}] to [{1}]", startCell, lastCell);
        }
    }

    public enum BlockStatus
    {
        // Try not to use more than 3 bits
        // because th max amount of storage a single entity can have is 2GB of memory,
        // Therefore trying to store the status of a 12TB drive with 512 Bytes per sector
        // Results in a large cluster count size, which far exceeds the "GB Mem limit".
        Unused = 0,
        NoWork,
        Passed,
        WriteDone,
        Writing,
        Reading,
        Validating,
        Selected = Validating,
        Failed,
        Max = Failed
    }
}
