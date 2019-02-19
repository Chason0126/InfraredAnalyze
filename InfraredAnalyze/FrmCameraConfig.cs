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
    public partial class FrmCameraConfig : Form
    {
        public FrmCameraConfig()
        {
            InitializeComponent();
        }

        private int iPCameraID;
        SqlCreate sqlCreate = new SqlCreate();

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

        private void IPAddressSbNetMask_Load(object sender, EventArgs e)
        {
            IPAddressSbNetMask.tbx1.Text = "255";
            IPAddressSbNetMask.tbx2.Text = "255";
            IPAddressSbNetMask.tbx3.Text = "255";
            IPAddressSbNetMask.tbx4.Text = "0";
        }

        private void FrmCameraConfig_Load(object sender, EventArgs e)
        {
            ArrayList arrayList = sqlCreate.Select_SMInfraredConfig(iPCameraID);
            if (arrayList.Count > 0)
            {
                StaticClass.StructIAnalyzeConfig structSM7003Tag = (StaticClass.StructIAnalyzeConfig)arrayList[0];
                DMSDK.DM_Init();
                StaticClass.intPtrs_Operate[iPCameraID - 1] = DMSDK.DM_Connect(this.Handle, structSM7003Tag.IP, structSM7003Tag.Port);
                arrayList.RemoveAt(0);
                DMSDK.DM_GetMAC(StaticClass.intPtrs_Operate[iPCameraID - 1], out string Mac);
                if(StaticClass.intPtrs_Operate[iPCameraID - 1]<=0)
                {
                    MessageBox.Show("连接失败，请检查参数后重试！");
                    this.Close();
                }
            }
        }

        private void IPAddressGateWay_Load(object sender, EventArgs e)
        {
            IPAddressGateWay.tbx1.Text = "192";
            IPAddressGateWay.tbx2.Text = "168";
            IPAddressGateWay.tbx3.Text = "1";
            IPAddressGateWay.tbx4.Text = "1";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DMSDK.DM_Disconnect(StaticClass.intPtrs_Operate[iPCameraID - 1]);
            this.Close();
        }
    }
}
