using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmTemperParamConfig : Form
    {
        public FrmTemperParamConfig()
        {
            InitializeComponent();
        }
        SqlCreate sqlCreate = new SqlCreate();
        private int cameraId;
        private string ip;
        int tempOperateIntptr;
        int tempConnectIntptr;
        int MeasureClass;//测温档位
        int RefeType;//参考温度类型
        int RefeTemp;//自定义参考温度
        int AmbientTemp;//环境温度
        int ObjDistance;//环境距离
        int AmbientHumidity;//环境湿度
        int ReviseParam;//修正系数
        int ReviseTemp;//修正温度
        int dwValue1;//修正温度范围1
        int dwValue2;//修正温度范围2
        bool IsEdit = false;
        bool IsUpdateArea = false;
        Point[] points = new Point[2];

       
       

        private DMSDK.fMessCallBack fMessCallBack;//回调函数实例
        public int CameraId { get => cameraId; set => cameraId = value; }
        public string Ip { get => ip; set => ip = value; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FrmTemperParamConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            DMSDK.DM_CloseMonitor(tempConnectIntptr);
            DMSDK.DM_Disconnect(tempOperateIntptr);
        }

        private void FrmTemperParamConfig_Load(object sender, EventArgs e)
        {
            //DMSDK.DM_Init();
            //tempOperateIntptr = DMSDK.DM_Connect(pbxScreen.Handle, ip, 80);
            //tempConnectIntptr = DMSDK.DM_OpenMonitor(pbxScreen.Handle, ip, 5000);
            //int InitValue = DMSDK.DM_PlayerInit(pbxScreen.Handle);
            //if (tempOperateIntptr <= 0 || InitValue < 0)
            //{
            //    MessageBox.Show("连接失败，请检查线路或者修改参数后重试！");
            //    this.Close();
            //    this.Dispose();
            //}
            //else
            //{
                //DMSDK.DM_ClearAllAretempOperateIntptr);
                //DMSDK.DM_ClearAllLine(tempOperateIntptr);
                //DMSDK.DM_ClearAllSpot(tempOperateIntptr);
                //DMSDK.DM_SetArea(tempOperateIntptr, 1, 0, 0, 50, 50, 90, 0);
                //DMSDK.DM_SetArea(tempOperateIntptr, 4, 60, 60, 100, 100, 90, 0);
                //DMSDK.DM_SetSpot(tempOperateIntptr, 1, 150, 150, 90);
                //DMSDK.DM_SetSpot(tempOperateIntptr, 2, 100, 100, 90);
                //DMSDK.DM_SetLine(tempOperateIntptr, 2, 50, 50, 200, 200, 100, 100, 90);
                //fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);//回调函数实例
                //DMSDK.DM_GetTemp(tempOperateIntptr, 0);//获取所有的测温目标的测温结果
                //DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
                //GetParam();
                //Load_Dgv();
                //}
        }

        //int len;
        //DMSDK.tagTemperature tagTemperature;
        //DMSDK.tagTempMessage tempMessage;
        //ArrayList arrayList_Area = new ArrayList();

        //private void dmMessCallBack(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        //{
        //    msg = msg - 0x8000;
        //    switch (msg)
        //    {
        //        case 0x3051:
                  
        //            break;
        //        case 0x3053:
        //            tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
        //            len = tempMessage.len;
        //            for (int i = 0; i < len; i++)
        //            {
        //                tagTemperature = new DMSDK.tagTemperature();
        //                tagTemperature = tempMessage.temperInfo[i];
        //                switch (tagTemperature.type)
        //                {
        //                    case 0://点
        //                        int SpotID = tagTemperature.number + 1;
        //                        break;
        //                    case 1://线
        //                        int LineID = tagTemperature.number + 1;
        //                        break;
        //                    case 2://区域
        //                        break;
        //                }
        //            }
        //            break;
        //    }
        //}
  
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
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        private void GetParam()
        {
            MeasureClass = DMSDK.DM_GetMeasureClass(tempOperateIntptr) - 1;//测温档位
            cbxMeasureClass.SelectedIndex = MeasureClass;

            RefeType = DMSDK.DM_GetRefeType(tempOperateIntptr);
            cbxRefeType.SelectedIndex = RefeType;

            RefeTemp = DMSDK.DM_GetRefeTemp(tempOperateIntptr) / 100;

            AmbientTemp = DMSDK.DM_GetAmbientTemp(tempOperateIntptr) / 100;
            tbxAmbientTemp.Text = AmbientTemp.ToString();

            ObjDistance = DMSDK.DM_GetObjDistance(tempOperateIntptr);
            tbxObjDistance.Text = ObjDistance.ToString();

            AmbientHumidity = DMSDK.DM_GetAmbientHumidity(tempOperateIntptr) / 100;
            tbxAmbientHumidity.Text = AmbientHumidity.ToString();

            ReviseParam = DMSDK.DM_GetReviseParam(tempOperateIntptr) / 100;
            tbxReviseParam.Text = ReviseParam.ToString();

            ReviseTemp = DMSDK.DM_GetReviseTemp(tempOperateIntptr) / 100;
            tbxReviseTemp.Text = ReviseTemp.ToString();

            DMSDK.DM_GetTemperatureScope(tempOperateIntptr, out dwValue1, out dwValue2);
            tbxdwValue1.Text = dwValue1.ToString();
            tbxdwValue2.Text = dwValue2.ToString();

            tbxAlarmTemp.Text = (DMSDK.DM_GetAlarmTemp(tempOperateIntptr) / 100).ToString();
            cbxAlarm.SelectedIndex = DMSDK.DM_GetRemoteAlarm(tempOperateIntptr);
            cbxAlarmColor.SelectedIndex = DMSDK.DM_GetRemoteAlarmColor(tempOperateIntptr);

        }

        private void cbxRefeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRefeType.SelectedIndex == 1)
            {
                gpbRefeTemp.Visible = true;
                tbxRefeTemp.Text = RefeTemp.ToString();
            }
            else
            {
                gpbRefeTemp.Visible = false;
            }
        }

        private void pbxScreen_MouseEnter(object sender, EventArgs e)
        {
            if (IsSet_Spot_1|| IsSet_Spot_2|| IsSet_Spot_3|| IsSet_Spot_4||IsSet_Area_1)
            {
                this.Cursor = Cursors.Cross;
            }
        }

        private void pbxScreen_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        ToolTip toolTip = new ToolTip();
        bool IsPbxMouseDown = false;
        private void pbxScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Cross)
            {
                point = new Point(e.X, e.Y);
                toolTip.SetToolTip(pbxScreen, "X:" + point.X / 2 + " " + "Y:" + point.Y / 2);
            }
            if (IsPbxMouseDown)
            {
                if (IsSet_Area_1)
                {
                    points[1] = new Point(e.X, e.Y);
                    graphics.Clear(Color.Transparent);
                    graphics.DrawRectangle(pen, points[0].X, points[0].Y, points[1].X - points[0].X, points[1].Y - points[0].Y);
                    pbxScreen.Image = bitmap;
                    tbxArea_1_X1.Text = (points[0].X / 2).ToString();
                    tbxArea_1_Y1.Text = (points[0].Y / 2).ToString();
                    tbxArea_1_X2.Text = (points[1].X / 2).ToString();
                    tbxArea_1_Y2.Text = (points[1].Y / 2).ToString();
                }
            }
        }

      
        private void pbxScreen_MouseDown(object sender, MouseEventArgs e)
        {
            IsPbxMouseDown = true;
            points[0] = new Point(e.X, e.Y);
            if (IsSet_Spot_1)
            {
                tbxSpot_1_X.Text = (e.X / 2).ToString();
                tbxSpot_1_Y.Text = (e.Y / 2).ToString();
            }
        }

        private void pbxScreen_MouseUp(object sender, MouseEventArgs e)
        {
            IsPbxMouseDown = false;
        }

        private void Load_Dgv()//从数据库中加载测温对象的参数
        {
            ArrayList arrayList = sqlCreate.Select_Spot(cameraId);
            foreach(DMSDK.temperAreaSpot areaSpot in arrayList)
            {
                //dgvSpot.Rows.Add(areaSpot.AreaId, areaSpot.X1, areaSpot.Y1, areaSpot.Emiss);
            }
        }

        bool IsSet_Spot_1 = false;
        bool IsSet_Spot_2 = false;
        bool IsSet_Spot_3 = false;
        bool IsSet_Spot_4 = false;
        bool IsSet_Area_1 = false;
        bool IsSet_Area_2 = false;
        bool IsSet_Area_3 = false;
        bool IsSet_Area_4 = false;
        private void btnAdd_Spot_1_Click(object sender, EventArgs e)
        {
            if(btnAdd_Spot_1.Text=="编辑")
            {
                IsSet_Spot_1 = true;
                tbxSpot_1_X.Enabled = true;
                tbxSpot_1_Y.Enabled = true;
                tbxSpot_1_Emiss.Enabled = true;
                btnAdd_Spot_1.Text = "确认";
            }
            else if (btnAdd_Spot_1.Text == "确认")
            {
                int x = Convert.ToInt32(tbxSpot_1_X.Text);
                int y = Convert.ToInt32(tbxSpot_1_Y.Text);
                IsSet_Spot_1 = false;
                tbxSpot_1_X.Enabled = false;
                tbxSpot_1_Y.Enabled = false;
                tbxSpot_1_Emiss.Enabled = false;
                if (x < 0 || x > 320 || y < 0 || y > 240) 
                {
                    MessageBox.Show("请输入合适的坐标");
                    return;
                }
                DMSDK.DM_SetSpot(tempOperateIntptr, 1, x, y);
                btnAdd_Spot_1.Text = "编辑";
            }
        }

        private void btnAdd_Spot_2_Click(object sender, EventArgs e)
        {
            if (btnAdd_Spot_2.Text == "编辑")
            {
                IsSet_Spot_2 = true;
                tbxSpot_2_X.Enabled = true;
                tbxSpot_2_Y.Enabled = true;
                tbxSpot_2_Emiss.Enabled = true;
                btnAdd_Spot_2.Text = "确认";
            }
            else if (btnAdd_Spot_2.Text == "确认")
            {
                int x = Convert.ToInt32(tbxSpot_2_X.Text);
                int y = Convert.ToInt32(tbxSpot_2_Y.Text);
                IsSet_Spot_2 = false;
                tbxSpot_2_X.Enabled = false;
                tbxSpot_2_Y.Enabled = false;
                tbxSpot_2_Emiss.Enabled = false;
                if (x < 0 || x > 320 || y < 0 || y > 240)
                {
                    MessageBox.Show("请输入合适的坐标");
                    return;
                }
                DMSDK.DM_SetSpot(tempOperateIntptr, 2, x, y);
                btnAdd_Spot_2.Text = "编辑";
            }
        }

        private void btnAdd_Spot_3_Click(object sender, EventArgs e)
        {
            if (btnAdd_Spot_3.Text == "编辑")
            {
                IsSet_Spot_3 = true;
                tbxSpot_3_X.Enabled = true;
                tbxSpot_3_Y.Enabled = true;
                tbxSpot_3_Emiss.Enabled = true;
                btnAdd_Spot_3.Text = "确认";
            }
            else if (btnAdd_Spot_3.Text == "确认")
            {
                int x = Convert.ToInt32(tbxSpot_3_X.Text);
                int y = Convert.ToInt32(tbxSpot_3_Y.Text);
                IsSet_Spot_3 = false;
                tbxSpot_3_X.Enabled = false;
                tbxSpot_3_Y.Enabled = false;
                tbxSpot_3_Emiss.Enabled = false;
                if (x < 0 || x > 320 || y < 0 || y > 240)
                {
                    MessageBox.Show("请输入合适的坐标");
                    return;
                }
                DMSDK.DM_SetSpot(tempOperateIntptr, 3, x, y);
                btnAdd_Spot_3.Text = "编辑";
            }
        }

        private void btnAdd_Spot_4_Click(object sender, EventArgs e)
        {
            if (btnAdd_Spot_4.Text == "编辑")
            {
                IsSet_Spot_4 = true;
                tbxSpot_4_X.Enabled = true;
                tbxSpot_4_Y.Enabled = true;
                tbxSpot_4_Emiss.Enabled = true;
                btnAdd_Spot_4.Text = "确认";
            }
            else if (btnAdd_Spot_4.Text == "确认")
            {
                int x = Convert.ToInt32(tbxSpot_4_X.Text);
                int y = Convert.ToInt32(tbxSpot_4_Y.Text);
                IsSet_Spot_4 = false;
                tbxSpot_4_X.Enabled = false;
                tbxSpot_4_Y.Enabled = false;
                tbxSpot_4_Emiss.Enabled = false;
                if (x < 0 || x > 320 || y < 0 || y > 240)
                {
                    MessageBox.Show("请输入合适的坐标");
                    return;
                }
                DMSDK.DM_SetSpot(tempOperateIntptr, 4, x, y);
                btnAdd_Spot_4.Text = "编辑";
            }
        }

        Graphics graphics;
        Bitmap bitmap;
        Pen pen;
        private void btnAdd_Area_1_Click(object sender, EventArgs e)
        {
            if (btnAdd_Area_1.Text == "编辑")
            {
                IsSet_Area_1 = true;
                tbxArea_1_X1.Enabled = true;
                tbxArea_1_Y1.Enabled = true;
                tbxArea_1_X2.Enabled = true;
                tbxArea_1_Y2.Enabled = true;
                tbxArea_1_Emiss.Enabled = true;
                cbxMeasureType_1.Enabled = true;
                btnAdd_Area_1.Text = "确认";
                bitmap = new Bitmap(pbxScreen.Width, pbxScreen.Height);
                graphics = Graphics.FromImage(bitmap);
                pen = new Pen(Color.Red, 3);
                

            }
            else if (btnAdd_Area_1.Text == "确认")
            {
                int x1 = Convert.ToInt32(tbxArea_1_X1.Text);
                int y1 = Convert.ToInt32(tbxArea_1_Y1.Text);
                int x2 = Convert.ToInt32(tbxArea_1_X2.Text);
                int y2 = Convert.ToInt32(tbxArea_1_Y2.Text);
                int emiss = Convert.ToInt32(tbxArea_1_Emiss.Text);
                int messuretype = Convert.ToInt32(cbxMeasureType_1.SelectedIndex);
                IsSet_Area_1 = false;
                graphics.Clear(Color.Transparent);
                tbxArea_1_X1.Enabled = false;
                tbxArea_1_Y1.Enabled = false;
                tbxArea_1_X2.Enabled = false;
                tbxArea_1_Y2.Enabled = false;
                tbxArea_1_Emiss.Enabled = false;
                cbxMeasureType_1.Enabled = false;
                if (x1 < 0 || x1 > 320 || y1 < 0 || y1 > 240|| x2 < 0 || x2 > 320 || y2 < 0 || y2 > 240)
                {
                    MessageBox.Show("请输入合适的坐标");
                    return;
                }
                graphics.Dispose();
                pen.Dispose();
                bitmap.Dispose();
                DMSDK.DM_SetArea(tempOperateIntptr, 1, x1, y1, x2, y2, emiss, messuretype);
                btnAdd_Area_1.Text = "编辑";
            }
            
        }
    }
}
