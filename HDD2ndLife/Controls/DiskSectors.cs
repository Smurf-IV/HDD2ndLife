using System;

using ComponentFactory.Krypton.Toolkit;

namespace HDD2ndLife.Controls
{
    public partial class DiskSectors : KryptonDataGridView
    {
        public DiskSectors()
        {
        }

        /// <summary>
        /// How Many blocks does this drive have
        /// </summary>
        public ulong NumberOfDriveBlocks { get; set; }

        private void DiskSectors_Resize(object sender, EventArgs e)
        {

        }
    }
}
