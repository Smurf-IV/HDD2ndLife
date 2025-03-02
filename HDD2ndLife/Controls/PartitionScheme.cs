using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Krypton.Toolkit;

using HDD2ndLife.Thirdparty;

using LoadingIndicator.WinForms;

using NLog;

namespace HDD2ndLife.Controls;

/// <summary>
/// Why GPT:
/// https://www.easeus.com/partition-master/change-dynamic-disk-to-basic.html
/// </summary>
public partial class PartitionScheme : KryptonForm
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private readonly BlockStatus[,] Blocks;
    private readonly string deviceId;
    private readonly LongOperation longOperation;
    private List<(long Start, long End)> goodPartitions;
    private int usedOffset;

    public PartitionScheme(BlockStatus[,] blocks, string deviceId)
    {
        Blocks = blocks;
        this.deviceId = deviceId;
        InitializeComponent();
        LongOperationSettings settings = LongOperationSettings.Default
                .AllowStopBeforeStartMethods()
                .HideIndicatorImmediatleyOnComplete()
            ;

        longOperation = new LongOperation(blurPanel, settings);
    }

    private async void Apply_Click(object sender, EventArgs e)
    {
        DialogResult res = KryptonMessageBox.Show(this,
            "This is going to really mess with your selected HDD.\r\nAre you Sure?",
            @"Destructive Operations about to start", KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Exclamation,
            KryptonMessageBoxDefaultButton.Button2);
        if (res != DialogResult.Yes)
            return;

        // https://stackoverflow.com/questions/36111876/positional-parameter-cannot-be-found-that-accepts-argument
        try
        {
            using IDisposable wait = longOperation.Start(true);
            using var cur = new WaitCursor(this);
            var diskNumber = deviceId.Split(new string[] { @"PHYSICALDRIVE" }, 2, StringSplitOptions.RemoveEmptyEntries)[1];

            AddLog(LogLevel.Info, @"Have to remove the partitions using 'diskpart'");
            // https://docs.microsoft.com/en-us/windows-server/storage/disk-management/change-a-dynamic-disk-back-to-a-basic-disk
            await RunDiskPart(new List<string>
            {
                $@"Sel Disk {diskNumber}",
                @"clean",
                @"exit"
            });

            //// https://docs.microsoft.com/en-us/powershell/module/storage/initialize-disk?view=win10-ps
            //AddLog(LogLevel.Info, @"'Initialize-Disk' to be sure");
            //await CallPowerShell(@"Initialize-Disk", new Dictionary<string, object>
            //{
            //    {@"-Number", diskNumber}
            //});

            //// https://docs.microsoft.com/en-us/powershell/module/storage/clear-disk?view=win10-ps
            //AddLog(LogLevel.Info, @"Force 'Clear-Disk' via PowerShell");
            //var psObjects = await CallPowerShell(@"Clear-Disk", new Dictionary<string, object>
            //{
            //    {@"-Number", diskNumber},
            //    {@"-RemoveData", null},
            //    {@"-RemoveOEM", null},
            //    {@"-Confirm:", false}
            //});

            AddLog(LogLevel.Info, @"'Initialize-Disk' ready to add partitions");
            //await CallPowerShell(@"Initialize-Disk", new Dictionary<string, object>
            //{
            //    {@"-Number", diskNumber}
            //});


            //if (psObjects == null)
            //    AddLog(LogLevel.Info, @"Sequence aborted");

            // New-Partition -Disknumber 2 -Size 100GB
            // Remove-Partition -Disknumber 2 -PartitionNumber

            //using var disk = new RawDisk(deviceId);
            //var ratio = disk.SizeBytes / usedOffset;
            //AddLog(LogLevel.Info, @"Create first partition");
            //var partition = goodPartitions[0];
            //bool firstIsBad = partition.Start != 0;
            //var gptBasicData = @"{ebd0a0a2-b9e5-4433-87c0-68b6b72699c7}";
            //PSDataCollection<PSObject> formatTargets = new PSDataCollection<PSObject>();
            //PSDataCollection<PSObject> removeTargets = new PSDataCollection<PSObject>();
            //long lastPos = 0;
            //if (firstIsBad)
            //{
            //    AddLog(LogLevel.Info, @"Add 1st Bad offset 'New-Partition");

            //    psObjects = await CallPowerShell(@"New-Partition", new Dictionary<string, object>
            //    {
            //        {@"-DiskNumber", diskNumber},
            //        {@"-Offset", 0},
            //        {@"-Size", partition.End * ratio - 1},
            //        {@"-GptType", gptBasicData},
            //        {@"-PassThru", null}
            //    });
            //    removeTargets.Add(psObjects[0]);
            //    lastPos = partition.End;
            //}
            //// https://docs.microsoft.com/en-us/powershell/module/storage/new-partition?view=win10-ps
            //for (var index = 0; index < goodPartitions.Count; index++)
            //{
            //    AddLog(LogLevel.Info, @"Add [{index}] 'New-Partition");
            //    partition = goodPartitions[index];
            //    // Do the bad
            //    if (lastPos != 0)
            //    {
            //        psObjects = await CallPowerShell(@"New-Partition", new Dictionary<string, object>
            //        {
            //            {@"-DiskNumber", diskNumber},
            //            {@"-Offset", lastPos * ratio},
            //            {@"-Size", (partition.Start - lastPos) * ratio - 1},
            //            {@"-GptType", gptBasicData}
            //        });
            //        removeTargets.Add(psObjects[0]);
            //        lastPos = partition.Start;
            //    }
            //    // Do the Good
            //    psObjects = await CallPowerShell(@"New-Partition", new Dictionary<string, object>
            //    {
            //        {@"-DiskNumber", diskNumber},
            //        {@"-Offset", lastPos * ratio},
            //        {@"-Size", (partition.End - lastPos) * ratio - 1},
            //        {@"-GptType", gptBasicData}
            //    });
            //    formatTargets.Add(psObjects[0]);
            //    lastPos = partition.End;
            //}

            //if (partition.End != usedOffset)
            //{
            //    // Do the last bad
            //    psObjects = await CallPowerShell(@"New-Partition", new Dictionary<string, object>
            //    {
            //        {@"-DiskNumber", diskNumber},
            //        {@"-Offset", lastPos * ratio},
            //        {@"-Size", (usedOffset - lastPos) * ratio - 1},
            //        {@"-GptType", gptBasicData}
            //    });
            //    removeTargets.Add(psObjects[0]);
            //}

            // https://docs.microsoft.com/en-us/powershell/module/storage/add-partitionaccesspath?view=win10-ps
            // Add - PartitionAccessPath
        }
        catch (Exception ex)
        {
            AddLog(LogLevel.Error, ex.Message);
        }
    }

    private async Task RunDiskPart(List<string> commands)
    {
        using var jClean1 = new Process
        {
            StartInfo =
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                FileName = @"C:\Windows\System32\diskpart.exe",
                RedirectStandardInput = true
            }
        };
        jClean1.Start();
        foreach (var command in commands)
        {
            jClean1.StandardInput.WriteLine(command);
        }
        AddLog(LogLevel.Warn, await jClean1.StandardOutput.ReadToEndAsync());

    }

    //private async Task<PSDataCollection<PSObject>> CallPowerShell(string command, Dictionary<string, object> parameters)
    //{
    //    try
    //    {
    //        // How to call powerShell from winform:
    //        // https://docs.microsoft.com/en-gb/archive/blogs/kebab/executing-powershell-scripts-from-c

    //        using var ps = PowerShell.Create();
    //        // Add Error handling from the PowerShell Streams
    //        // https://stackoverflow.com/questions/1233640/capturing-powershell-output-in-c-sharp-after-pipeline-invoke-throws
    //        ps.Streams.Error.DataAdded += (sender1, args) =>
    //        {
    //            ErrorRecord err = ((PSDataCollection<ErrorRecord>)sender1)[args.Index];
    //            AddLog(LogLevel.Error, err.ToString());
    //        };

    //        ps.Streams.Warning.DataAdded += (sender2, args) =>
    //        {
    //            WarningRecord warning = ((PSDataCollection<WarningRecord>)sender2)[args.Index];
    //            AddLog(LogLevel.Warn, warning.ToString());
    //        };


    //        var pipelineObjects = new PSDataCollection<PSObject>();
    //        pipelineObjects.DataAdded += (sender3, args) =>
    //        {
    //            PSObject output = ((PSDataCollection<PSObject>)sender3)[args.Index];
    //            AddLog(LogLevel.Info, output.ToString());
    //        };

    //        // specify the script code to run.
    //        ps.AddCommand(command);

    //        // specify the parameters to pass into the script.
    //        if (parameters != null)
    //            ps.AddParameters(parameters);

    //        // execute the script via
    //        // https://stackoverflow.com/questions/17640575/how-to-create-c-sharp-async-powershell-method
    //        var beginInvoke = ps.BeginInvoke<object, PSObject>(null, pipelineObjects);
    //        await Task.Factory.FromAsync(beginInvoke, pResult => ps.EndInvoke(pResult));
    //        return pipelineObjects;
    //    }
    //    catch (Exception ex)
    //    {
    //        AddLog(LogLevel.Error, ex.Message);
    //        return null;
    //    }
    //}

    private bool lastWasIncrease;
    private void AddLog(LogLevel level, string message)
    {
        Log.Log(level, message);
        lbLog.BeginInvoke((MethodInvoker)delegate
        {
            lastWasIncrease = !lastWasIncrease;
            var cur = lbLog.Items.Add($@"{DateTime.Now:u}: {level} {message}");
            lbLog.SelectedIndex = cur;
            // Send Size Changed to invoke an image update
            Height += (lastWasIncrease ? -1 : 1);
        });
    }

    private void PartitionScheme_Load(object sender, EventArgs e)
    {
        goodPartitions = new List<(long Start, long End)>();
        var lastGood = 0;
        usedOffset = 0;
        // Calculate the display characteristics
        for (var col = 0; col < Blocks.GetLength(1); col++)
        for (var row = 0; row < Blocks.GetLength(0); row++)
        {
            BlockStatus status = Blocks[row, col];
            if (status == BlockStatus.Unused)
                break;
            switch (status)
            {
                case BlockStatus.Selected:
                case BlockStatus.Failed:
                    if (lastGood != -1)
                    {
                        goodPartitions.Add((lastGood, usedOffset));
                        lastGood = -1;
                    }

                    break;
                case BlockStatus.NoWork:
                case BlockStatus.Passed:
                    if (lastGood == -1)
                        lastGood = usedOffset;
                    break;
            }

            usedOffset++;
        }

        // Add in the last one
        if (lastGood != -1)
            goodPartitions.Add((lastGood, usedOffset));

        var builder = new StringBuilder(@"Expected Partitions:");
        builder.AppendLine();
        foreach ((long Start, long End) part in goodPartitions)
        {
            builder.Append("\t").AppendLine(part.ToString());
        }

        if (goodPartitions.Count == 1)
        {
            chkSingleVolume.Checked = true;
            chkSingleVolume.Enabled = false;
        }

        lbLog.Items.Add(builder.ToString());
    }
}