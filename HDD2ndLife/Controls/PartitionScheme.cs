using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ComponentFactory.Krypton.Toolkit;

using HDD2ndLife.Thirdparty;

using LoadingIndicator.WinForms;

using NLog;

using RawDiskLib;

using Size = System.Windows.Size;

namespace HDD2ndLife.Controls
{
    public partial class PartitionScheme : KryptonForm
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly BlockStatus[,] Blocks;
        private readonly string deviceId;
        private readonly LongOperation longOperation;
        private List<Size> goodPartitions;

        public PartitionScheme(BlockStatus[,] blocks, string deviceId)
        {
            Blocks = blocks;
            this.deviceId = deviceId;
            InitializeComponent();
            var settings = LongOperationSettings.Default
                .AllowStopBeforeStartMethods()
                .HideIndicatorImmediatleyOnComplete()
                ;

            longOperation = new LongOperation(blurPanel, settings);
        }

        private async void Apply_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/36111876/positional-parameter-cannot-be-found-that-accepts-argument
            try
            {
                using var wait = longOperation.Start(true);
                using var cur = new WaitCursor(this);
                // New-Partition -Disknumber 2 -Size 100GB
                // Remove-Partition -Disknumber 2 -PartitionNumber

                var psObjects = await CallPowerShell(@"Get-Disk", null);
                if (psObjects == null)
                    AddLog(LogLevel.Info, @"Sequence aborted");
            }
            catch (Exception ex)
            {
                AddLog(LogLevel.Error, ex.Message);
            }
        }

        private async Task<PSDataCollection<PSObject>> CallPowerShell(string command, Dictionary<string, object> parameters)
        {
            try
            {
                // How to call powerShell from winform:
                // https://docs.microsoft.com/en-gb/archive/blogs/kebab/executing-powershell-scripts-from-c

                using var ps = PowerShell.Create();
                // Add Error handling from the PowerShell Streams
                // https://stackoverflow.com/questions/1233640/capturing-powershell-output-in-c-sharp-after-pipeline-invoke-throws
                ps.Streams.Error.DataAdded += (sender1, args) =>
                {
                    ErrorRecord err = ((PSDataCollection<ErrorRecord>)sender1)[args.Index];
                    AddLog(LogLevel.Error, err.ToString());
                };

                ps.Streams.Warning.DataAdded += (sender2, args) =>
                {
                    WarningRecord warning = ((PSDataCollection<WarningRecord>)sender2)[args.Index];
                    AddLog(LogLevel.Warn, warning.ToString());
                };


                var pipelineObjects = new PSDataCollection<PSObject>();
                pipelineObjects.DataAdded += (sender3, args) =>
                {
                    PSObject output = ((PSDataCollection<PSObject>)sender3)[args.Index];
                    AddLog(LogLevel.Info, output.ToString());
                };

                // specify the script code to run.
                ps.AddCommand(command);

                // specify the parameters to pass into the script.
                if (parameters != null)
                    ps.AddParameters(parameters);

                // execute the script via
                // https://stackoverflow.com/questions/17640575/how-to-create-c-sharp-async-powershell-method
                var beginInvoke = ps.BeginInvoke<object, PSObject>(null, pipelineObjects);
                await Task.Factory.FromAsync(beginInvoke, pResult => ps.EndInvoke(pResult));
                return pipelineObjects;
            }
            catch (Exception ex)
            {
                AddLog(LogLevel.Error, ex.Message);
                return null;
            }
        }

        private void AddLog(LogLevel level, string message)
        {
            Log.Log(level, message);
            lbLog.BeginInvoke((MethodInvoker)delegate { lbLog.Items.Add($@"{DateTime.Now:u}: {level} {message}"); });
        }

        private void PartitionScheme_Load(object sender, EventArgs e)
        {
            var disk = new RawDisk(deviceId, FileAccess.Read);

            goodPartitions = new List<Size>();
            var lastGood = 0;
            int offset = 0;
            // Calculate the display characteristics
            for (var col = 0; col < Blocks.GetLength(1); col++)
                for (var row = 0; row < Blocks.GetLength(0); row++)
                {
                    var status = Blocks[row, col];
                    if (status == BlockStatus.Unused)
                        break;
                    switch (status)
                    {
                        case BlockStatus.Selected:
                        case BlockStatus.Failed:
                            if (lastGood != -1)
                            {
                                goodPartitions.Add(new Size(lastGood, offset));
                                lastGood = -1;
                            }

                            break;
                        case BlockStatus.NoWork:
                        case BlockStatus.Passed:
                            if (lastGood == -1)
                                lastGood = offset;
                            break;
                    }

                    offset++;
                }

            // Add in the last one
            goodPartitions.Add(new Size(lastGood, offset));

            var builder = new StringBuilder(@"Expected Partitions:");
            builder.AppendLine();
            foreach (var part in goodPartitions)
            {
                builder.Append("\t").AppendLine(part.ToString());
            }

            lbLog.Items.Add(builder.ToString());
        }
    }
}
