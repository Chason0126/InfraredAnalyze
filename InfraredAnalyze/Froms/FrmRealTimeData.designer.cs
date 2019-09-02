namespace InfraredAnalyze
{
    partial class FrmRealTimeData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealTimeData));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSensor = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnMaxOrNormal = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tlpRealTimeData = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cbxSensor);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.pnlHeader);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tlpRealTimeData);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 800);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "探测器：";
            // 
            // cbxSensor
            // 
            this.cbxSensor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxSensor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSensor.FormattingEnabled = true;
            this.cbxSensor.Location = new System.Drawing.Point(432, 67);
            this.cbxSensor.Name = "cbxSensor";
            this.cbxSensor.Size = new System.Drawing.Size(121, 20);
            this.cbxSensor.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.Location = new System.Drawing.Point(644, 66);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "确认";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlHeader.Controls.Add(this.btnMaxOrNormal);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(998, 50);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            // 
            // btnMaxOrNormal
            // 
            this.btnMaxOrNormal.BackColor = System.Drawing.Color.Transparent;
            this.btnMaxOrNormal.BackgroundImage = global::InfraredAnalyze.Properties.Resources.最大化;
            this.btnMaxOrNormal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaxOrNormal.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMaxOrNormal.FlatAppearance.BorderSize = 0;
            this.btnMaxOrNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaxOrNormal.Location = new System.Drawing.Point(898, 0);
            this.btnMaxOrNormal.Name = "btnMaxOrNormal";
            this.btnMaxOrNormal.Size = new System.Drawing.Size(50, 50);
            this.btnMaxOrNormal.TabIndex = 7;
            this.btnMaxOrNormal.UseVisualStyleBackColor = false;
            this.btnMaxOrNormal.Click += new System.EventHandler(this.btnMaxOrNormal_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::InfraredAnalyze.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(948, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 50);
            this.btnClose.TabIndex = 6;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(421, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(129, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "实时温度曲线";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // tlpRealTimeData
            // 
            this.tlpRealTimeData.ColumnCount = 4;
            this.tlpRealTimeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRealTimeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRealTimeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRealTimeData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRealTimeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRealTimeData.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tlpRealTimeData.Location = new System.Drawing.Point(0, 0);
            this.tlpRealTimeData.Name = "tlpRealTimeData";
            this.tlpRealTimeData.RowCount = 4;
            this.tlpRealTimeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRealTimeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRealTimeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRealTimeData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRealTimeData.Size = new System.Drawing.Size(998, 697);
            this.tlpRealTimeData.TabIndex = 0;
            // 
            // FrmRealTimeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRealTimeData";
            this.Text = "实时温度";
            this.Load += new System.EventHandler(this.FrmRealTimeData_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpRealTimeData;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxSensor;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMaxOrNormal;
    }
}