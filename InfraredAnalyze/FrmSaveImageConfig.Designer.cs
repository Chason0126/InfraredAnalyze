namespace InfraredAnalyze
{
    partial class FrmSaveImageConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSaveImage = new System.Windows.Forms.TextBox();
            this.btnSnapShot = new System.Windows.Forms.Button();
            this.btnChangePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "保存路径：";
            // 
            // tbxSaveImage
            // 
            this.tbxSaveImage.Location = new System.Drawing.Point(254, 76);
            this.tbxSaveImage.Name = "tbxSaveImage";
            this.tbxSaveImage.Size = new System.Drawing.Size(208, 21);
            this.tbxSaveImage.TabIndex = 1;
            // 
            // btnSnapShot
            // 
            this.btnSnapShot.Location = new System.Drawing.Point(294, 177);
            this.btnSnapShot.Name = "btnSnapShot";
            this.btnSnapShot.Size = new System.Drawing.Size(75, 23);
            this.btnSnapShot.TabIndex = 2;
            this.btnSnapShot.Text = "拍摄图片";
            this.btnSnapShot.UseVisualStyleBackColor = true;
            this.btnSnapShot.Click += new System.EventHandler(this.btnSnapShot_Click);
            // 
            // btnChangePath
            // 
            this.btnChangePath.Location = new System.Drawing.Point(505, 73);
            this.btnChangePath.Name = "btnChangePath";
            this.btnChangePath.Size = new System.Drawing.Size(75, 23);
            this.btnChangePath.TabIndex = 3;
            this.btnChangePath.Text = "更改路径";
            this.btnChangePath.UseVisualStyleBackColor = true;
            this.btnChangePath.Click += new System.EventHandler(this.btnChangePath_Click);
            // 
            // FrmSaveImageConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 284);
            this.Controls.Add(this.btnChangePath);
            this.Controls.Add(this.btnSnapShot);
            this.Controls.Add(this.tbxSaveImage);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSaveImageConfig";
            this.Text = "FrmImageSaveConfig";
            this.Load += new System.EventHandler(this.FrmSaveImageConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSaveImage;
        private System.Windows.Forms.Button btnSnapShot;
        private System.Windows.Forms.Button btnChangePath;
    }
}