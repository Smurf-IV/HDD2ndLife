namespace HDD2ndLife.Controls
{
    partial class DiskStatsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            lblPassed = new Krypton.Toolkit.KryptonLabel();
            lblFailed = new Krypton.Toolkit.KryptonLabel();
            lblValidating = new Krypton.Toolkit.KryptonLabel();
            lblWriteDone = new Krypton.Toolkit.KryptonLabel();
            lblWriting = new Krypton.Toolkit.KryptonLabel();
            lblReading = new Krypton.Toolkit.KryptonLabel();
            lblNoWork = new Krypton.Toolkit.KryptonLabel();
            lblPhase = new Elucidate.Shared.TextOverProgressBar();
            kryptonLabel7 = new Krypton.Toolkit.KryptonLabel();
            btnPartitioning = new Krypton.Toolkit.KryptonButton();
            btnStartStop = new Krypton.Toolkit.KryptonButton();
            kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            chkUseSpeed = new Krypton.Toolkit.KryptonCheckBox();
            chkFailFirst = new Krypton.Toolkit.KryptonCheckBox();
            pnlSpeed = new Krypton.Toolkit.KryptonPanel();
            kryptonBorderEdge4 = new Krypton.Toolkit.KryptonBorderEdge();
            kryptonBorderEdge3 = new Krypton.Toolkit.KryptonBorderEdge();
            kryptonBorderEdge2 = new Krypton.Toolkit.KryptonBorderEdge();
            kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            rb75 = new Krypton.Toolkit.KryptonRadioButton();
            rb30 = new Krypton.Toolkit.KryptonRadioButton();
            rb50 = new Krypton.Toolkit.KryptonRadioButton();
            rb20 = new Krypton.Toolkit.KryptonRadioButton();
            grpScanType = new Krypton.Toolkit.KryptonGroupBox();
            rb2Pass = new Krypton.Toolkit.KryptonRadioButton();
            rbWrite = new Krypton.Toolkit.KryptonRadioButton();
            rbRead = new Krypton.Toolkit.KryptonRadioButton();
            lblTimeRemaining = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            lblSpeed = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            lblDriveSize = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            tmrUpdate = new System.Windows.Forms.Timer(components);
            diskSectors1 = new HDD2ndLife.Controls.DiskSectors();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).BeginInit();
            kryptonGroupBox2.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlSpeed).BeginInit();
            pnlSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grpScanType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grpScanType.Panel).BeginInit();
            grpScanType.Panel.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.AutoScroll = true;
            kryptonPanel1.AutoScrollMinSize = new System.Drawing.Size(150, 550);
            kryptonPanel1.Controls.Add(lblPassed);
            kryptonPanel1.Controls.Add(lblFailed);
            kryptonPanel1.Controls.Add(lblValidating);
            kryptonPanel1.Controls.Add(lblWriteDone);
            kryptonPanel1.Controls.Add(lblWriting);
            kryptonPanel1.Controls.Add(lblReading);
            kryptonPanel1.Controls.Add(lblNoWork);
            kryptonPanel1.Controls.Add(lblPhase);
            kryptonPanel1.Controls.Add(kryptonLabel7);
            kryptonPanel1.Controls.Add(btnPartitioning);
            kryptonPanel1.Controls.Add(btnStartStop);
            kryptonPanel1.Controls.Add(kryptonGroupBox2);
            kryptonPanel1.Controls.Add(grpScanType);
            kryptonPanel1.Controls.Add(lblTimeRemaining);
            kryptonPanel1.Controls.Add(kryptonLabel6);
            kryptonPanel1.Controls.Add(lblSpeed);
            kryptonPanel1.Controls.Add(kryptonLabel4);
            kryptonPanel1.Controls.Add(lblDriveSize);
            kryptonPanel1.Controls.Add(kryptonLabel1);
            kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            kryptonPanel1.Location = new System.Drawing.Point(862, 0);
            kryptonPanel1.Margin = new System.Windows.Forms.Padding(4);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new System.Drawing.Size(249, 1007);
            kryptonPanel1.TabIndex = 0;
            // 
            // lblPassed
            // 
            lblPassed.Location = new System.Drawing.Point(9, 760);
            lblPassed.Name = "lblPassed";
            lblPassed.Size = new System.Drawing.Size(92, 25);
            lblPassed.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblPassed.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblPassed.TabIndex = 19;
            lblPassed.Values.Text = "- Passed";
            // 
            // lblFailed
            // 
            lblFailed.Location = new System.Drawing.Point(9, 741);
            lblFailed.Name = "lblFailed";
            lblFailed.Size = new System.Drawing.Size(92, 25);
            lblFailed.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblFailed.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblFailed.TabIndex = 18;
            lblFailed.Values.Text = "- Failed";
            // 
            // lblValidating
            // 
            lblValidating.Location = new System.Drawing.Point(9, 722);
            lblValidating.Name = "lblValidating";
            lblValidating.Size = new System.Drawing.Size(131, 25);
            lblValidating.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblValidating.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblValidating.TabIndex = 17;
            lblValidating.Values.Text = "- Validating";
            // 
            // lblWriteDone
            // 
            lblWriteDone.Location = new System.Drawing.Point(9, 703);
            lblWriteDone.Name = "lblWriteDone";
            lblWriteDone.Size = new System.Drawing.Size(131, 25);
            lblWriteDone.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblWriteDone.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblWriteDone.TabIndex = 16;
            lblWriteDone.Values.Text = "- Write Done";
            // 
            // lblWriting
            // 
            lblWriting.Location = new System.Drawing.Point(9, 684);
            lblWriting.Name = "lblWriting";
            lblWriting.Size = new System.Drawing.Size(102, 25);
            lblWriting.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblWriting.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblWriting.TabIndex = 15;
            lblWriting.Values.Text = "- Writing";
            // 
            // lblReading
            // 
            lblReading.Location = new System.Drawing.Point(9, 665);
            lblReading.Name = "lblReading";
            lblReading.Size = new System.Drawing.Size(102, 25);
            lblReading.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblReading.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblReading.TabIndex = 14;
            lblReading.Values.Text = "- Reading";
            // 
            // lblNoWork
            // 
            lblNoWork.Location = new System.Drawing.Point(9, 646);
            lblNoWork.Name = "lblNoWork";
            lblNoWork.Size = new System.Drawing.Size(102, 25);
            lblNoWork.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            lblNoWork.StateCommon.ShortText.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterLeft;
            lblNoWork.TabIndex = 13;
            lblNoWork.Values.Text = "- No Work";
            // 
            // lblPhase
            // 
            lblPhase.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblPhase.BackColor = System.Drawing.Color.Transparent;
            lblPhase.DisplayText = "Test";
            lblPhase.Location = new System.Drawing.Point(9, 573);
            lblPhase.Margin = new System.Windows.Forms.Padding(4);
            lblPhase.Name = "lblPhase";
            lblPhase.ShowInTaskbar = true;
            lblPhase.Size = new System.Drawing.Size(225, 31);
            lblPhase.Step = 1;
            lblPhase.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            lblPhase.TabIndex = 11;
            lblPhase.TabStop = false;
            lblPhase.Text = "Test";
            lblPhase.TextAlignment = System.Drawing.StringAlignment.Center;
            lblPhase.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // kryptonLabel7
            // 
            kryptonLabel7.Location = new System.Drawing.Point(9, 544);
            kryptonLabel7.Margin = new System.Windows.Forms.Padding(4);
            kryptonLabel7.Name = "kryptonLabel7";
            kryptonLabel7.Size = new System.Drawing.Size(74, 33);
            kryptonLabel7.TabIndex = 10;
            kryptonLabel7.TabStop = false;
            kryptonLabel7.Values.Text = "Phase:";
            // 
            // btnPartitioning
            // 
            btnPartitioning.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnPartitioning.Location = new System.Drawing.Point(9, 607);
            btnPartitioning.Margin = new System.Windows.Forms.Padding(4);
            btnPartitioning.Name = "btnPartitioning";
            btnPartitioning.Size = new System.Drawing.Size(225, 31);
            btnPartitioning.TabIndex = 9;
            btnPartitioning.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnPartitioning.Values.Text = "Par&tition Scheme";
            btnPartitioning.Click += btnPartitioning_Click;
            // 
            // btnStartStop
            // 
            btnStartStop.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnStartStop.Location = new System.Drawing.Point(9, 317);
            btnStartStop.Margin = new System.Windows.Forms.Padding(4);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new System.Drawing.Size(225, 31);
            btnStartStop.TabIndex = 8;
            btnStartStop.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            btnStartStop.Values.Text = "&Start";
            btnStartStop.Click += btnStartStop_Click;
            // 
            // kryptonGroupBox2
            // 
            kryptonGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            kryptonGroupBox2.Location = new System.Drawing.Point(0, 154);
            kryptonGroupBox2.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            kryptonGroupBox2.Panel.Controls.Add(chkUseSpeed);
            kryptonGroupBox2.Panel.Controls.Add(chkFailFirst);
            kryptonGroupBox2.Panel.Controls.Add(pnlSpeed);
            kryptonGroupBox2.Size = new System.Drawing.Size(242, 154);
            kryptonGroupBox2.TabIndex = 7;
            kryptonGroupBox2.Values.Heading = "Options";
            // 
            // chkUseSpeed
            // 
            chkUseSpeed.Enabled = false;
            chkUseSpeed.Location = new System.Drawing.Point(7, 32);
            chkUseSpeed.Margin = new System.Windows.Forms.Padding(4);
            chkUseSpeed.Name = "chkUseSpeed";
            chkUseSpeed.Size = new System.Drawing.Size(136, 33);
            chkUseSpeed.TabIndex = 1;
            chkUseSpeed.ToolTipValues.Description = "Mark block warning if throughput drops off";
            chkUseSpeed.ToolTipValues.EnableToolTips = true;
            chkUseSpeed.ToolTipValues.Heading = "Use Speed";
            chkUseSpeed.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            chkUseSpeed.Values.Text = "&Use Speed";
            chkUseSpeed.CheckedChanged += chkUseSpeed_CheckedChanged;
            // 
            // chkFailFirst
            // 
            chkFailFirst.Location = new System.Drawing.Point(7, -3);
            chkFailFirst.Margin = new System.Windows.Forms.Padding(4);
            chkFailFirst.Name = "chkFailFirst";
            chkFailFirst.Size = new System.Drawing.Size(205, 33);
            chkFailFirst.TabIndex = 0;
            chkFailFirst.ToolTipValues.Description = "Stop Processing on First failure.";
            chkFailFirst.ToolTipValues.EnableToolTips = true;
            chkFailFirst.ToolTipValues.Heading = "Fail First";
            chkFailFirst.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            chkFailFirst.Values.Text = "&Fail Fast (On First)";
            // 
            // pnlSpeed
            // 
            pnlSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlSpeed.Controls.Add(kryptonBorderEdge4);
            pnlSpeed.Controls.Add(kryptonBorderEdge3);
            pnlSpeed.Controls.Add(kryptonBorderEdge2);
            pnlSpeed.Controls.Add(kryptonBorderEdge1);
            pnlSpeed.Controls.Add(rb75);
            pnlSpeed.Controls.Add(rb30);
            pnlSpeed.Controls.Add(rb50);
            pnlSpeed.Controls.Add(rb20);
            pnlSpeed.Enabled = false;
            pnlSpeed.Location = new System.Drawing.Point(15, 50);
            pnlSpeed.Margin = new System.Windows.Forms.Padding(4);
            pnlSpeed.Name = "pnlSpeed";
            pnlSpeed.Size = new System.Drawing.Size(219, 68);
            pnlSpeed.TabIndex = 2;
            // 
            // kryptonBorderEdge4
            // 
            kryptonBorderEdge4.Dock = System.Windows.Forms.DockStyle.Left;
            kryptonBorderEdge4.Location = new System.Drawing.Point(0, 1);
            kryptonBorderEdge4.Margin = new System.Windows.Forms.Padding(4);
            kryptonBorderEdge4.Name = "kryptonBorderEdge4";
            kryptonBorderEdge4.Orientation = System.Windows.Forms.Orientation.Vertical;
            kryptonBorderEdge4.Size = new System.Drawing.Size(1, 66);
            kryptonBorderEdge4.Text = "kryptonBorderEdge4";
            // 
            // kryptonBorderEdge3
            // 
            kryptonBorderEdge3.Dock = System.Windows.Forms.DockStyle.Bottom;
            kryptonBorderEdge3.Location = new System.Drawing.Point(0, 67);
            kryptonBorderEdge3.Margin = new System.Windows.Forms.Padding(4);
            kryptonBorderEdge3.Name = "kryptonBorderEdge3";
            kryptonBorderEdge3.Size = new System.Drawing.Size(218, 1);
            kryptonBorderEdge3.Text = "kryptonBorderEdge3";
            // 
            // kryptonBorderEdge2
            // 
            kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Right;
            kryptonBorderEdge2.Location = new System.Drawing.Point(218, 1);
            kryptonBorderEdge2.Margin = new System.Windows.Forms.Padding(4);
            kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            kryptonBorderEdge2.Orientation = System.Windows.Forms.Orientation.Vertical;
            kryptonBorderEdge2.Size = new System.Drawing.Size(1, 67);
            kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // kryptonBorderEdge1
            // 
            kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(4);
            kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            kryptonBorderEdge1.Size = new System.Drawing.Size(219, 1);
            kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // rb75
            // 
            rb75.Location = new System.Drawing.Point(81, 39);
            rb75.Margin = new System.Windows.Forms.Padding(4);
            rb75.Name = "rb75";
            rb75.Size = new System.Drawing.Size(76, 33);
            rb75.TabIndex = 3;
            rb75.Values.Text = "75%";
            // 
            // rb30
            // 
            rb30.Location = new System.Drawing.Point(81, 7);
            rb30.Margin = new System.Windows.Forms.Padding(4);
            rb30.Name = "rb30";
            rb30.Size = new System.Drawing.Size(76, 33);
            rb30.TabIndex = 2;
            rb30.Values.Text = "30%";
            // 
            // rb50
            // 
            rb50.Checked = true;
            rb50.Location = new System.Drawing.Point(5, 39);
            rb50.Margin = new System.Windows.Forms.Padding(4);
            rb50.Name = "rb50";
            rb50.Size = new System.Drawing.Size(76, 33);
            rb50.TabIndex = 1;
            rb50.Values.Text = "50%";
            // 
            // rb20
            // 
            rb20.Location = new System.Drawing.Point(5, 7);
            rb20.Margin = new System.Windows.Forms.Padding(4);
            rb20.Name = "rb20";
            rb20.Size = new System.Drawing.Size(76, 33);
            rb20.TabIndex = 0;
            rb20.Values.Text = "20%";
            // 
            // grpScanType
            // 
            grpScanType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpScanType.Location = new System.Drawing.Point(0, 0);
            grpScanType.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            grpScanType.Panel.Controls.Add(rb2Pass);
            grpScanType.Panel.Controls.Add(rbWrite);
            grpScanType.Panel.Controls.Add(rbRead);
            grpScanType.Size = new System.Drawing.Size(242, 149);
            grpScanType.TabIndex = 6;
            grpScanType.Values.Heading = "Scan Type:";
            // 
            // rb2Pass
            // 
            rb2Pass.Location = new System.Drawing.Point(7, 79);
            rb2Pass.Margin = new System.Windows.Forms.Padding(4);
            rb2Pass.Name = "rb2Pass";
            rb2Pass.Size = new System.Drawing.Size(215, 33);
            rb2Pass.TabIndex = 3;
            rb2Pass.ToolTipValues.Description = "Using a differnet pattern on each pass,\r\nPerform a linear Write, then,\r\na Read verification on the pattern.\r\n!! ALL DATA WILL BE DESTROYED !!";
            rb2Pass.ToolTipValues.EnableToolTips = true;
            rb2Pass.ToolTipValues.Heading = "2 Pass Verify";
            rb2Pass.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            rb2Pass.Values.Text = "2 &Pass (W/R) Verify";
            // 
            // rbWrite
            // 
            rbWrite.Location = new System.Drawing.Point(7, 39);
            rbWrite.Margin = new System.Windows.Forms.Padding(4);
            rbWrite.Name = "rbWrite";
            rbWrite.Size = new System.Drawing.Size(181, 33);
            rbWrite.TabIndex = 1;
            rbWrite.ToolTipValues.Description = "Perform a Write, then,\r\na linear Read.\r\n!! ALL DATA WILL BE DESTROYED !!";
            rbWrite.ToolTipValues.EnableToolTips = true;
            rbWrite.ToolTipValues.Heading = "Write (+ Read)";
            rbWrite.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            rbWrite.Values.Text = "&Write (+ Verify)";
            // 
            // rbRead
            // 
            rbRead.Checked = true;
            rbRead.Location = new System.Drawing.Point(7, 5);
            rbRead.Margin = new System.Windows.Forms.Padding(4);
            rbRead.Name = "rbRead";
            rbRead.Size = new System.Drawing.Size(133, 33);
            rbRead.TabIndex = 0;
            rbRead.ToolTipValues.Description = "Performs a linear Read of existing sectors.\r\nNone destructive.";
            rbRead.ToolTipValues.EnableToolTips = true;
            rbRead.ToolTipValues.Heading = "Read Only";
            rbRead.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            rbRead.Values.Text = "&Read Only";
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblTimeRemaining.AutoSize = false;
            lblTimeRemaining.Location = new System.Drawing.Point(76, 514);
            lblTimeRemaining.Margin = new System.Windows.Forms.Padding(4);
            lblTimeRemaining.Name = "lblTimeRemaining";
            lblTimeRemaining.Size = new System.Drawing.Size(163, 23);
            lblTimeRemaining.TabIndex = 5;
            lblTimeRemaining.TabStop = false;
            lblTimeRemaining.Values.Text = "1234 hrs";
            // 
            // kryptonLabel6
            // 
            kryptonLabel6.Location = new System.Drawing.Point(7, 482);
            kryptonLabel6.Margin = new System.Windows.Forms.Padding(4);
            kryptonLabel6.Name = "kryptonLabel6";
            kryptonLabel6.Size = new System.Drawing.Size(215, 33);
            kryptonLabel6.TabIndex = 4;
            kryptonLabel6.TabStop = false;
            kryptonLabel6.Values.Text = "Phase Time Left (est):";
            // 
            // lblSpeed
            // 
            lblSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblSpeed.AutoSize = false;
            lblSpeed.Location = new System.Drawing.Point(76, 451);
            lblSpeed.Margin = new System.Windows.Forms.Padding(4);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new System.Drawing.Size(163, 23);
            lblSpeed.TabIndex = 3;
            lblSpeed.TabStop = false;
            lblSpeed.Values.Text = "1234 GoogleB/s";
            // 
            // kryptonLabel4
            // 
            kryptonLabel4.Location = new System.Drawing.Point(7, 419);
            kryptonLabel4.Margin = new System.Windows.Forms.Padding(4);
            kryptonLabel4.Name = "kryptonLabel4";
            kryptonLabel4.Size = new System.Drawing.Size(78, 33);
            kryptonLabel4.TabIndex = 2;
            kryptonLabel4.TabStop = false;
            kryptonLabel4.Values.Text = "Speed:";
            // 
            // lblDriveSize
            // 
            lblDriveSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblDriveSize.AutoSize = false;
            lblDriveSize.Location = new System.Drawing.Point(73, 387);
            lblDriveSize.Margin = new System.Windows.Forms.Padding(4);
            lblDriveSize.Name = "lblDriveSize";
            lblDriveSize.Size = new System.Drawing.Size(163, 25);
            lblDriveSize.TabIndex = 1;
            lblDriveSize.TabStop = false;
            lblDriveSize.Values.Text = "123456789GoogleB";
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.Location = new System.Drawing.Point(7, 355);
            kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new System.Drawing.Size(56, 33);
            kryptonLabel1.TabIndex = 0;
            kryptonLabel1.TabStop = false;
            kryptonLabel1.Values.Text = "Size:";
            // 
            // tmrUpdate
            // 
            tmrUpdate.Interval = 250;
            tmrUpdate.Tick += new System.EventHandler(tmrUpdate_Tick);
            // 
            // diskSectors1
            // 
            diskSectors1.Dock = System.Windows.Forms.DockStyle.Fill;
            diskSectors1.Location = new System.Drawing.Point(0, 0);
            diskSectors1.Margin = new System.Windows.Forms.Padding(4);
            diskSectors1.Name = "diskSectors1";
            diskSectors1.Size = new System.Drawing.Size(844, 791);
            diskSectors1.TabIndex = 0;
            // 
            // DiskStatsView
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(diskSectors1);
            Controls.Add(kryptonPanel1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "DiskStatsView";
            Size = new System.Drawing.Size(1111, 1007);
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).EndInit();
            kryptonGroupBox2.Panel.ResumeLayout(false);
            kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlSpeed).EndInit();
            pnlSpeed.ResumeLayout(false);
            pnlSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grpScanType.Panel).EndInit();
            grpScanType.Panel.ResumeLayout(false);
            grpScanType.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grpScanType).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel lblTimeRemaining;
        private Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private Krypton.Toolkit.KryptonLabel lblSpeed;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonLabel lblDriveSize;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private DiskSectors diskSectors1;
        private Krypton.Toolkit.KryptonGroupBox grpScanType;
        private Krypton.Toolkit.KryptonRadioButton rb2Pass;
        private Krypton.Toolkit.KryptonRadioButton rbWrite;
        private Krypton.Toolkit.KryptonRadioButton rbRead;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonCheckBox chkUseSpeed;
        private Krypton.Toolkit.KryptonCheckBox chkFailFirst;
        private Krypton.Toolkit.KryptonPanel pnlSpeed;
        private Krypton.Toolkit.KryptonRadioButton rb75;
        private Krypton.Toolkit.KryptonRadioButton rb30;
        private Krypton.Toolkit.KryptonRadioButton rb50;
        private Krypton.Toolkit.KryptonRadioButton rb20;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge4;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge3;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonButton btnStartStop;
        private Krypton.Toolkit.KryptonButton btnPartitioning;
        private Elucidate.Shared.TextOverProgressBar lblPhase;
        private Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private System.Windows.Forms.Timer tmrUpdate;
        private Krypton.Toolkit.KryptonLabel lblNoWork;
        private Krypton.Toolkit.KryptonLabel lblReading;
        private Krypton.Toolkit.KryptonLabel lblWriting;
        private Krypton.Toolkit.KryptonLabel lblWriteDone;
        private Krypton.Toolkit.KryptonLabel lblValidating;
        private Krypton.Toolkit.KryptonLabel lblFailed;
        private Krypton.Toolkit.KryptonLabel lblPassed;
    }
}
