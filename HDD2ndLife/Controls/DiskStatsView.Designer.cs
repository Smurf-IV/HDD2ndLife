﻿namespace HDD2ndLife.Controls
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
            this.components = new System.ComponentModel.Container();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel7 = new Krypton.Toolkit.KryptonLabel();
            this.btnPartitioning = new Krypton.Toolkit.KryptonButton();
            this.btnStartStop = new Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            this.chkUseSpeed = new Krypton.Toolkit.KryptonCheckBox();
            this.chkFailFirst = new Krypton.Toolkit.KryptonCheckBox();
            this.pnlSpeed = new Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge4 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge3 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge2 = new Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.rb75 = new Krypton.Toolkit.KryptonRadioButton();
            this.rb30 = new Krypton.Toolkit.KryptonRadioButton();
            this.rb50 = new Krypton.Toolkit.KryptonRadioButton();
            this.rb20 = new Krypton.Toolkit.KryptonRadioButton();
            this.grpScanType = new Krypton.Toolkit.KryptonGroupBox();
            this.rb2Pass = new Krypton.Toolkit.KryptonRadioButton();
            this.rbVerify = new Krypton.Toolkit.KryptonRadioButton();
            this.rbWrite = new Krypton.Toolkit.KryptonRadioButton();
            this.rbRead = new Krypton.Toolkit.KryptonRadioButton();
            this.lblTimeRemaining = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            this.lblSpeed = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.lblDriveSize = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.diskSectors1 = new HDD2ndLife.Controls.DiskSectors();
            this.diskSectors2 = new HDD2ndLife.Controls.DiskSectors();
            this.lblPhase = new Elucidate.Shared.TextOverProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSpeed)).BeginInit();
            this.pnlSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpScanType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpScanType.Panel)).BeginInit();
            this.grpScanType.Panel.SuspendLayout();
            this.grpScanType.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.AutoScroll = true;
            this.kryptonPanel1.AutoScrollMinSize = new System.Drawing.Size(150, 550);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.diskSectors2);
            this.kryptonPanel1.Controls.Add(this.lblPhase);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.btnPartitioning);
            this.kryptonPanel1.Controls.Add(this.btnStartStop);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.grpScanType);
            this.kryptonPanel1.Controls.Add(this.lblTimeRemaining);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.lblSpeed);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.lblDriveSize);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel1.Location = new System.Drawing.Point(844, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(221, 791);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(9, 562);
            this.kryptonLabel7.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(55, 24);
            this.kryptonLabel7.TabIndex = 10;
            this.kryptonLabel7.TabStop = false;
            this.kryptonLabel7.Values.Text = "Phase:";
            // 
            // btnPartitioning
            // 
            this.btnPartitioning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPartitioning.Location = new System.Drawing.Point(9, 625);
            this.btnPartitioning.Margin = new System.Windows.Forms.Padding(4);
            this.btnPartitioning.Name = "btnPartitioning";
            this.btnPartitioning.Size = new System.Drawing.Size(204, 31);
            this.btnPartitioning.TabIndex = 9;
            this.btnPartitioning.Values.Text = "Par&tition Scheme";
            this.btnPartitioning.Click += new System.EventHandler(this.btnPartitioning_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.Location = new System.Drawing.Point(9, 335);
            this.btnStartStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(204, 31);
            this.btnStartStop.TabIndex = 8;
            this.btnStartStop.Values.Text = "&Start";
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox2.Location = new System.Drawing.Point(0, 172);
            this.kryptonGroupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.chkUseSpeed);
            this.kryptonGroupBox2.Panel.Controls.Add(this.chkFailFirst);
            this.kryptonGroupBox2.Panel.Controls.Add(this.pnlSpeed);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(221, 154);
            this.kryptonGroupBox2.TabIndex = 7;
            this.kryptonGroupBox2.Values.Heading = "Options";
            // 
            // chkUseSpeed
            // 
            this.chkUseSpeed.Enabled = false;
            this.chkUseSpeed.Location = new System.Drawing.Point(7, 37);
            this.chkUseSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.chkUseSpeed.Name = "chkUseSpeed";
            this.chkUseSpeed.Size = new System.Drawing.Size(97, 24);
            this.chkUseSpeed.TabIndex = 1;
            this.chkUseSpeed.ToolTipValues.Description = "Mark block warning if throughput drops off";
            this.chkUseSpeed.ToolTipValues.EnableToolTips = true;
            this.chkUseSpeed.ToolTipValues.Heading = "Use Speed";
            this.chkUseSpeed.ToolTipValues.Image = null;
            this.chkUseSpeed.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.chkUseSpeed.Values.Text = "&Use Speed";
            this.chkUseSpeed.CheckedChanged += new System.EventHandler(this.chkUseSpeed_CheckedChanged);
            // 
            // chkFailFirst
            // 
            this.chkFailFirst.Location = new System.Drawing.Point(7, 5);
            this.chkFailFirst.Margin = new System.Windows.Forms.Padding(4);
            this.chkFailFirst.Name = "chkFailFirst";
            this.chkFailFirst.Size = new System.Drawing.Size(80, 24);
            this.chkFailFirst.TabIndex = 0;
            this.chkFailFirst.ToolTipValues.Description = "Stop Processing on First failure.";
            this.chkFailFirst.ToolTipValues.EnableToolTips = true;
            this.chkFailFirst.ToolTipValues.Heading = "Fail First";
            this.chkFailFirst.ToolTipValues.Image = null;
            this.chkFailFirst.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.chkFailFirst.Values.Text = "&Fail First";
            // 
            // pnlSpeed
            // 
            this.pnlSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSpeed.Controls.Add(this.kryptonBorderEdge4);
            this.pnlSpeed.Controls.Add(this.kryptonBorderEdge3);
            this.pnlSpeed.Controls.Add(this.kryptonBorderEdge2);
            this.pnlSpeed.Controls.Add(this.kryptonBorderEdge1);
            this.pnlSpeed.Controls.Add(this.rb75);
            this.pnlSpeed.Controls.Add(this.rb30);
            this.pnlSpeed.Controls.Add(this.rb50);
            this.pnlSpeed.Controls.Add(this.rb20);
            this.pnlSpeed.Enabled = false;
            this.pnlSpeed.Location = new System.Drawing.Point(15, 50);
            this.pnlSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSpeed.Name = "pnlSpeed";
            this.pnlSpeed.Size = new System.Drawing.Size(198, 68);
            this.pnlSpeed.TabIndex = 2;
            // 
            // kryptonBorderEdge4
            // 
            this.kryptonBorderEdge4.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonBorderEdge4.Location = new System.Drawing.Point(0, 1);
            this.kryptonBorderEdge4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonBorderEdge4.Name = "kryptonBorderEdge4";
            this.kryptonBorderEdge4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge4.Size = new System.Drawing.Size(1, 66);
            this.kryptonBorderEdge4.Text = "kryptonBorderEdge4";
            // 
            // kryptonBorderEdge3
            // 
            this.kryptonBorderEdge3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge3.Location = new System.Drawing.Point(0, 67);
            this.kryptonBorderEdge3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonBorderEdge3.Name = "kryptonBorderEdge3";
            this.kryptonBorderEdge3.Size = new System.Drawing.Size(197, 1);
            this.kryptonBorderEdge3.Text = "kryptonBorderEdge3";
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(197, 1);
            this.kryptonBorderEdge2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(1, 67);
            this.kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(198, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // rb75
            // 
            this.rb75.Location = new System.Drawing.Point(81, 39);
            this.rb75.Margin = new System.Windows.Forms.Padding(4);
            this.rb75.Name = "rb75";
            this.rb75.Size = new System.Drawing.Size(53, 24);
            this.rb75.TabIndex = 3;
            this.rb75.Values.Text = "75%";
            // 
            // rb30
            // 
            this.rb30.Location = new System.Drawing.Point(81, 7);
            this.rb30.Margin = new System.Windows.Forms.Padding(4);
            this.rb30.Name = "rb30";
            this.rb30.Size = new System.Drawing.Size(53, 24);
            this.rb30.TabIndex = 2;
            this.rb30.Values.Text = "30%";
            // 
            // rb50
            // 
            this.rb50.Checked = true;
            this.rb50.Location = new System.Drawing.Point(5, 39);
            this.rb50.Margin = new System.Windows.Forms.Padding(4);
            this.rb50.Name = "rb50";
            this.rb50.Size = new System.Drawing.Size(53, 24);
            this.rb50.TabIndex = 1;
            this.rb50.Values.Text = "50%";
            // 
            // rb20
            // 
            this.rb20.Location = new System.Drawing.Point(5, 7);
            this.rb20.Margin = new System.Windows.Forms.Padding(4);
            this.rb20.Name = "rb20";
            this.rb20.Size = new System.Drawing.Size(53, 24);
            this.rb20.TabIndex = 0;
            this.rb20.Values.Text = "20%";
            // 
            // grpScanType
            // 
            this.grpScanType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpScanType.Location = new System.Drawing.Point(0, 0);
            this.grpScanType.Margin = new System.Windows.Forms.Padding(4);
            this.grpScanType.Name = "grpScanType";
            // 
            // grpScanType.Panel
            // 
            this.grpScanType.Panel.Controls.Add(this.rb2Pass);
            this.grpScanType.Panel.Controls.Add(this.rbVerify);
            this.grpScanType.Panel.Controls.Add(this.rbWrite);
            this.grpScanType.Panel.Controls.Add(this.rbRead);
            this.grpScanType.Size = new System.Drawing.Size(221, 165);
            this.grpScanType.TabIndex = 6;
            this.grpScanType.Values.Heading = "Scan Type:";
            // 
            // rb2Pass
            // 
            this.rb2Pass.Location = new System.Drawing.Point(7, 101);
            this.rb2Pass.Margin = new System.Windows.Forms.Padding(4);
            this.rb2Pass.Name = "rb2Pass";
            this.rb2Pass.Size = new System.Drawing.Size(109, 24);
            this.rb2Pass.TabIndex = 3;
            this.rb2Pass.ToolTipValues.Description = "Using a differnet pattern on each pass,\r\nPerform a linear Write, then,\r\na Read ve" +
    "rification on the pattern.\r\n!! ALL DATA WILL BE DESTROYED !!";
            this.rb2Pass.ToolTipValues.EnableToolTips = true;
            this.rb2Pass.ToolTipValues.Heading = "2 Pass Verify";
            this.rb2Pass.ToolTipValues.Image = null;
            this.rb2Pass.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.rb2Pass.Values.Text = "2 &Pass Verify";
            // 
            // rbVerify
            // 
            this.rbVerify.Location = new System.Drawing.Point(7, 69);
            this.rbVerify.Margin = new System.Windows.Forms.Padding(4);
            this.rbVerify.Name = "rbVerify";
            this.rbVerify.Size = new System.Drawing.Size(110, 24);
            this.rbVerify.TabIndex = 2;
            this.rbVerify.ToolTipValues.Description = "Perform a linear Write, then,\r\na Read verification on the pattern.\r\n!! ALL DATA W" +
    "ILL BE DESTROYED !!";
            this.rbVerify.ToolTipValues.EnableToolTips = true;
            this.rbVerify.ToolTipValues.Heading = "Verify (W+R)";
            this.rbVerify.ToolTipValues.Image = null;
            this.rbVerify.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.rbVerify.Values.Text = "&Verify (W+R)";
            // 
            // rbWrite
            // 
            this.rbWrite.Location = new System.Drawing.Point(7, 37);
            this.rbWrite.Margin = new System.Windows.Forms.Padding(4);
            this.rbWrite.Name = "rbWrite";
            this.rbWrite.Size = new System.Drawing.Size(123, 24);
            this.rbWrite.TabIndex = 1;
            this.rbWrite.ToolTipValues.Description = "Perform a Write, then,\r\na linear Read.\r\n!! ALL DATA WILL BE DESTROYED !!";
            this.rbWrite.ToolTipValues.EnableToolTips = true;
            this.rbWrite.ToolTipValues.Heading = "Write (+ Read)";
            this.rbWrite.ToolTipValues.Image = null;
            this.rbWrite.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.rbWrite.Values.Text = "&Write (+ Read)";
            // 
            // rbRead
            // 
            this.rbRead.Checked = true;
            this.rbRead.Location = new System.Drawing.Point(7, 5);
            this.rbRead.Margin = new System.Windows.Forms.Padding(4);
            this.rbRead.Name = "rbRead";
            this.rbRead.Size = new System.Drawing.Size(94, 24);
            this.rbRead.TabIndex = 0;
            this.rbRead.ToolTipValues.Description = "Performs a linear Read of existing sectors.\r\nNone destructive.";
            this.rbRead.ToolTipValues.EnableToolTips = true;
            this.rbRead.ToolTipValues.Heading = "Read Only";
            this.rbRead.ToolTipValues.Image = null;
            this.rbRead.ToolTipValues.ToolTipStyle = Krypton.Toolkit.LabelStyle.ToolTip;
            this.rbRead.Values.Text = "&Read Only";
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeRemaining.AutoSize = false;
            this.lblTimeRemaining.Location = new System.Drawing.Point(55, 532);
            this.lblTimeRemaining.Margin = new System.Windows.Forms.Padding(4);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(163, 23);
            this.lblTimeRemaining.TabIndex = 5;
            this.lblTimeRemaining.TabStop = false;
            this.lblTimeRemaining.Values.Text = "1234 hrs";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(7, 500);
            this.kryptonLabel6.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(140, 24);
            this.kryptonLabel6.TabIndex = 4;
            this.kryptonLabel6.TabStop = false;
            this.kryptonLabel6.Values.Text = "Estimate Time Left:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeed.AutoSize = false;
            this.lblSpeed.Location = new System.Drawing.Point(55, 469);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(163, 23);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.TabStop = false;
            this.lblSpeed.Values.Text = "1234 GoogleB/s";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 437);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(57, 24);
            this.kryptonLabel4.TabIndex = 2;
            this.kryptonLabel4.TabStop = false;
            this.kryptonLabel4.Values.Text = "Speed:";
            // 
            // lblDriveSize
            // 
            this.lblDriveSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDriveSize.AutoSize = false;
            this.lblDriveSize.Location = new System.Drawing.Point(52, 405);
            this.lblDriveSize.Margin = new System.Windows.Forms.Padding(4);
            this.lblDriveSize.Name = "lblDriveSize";
            this.lblDriveSize.Size = new System.Drawing.Size(163, 25);
            this.lblDriveSize.TabIndex = 1;
            this.lblDriveSize.TabStop = false;
            this.lblDriveSize.Values.Text = "123456789GoogleB";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 373);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(42, 24);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.TabStop = false;
            this.kryptonLabel1.Values.Text = "Size:";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 250;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(23, 660);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(96, 107);
            this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Consolas", 7.5F);
            this.kryptonLabel2.StateCommon.ShortText.MultiLine = Krypton.Toolkit.InheritBool.True;
            this.kryptonLabel2.TabIndex = 12;
            this.kryptonLabel2.TabStop = false;
            this.kryptonLabel2.Values.Text = "- NoWork\r\n- Reading\r\n- Writing\r\n- WriteDone\r\n- Validating\r\n- Failed\r\n- Passed";
            // 
            // diskSectors1
            // 
            this.diskSectors1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskSectors1.Location = new System.Drawing.Point(0, 0);
            this.diskSectors1.Margin = new System.Windows.Forms.Padding(4);
            this.diskSectors1.Name = "diskSectors1";
            this.diskSectors1.Size = new System.Drawing.Size(844, 791);
            this.diskSectors1.TabIndex = 0;
            // 
            // diskSectors2
            // 
            this.diskSectors2.Location = new System.Drawing.Point(7, 664);
            this.diskSectors2.Margin = new System.Windows.Forms.Padding(4);
            this.diskSectors2.Name = "diskSectors2";
            this.diskSectors2.Size = new System.Drawing.Size(13, 112);
            this.diskSectors2.TabIndex = 1;
            // 
            // lblPhase
            // 
            this.lblPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhase.BackColor = System.Drawing.Color.Transparent;
            this.lblPhase.ContainerControl = this;
            this.lblPhase.DisplayText = "Test";
            this.lblPhase.Location = new System.Drawing.Point(17, 590);
            this.lblPhase.Margin = new System.Windows.Forms.Padding(4);
            this.lblPhase.Name = "lblPhase";
            this.lblPhase.ShowInTaskbar = true;
            this.lblPhase.Size = new System.Drawing.Size(201, 26);
            this.lblPhase.Step = 1;
            this.lblPhase.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.lblPhase.TabIndex = 11;
            this.lblPhase.TabStop = false;
            this.lblPhase.Text = "Test";
            this.lblPhase.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblPhase.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // DiskStatsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.diskSectors1);
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DiskStatsView";
            this.Size = new System.Drawing.Size(1065, 791);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            this.kryptonGroupBox2.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSpeed)).EndInit();
            this.pnlSpeed.ResumeLayout(false);
            this.pnlSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpScanType.Panel)).EndInit();
            this.grpScanType.Panel.ResumeLayout(false);
            this.grpScanType.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpScanType)).EndInit();
            this.grpScanType.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private Krypton.Toolkit.KryptonRadioButton rbVerify;
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
        private DiskSectors diskSectors2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
    }
}
