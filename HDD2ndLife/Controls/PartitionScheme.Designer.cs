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
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkFormat = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkSingleVolume = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkGPTPartition = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.lbLog = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.SuspendLayout();
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(12, 379);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(142, 25);
            this.kryptonButton1.TabIndex = 0;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // chkFormat
            // 
            this.chkFormat.Location = new System.Drawing.Point(13, 43);
            this.chkFormat.Name = "chkFormat";
            this.chkFormat.Size = new System.Drawing.Size(192, 24);
            this.chkFormat.TabIndex = 2;
            this.chkFormat.Values.Text = "&Format and Assign letter";
            // 
            // chkSingleVolume
            // 
            this.chkSingleVolume.Location = new System.Drawing.Point(12, 73);
            this.chkSingleVolume.Name = "chkSingleVolume";
            this.chkSingleVolume.Size = new System.Drawing.Size(123, 24);
            this.chkSingleVolume.TabIndex = 3;
            this.chkSingleVolume.Values.Text = "&Single Volume";
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
            this.lbLog.Location = new System.Drawing.Point(12, 410);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(1004, 271);
            this.lbLog.StateCommon.Item.Content.LongText.MultiLine = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.lbLog.StateCommon.Item.Content.ShortText.MultiLine = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.lbLog.TabIndex = 5;
            // 
            // PartitionScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 693);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.chkGPTPartition);
            this.Controls.Add(this.chkSingleVolume);
            this.Controls.Add(this.chkFormat);
            this.Controls.Add(this.kryptonButton1);
            this.Name = "PartitionScheme";
            this.Text = "PartitionScheme";
            this.Load += new System.EventHandler(this.PartitionScheme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkFormat;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSingleVolume;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkGPTPartition;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lbLog;
    }
}