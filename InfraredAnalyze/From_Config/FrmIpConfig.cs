using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmIpConfig : Form
    {
        public FrmIpConfig()
        {
            InitializeComponent();
            this.Disposed += FrmIpConfig_Disposed;
            DMSDK.DM_Init();
            DMSDK.DM_PlayerInit(ucPbx1.Handle);
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        Point point;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point temp_Point = MousePosition;
                temp_Point.Offset(-point.X, -point.Y);
                this.Location = temp_Point;
            }
        }

        int temp_OperateHandle;
        int temp_ConnectHandle;
        StringBuilder SubMask = new StringBuilder();
        StringBuilder GateWay = new StringBuilder();
        private void FrmIpConfig_Load(object sender, EventArgs e)
        {
            if (DMSDK.DM_CheckOnline("192.168.1.2", 5000) < 0)
            {
                MessageBox.Show("未检测到在线仪器，请检查后重试！");
                btnConfirm.Text = "重新连接";
            }
            else
            {
                temp_OperateHandle = DMSDK.DM_Connect(ucPbx1.Handle, "192.168.1.2", 80);
                if (temp_OperateHandle > 0)
                {
                    temp_ConnectHandle = DMSDK.DM_OpenMonitor(ucPbx1.Handle, "192.168.1.2", 5000, 0);
                }
                if(temp_OperateHandle > 0 && temp_ConnectHandle > 0)
                {
                    DMSDK.DM_GetNetmask(temp_OperateHandle, SubMask);
                    DMSDK.DM_GetGateway(temp_OperateHandle, GateWay);
                }
                else
                {
                    MessageBox.Show("连接失败，请重试！");
                    this.Close();
                }
                if (SubMask.Length == 0)
                {
                    MessageBox.Show("连接失败，请重试！");
                    this.Close();
                }
               
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (btnConfirm.Text == "重新连接")
            {
                if (DMSDK.DM_CheckOnline("192.168.1.2", 5000) < 0)
                {
                    MessageBox.Show("未检测到在线仪器，请检查后重试！");
                    btnConfirm.Text = "重新连接";
                }
                else
                {
                    temp_OperateHandle = DMSDK.DM_Connect(ucPbx1.Handle, "192.168.1.2", 80);
                    if (temp_OperateHandle > 0)
                    {
                        temp_ConnectHandle = DMSDK.DM_OpenMonitor(ucPbx1.Handle, "192.168.1.2", 5000, 0);
                    }
                    DMSDK.DM_GetNetmask(temp_OperateHandle, SubMask);
                    DMSDK.DM_GetGateway(temp_OperateHandle, GateWay);
                    if (SubMask.Length == 0)
                    {
                        MessageBox.Show("连接失败！");
                        return;
                    }
                    btnConfirm.Text = "确认修改";
                }
            }
            else if (btnConfirm.Text == "确认修改")
            {
                if(SubMask.ToString()==""|| GateWay.ToString() == "")
                {
                    MessageBox.Show("参数异常！请重试！");
                    return;
                }
                DMSDK.DM_SetIPAddr(temp_OperateHandle, ipAddressTextBox1.IPAdd.ToString(), SubMask.ToString(), GateWay.ToString());
                Thread.Sleep(200);
                MessageBox.Show("修改完成");
                this.Close();
            }
        }

        private void FrmIpConfig_Disposed(object sender, EventArgs e)
        {
            if (temp_OperateHandle > 0)
            {
                DMSDK.DM_Disconnect(temp_OperateHandle);
                temp_OperateHandle = -1;
                DMSDK.DM_CloseMonitor(temp_ConnectHandle);
                DMSDK.DM_PlayerCleanup();
            }
        }

        private void FrmIpConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
