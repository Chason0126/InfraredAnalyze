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
            this.rbdNot = new System.Windows.Forms.RadioButton();
            this.rdbIs = new System.Windows.Forms.RadioButton();
            this.gpb.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpb
            // 
            this.gpb.Controls.Add(this.rbdNot);
            this.gpb.Controls.Add(this.rdbIs);
            this.gpb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpb.Location = new System.Drawing.Point(0, 0);
            this.gpb.Name = "gpb";
            this.gpb.Size = new System.Drawing.Size(138, 83);
            this.gpb.TabIndex = 0;
            this.gpb.TabStop = false;
            this.gpb.Text = "groupBox1";
            // 
            // rbdNot
            // 
            this.rbdNot.AutoSize = true;
            this.rbdNot.Location = new System.Drawing.Point(82, 30);
            this.rbdNot.Name = "rbdNot";
            this.rbdNot.Size = new System.Drawing.Size(35, 16);
            this.rbdNot.TabIndex = 1;
            this.rbdNot.TabStop = true;
            this.rbdNot.Text = "否";
            this.rbdNot.UseVisualStyleBackColor = true;
            this.rbdNot.CheckedChanged += new System.EventHandler(this.rbdNot_CheckedChanged);
            // 
            // rdbIs
            // 
            this.rdbIs.AutoSize = true;
            this.rdbIs.Location = new System.Drawing.Point(15, 30);
            this.rdbIs.Name = "rdbIs";
            this.rdbIs.Size = new System.Drawing.Size(35, 16);
            this.rdbIs.TabIndex = 0;
            this.rdbIs.TabStop = true;
            this.rdbIs.Text = "是";
            this.rdbIs.UseVisualStyleBackColor = true;
            this.rdbIs.CheckedChanged += new System.EventHandler(this.rdbIs_CheckedChanged);
            // 
            // UCIsOrNot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpb);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "UCIsOrNot";
            this.Size = new System.Drawing.Size(138, 83);
            this.gpb.ResumeLayout(false);
            this.gpb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpb;
        private System.Windows.Forms.RadioButton rbdNot;
        private System.Windows.Forms.RadioButton rdbIs;
    }
}
