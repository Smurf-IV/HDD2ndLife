using System.Windows.Forms;

using ByteSizeLib;

namespace HDD2ndLife.Controls
{
    public partial class DiskStatsView : UserControl
    {
        public DiskStatsView()
        {
            InitializeComponent();
        }

        public ulong DriveSize
        {
            set
            {
                var byteSize = ByteSize.FromBytes(value);
                lblDriveSize.Text =
                    $@"{byteSize.LargestWholeNumberBinaryValue:N2} {byteSize.LargestWholeNumberBinarySymbol}";
            }
        }
    }
}
