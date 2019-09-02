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
            DMSDK.DM_Init();
            this.Disposed += FrmMaintain_Disposed;
        }

       

        int tempConnect;
        private void btnReset_Click(object sender, EventArgs e)//重启仪器
        {
            DMSDK.DM_Reset(tempConnect);
            MessageBox.Show("操作成功");
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)//恢复出厂设置
        {
            DMSDK.DM_LoadDefault(tempConnect);
            MessageBox.Show("操作成功");
        }

        StringBuilder systemInfo = new StringBuilder();
        private void FrmMaintain_Load(object sender, EventArgs e)
        {
            tempConnect = DMSDK.DM_Connect(this.Handle, StaticClass.Temper_Ip, 80);//
            if (tempConnect < 0)
            {
                MessageBox.Show("连接失败，请重试！");
                return;
            }
            DMSDK.DM_GetSystemInfo(tempConnect, systemInfo);
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
            DMSDK.DM_Disconnect(tempConnect);
        }

        private void FrmMaintain_Disposed(object sender, EventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
        }
    }
}
