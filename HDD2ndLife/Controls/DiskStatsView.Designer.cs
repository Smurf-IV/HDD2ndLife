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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnStartStop = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.chkUseSpeed = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkFailFirst = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.pnlSpeed = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonBorderEdge4 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge3 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.rb40 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rb20 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rb30 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rb10 = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.rb2Pass = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbVerify = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbWrite = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbRead = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblDriveSize = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnPartitioning = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblPhase = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.diskSectors1 = new HDD2ndLife.Controls.DiskSectors();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSpeed)).BeginInit();
            this.pnlSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diskSectors1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.AutoScroll = true;
            this.kryptonPanel1.AutoScrollMinSize = new System.Drawing.Size(150, 550);
            this.kryptonPanel1.Controls.Add(this.lblPhase);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.btnPartitioning);
            this.kryptonPanel1.Controls.Add(this.btnStartStop);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.lblDriveSize);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonPanel1.Location = new System.Drawing.Point(649, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(150, 574);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.Location = new System.Drawing.Point(7, 272);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(137, 25);
            this.btnStartStop.TabIndex = 8;
            this.btnStartStop.Values.Text = "&Start";
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox2.Location = new System.Drawing.Point(0, 140);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.chkUseSpeed);
            this.kryptonGroupBox2.Panel.Controls.Add(this.chkFailFirst);
            this.kryptonGroupBox2.Panel.Controls.Add(this.pnlSpeed);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(150, 125);
            this.kryptonGroupBox2.TabIndex = 7;
            this.kryptonGroupBox2.Values.Heading = "Options";
            // 
            // chkUseSpeed
            // 
            this.chkUseSpeed.Enabled = false;
            this.chkUseSpeed.Location = new System.Drawing.Point(5, 30);
            this.chkUseSpeed.Name = "chkUseSpeed";
            this.chkUseSpeed.Size = new System.Drawing.Size(81, 20);
            this.chkUseSpeed.TabIndex = 1;
            this.chkUseSpeed.ToolTipValues.Description = "Mark block warning if throughput drops off";
            this.chkUseSpeed.ToolTipValues.EnableToolTips = true;
            this.chkUseSpeed.ToolTipValues.Heading = "Use Speed";
            this.chkUseSpeed.ToolTipValues.Image = null;
            this.chkUseSpeed.ToolTipValues.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.chkUseSpeed.Values.Text = "&Use Speed";
            this.chkUseSpeed.CheckedChanged += new System.EventHandler(this.chkUseSpeed_CheckedChanged);
            // 
            // chkFailFirst
            // 
            this.chkFailFirst.Enabled = false;
            this.chkFailFirst.Location = new System.Drawing.Point(5, 4);
            this.chkFailFirst.Name = "chkFailFirst";
            this.chkFailFirst.Size = new System.Drawing.Size(68, 20);
            this.chkFailFirst.TabIndex = 0;
            this.chkFailFirst.ToolTipValues.Description = "Mark block bad on First failure";
            this.chkFailFirst.ToolTipValues.EnableToolTips = true;
            this.chkFailFirst.ToolTipValues.Heading = "Fail First";
            this.chkFailFirst.ToolTipValues.Image = null;
            this.chkFailFirst.ToolTipValues.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
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
            this.pnlSpeed.Controls.Add(this.rb40);
            this.pnlSpeed.Controls.Add(this.rb20);
            this.pnlSpeed.Controls.Add(this.rb30);
            this.pnlSpeed.Controls.Add(this.rb10);
            this.pnlSpeed.Enabled = false;
            this.pnlSpeed.Location = new System.Drawing.Point(11, 41);
            this.pnlSpeed.Name = "pnlSpeed";
            this.pnlSpeed.Size = new System.Drawing.Size(132, 55);
            this.pnlSpeed.TabIndex = 2;
            // 
            // kryptonBorderEdge4
            // 
            this.kryptonBorderEdge4.Dock = System.Windows.Forms.DockStyle.Left;
            this.kryptonBorderEdge4.Location = new System.Drawing.Point(0, 1);
            this.kryptonBorderEdge4.Name = "kryptonBorderEdge4";
            this.kryptonBorderEdge4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge4.Size = new System.Drawing.Size(1, 53);
            this.kryptonBorderEdge4.Text = "kryptonBorderEdge4";
            // 
            // kryptonBorderEdge3
            // 
            this.kryptonBorderEdge3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonBorderEdge3.Location = new System.Drawing.Point(0, 54);
            this.kryptonBorderEdge3.Name = "kryptonBorderEdge3";
            this.kryptonBorderEdge3.Size = new System.Drawing.Size(131, 1);
            this.kryptonBorderEdge3.Text = "kryptonBorderEdge3";
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(131, 1);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(1, 54);
            this.kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(132, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // rb40
            // 
            this.rb40.Checked = true;
            this.rb40.Location = new System.Drawing.Point(61, 32);
            this.rb40.Name = "rb40";
            this.rb40.Size = new System.Drawing.Size(46, 20);
            this.rb40.TabIndex = 3;
            this.rb40.Values.Text = "40%";
            // 
            // rb20
            // 
            this.rb20.Location = new System.Drawing.Point(61, 6);
            this.rb20.Name = "rb20";
            this.rb20.Size = new System.Drawing.Size(46, 20);
            this.rb20.TabIndex = 2;
            this.rb20.Values.Text = "20%";
            // 
            // rb30
            // 
            this.rb30.Location = new System.Drawing.Point(4, 32);
            this.rb30.Name = "rb30";
            this.rb30.Size = new System.Drawing.Size(46, 20);
            this.rb30.TabIndex = 1;
            this.rb30.Values.Text = "30%";
            // 
            // rb10
            // 
            this.rb10.Location = new System.Drawing.Point(4, 6);
            this.rb10.Name = "rb10";
            this.rb10.Size = new System.Drawing.Size(46, 20);
            this.rb10.TabIndex = 0;
            this.rb10.Values.Text = "10%";
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.rb2Pass);
            this.kryptonGroupBox1.Panel.Controls.Add(this.rbVerify);
            this.kryptonGroupBox1.Panel.Controls.Add(this.rbWrite);
            this.kryptonGroupBox1.Panel.Controls.Add(this.rbRead);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(150, 134);
            this.kryptonGroupBox1.TabIndex = 6;
            this.kryptonGroupBox1.Values.Heading = "Scan Type:";
            // 
            // rb2Pass
            // 
            this.rb2Pass.Enabled = false;
            this.rb2Pass.Location = new System.Drawing.Point(5, 82);
            this.rb2Pass.Name = "rb2Pass";
            this.rb2Pass.Size = new System.Drawing.Size(91, 20);
            this.rb2Pass.TabIndex = 3;
            this.rb2Pass.ToolTipValues.Description = "Using a differnet pattern on each pass,\r\nPerform a linear Write, then,\r\na Read ve" +
    "rification on the pattern.";
            this.rb2Pass.ToolTipValues.EnableToolTips = true;
            this.rb2Pass.ToolTipValues.Heading = "2 Pass Verify";
            this.rb2Pass.ToolTipValues.Image = null;
            this.rb2Pass.ToolTipValues.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.rb2Pass.Values.Text = "2 &Pass Verify";
            // 
            // rbVerify
            // 
            this.rbVerify.Enabled = false;
            this.rbVerify.Location = new System.Drawing.Point(5, 56);
            this.rbVerify.Name = "rbVerify";
            this.rbVerify.Size = new System.Drawing.Size(92, 20);
            this.rbVerify.TabIndex = 2;
            this.rbVerify.ToolTipValues.Description = "Perform a linear Write, then,\r\na Read verification on the pattern.";
            this.rbVerify.ToolTipValues.EnableToolTips = true;
            this.rbVerify.ToolTipValues.Heading = "Verify (W+R)";
            this.rbVerify.ToolTipValues.Image = null;
            this.rbVerify.ToolTipValues.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.rbVerify.Values.Text = "&Verify (W+R)";
            // 
            // rbWrite
            // 
            this.rbWrite.Enabled = false;
            this.rbWrite.Location = new System.Drawing.Point(5, 30);
            this.rbWrite.Name = "rbWrite";
            this.rbWrite.Size = new System.Drawing.Size(102, 20);
            this.rbWrite.TabIndex = 1;
            this.rbWrite.ToolTipValues.Description = "Perform a Write, then,\r\na linear Read.";
            this.rbWrite.ToolTipValues.EnableToolTips = true;
            this.rbWrite.ToolTipValues.Heading = "Write (+ Read)";
            this.rbWrite.ToolTipValues.Image = null;
            this.rbWrite.ToolTipValues.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.rbWrite.Values.Text = "&Write (+ Read)";
            // 
            // rbRead
            // 
            this.rbRead.Checked = true;
            this.rbRead.Location = new System.Drawing.Point(5, 4);
            this.rbRead.Name = "rbRead";
            this.rbRead.Size = new System.Drawing.Size(79, 20);
            this.rbRead.TabIndex = 0;
            this.rbRead.ToolTipValues.Description = "Performs a linear Read of existing sectors.\r\nNone destructive.";
            this.rbRead.ToolTipValues.EnableToolTips = true;
            this.rbRead.ToolTipValues.Heading = "Read Only";
            this.rbRead.ToolTipValues.Image = null;
            this.rbRead.ToolTipValues.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.rbRead.Values.Text = "&Read Only";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel5.AutoSize = false;
            this.kryptonLabel5.Location = new System.Drawing.Point(25, 432);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(122, 19);
            this.kryptonLabel5.TabIndex = 5;
            this.kryptonLabel5.TabStop = false;
            this.kryptonLabel5.Values.Text = "1234 hrs";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(5, 406);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(113, 20);
            this.kryptonLabel6.TabIndex = 4;
            this.kryptonLabel6.TabStop = false;
            this.kryptonLabel6.Values.Text = "Estimate Time Left:";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabel3.AutoSize = false;
            this.kryptonLabel3.Location = new System.Drawing.Point(25, 381);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(122, 19);
            this.kryptonLabel3.TabIndex = 3;
            this.kryptonLabel3.TabStop = false;
            this.kryptonLabel3.Values.Text = "1234 GoogleB/s";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(5, 355);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel4.TabIndex = 2;
            this.kryptonLabel4.TabStop = false;
            this.kryptonLabel4.Values.Text = "Speed:";
            // 
            // lblDriveSize
            // 
            this.lblDriveSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDriveSize.AutoSize = false;
            this.lblDriveSize.Location = new System.Drawing.Point(23, 329);
            this.lblDriveSize.Name = "lblDriveSize";
            this.lblDriveSize.Size = new System.Drawing.Size(122, 20);
            this.lblDriveSize.TabIndex = 1;
            this.lblDriveSize.TabStop = false;
            this.lblDriveSize.Values.Text = "123456789GoogleB";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(5, 303);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(35, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.TabStop = false;
            this.kryptonLabel1.Values.Text = "Size:";
            // 
            // btnPartitioning
            // 
            this.btnPartitioning.Enabled = false;
            this.btnPartitioning.Location = new System.Drawing.Point(7, 508);
            this.btnPartitioning.Name = "btnPartitioning";
            this.btnPartitioning.Size = new System.Drawing.Size(137, 25);
            this.btnPartitioning.TabIndex = 9;
            this.btnPartitioning.Values.Text = "&Partition Scheme";
            // 
            // lblPhase
            // 
            this.lblPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhase.AutoSize = false;
            this.lblPhase.Location = new System.Drawing.Point(25, 483);
            this.lblPhase.Name = "lblPhase";
            this.lblPhase.Size = new System.Drawing.Size(122, 19);
            this.lblPhase.TabIndex = 11;
            this.lblPhase.TabStop = false;
            this.lblPhase.Values.Text = "Stopped";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(7, 457);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(45, 20);
            this.kryptonLabel7.TabIndex = 10;
            this.kryptonLabel7.TabStop = false;
            this.kryptonLabel7.Values.Text = "Phase:";
            // 
            // diskSectors1
            // 
            this.diskSectors1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskSectors1.Location = new System.Drawing.Point(0, 0);
            this.diskSectors1.Name = "diskSectors1";
            this.diskSectors1.NumberOfDriveBlocks = ((ulong)(0ul));
            this.diskSectors1.Size = new System.Drawing.Size(649, 574);
            this.diskSectors1.TabIndex = 0;
            // 
            // DiskStatsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.diskSectors1);
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "DiskStatsView";
            this.Size = new System.Drawing.Size(799, 574);
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
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diskSectors1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDriveSize;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private DiskSectors diskSectors1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rb2Pass;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbVerify;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbWrite;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbRead;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkUseSpeed;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkFailFirst;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel pnlSpeed;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rb40;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rb20;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rb30;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rb10;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge4;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge3;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnStartStop;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPartitioning;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPhase;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
    }
}
