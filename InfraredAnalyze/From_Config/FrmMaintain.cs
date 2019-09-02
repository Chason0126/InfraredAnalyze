using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmMaintain : Form
    {
        public FrmMaintain()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)//重启仪器
        {
            DMSDK.DM_Reset(StaticClass.tempConnect);
            MessageBox.Show("操作成功");
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)//恢复出厂设置
        {
            DMSDK.DM_LoadDefault(StaticClass.tempConnect);
            MessageBox.Show("操作成功");
        }

        StringBuilder systemInfo = new StringBuilder();
        private void FrmMaintain_Load(object sender, EventArgs e)
        {
            if (StaticClass.tempConnect < 0)
            {
                MessageBox.Show("连接失败，请重新连接！");
                foreach (Control control in this.Controls)
                {
                    control.Enabled = false;
                }
                return;
            }
            DMSDK.DM_GetSystemInfo(StaticClass.tempConnect, systemInfo);
            lblSystemInfo.Text = systemInfo.ToString();
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            btnReset.BackColor = Color.Red;
        }

        private void btnReset_MouseLeave(object sender, EventArgs e)
        {
            btnReset.BackColor = Color.Transparent;
        }

        private void btnLoadDefault_MouseLeave(object sender, EventArgs e)
        {
            btnLoadDefault.BackColor = Color.Transparent;
        }

        private void btnLoadDefault_MouseEnter(object sender, EventArgs e)
        {
            btnLoadDefault.BackColor = Color.Red;
        }

        private void FrmMaintain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
