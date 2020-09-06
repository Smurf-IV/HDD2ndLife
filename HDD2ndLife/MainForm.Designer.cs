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
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.changeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.treeGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.driveTree = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.driveImageList = new System.Windows.Forms.ImageList(this.components);
            this.driveContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.driveHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.kryptonBorderEdge2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.lblDetails = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.detailsHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.diskStatsView1 = new HDD2ndLife.Controls.DiskStatsView();
            this.blurPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
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
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem});
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(96, 28);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeComboBox});
            this.themeToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(152, 28);
            this.themeToolStripMenuItem.Text = "&Theme";
            // 
            // themeComboBox
            // 
            this.themeComboBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themeComboBox.Name = "themeComboBox";
            this.themeComboBox.Size = new System.Drawing.Size(221, 31);
            this.themeComboBox.SelectedIndexChanged += new System.EventHandler(this.themeComboBox_SelectedIndexChanged);
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
            this.settingsToolStripMenuItem,
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
            this.treeContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighInternalProfile;
            this.treeContainer.Size = new System.Drawing.Size(1312, 722);
            this.treeContainer.SplitterDistance = 330;
            this.treeContainer.TabIndex = 2;
            // 
            // treeGroup
            // 
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
            this.driveContainer.SeparatorStyle = ComponentFactory.Krypton.Toolkit.SeparatorStyle.HighProfile;
            this.driveContainer.Size = new System.Drawing.Size(977, 722);
            this.driveContainer.SplitterDistance = 417;
            this.driveContainer.TabIndex = 0;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(976, 37);
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
            this.driveHeader.Size = new System.Drawing.Size(977, 37);
            this.driveHeader.TabIndex = 0;
            this.driveHeader.Values.Description = "";
            this.driveHeader.Values.Heading = "Drive:";
            this.driveHeader.Values.Image = null;
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Right;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(976, 37);
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
            this.lblDetails.Size = new System.Drawing.Size(977, 263);
            this.lblDetails.TabIndex = 1;
            this.lblDetails.Text = "kryptonWrapLabel1";
            // 
            // detailsHeader
            // 
            this.detailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailsHeader.Location = new System.Drawing.Point(0, 0);
            this.detailsHeader.Margin = new System.Windows.Forms.Padding(4);
            this.detailsHeader.Name = "detailsHeader";
            this.detailsHeader.Size = new System.Drawing.Size(977, 37);
            this.detailsHeader.TabIndex = 0;
            this.detailsHeader.Values.Description = "";
            this.detailsHeader.Values.Heading = "Details:";
            this.detailsHeader.Values.Image = null;
            // 
            // diskStatsView1
            // 
            this.diskStatsView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskStatsView1.Location = new System.Drawing.Point(0, 37);
            this.diskStatsView1.Margin = new System.Windows.Forms.Padding(5);
            this.diskStatsView1.Name = "diskStatsView1";
            this.diskStatsView1.Size = new System.Drawing.Size(976, 380);
            this.diskStatsView1.TabIndex = 1;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 754);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private System.Windows.Forms.MenuStrip miniToolStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox themeComboBox;
        private System.Windows.Forms.ToolStripMenuItem changeLogToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer treeContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup treeGroup;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView driveTree;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer driveContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader driveHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox lblDetails;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader detailsHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private System.Windows.Forms.ImageList driveImageList;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private Controls.DiskStatsView diskStatsView1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel blurPanel;
    }
}

