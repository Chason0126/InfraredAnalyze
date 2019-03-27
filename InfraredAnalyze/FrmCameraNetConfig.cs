using System;
using System.Collections;
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
    public partial class FrmCameraNetConfig : Form
    {
        public FrmCameraNetConfig()
        {
            InitializeComponent();
        }

        private int iPCameraID;
        SqlCreate sqlCreate = new SqlCreate();
        int tempOperateIntptr;

        public int IPCameraID { get => iPCameraID; set => iPCameraID = value; }



        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        Point point;
        private void panHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void panHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        StringBuilder Mac;
        StringBuilder SubMask;
        StringBuilder GateWay;
        private void FrmCameraConfig_Load(object sender, EventArgs e)
        {
            ArrayList arrayList = sqlCreate.Select_SMInfraredConfig(iPCameraID);
            Mac = new StringBuilder();
            SubMask = new StringBuilder();
            GateWay = new StringBuilder();
            if (arrayList.Count > 0)
            {
                StructClass.StructIAnalyzeConfig structSM7003Tag = (StructClass.StructIAnalyzeConfig)arrayList[0];
                string[] str = structSM7003Tag.IP.Split('.');
                IPAddressIP.tbx1.Text = str[0];
                IPAddressIP.tbx2.Text = str[1];
                IPAddressIP.tbx3.Text = str[2];
                IPAddressIP.tbx4.Text = str[3];
                DMSDK.DM_Init();
                tempOperateIntptr = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[iPCameraID - 1], structSM7003Tag.IP, 80);//
                if (tempOperateIntptr <= 0)
                {
                    MessageBox.Show("连接失败，请检查线路,或修改参数后新连接试！");
                    btnConfirm.Tag = "Reconnect";
                    Update_IpAddrGateWay("0.0.0.0");
                    Update_IpAddrSubMask("0.0.0.0");
                    chbModifyGateWay.Enabled = false;
                    chbModifyMac.Enabled = false;
                    chbModifyNetMask.Enabled = false;
                }
                else
                {
                    DMSDK.DM_GetMAC(tempOperateIntptr, Mac);
                    DMSDK.DM_GetNetmask(tempOperateIntptr, SubMask);
                    DMSDK.DM_GetGateway(tempOperateIntptr, GateWay);
                    Update_IpAddrGateWay(GateWay.ToString());
                    Update_IpAddrSubMask(SubMask.ToString());
                    tbxMAC.Text = Mac.ToString();
                    btnConfirm.Tag = "Confirm";
                    chbModifyGateWay.Enabled = true;
                    chbModifyMac.Enabled = true;
                    chbModifyNetMask.Enabled = true;
                }
            }
            arrayList.Clear();
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
            DMSDK.DM_Disconnect(tempOperateIntptr);
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
                DMSDK.DM_SetIPAddr(tempOperateIntptr, IPAddressIP.IPAdd.ToString(), IPAddressSubMask.IPAdd.ToString(), IPAddressGateWay.IPAdd.ToString());
                sqlCreate.UpDate_IPAddress(iPCameraID, IPAddressIP.IPAdd.ToString());
                MessageBox.Show("修改成功！");
            }
            else if(btnConfirm.Tag.ToString()=="Reconnect")
            {
                btnConfirm.Text = "重新连接";
                DMSDK.DM_Init();
                tempOperateIntptr = DMSDK.DM_Connect(StaticClass.intPtrs_UCPbx[iPCameraID - 1], IPAddressIP.IPAdd.ToString(), 80);
                if (tempOperateIntptr <= 0)
                {
                    MessageBox.Show("无法连接探测器，请检查线路,或修改参数后重新连接！");
                    btnConfirm.Tag = "Reconnect";
                    chbModifyGateWay.Enabled = false;
                    chbModifyMac.Enabled = false;
                    chbModifyNetMask.Enabled = false;
                }
                else
                {
                    btnConfirm.Text = "确认修改";
                    btnConfirm.Tag = "Confirm";
                    Mac = new StringBuilder();
                    SubMask = new StringBuilder();
                    GateWay = new StringBuilder();
                    DMSDK.DM_GetMAC(tempOperateIntptr, Mac);
                    DMSDK.DM_GetNetmask(tempOperateIntptr, SubMask);
                    DMSDK.DM_GetGateway(tempOperateIntptr, GateWay);
                    Update_IpAddrGateWay(GateWay.ToString());
                    Update_IpAddrSubMask(SubMask.ToString());
                    tbxMAC.Text = Mac.ToString();
                    tbxMAC.Text = Mac.ToString();
                    chbModifyGateWay.Enabled = true;
                    chbModifyMac.Enabled = true;
                    chbModifyNetMask.Enabled = true;
                }
            }
        }
    }
}
