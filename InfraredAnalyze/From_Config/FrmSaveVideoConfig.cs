using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmSaveVideoConfig : Form
    {
        public FrmSaveVideoConfig(UCPbx uCPbx)
        {
            InitializeComponent();
            pnlMonitor.Controls.Add(uCPbx);
            uCPbx.Dock = DockStyle.Fill;
        }

        string path;
        private void btnChangePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            tbxSaveVideo.Text = path;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//保存配置文件
            config.AppSettings.Settings["VideoSavePath"].Value = path.ToString();
            ConfigurationManager.RefreshSection("VideoSavePath");
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void FrmSaveVideoConfig_Load(object sender, EventArgs e)
        {
            path = ConfigurationManager.AppSettings["ImageSavePath"];
            tbxSaveVideo.Text = path;
            if (StaticClass.tempMonitor < 0)
            {
                MessageBox.Show("连接失败，重新连接！");
                foreach (Control control in this.Controls)
                {
                    control.Enabled = false;
                }
                return;
            }
        }

        private void btnSaveVideo_Click(object sender, EventArgs e)
        {
            if (btnSaveVideo.Text == "保存视频")
            {
                DMSDK.DM_Record(StaticClass.tempMonitor,path);
                btnSaveVideo.Text = "停止保存";
            }
            else if(btnSaveVideo.Text == "停止保存")
            {
                DMSDK.DM_StopRecord(StaticClass.tempMonitor);
                btnSaveVideo.Text = "保存视频";
            }
        }

        private void FrmSaveVideoConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

    }
}
