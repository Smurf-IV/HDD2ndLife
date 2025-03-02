namespace HDD2ndLife
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.changeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeContainer = new Krypton.Toolkit.KryptonSplitContainer();
            this.treeGroup = new Krypton.Toolkit.KryptonHeaderGroup();
            this.btnDriveMinRestore = new Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.driveTree = new Krypton.Toolkit.KryptonTreeView();
            this.driveImageList = new System.Windows.Forms.ImageList(this.components);
            this.driveContainer = new Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonBorderEdge1 = new Krypton.Toolkit.KryptonBorderEdge();
            this.driveHeader = new Krypton.Toolkit.KryptonHeader();
            this.kryptonBorderEdge2 = new Krypton.Toolkit.KryptonBorderEdge();
            this.lblDetails = new Krypton.Toolkit.KryptonRichTextBox();
            this.detailsHeader = new Krypton.Toolkit.KryptonHeader();
            this.btnDetailsMinRestore = new Krypton.Toolkit.ButtonSpecAny();
            this.blurPanel = new Krypton.Toolkit.KryptonPanel();
            this.kryptonThemeComboBox1 = new Krypton.Toolkit.KryptonThemeComboBox();
            this.diskStatsView1 = new HDD2ndLife.Controls.DiskStatsView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeContainer.Panel1)).BeginInit();
            this.treeContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeContainer.Panel2)).BeginInit();
            this.treeContainer.Panel2.SuspendLayout();
            this.treeContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeGroup.Panel)).BeginInit();
            this.treeGroup.Panel.SuspendLayout();
            this.treeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driveContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.driveContainer.Panel1)).BeginInit();
            this.driveContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driveContainer.Panel2)).BeginInit();
            this.driveContainer.Panel2.SuspendLayout();
            this.driveContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blurPanel)).BeginInit();
            this.blurPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "New item selection";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.miniToolStrip.Location = new System.Drawing.Point(77, 4);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(800, 27);
            this.miniToolStrip.TabIndex = 0;
            // 
            // changeLogToolStripMenuItem
            // 
            this.changeLogToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.changeLogToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeLogToolStripMenuItem.Name = "changeLogToolStripMenuItem";
            this.changeLogToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.changeLogToolStripMenuItem.Text = "Change_Log";
            this.changeLogToolStripMenuItem.Click += new System.EventHandler(this.changeLogToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.changeLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1312, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 28);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.ToolTipText = "Goto the Help page.";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // treeContainer
            // 
            this.treeContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeContainer.Location = new System.Drawing.Point(0, 0);
            this.treeContainer.Margin = new System.Windows.Forms.Padding(4);
            this.treeContainer.Name = "treeContainer";
            // 
            // treeContainer.Panel1
            // 
            this.treeContainer.Panel1.Controls.Add(this.treeGroup);
            // 
            // treeContainer.Panel2
            // 
            this.treeContainer.Panel2.Controls.Add(this.driveContainer);
            this.treeContainer.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.treeContainer.Size = new System.Drawing.Size(1312, 722);
            this.treeContainer.SplitterDistance = 330;
            this.treeContainer.SplitterWidth = 7;
            this.treeContainer.TabIndex = 2;
            // 
            // treeGroup
            // 
            this.treeGroup.ButtonSpecs.Add(this.btnDriveMinRestore);
            this.treeGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGroup.Location = new System.Drawing.Point(0, 0);
            this.treeGroup.Margin = new System.Windows.Forms.Padding(4);
            this.treeGroup.Name = "treeGroup";
            // 
            // treeGroup.Panel
            // 
            this.treeGroup.Panel.Controls.Add(this.driveTree);
            this.treeGroup.Size = new System.Drawing.Size(330, 722);
            this.treeGroup.TabIndex = 0;
            this.treeGroup.ValuesPrimary.Heading = "Drives";
            this.treeGroup.ValuesPrimary.Image = null;
            this.treeGroup.ValuesSecondary.Heading = "Select a drive";
            // 
            // btnDriveMinRestore
            // 
            this.btnDriveMinRestore.Checked = Krypton.Toolkit.ButtonCheckState.Checked;
            this.btnDriveMinRestore.Style = Krypton.Toolkit.PaletteButtonStyle.Form;
            this.btnDriveMinRestore.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            this.btnDriveMinRestore.UniqueName = "92732904fb404def96fcf8d733e878d2";
            this.btnDriveMinRestore.Click += new System.EventHandler(this.btnDriveMinRestore_Click);
            // 
            // driveTree
            // 
            this.driveTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.driveTree.FullRowSelect = true;
            this.driveTree.HideSelection = false;
            this.driveTree.ImageIndex = 0;
            this.driveTree.ImageList = this.driveImageList;
            this.driveTree.Location = new System.Drawing.Point(0, 0);
            this.driveTree.Margin = new System.Windows.Forms.Padding(4);
            this.driveTree.Name = "driveTree";
            this.driveTree.SelectedImageIndex = 0;
            this.driveTree.Size = new System.Drawing.Size(328, 659);
            this.driveTree.TabIndex = 0;
            this.driveTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.driveTree_AfterSelect);
            this.driveTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.driveTree_BeforeSelect);
            // 
            // driveImageList
            // 
            this.driveImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.driveImageList.ImageSize = new System.Drawing.Size(24, 24);
            this.driveImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // driveContainer
            // 
            this.driveContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.driveContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.driveContainer.Location = new System.Drawing.Point(0, 0);
            this.driveContainer.Margin = new System.Windows.Forms.Padding(4);
            this.driveContainer.Name = "driveContainer";
            this.driveContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // driveContainer.Panel1
            // 
            this.driveContainer.Panel1.Controls.Add(this.diskStatsView1);
            this.driveContainer.Panel1.Controls.Add(this.kryptonBorderEdge1);
            this.driveContainer.Panel1.Controls.Add(this.driveHeader);
            // 
            // driveContainer.Panel2
            // 
            this.driveContainer.Panel2.Controls.Add(this.kryptonBorderEdge2);
            this.driveContainer.Panel2.Controls.Add(this.lblDetails);
            this.driveContainer.Panel2.Controls.Add(this.detailsHeader);
            this.driveContainer.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.driveContainer.Size = new System.Drawing.Size(975, 722);
            this.driveContainer.SplitterDistance = 417;
            this.driveContainer.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(974, 37);
            this.kryptonBorderEdge1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1, 380);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // driveHeader
            // 
            this.driveHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.driveHeader.Location = new System.Drawing.Point(0, 0);
            this.driveHeader.Margin = new System.Windows.Forms.Padding(4);
            this.driveHeader.Name = "driveHeader";
            this.driveHeader.Size = new System.Drawing.Size(975, 37);
            this.driveHeader.TabIndex = 0;
            this.driveHeader.Values.Description = "";
            this.driveHeader.Values.Heading = "Drive:";
            this.driveHeader.Values.Image = null;
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(974, 37);
            this.kryptonBorderEdge2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(1, 263);
            this.kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // lblDetails
            // 
            this.lblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.lblDetails.Location = new System.Drawing.Point(0, 37);
            this.lblDetails.Margin = new System.Windows.Forms.Padding(4);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.ReadOnly = true;
            this.lblDetails.Size = new System.Drawing.Size(975, 263);
            this.lblDetails.TabIndex = 1;
            this.lblDetails.Text = "kryptonWrapLabel1";
            // 
            // detailsHeader
            // 
            this.detailsHeader.ButtonSpecs.Add(this.btnDetailsMinRestore);
            this.detailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailsHeader.Location = new System.Drawing.Point(0, 0);
            this.detailsHeader.Margin = new System.Windows.Forms.Padding(4);
            this.detailsHeader.Name = "detailsHeader";
            this.detailsHeader.Size = new System.Drawing.Size(975, 37);
            this.detailsHeader.TabIndex = 0;
            this.detailsHeader.Values.Description = "";
            this.detailsHeader.Values.Heading = "Details:";
            this.detailsHeader.Values.Image = null;
            // 
            // btnDetailsMinRestore
            // 
            this.btnDetailsMinRestore.Checked = Krypton.Toolkit.ButtonCheckState.Checked;
            this.btnDetailsMinRestore.Style = Krypton.Toolkit.PaletteButtonStyle.Form;
            this.btnDetailsMinRestore.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowDown;
            this.btnDetailsMinRestore.UniqueName = "fb4f9f0ded5f466aad83dc7b0a132dac";
            this.btnDetailsMinRestore.Click += new System.EventHandler(this.btnDetailsMinRestore_Click);
            // 
            // blurPanel
            // 
            this.blurPanel.Controls.Add(this.treeContainer);
            this.blurPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blurPanel.Location = new System.Drawing.Point(0, 32);
            this.blurPanel.Name = "blurPanel";
            this.blurPanel.Size = new System.Drawing.Size(1312, 722);
            this.blurPanel.TabIndex = 3;
            // 
            // kryptonThemeComboBox1
            // 
            this.kryptonThemeComboBox1.DefaultPalette = Krypton.Toolkit.PaletteMode.Microsoft365Blue;
            this.kryptonThemeComboBox1.DropDownWidth = 183;
            this.kryptonThemeComboBox1.IntegralHeight = false;
            this.kryptonThemeComboBox1.Location = new System.Drawing.Point(0, 0);
            this.kryptonThemeComboBox1.Name = "kryptonThemeComboBox1";
            this.kryptonThemeComboBox1.Size = new System.Drawing.Size(497, 31);
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonThemeComboBox1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.kryptonThemeComboBox1.TabIndex = 4;
            // 
            // diskStatsView1
            // 
            this.diskStatsView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskStatsView1.Location = new System.Drawing.Point(0, 37);
            this.diskStatsView1.Margin = new System.Windows.Forms.Padding(5);
            this.diskStatsView1.Name = "diskStatsView1";
            this.diskStatsView1.Size = new System.Drawing.Size(974, 380);
            this.diskStatsView1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1312, 754);
            this.Controls.Add(this.kryptonThemeComboBox1);
            this.Controls.Add(this.blurPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.miniToolStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "HDD 2nd Life";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeContainer.Panel1)).EndInit();
            this.treeContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeContainer.Panel2)).EndInit();
            this.treeContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeContainer)).EndInit();
            this.treeContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeGroup.Panel)).EndInit();
            this.treeGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeGroup)).EndInit();
            this.treeGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.driveContainer.Panel1)).EndInit();
            this.driveContainer.Panel1.ResumeLayout(false);
            this.driveContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driveContainer.Panel2)).EndInit();
            this.driveContainer.Panel2.ResumeLayout(false);
            this.driveContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.driveContainer)).EndInit();
            this.driveContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blurPanel)).EndInit();
            this.blurPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonThemeComboBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip miniToolStrip;
        private System.Windows.Forms.ToolStripMenuItem changeLogToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Krypton.Toolkit.KryptonSplitContainer treeContainer;
        private Krypton.Toolkit.KryptonHeaderGroup treeGroup;
        private Krypton.Toolkit.KryptonTreeView driveTree;
        private Krypton.Toolkit.KryptonSplitContainer driveContainer;
        private Krypton.Toolkit.KryptonHeader driveHeader;
        private Krypton.Toolkit.KryptonRichTextBox lblDetails;
        private Krypton.Toolkit.KryptonHeader detailsHeader;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private System.Windows.Forms.ImageList driveImageList;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private Controls.DiskStatsView diskStatsView1;
        private Krypton.Toolkit.KryptonPanel blurPanel;
        private Krypton.Toolkit.KryptonThemeComboBox kryptonThemeComboBox1;
        private Krypton.Toolkit.ButtonSpecHeaderGroup btnDriveMinRestore;
        private Krypton.Toolkit.ButtonSpecAny btnDetailsMinRestore;
    }
}

