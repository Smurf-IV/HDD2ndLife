namespace HDD2ndLife.Controls
{
    partial class PartitionScheme
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
            this.btnApply = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkFormat = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkSingleVolume = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkGPTPartition = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.lbLog = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.blurPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.blurPanel)).BeginInit();
            this.blurPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(12, 243);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(142, 25);
            this.btnApply.TabIndex = 0;
            this.btnApply.Values.Text = "&Apply";
            this.btnApply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // chkFormat
            // 
            this.chkFormat.Location = new System.Drawing.Point(12, 42);
            this.chkFormat.Name = "chkFormat";
            this.chkFormat.Size = new System.Drawing.Size(192, 24);
            this.chkFormat.TabIndex = 2;
            this.chkFormat.Values.Text = "&Format and Assign letter";
            // 
            // chkSingleVolume
            // 
            this.chkSingleVolume.Location = new System.Drawing.Point(12, 72);
            this.chkSingleVolume.Name = "chkSingleVolume";
            this.chkSingleVolume.Size = new System.Drawing.Size(269, 24);
            this.chkSingleVolume.TabIndex = 3;
            this.chkSingleVolume.Values.Text = "&Single Volume (via Dynamic Extend)";
            // 
            // chkGPTPartition
            // 
            this.chkGPTPartition.Checked = true;
            this.chkGPTPartition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGPTPartition.Enabled = false;
            this.chkGPTPartition.Location = new System.Drawing.Point(12, 12);
            this.chkGPTPartition.Name = "chkGPTPartition";
            this.chkGPTPartition.Size = new System.Drawing.Size(114, 24);
            this.chkGPTPartition.TabIndex = 4;
            this.chkGPTPartition.Values.Text = "&GPT Partition";
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.Location = new System.Drawing.Point(12, 274);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(1004, 407);
            this.lbLog.StateCommon.Item.Content.LongText.MultiLine = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.lbLog.StateCommon.Item.Content.ShortText.MultiLine = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.lbLog.TabIndex = 5;
            // 
            // blurPanel
            // 
            this.blurPanel.Controls.Add(this.btnApply);
            this.blurPanel.Controls.Add(this.chkFormat);
            this.blurPanel.Controls.Add(this.chkSingleVolume);
            this.blurPanel.Controls.Add(this.chkGPTPartition);
            this.blurPanel.Controls.Add(this.lbLog);
            this.blurPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blurPanel.Location = new System.Drawing.Point(0, 0);
            this.blurPanel.Name = "blurPanel";
            this.blurPanel.Size = new System.Drawing.Size(1028, 693);
            this.blurPanel.TabIndex = 6;
            // 
            // PartitionScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 693);
            this.Controls.Add(this.blurPanel);
            this.Name = "PartitionScheme";
            this.Text = "PartitionScheme";
            this.Load += new System.EventHandler(this.PartitionScheme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.blurPanel)).EndInit();
            this.blurPanel.ResumeLayout(false);
            this.blurPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnApply;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkFormat;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSingleVolume;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkGPTPartition;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lbLog;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel blurPanel;
    }
}