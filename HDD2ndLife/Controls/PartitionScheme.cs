using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Windows;

using ComponentFactory.Krypton.Toolkit;

using NLog;

using RawDiskLib;

namespace HDD2ndLife.Controls
{
    public partial class PartitionScheme : KryptonForm
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly BlockStatus[,] Blocks;
        private readonly string deviceId;

        public PartitionScheme(BlockStatus[,] blocks, string deviceId)
        {
            Blocks = blocks;
            this.deviceId = deviceId;
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/36111876/positional-parameter-cannot-be-found-that-accepts-argument
            try
            {
                // New-Partition -Disknumber 2 -Size 100GB
                // Remove-Partition -Disknumber 2 -PartitionNumber

                // How to call powerShell from winform:
                // https://docs.microsoft.com/en-gb/archive/blogs/kebab/executing-powershell-scripts-from-c

                using var ps = PowerShell.Create();
                // Add Error handling from the PowerShell Streams
                // https://stackoverflow.com/questions/1233640/capturing-powershell-output-in-c-sharp-after-pipeline-invoke-throws
                ps.Streams.Error.DataAdded += (sender1, args) =>
                {
                    ErrorRecord err = ((PSDataCollection<ErrorRecord>)sender1)[args.Index];
                    Log.Error(err);
                };

                ps.Streams.Warning.DataAdded += (sender2, args) =>
                {
                    WarningRecord warning = ((PSDataCollection<WarningRecord>)sender2)[args.Index];
                    Log.Warn(warning);
                };


                var pipelineObjects = new PSDataCollection<PSObject>();
                pipelineObjects.DataAdded += (sender3, args) =>
                {
                    PSObject output = ((PSDataCollection<PSObject>)sender3)[args.Index];
                    Log.Info(output);
                };

                // specify the script code to run.
                ps.AddScript(@"Get-Disk");

                // specify the parameters to pass into the script.
                //ps.AddParameters(scriptParameters);

                // execute the script

                ps.Invoke(null, pipelineObjects);

                // print the resulting pipeline objects to the console.
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var obj in pipelineObjects)
                {
                    stringBuilder.AppendLine(obj.ToString());
                    stringBuilder.AppendLine(obj.BaseObject.ToString());
                }

                lbLog.Items.Add(stringBuilder.ToString());


            }
            catch (Exception ex)
            {
                lbLog.Items.Add(ex.Message);
            }
        }

        private void PartitionScheme_Load(object sender, EventArgs e)
        {
            var disk = new RawDisk(deviceId, FileAccess.Read);

            var good = new List<Size>();
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
                                good.Add(new Size(lastGood, offset));
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
            good.Add(new Size(lastGood, offset));

            var builder = new StringBuilder(@"Expected Partitions:");
            builder.AppendLine();
            foreach (var part in good)
            {
                builder.Append("\t").AppendLine(part.ToString());
            }

            lbLog.Items.Add(builder.ToString());
        }
    }
}
