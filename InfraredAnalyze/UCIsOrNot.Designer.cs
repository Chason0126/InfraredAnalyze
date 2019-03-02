namespace InfraredAnalyze
{
    partial class UCIsOrNot
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gpb = new System.Windows.Forms.GroupBox();
            this.rdbIs = new System.Windows.Forms.RadioButton();
            this.rdbNot = new System.Windows.Forms.RadioButton();
            this.gpb.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpb
            // 
            this.gpb.Controls.Add(this.rdbNot);
            this.gpb.Controls.Add(this.rdbIs);
            this.gpb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpb.Location = new System.Drawing.Point(0, 0);
            this.gpb.Name = "gpb";
            this.gpb.Size = new System.Drawing.Size(147, 75);
            this.gpb.TabIndex = 0;
            this.gpb.TabStop = false;
            this.gpb.Text = "groupBox1";
            // 
            // rdbIs
            // 
            this.rdbIs.AutoSize = true;
            this.rdbIs.Location = new System.Drawing.Point(17, 37);
            this.rdbIs.Name = "rdbIs";
            this.rdbIs.Size = new System.Drawing.Size(35, 16);
            this.rdbIs.TabIndex = 0;
            this.rdbIs.TabStop = true;
            this.rdbIs.Text = "是";
            this.rdbIs.UseVisualStyleBackColor = true;
            // 
            // rdbNot
            // 
            this.rdbNot.AutoSize = true;
            this.rdbNot.Location = new System.Drawing.Point(91, 37);
            this.rdbNot.Name = "rdbNot";
            this.rdbNot.Size = new System.Drawing.Size(35, 16);
            this.rdbNot.TabIndex = 0;
            this.rdbNot.TabStop = true;
            this.rdbNot.Text = "否";
            this.rdbNot.UseVisualStyleBackColor = true;
            // 
            // UCIsOrNot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpb);
            this.Name = "UCIsOrNot";
            this.Size = new System.Drawing.Size(147, 75);
            this.Load += new System.EventHandler(this.UCIsOrNot_Load);
            this.gpb.ResumeLayout(false);
            this.gpb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb;
        private System.Windows.Forms.RadioButton rdbNot;
        private System.Windows.Forms.RadioButton rdbIs;
    }
}
