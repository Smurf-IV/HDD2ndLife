using System.Threading;
using System.Windows.Forms;

using ByteSizeLib;

namespace HDD2ndLife.Controls
{
    public partial class DiskStatsView : UserControl
    {
        private bool scanning;

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

        private void chkUseSpeed_CheckedChanged(object sender, System.EventArgs e)
        {
            pnlSpeed.Enabled = chkUseSpeed.Checked;
        }

        private void btnStartStop_Click(object sender, System.EventArgs e)
        {
            if (scanning)
            {
                btnStartStop.Text = @"&Stopping";
                lblPhase.Text = @"Stopping";
                Thread.Sleep(1000);
                btnStartStop.Text = @"&Start";
                lblPhase.Text = @"Stopped";
                scanning = false;
            }
            else
            {
                scanning = true;
                btnStartStop.Text = @"&Stop";
                lblPhase.Text = @"Starting";
            }
        }
    }
}
