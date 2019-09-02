using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public delegate void childclose();
    public partial class FrmCameraNetConfig : Form
    {
        public FrmCameraNetConfig()
        {
            InitializeComponent();
            this.Disposed += FrmCameraNetConfig_Disposed;
            DMSDK.DM_Init();
        }

       

        SqlCreate sqlCreate = new SqlCreate();

        StringBuilder Mac;
        StringBuilder SubMask;
        StringBuilder GateWay;
        public event childclose closefather;
        private int tempConnect;//临时连接句柄

        private void FrmCameraConfig_Load(object sender, EventArgs e)
        {
            Mac = new StringBuilder();
            SubMask = new StringBuilder();
            GateWay = new StringBuilder();
            if (StaticClass.Temper_Ip != "")
            {
                string[] str = StaticClass.Temper_Ip.Split('.');
                IPAddressIP.tbx1.Text = str[0];
                IPAddressIP.tbx2.Text = str[1];
                IPAddressIP.tbx3.Text = str[2];
                IPAddressIP.tbx4.Text = str[3];
                if (DMSDK.DM_CheckOnline(StaticClass.Temper_Ip, 80) < 0)// 离线检测  为离线
                {
                    btnConfirm.Tag = "Reconnect";
                    Update_IpAddrGateWay("0.0.0.0");
                    Update_IpAddrSubMask("0.0.0.0");
                    chbModifyGateWay.Enabled = false;
                    chbModifyMac.Enabled = false;
                    chbModifyNetMask.Enabled = false;
                    btnUpdateIsenable.Visible = true;
                    cbxIsEnable.SelectedIndex = Convert.ToInt32(StaticClass.Temper_IsEnanle);
                    StaticClass.Temper_IsEnanle = Convert.ToBoolean(cbxIsEnable.SelectedIndex);
                }
                else if (DMSDK.DM_CheckOnline(StaticClass.Temper_Ip, 80) > 0)//连接成功
                {
                    tempConnect = DMSDK.DM_Connect(this.Handle, StaticClass.Temper_Ip, 80);//
                    if (tempConnect <= 0)
                    {
                        MessageBox.Show("连接失败！请检查设备与参数后重试！");
                        return;
                    }
                    DMSDK.DM_GetMAC(tempConnect, Mac);
                    if (Mac.Length==0)
                    {
                        btnConfirm.Tag = "Reconnect";
                        Update_IpAddrGateWay("0.0.0.0");
                        Update_IpAddrSubMask("0.0.0.0");
                        chbModifyGateWay.Enabled = false;
                        chbModifyMac.Enabled = false;
                        chbModifyNetMask.Enabled = false;
                        btnUpdateIsenable.Visible = true;
                        cbxIsEnable.SelectedIndex = Convert.ToInt32(StaticClass.Temper_IsEnanle);
                        StaticClass.Temper_IsEnanle = Convert.ToBoolean(cbxIsEnable.SelectedIndex);
                        MessageBox.Show("连接失败！请检查设备与参数后重试！");
                    }
                    else
                    {
                        DMSDK.DM_GetNetmask(tempConnect, SubMask);
                        DMSDK.DM_GetGateway(tempConnect, GateWay);
                        Update_IpAddrGateWay(GateWay.ToString());
                        Update_IpAddrSubMask(SubMask.ToString());
                        tbxMAC.Text = Mac.ToString();
                        cbxIsEnable.SelectedIndex = Convert.ToInt32(StaticClass.Temper_IsEnanle);
                        StaticClass.Temper_IsEnanle = Convert.ToBoolean(cbxIsEnable.SelectedIndex);
                        btnConfirm.Tag = "Confirm";
                        chbModifyGateWay.Enabled = true;
                        chbModifyMac.Enabled = true;
                        chbModifyNetMask.Enabled = true;
                        btnUpdateIsenable.Visible = false;
                    }
                }
            }
            if (btnConfirm.Tag.ToString() == "Confirm")
            {
                btnConfirm.Text = "确认修改";
            }
            else if (btnConfirm.Tag.ToString() == "Reconnect")
            {
                btnConfirm.Text = "重新连接";
            }
        }

        private void Update_IpAddrGateWay(string gateway)
        {
            if (gateway != "")
            {
                string[] GWay = gateway.Split('.');
                IPAddressGateWay.tbx1.Text = GWay[0];
                IPAddressGateWay.tbx2.Text = GWay[1];
                IPAddressGateWay.tbx3.Text = GWay[2];
                IPAddressGateWay.tbx4.Text = GWay[3];
            }
        }

        private void Update_IpAddrSubMask(string submask)
        {
            if (submask != "")
            {
                string[] SbMask = submask.Split('.');
                IPAddressSubMask.tbx1.Text = SbMask[0];
                IPAddressSubMask.tbx2.Text = SbMask[1];
                IPAddressSubMask.tbx3.Text = SbMask[2];
                IPAddressSubMask.tbx4.Text = SbMask[3];
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbModifyMac_CheckedChanged(object sender, EventArgs e)
        {
            if(chbModifyMac.Checked==true)
            {
                tbxMAC.Enabled = true;
            }
            else
            {
                tbxMAC.Enabled = false;
            }
        }

        private void chbModifyNetMask_CheckedChanged(object sender, EventArgs e)
        {
            if (chbModifyNetMask.Checked == true)
            {
                IPAddressSubMask.Enabled = true;
            }
            else
            {
                IPAddressSubMask.Enabled = false;
            }
        }

        private void chbModifyGateWay_CheckedChanged(object sender, EventArgs e)
        {
            if (chbModifyGateWay.Checked == true)
            {
                IPAddressGateWay.Enabled = true;
            }
            else
            {
                IPAddressGateWay.Enabled = false;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(btnConfirm.Tag.ToString()=="Confirm")
            {
                btnConfirm.Text = "确认修改";
                if (Mac.Length == 0)
                {
                    MessageBox.Show("MAC地址无效！重试！");
                    return;
                }
                DMSDK.DM_SetIPAddr(tempConnect, IPAddressIP.IPAdd.ToString(), IPAddressSubMask.IPAdd.ToString(), IPAddressGateWay.IPAdd.ToString());
                sqlCreate.UpDate_CameraEnable(StaticClass.Temper_CameraId, "无", Convert.ToBoolean(cbxIsEnable.SelectedIndex), StaticClass.DataBaseName);
                MessageBox.Show("修改成功！请重新进入！");
                StaticClass.Temper_IsEnanle = Convert.ToBoolean(cbxIsEnable.SelectedIndex);
                this.Close();
            }
            else if(btnConfirm.Tag.ToString()=="Reconnect")
            {
                btnConfirm.Text = "重新连接";
                if (DMSDK.DM_CheckOnline(StaticClass.Temper_Ip, 80) < 0)// 离线检测  为离线
                {
                    MessageBox.Show("无法连接探测器，请检查线路,或修改参数后重新连接！");
                    btnConfirm.Tag = "Reconnect";
                    chbModifyGateWay.Enabled = false;
                    chbModifyMac.Enabled = false;
                    chbModifyNetMask.Enabled = false;
                    cbxIsEnable.SelectedIndex = Convert.ToInt32(StaticClass.Temper_IsEnanle);
                    StaticClass.Temper_IsEnanle = Convert.ToBoolean(cbxIsEnable.SelectedIndex);
                }
                else
                {
                    tempConnect = DMSDK.DM_Connect(this.Handle, IPAddressIP.IPAdd.ToString(), 80);
                    if (tempConnect < 0)
                    {
                        MessageBox.Show("连接失败！请检查设备与参数后重试！");
                        return;
                    }
                    btnConfirm.Text = "确认修改";
                    btnConfirm.Tag = "Confirm";
                    Mac = new StringBuilder();
                    SubMask = new StringBuilder();
                    GateWay = new StringBuilder();
                    DMSDK.DM_GetMAC(tempConnect, Mac);
                    DMSDK.DM_GetNetmask(tempConnect, SubMask);
                    DMSDK.DM_GetGateway(tempConnect, GateWay);
                    cbxIsEnable.SelectedIndex = Convert.ToInt32(StaticClass.Temper_IsEnanle);
                    StaticClass.Temper_IsEnanle = Convert.ToBoolean(cbxIsEnable.SelectedIndex);
                    Update_IpAddrGateWay(GateWay.ToString());
                    Update_IpAddrSubMask(SubMask.ToString());
                    tbxMAC.Text = Mac.ToString();
                    tbxMAC.Text = Mac.ToString();
                    chbModifyGateWay.Enabled = true;
                    chbModifyMac.Enabled = true;
                    chbModifyNetMask.Enabled = true;
                    cbxIsEnable.Enabled = true; ;

                }
            }
        }

        private void btnUpdateIsenable_Click(object sender, EventArgs e)
        {
            sqlCreate.UpDate_CameraEnable(StaticClass.Temper_CameraId, "无", Convert.ToBoolean(cbxIsEnable.SelectedIndex), StaticClass.DataBaseName);
            MessageBox.Show("修改成功！请重新进入！");
            this.Close();

        }

        private void FrmCameraNetConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            closefather();
            DMSDK.DM_Disconnect(tempConnect);
        }

        private void FrmCameraNetConfig_Disposed(object sender, EventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
        }

    }
}
