using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class pnl : Form
    {
        public pnl()
        {
            InitializeComponent();
        }
        SqlCreate sqlCreate = new SqlCreate();
        private int cameraId;
        private string ip;
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
        int AlarmMode;
        int AlarmPower;
        int AlarmType;
        int AlarmTemp;
        int AlarmColorID;
        int AlarmMessageType;
        Point[] points = new Point[2];
        Point[] pointss = new Point[8];
        //private DMSDK.fMessCallBack fMessCallBack;//回调函数实例
        public int CameraId { get => cameraId; set => cameraId = value; }
        public string Ip { get => ip; set => ip = value; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FrmTemperParamConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            DMSDK.DM_CloseMonitor(StaticClass.Temper_Monitor);
            DMSDK.DM_Disconnect(StaticClass.Temper_Connect);
        }

        private void FrmTemperParamConfig_Load(object sender, EventArgs e)
        {
            worker.WorkerSupportsCancellation = true;
            DMSDK.DM_Init();
            StaticClass.Temper_Connect = DMSDK.DM_Connect(pbxScreen.Handle, ip, 80);
            StaticClass.Temper_Monitor = DMSDK.DM_OpenMonitor(pbxScreen.Handle, ip, 5000);
            if (StaticClass.Temper_Connect <= 0)
            {
                MessageBox.Show("连接失败，请检查线路或者修改参数后重试！");
                this.Close();
                this.Dispose();
            }
            else
            {
                //int iEnable = new int();
                //string IP = "";
                //string ID = "";
                //string Password = "";
                //string Path = "";
                //int iOverWrite = new int();
                //int iFullAlarm = new int();
                //int iFullGrade = new int();
                //string cSize = "";
                //string cAvailableSpace = "";
                //fMessCallBack = new DMSDK.fMessCallBack(dmMessCallBack);//回调函数实例
                //DMSDK.DM_GetTemp(StaticClass.Temper_Connect, 0);//获取所有的测温目标的测温结果
                //DMSDK.DM_SetAllMessCallBack(fMessCallBack, 0);
                //int revalue = DMSDK.DM_GetNASInfo(StaticClass.Temper_Connect, ref iEnable, IP, ID, Password, Path, ref iOverWrite, ref iFullAlarm, ref iFullGrade, cSize, cAvailableSpace);
                GetAlarmParam();
                Get_Area_Param();
            }
        }

        #region//回调函数 测试使用
        int len;
        DMSDK.tagTemperature tagTemperature;
        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagAlarm tagAlarm;
        ArrayList arrayList_Area = new ArrayList();
        private void dmMessCallBack(int msg, IntPtr pBuf, int dwBufLen, uint dwUser)
        {
            msg = msg - 0x8000;
            switch (msg)
            {
                case 0x3051:

                    break;
                case 0x3053:
                    tempMessage = (DMSDK.tagTempMessage)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagTempMessage));
                    len = tempMessage.len;
                    for (int i = 0; i < len; i++)
                    {
                        tagTemperature = new DMSDK.tagTemperature();
                        tagTemperature = tempMessage.temperInfo[i];
                        switch (tagTemperature.type)
                        {
                            case 0://点
                                int SpotID = tagTemperature.number + 1;
                                break;
                            case 1://线
                                int LineID = tagTemperature.number + 1;
                                break;
                            case 2://区域
                                break;
                        }
                    }
                    break;
                case 0x3054:
                    tagAlarm =(DMSDK.tagAlarm)Marshal.PtrToStructure(pBuf, typeof(DMSDK.tagAlarm));
                    break;
            }
        }
        #endregion

        Point point;

        private void GetAlarmParam()
        {
            cbxMeasureClass.SelectedIndex = DMSDK.DM_GetMeasureClass(StaticClass.Temper_Connect) - 1;//测温档位

            cbxRefeType.SelectedIndex = DMSDK.DM_GetRefeType(StaticClass.Temper_Connect);//参考温度类型

            RefeTemp = DMSDK.DM_GetRefeTemp(StaticClass.Temper_Connect) / 100;//自定义参考温度的值

            tbxAmbientTemp.Text = (DMSDK.DM_GetAmbientTemp(StaticClass.Temper_Connect) / 100).ToString();//环境温度

            tbxObjDistance.Text = (DMSDK.DM_GetObjDistance(StaticClass.Temper_Connect)).ToString();//环境距离

            tbxAmbientHumidity.Text =(DMSDK.DM_GetAmbientHumidity(StaticClass.Temper_Connect) / 100).ToString();//环境湿度

            tbxReviseParam.Text = (DMSDK.DM_GetReviseParam(StaticClass.Temper_Connect) / 100).ToString();//修正系数

            tbxReviseTemp.Text = (DMSDK.DM_GetReviseTemp(StaticClass.Temper_Connect) / 100).ToString();//修正温度

            DMSDK.DM_GetTemperatureScope(StaticClass.Temper_Connect, out dwValue1, out dwValue2);//修正温度范围
            tbxdwValue1.Text = dwValue1.ToString();
            tbxdwValue2.Text = dwValue2.ToString();

            tbxAlarmTemp.Text = (DMSDK.DM_GetAlarmTemp(StaticClass.Temper_Connect) / 100).ToString();//报警温度

            cbxAlarm.SelectedIndex = DMSDK.DM_GetRemoteAlarm(StaticClass.Temper_Connect);//报警状态

            cbxAlarmColor.SelectedIndex = DMSDK.DM_GetRemoteAlarmColor(StaticClass.Temper_Connect);//仪器端报警色

            for(int i = 0; i <3; i++)
            {
                for(int j = 1; j <= 4; j++)
                {
                    switch (i)
                    {
                        case 0://点
                            DMSDK.DM_GetAlarmInfo(StaticClass.Temper_Connect, i, j + 1, out AlarmPower, out AlarmType, out AlarmTemp, out AlarmColorID, out AlarmMessageType);
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeSpot_" + j + "", false)[0])).SelectedIndex = AlarmType;
                            ((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempSpot_" + j + "", false)[0])).Text = (AlarmTemp / 100).ToString();
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorSpot_" + j + "", false)[0])).SelectedIndex = AlarmColorID;
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerSpot_" + j + "", false)[0])).SelectedIndex = AlarmPower;
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeSpot_" + j + "", false)[0])).SelectedIndex = AlarmMessageType;
                            break;
                        case 2://区域
                            DMSDK.DM_GetAlarmInfo(StaticClass.Temper_Connect, i, j + 5, out AlarmPower, out AlarmType, out AlarmTemp, out AlarmColorID, out AlarmMessageType);
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeArea_" + j + "", false)[0])).SelectedIndex = AlarmType;
                            ((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempArea_" + j + "", false)[0])).Text = (AlarmTemp / 100).ToString();
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorArea_" + j + "", false)[0])).SelectedIndex = AlarmColorID;
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerArea_" + j + "", false)[0])).SelectedIndex = AlarmPower;
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeArea_" + j + "", false)[0])).SelectedIndex = AlarmMessageType;
                            break;
                        case 1://线
                            DMSDK.DM_GetAlarmInfo(StaticClass.Temper_Connect, i, j, out AlarmPower, out AlarmType, out AlarmTemp, out AlarmColorID, out AlarmMessageType);
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeLine_1", false)[0])).SelectedIndex = AlarmType;
                            ((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempLine_1", false)[0])).Text = (AlarmTemp / 100).ToString();
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorLine_1", false)[0])).SelectedIndex = AlarmColorID;
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerLine_1", false)[0])).SelectedIndex = AlarmPower;
                            ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeLine_1", false)[0])).SelectedIndex = AlarmMessageType;
                            break;
                    }
                }
            }
          
        }

        private void cbxRefeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRefeType.SelectedIndex == 1)
            {
                grpRefeTemp.Visible = true;
                tbxRefeTemp.Text = RefeTemp.ToString();
            }
            else
            {
                grpRefeTemp.Visible = false;
            }
        }

        private void btnUpdateAlarmParam_Click(object sender, EventArgs e)
        {
            DMSDK.DM_SetMeasureClass(StaticClass.Temper_Connect, cbxMeasureClass.SelectedIndex + 1);//设置测温档位
            DMSDK.DM_SetRefeType(StaticClass.Temper_Connect, cbxRefeType.SelectedIndex);//设置参考温度类型
            if (cbxRefeType.SelectedIndex == 1)
            {
                DMSDK.DM_SetRefeTemp(StaticClass.Temper_Connect, Convert.ToInt32(tbxRefeTemp.Text) * 100);//自定义温度时
            }
            DMSDK.DM_SetAmbientTemp(StaticClass.Temper_Connect, Convert.ToInt32(tbxAmbientTemp.Text) * 100);//设置环境温度
            DMSDK.DM_SetObjDistance(StaticClass.Temper_Connect, Convert.ToInt32(tbxObjDistance.Text));//设置环境距离
            DMSDK.DM_SetAmbientHumidity(StaticClass.Temper_Connect, Convert.ToInt32(tbxAmbientHumidity.Text) * 100);//设置环境湿度
            DMSDK.DM_SetReviseParam(StaticClass.Temper_Connect, Convert.ToInt32(tbxReviseParam.Text) * 100);//修正系数
            DMSDK.DM_SetReviseTemp(StaticClass.Temper_Connect, Convert.ToInt32(tbxReviseTemp.Text) * 100);//修正温度
            DMSDK.DM_SetTemperatureScope(StaticClass.Temper_Connect, Convert.ToInt32(tbxdwValue1.Text), Convert.ToInt32(tbxdwValue2.Text));
            GetAlarmParam();
            MessageBox.Show("设置成功！");
        }

        FrmIsRunning isRunning;
        BackgroundWorker worker = new BackgroundWorker();
        private void btnUpdateAlarmInfo_Click(object sender, EventArgs e)
        {
            isRunning = new FrmIsRunning(worker);
            worker.DoWork += new DoWorkEventHandler(showIsRunning);
            worker.RunWorkerAsync();
            isRunning.ShowDialog();
            MessageBox.Show("设置完成！");
        }


        private void showIsRunning(object sender,DoWorkEventArgs e)
        {
            if (cbxAlarm.SelectedIndex == 0)//打开或关闭 报警功能
            {
                DMSDK.DM_CloseRemoteAlarm(StaticClass.Temper_Connect);
            }
            else if (cbxAlarm.SelectedIndex == 1)
            {
                DMSDK.DM_OpenAlarm(StaticClass.Temper_Connect);
            }
            DMSDK.DM_SetRemoteAlarmTemp(StaticClass.Temper_Connect, Convert.ToInt32(tbxAlarmTemp.Text) * 100);//设置报警温度
            DMSDK.DM_SetRemoteAlarmColor(StaticClass.Temper_Connect, cbxAlarmColor.SelectedIndex);//设置报警颜色
            if (ckbIO.Checked)
            {
                if (cbxiIO.SelectedIndex == -1 || cbxiEnable.SelectedIndex == -1)
                {
                    DMSDK.DM_IOAlarm(StaticClass.Temper_Connect, cbxiIO.SelectedIndex + 2, cbxiEnable.SelectedIndex);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 4; j++)
                {

                    switch (i)
                    {
                        case 0://设置点 温度告警
                            AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeSpot_" + j + "", false)[0])).SelectedIndex;
                            AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempSpot_" + j + "", false)[0])).Text) * 100;
                            AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorSpot_" + j + "", false)[0])).SelectedIndex;
                            AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerSpot_" + j + "", false)[0])).SelectedIndex;
                            AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeSpot_" + j + "", false)[0])).SelectedIndex;
                            DMSDK.DM_SetAlarmInfo(StaticClass.Temper_Connect, i, j + 1, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测温点 编号+1 从1开始算
                            sqlCreate.Update_Alarmconfig(cameraId, "S" + (j + 1), AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower));//向数据库写入 告警设置信息 供 判断告警时使用
                            break;
                        case 2://设置区域温度告警
                            AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeArea_" + j + "", false)[0])).SelectedIndex;
                            AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempArea_" + j + "", false)[0])).Text) * 100;
                            AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorArea_" + j + "", false)[0])).SelectedIndex;
                            AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerArea_" + j + "", false)[0])).SelectedIndex;
                            AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeArea_" + j + "", false)[0])).SelectedIndex;
                            DMSDK.DM_SetAlarmInfo(StaticClass.Temper_Connect, i, j + 5, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测区域 编号 +5 从6开始算起
                            sqlCreate.Update_Alarmconfig(cameraId, "A" + (j + 5), AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower));
                            break;
                        case 1://设置线 温度告警
                            AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeLine_1", false)[0])).SelectedIndex;
                            AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempLine_1", false)[0])).Text) * 100;
                            AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorLine_1", false)[0])).SelectedIndex;
                            AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerLine_1", false)[0])).SelectedIndex;
                            AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeLine_1", false)[0])).SelectedIndex;
                            DMSDK.DM_SetAlarmInfo(StaticClass.Temper_Connect, i, j, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测温线  仅一条
                            sqlCreate.Update_Alarmconfig(cameraId, "L1", AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower));
                            break;
                    }
                }
            }
            GetAlarmParam();
        }

        private void ckbIO_CheckedChanged(object sender, EventArgs e)
        {
            grpIO.Enabled = ckbIO.Checked;
        }

        #region//只能有一个报警颜色  区域  
        private void cbxAlarmPowerSpot_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerSpot_1.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerSpot_1, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerArea_1, cbxAlarmPowerArea_2, cbxAlarmPowerArea_3, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
            }
        }

        private void cbxAlarmPowerSpot_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerSpot_2.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_1, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerArea_1, cbxAlarmPowerArea_2, cbxAlarmPowerArea_3, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
            }
        }

        private void cbxAlarmPowerSpot_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerSpot_3.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_1, cbxAlarmPowerSpot_4, cbxAlarmPowerArea_1, cbxAlarmPowerArea_2, cbxAlarmPowerArea_3, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
            }
        }

        private void cbxAlarmPowerSpot_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerSpot_4.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerSpot_4, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_1, cbxAlarmPowerArea_1, cbxAlarmPowerArea_2, cbxAlarmPowerArea_3, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
            }
        }

        private void cbxAlarmPowerArea_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerArea_1.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerArea_1, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerSpot_1, cbxAlarmPowerArea_2, cbxAlarmPowerArea_3, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
                //cbxAlarmModeEnable(cbxAlarmModeArea_1, cbxAlarmModeArea_2, cbxAlarmModeArea_3, cbxAlarmModeArea_4, cbxAlarmModeLine_1);
            }
        }

        private void cbxAlarmPowerArea_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerArea_2.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerArea_2, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerSpot_1, cbxAlarmPowerArea_1, cbxAlarmPowerArea_3, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
                //cbxAlarmModeEnable(cbxAlarmModeArea_2, cbxAlarmModeArea_1, cbxAlarmModeArea_3, cbxAlarmModeArea_4, cbxAlarmModeLine_1);
            }
        }

        private void cbxAlarmPowerArea_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerArea_3.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerArea_3, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerSpot_1, cbxAlarmPowerArea_1, cbxAlarmPowerArea_2, cbxAlarmPowerArea_4, cbxAlarmPowerLine_1);
                //cbxAlarmModeEnable(cbxAlarmModeArea_3, cbxAlarmModeArea_2, cbxAlarmModeArea_1, cbxAlarmModeArea_4, cbxAlarmModeLine_1);
            }
        }

        private void cbxAlarmPowerArea_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerArea_4.SelectedIndex == 1)
            {
               //cbxSelectedIndexChanged(cbxAlarmPowerArea_4, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerSpot_1, cbxAlarmPowerArea_1, cbxAlarmPowerArea_3, cbxAlarmPowerArea_2, cbxAlarmPowerLine_1);
                //cbxAlarmModeEnable(cbxAlarmModeArea_4, cbxAlarmModeArea_2, cbxAlarmModeArea_3, cbxAlarmModeArea_1, cbxAlarmModeLine_1);
            }
        }

        private void cbxAlarmPowerLine_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlarmPowerArea_4.SelectedIndex == 1)
            {
                //cbxSelectedIndexChanged(cbxAlarmPowerLine_1, cbxAlarmPowerSpot_2, cbxAlarmPowerSpot_3, cbxAlarmPowerSpot_4, cbxAlarmPowerSpot_1, cbxAlarmPowerArea_1, cbxAlarmPowerArea_3, cbxAlarmPowerArea_2, cbxAlarmPowerArea_4);
                //cbxAlarmModeEnable(cbxAlarmModeLine_1, cbxAlarmModeArea_2, cbxAlarmModeArea_3, cbxAlarmModeArea_4, cbxAlarmModeArea_1);
            }
        }

        private void cbxSelectedIndexChanged(ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3,ComboBox comboBox4,ComboBox comboBox5, ComboBox comboBox6, ComboBox comboBox7, ComboBox comboBox8, ComboBox comboBox9)
        {
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
        }

        private void cbxAlarmModeEnable(ComboBox comboBox1,ComboBox comboBox2,ComboBox comboBox3,ComboBox comboBox4,ComboBox comboBox5)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
        }
        #endregion

        #region//测温区域设置
        #region//从数据库中加载测温区域的参数
        ArrayList arrayList_All_Spot;
        DMSDK.temperSpot Struct_temperSpot;
        ArrayList arrayList_All_Area;
        DMSDK.temperArea Struct_temperArea;
        ArrayList arrayList_All_Line;
        DMSDK.temperLine Struct_temperLine;
        private void Get_Area_Param()
        {
            arrayList_All_Spot = sqlCreate.Select_All_Spot(cameraId,"S");
            arrayList_All_Area = sqlCreate.Select_All_Area(cameraId, "A");
            arrayList_All_Line = sqlCreate.Select_All_Line(cameraId, "L");
            Struct_temperSpot = new DMSDK.temperSpot();
            Struct_temperArea = new DMSDK.temperArea();
            Struct_temperLine = new DMSDK.temperLine();
            if (arrayList_All_Spot.Count > 0 && arrayList_All_Area.Count > 0 && arrayList_All_Line.Count > 0)
            {
               for(int i = 0; i < arrayList_All_Spot.Count; i++)
                {
                    Struct_temperSpot = (DMSDK.temperSpot)arrayList_All_Spot[i];
                    ((TextBox)(grpSpot.Controls.Find("tbxSpot_" + (i + 1) + "_X", false)[0])).Text = Struct_temperSpot.X1.ToString();
                    ((TextBox)(grpSpot.Controls.Find("tbxSpot_" + (i + 1) + "_Y", false)[0])).Text = Struct_temperSpot.Y1.ToString();
                    ((TextBox)(grpSpot.Controls.Find("tbxSpot_" + (i + 1) + "_Emiss", false)[0])).Text = Struct_temperSpot.Emiss.ToString();
                }
                for (int i = 0; i < arrayList_All_Area.Count; i++)
                {
                    Struct_temperArea = (DMSDK.temperArea)arrayList_All_Area[i];
                    ((TextBox)(grpArea.Controls.Find("tbxArea_" + (i + 1) + "_X1", false)[0])).Text = Struct_temperArea.X1.ToString();
                    ((TextBox)(grpArea.Controls.Find("tbxArea_" + (i + 1) + "_Y1", false)[0])).Text = Struct_temperArea.Y1.ToString();
                    ((TextBox)(grpArea.Controls.Find("tbxArea_" + (i + 1) + "_X2", false)[0])).Text = Struct_temperArea.X2.ToString();
                    ((TextBox)(grpArea.Controls.Find("tbxArea_" + (i + 1) + "_Y2", false)[0])).Text = Struct_temperArea.Y2.ToString();
                    ((TextBox)(grpArea.Controls.Find("tbxArea_" + (i + 1) + "_Emiss", false)[0])).Text = Struct_temperArea.Emiss.ToString();
                    ((ComboBox)(grpArea.Controls.Find("cbxMeasureType_" + (i + 1) + "", false)[0])).SelectedIndex = Struct_temperArea.MeasureType;//cbxMeasureType_1
                }
                for (int i = 0; i < arrayList_All_Line.Count; i++)
                {
                    Struct_temperLine = (DMSDK.temperLine)arrayList_All_Line[i];
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_X1", false)[0])).Text = Struct_temperLine.X1.ToString();//
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_Y1", false)[0])).Text = Struct_temperLine.Y1.ToString();//
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_X2", false)[0])).Text = Struct_temperLine.X2.ToString();//
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_Y2", false)[0])).Text = Struct_temperLine.Y2.ToString();//
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_X3", false)[0])).Text = Struct_temperLine.X3.ToString();//
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_Y3", false)[0])).Text = Struct_temperLine.Y3.ToString();//
                    ((TextBox)(grpLine.Controls.Find("tbxLine_" + (i + 1) + "_Emiss", false)[0])).Text = Struct_temperLine.Emiss.ToString();//
                }
            }
            else
            {
                MessageBox.Show("测温参数-数据库异常！请检查数据库！");
            }
        }
        #endregion


        #region//添加pbx 存放截图
        PictureBox pbxBitmap;
        private void Create_Pbx()
        {
            pbxBitmap = new PictureBox();
            pbxBitmap.Height = pbxScreen.Height;
            pbxBitmap.Width = pbxScreen.Width;
            pbxScreen.Controls.Add(pbxBitmap);
            pbxBitmap.Parent = pbxScreen;
            pbxBitmap.MouseEnter += PbxBitmap_MouseEnter;
            pbxBitmap.MouseLeave += PbxBitmap_MouseLeave;
            pbxBitmap.MouseDown += PbxBitmap_MouseDown;
            pbxBitmap.MouseMove += PbxBitmap_MouseMove;
            pbxBitmap.MouseUp += PbxBitmap_MouseUp;
            pen = new Pen(Color.Green, 3);
            bitmapDraw = new Bitmap(pbxBitmap.Width, pbxBitmap.Height);
            graphicsDraw = Graphics.FromImage(bitmapDraw);
            bitmap = new Bitmap(pbxScreen.Width, pbxScreen.Height);
            graphics = Graphics.FromImage(bitmap);
            //Size size = new Size(320,240);
           // Rectangle rectangle = new Rectangle(pbxScreen.Location, size);
            graphics.CopyFromScreen(pbxScreen.PointToScreen(Point.Empty), Point.Empty, bitmap.Size);//截图 会被遮挡
            //pbxScreen.DrawToBitmap(bitmap, rectangle);
            pbxBitmap.BackgroundImage = bitmap;
        }
        #endregion

        #region//释放GDI+资源
        Graphics graphics;
        Bitmap bitmap;
        Graphics graphicsDraw;
        Bitmap bitmapDraw;
        Pen pen;
        private void Dispose_Graph()
        {
            pbxBitmap.Dispose();
            graphics.Dispose();
            bitmap.Dispose();
            pen.Dispose();
            graphicsDraw.Dispose();
            bitmapDraw.Dispose();
        }
        #endregion

        bool IsSet_Spot = false;
        bool IsSet_Area = false;
        bool IsSet_Line = false;
       
        #region//取消事件
        private void Cancel_SetSpot(string type,Button btnAdd_Spot, TextBox tbxSpot_X, TextBox tbxSpot_Y, TextBox tbxSpot_Emiss, Button btnClear_Spot)//取消设置点
        {
            arrayList_All_Spot = sqlCreate.Select_Spot(cameraId, type);
            Struct_temperSpot = (DMSDK.temperSpot)arrayList_All_Spot[0];
            tbxSpot_X.Text = Struct_temperSpot.X1.ToString();
            tbxSpot_Y.Text = Struct_temperSpot.Y1.ToString();
            tbxSpot_Emiss.Text = Struct_temperSpot.Emiss.ToString();//文本框数据还原
            foreach (Control control in pnlBtnSpot.Controls)//控件enable为true
            {
                control.Enabled = true;
            }
            IsSet_Spot = false;
            btnAdd_Spot.Text = "编辑";
            btnClear_Spot.Text = "清除";
            grpArea.Enabled = true;//其余控件为false
            grpLine.Enabled = true;
            tbxSpot_X.Enabled = false;
            tbxSpot_Y.Enabled = false;
            tbxSpot_Emiss.Enabled = false;
            Dispose_Graph();
        }

        private void Cancel_SetArea(string type,Button btnAdd_Area, TextBox tbxArea_X1, TextBox tbxArea_Y1, TextBox tbxArea_X2, TextBox tbxArea_Y2, TextBox tbxArea_Emiss, ComboBox cbxMeasureType, Button btnClear_Area)//取消设置区域
        {
            arrayList_All_Area = sqlCreate.Select_Area(cameraId, type);
            Struct_temperArea = (DMSDK.temperArea)arrayList_All_Area[0];
            tbxArea_X1.Text = Struct_temperArea.X1.ToString();
            tbxArea_Y1.Text = Struct_temperArea.Y1.ToString();
            tbxArea_X2.Text = Struct_temperArea.X2.ToString();
            tbxArea_Y2.Text = Struct_temperArea.Y2.ToString();
            tbxArea_Emiss.Text = Struct_temperArea.Emiss.ToString();
            cbxMeasureType.SelectedIndex = Struct_temperArea.MeasureType;
            foreach (Control control in pnlBtnArea.Controls)
            {
                control.Enabled = true;
            }
            IsSet_Area = false;
            btnAdd_Area.Text = "编辑";
            btnClear_Area.Text = "清除";
            grpSpot.Enabled = true;
            grpLine.Enabled = true;
            tbxArea_X1.Enabled = false;
            tbxArea_Y1.Enabled = false;
            tbxArea_X2.Enabled = false;
            tbxArea_Y2.Enabled = false;
            tbxArea_Emiss.Enabled = false;
            cbxMeasureType.Enabled = false;
            Dispose_Graph();
        }

        private void Cancel_SetLine(string type, Button btnAdd_Line, TextBox tbxLine_X1, TextBox tbxLine_Y1, TextBox tbxLine_X2, TextBox tbxLine_Y2, TextBox tbxLine_X3, TextBox tbxLine_Y3, TextBox tbxLine_Emiss, Button btnClear_Line)//取消设置线
        {
            arrayList_All_Line = sqlCreate.Select_Line(cameraId, type);
            Struct_temperLine = (DMSDK.temperLine)arrayList_All_Line[0];
            tbxLine_X1.Text = Struct_temperLine.X1.ToString();
            tbxLine_Y1.Text = Struct_temperLine.Y1.ToString();
            tbxLine_X2.Text = Struct_temperLine.X2.ToString();
            tbxLine_Y2.Text = Struct_temperLine.Y2.ToString();
            tbxLine_X3.Text = Struct_temperLine.X3.ToString();
            tbxLine_Y3.Text = Struct_temperLine.Y3.ToString();
            tbxLine_Emiss.Text = Struct_temperLine.Emiss.ToString();
            foreach (Control control in pnlBtnLine.Controls)
            {
                control.Enabled = true;
            }
            IsSet_Line = false;
            btnAdd_Line.Text = "编辑";
            btnClear_Line.Text = "清除";
            grpSpot.Enabled = true;
            grpArea.Enabled = true;
            tbxLine_X1.Enabled = false;
            tbxLine_Y1.Enabled = false;
            tbxLine_X2.Enabled = false;
            tbxLine_Y2.Enabled = false;
            tbxLine_X3.Enabled = false;
            tbxLine_Y3.Enabled = false;
            tbxLine_Emiss.Enabled = false;
            Dispose_Graph();
        }
        #endregion

        string type;
        #region//设置测温点
        private void Set_Spot(Button btnAdd_Spot, TextBox tbxSpot_X, TextBox tbxSpot_Y, TextBox tbxSpot_Emiss, Button btnClear_Spot)
        {
            try
            {
                if (btnAdd_Spot.Text == "编辑")
                {
                    IsSet_Spot = true;
                    tbxSpot_X.Enabled = true;
                    tbxSpot_Y.Enabled = true;
                    tbxSpot_Emiss.Enabled = true;
                    btnAdd_Spot.Text = "确认";
                    btnClear_Spot.Text = "取消";
                    Create_Pbx();
                    foreach (Control control in pnlBtnSpot.Controls)
                    {
                        control.Enabled = false;
                    }
                    grpArea.Enabled = false;
                    grpLine.Enabled = false;
                    btnAdd_Spot.Enabled = true;
                    btnClear_Spot.Enabled = true;
                }
                else if (btnAdd_Spot.Text == "确认")
                {
                    int x = Convert.ToInt32(tbxSpot_X.Text);
                    int y = Convert.ToInt32(tbxSpot_Y.Text);
                    int emiss = Convert.ToInt32(tbxSpot_1_Emiss.Text);
                    if (x <= 0 || x > 320 || y <= 0 || y > 240)
                    {
                        MessageBox.Show("请输入合适的坐标");
                        return;
                    }
                    if (tbxSpot_1_X.Enabled)
                    {
                        type = "S2";
                        DMSDK.DM_SetSpot(StaticClass.Temper_Connect, 2, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(cameraId, type, x - 2, y - 2, emiss);
                    }
                    else if (tbxSpot_2_X.Enabled)
                    {
                        type = "S3";
                        DMSDK.DM_SetSpot(StaticClass.Temper_Connect, 3, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(cameraId, type, x - 2, y - 2, emiss);
                    }
                    else if (tbxSpot_3_X.Enabled)
                    {
                        type = "S4";
                        DMSDK.DM_SetSpot(StaticClass.Temper_Connect, 4, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(cameraId, type, x - 2, y - 2, emiss);
                    }
                    else if (tbxSpot_4_X.Enabled)
                    {
                        type = "S5";
                        DMSDK.DM_SetSpot(StaticClass.Temper_Connect, 5, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(cameraId, type, x - 2, y - 2, emiss);
                    }
                    Cancel_SetSpot(type, btnAdd_Spot, tbxSpot_X, tbxSpot_Y, tbxSpot_Emiss, btnClear_Spot);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "设置测温点失败！");
            }
        }
        #endregion

        #region//设置测温区域
        private void Set_Area(Button btnAdd_Area, TextBox tbxArea_X1, TextBox tbxArea_Y1, TextBox tbxArea_X2, TextBox tbxArea_Y2, TextBox tbxArea_Emiss, ComboBox cbxMeasureType,Button btnClear_Area)//设置测温区域
        {
            if (btnAdd_Area.Text == "编辑") //C++中将画面绑定到控件上了 无法在画面中动态的绘制矩形  转而为 截图绘制。
            {
                IsSet_Area = true;
                tbxArea_X1.Enabled = true;
                tbxArea_Y1.Enabled = true;
                tbxArea_X2.Enabled = true;
                tbxArea_Y2.Enabled = true;
                tbxArea_Emiss.Enabled = true;
                cbxMeasureType.Enabled = true;
                btnAdd_Area.Text = "确认";
                btnClear_Area.Text = "取消";
                Create_Pbx();
                foreach (Control control in pnlBtnArea.Controls)
                {
                    control.Enabled = false;
                }
                btnAdd_Area.Enabled = true;
                btnClear_Area.Enabled = true;
                grpSpot.Enabled = false;
                grpLine.Enabled = false;
            }
            else if (btnAdd_Area.Text == "确认")
            {
                try
                {
                    if (cbxMeasureType.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择区域测温方式！");
                        return;
                    }
                    int x1 = Convert.ToInt32(tbxArea_X1.Text);
                    int y1 = Convert.ToInt32(tbxArea_Y1.Text);
                    int x2 = Convert.ToInt32(tbxArea_X2.Text);
                    int y2 = Convert.ToInt32(tbxArea_Y2.Text);
                    int emiss = Convert.ToInt32(tbxArea_Emiss.Text);
                    int messuretype = Convert.ToInt32(cbxMeasureType.SelectedIndex);
                    if (x1 <= 0 || x1 > 320 || y1 <= 0 || y1 > 240 || x2 <= 0 || x2 > 320 || y2 <= 0 || y2 > 240)
                    {
                        MessageBox.Show("请输入合适的坐标");
                        return;
                    }
                    if (tbxArea_1_X1.Enabled)
                    {
                        type = "A6";
                        DMSDK.DM_SetArea(StaticClass.Temper_Connect, 6, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(cameraId, "A6", x1, y1, x2, y2, emiss, messuretype);
                    }
                    else if (tbxArea_2_X1.Enabled)
                    {
                        type = "A7";
                        DMSDK.DM_SetArea(StaticClass.Temper_Connect, 7, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(cameraId, "A7", x1, y1, x2, y2, emiss, messuretype);
                    }
                    else if (tbxArea_3_X1.Enabled)
                    {
                        type = "A8";
                        DMSDK.DM_SetArea(StaticClass.Temper_Connect, 8, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(cameraId, "A8", x1, y1, x2, y2, emiss, messuretype);
                    }
                    else if (tbxArea_4_X1.Enabled)
                    {
                        type = "A9";
                        DMSDK.DM_SetArea(StaticClass.Temper_Connect, 9, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(cameraId, "A9", x1, y1, x2, y2, emiss, messuretype);
                    }
                    Cancel_SetArea(type, btnAdd_Area, tbxArea_X1, tbxArea_Y1, tbxArea_X2, tbxArea_Y2, tbxArea_Emiss, cbxMeasureType, btnClear_Area);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "设置测温区域失败！");
                }
            }
        }
        #endregion

        #region//设置测温线
        private void Set_Line(Button btnAdd_Line,TextBox tbxLine_X1, TextBox tbxLine_Y1, TextBox tbxLine_X2, TextBox tbxLine_Y2, TextBox tbxLine_X3, TextBox tbxLine_Y3, TextBox tbxLine_Emiss,Button btnClear_Line)
        {
            if (btnAdd_Line.Text == "编辑")
            {
                IsSet_Line = true;
                tbxLine_X1.Enabled = true;
                tbxLine_Y1.Enabled = true;
                tbxLine_X2.Enabled = true;
                tbxLine_Y2.Enabled = true;
                tbxLine_X3.Enabled = true;
                tbxLine_Y3.Enabled = true;
                tbxLine_Emiss.Enabled = true;
                btnAdd_Line.Text = "确认";
                btnClear_Line.Text = "取消";
                Create_Pbx();
                foreach (Control control in pnlBtnLine.Controls)
                {
                    control.Enabled = false;
                }
                btnAdd_Line.Enabled = true;
                btnClear_Line.Enabled = true;
                grpSpot.Enabled = false;
                grpArea.Enabled = false;
            }
            else if (btnAdd_Line.Text == "确认")
            {
                try
                {
                    int x1 = Convert.ToInt32(tbxLine_X1.Text);
                    int y1 = Convert.ToInt32(tbxLine_Y1.Text);
                    int x2 = Convert.ToInt32(tbxLine_X2.Text);
                    int y2 = Convert.ToInt32(tbxLine_Y2.Text);
                    int x3 = Convert.ToInt32(tbxLine_X3.Text);
                    int y3 = Convert.ToInt32(tbxLine_Y3.Text);
                    int emiss = Convert.ToInt32(tbxLine_Emiss.Text);
                    if (x1 <= 0 || x1 > 320 || y1 <= 0 || y1 > 240 || x2 <= 0 || x2 > 320 || y2 <= 0 || y2 > 240)
                    {
                        MessageBox.Show("请输入合适的坐标");
                        return;
                    }
                    if (tbxLine_X1.Enabled)
                    {
                        type = "L1";
                        DMSDK.DM_SetLine(StaticClass.Temper_Connect, 1, x1, y1, x2, y2, ((x1 + x2) / 2) - 2, ((y1 + y2) / 2) - 4, emiss);
                        sqlCreate.Update_Line(cameraId, "L1", x1, y1, x2, y2, (x1 + x2) / 2, (y1 + y2) / 2, emiss);
                    }
                    Cancel_SetLine(type, btnAdd_Line, tbxLine_X1, tbxLine_Y1, tbxLine_X2, tbxLine_Y2, tbxLine_X3, tbxLine_Y3, tbxLine_Emiss, btnClear_Line);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "设置测温线失败！");
                }
            }
        }
        #endregion

        #region//设置测温目标事件
        private void btnAdd_Spot_1_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_1, tbxSpot_1_X, tbxSpot_1_Y, tbxSpot_1_Emiss, btnClear_Spot_1);
        }

        private void btnAdd_Spot_2_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_2, tbxSpot_2_X, tbxSpot_2_Y, tbxSpot_2_Emiss, btnClear_Spot_2);
        }

        private void btnAdd_Spot_3_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_3, tbxSpot_3_X, tbxSpot_3_Y, tbxSpot_3_Emiss, btnClear_Spot_3);
        }

        private void btnAdd_Spot_4_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_4, tbxSpot_4_X, tbxSpot_4_Y, tbxSpot_4_Emiss, btnClear_Spot_4);
        }

        private void btnAdd_Line_1_Click(object sender, EventArgs e)
        {
            Set_Line(btnAdd_Line_1, tbxLine_1_X1, tbxLine_1_Y1, tbxLine_1_X2, tbxLine_1_Y2, tbxLine_1_X3, tbxLine_1_Y3, tbxLine_1_Emiss, btnClear_Line_1);
        }

        private void btnAdd_Area_1_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_1, tbxArea_1_X1, tbxArea_1_Y1, tbxArea_1_X2, tbxArea_1_Y2, tbxArea_1_Emiss, cbxMeasureType_1,btnClear_Area_1);
        }

        private void btnAdd_Area_2_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_2, tbxArea_2_X1, tbxArea_2_Y1, tbxArea_2_X2, tbxArea_2_Y2, tbxArea_2_Emiss, cbxMeasureType_2, btnClear_Area_2);
        }

        private void btnAdd_Area_3_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_3, tbxArea_3_X1, tbxArea_3_Y1, tbxArea_3_X2, tbxArea_3_Y2, tbxArea_3_Emiss, cbxMeasureType_3, btnClear_Area_3);
        }

        private void btnAdd_Area_4_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_4, tbxArea_4_X1, tbxArea_4_Y1, tbxArea_4_X2, tbxArea_4_Y2, tbxArea_4_Emiss, cbxMeasureType_4, btnClear_Area_4);
        }
        #endregion

        #region//存放截图的pbx相关操作事件(离屏表面)
        bool IsPbxMouseDown = false;
        ToolTip toolTip = new ToolTip();
        private void PbxBitmap_MouseUp(object sender, MouseEventArgs e)//鼠标按下事件
        {
            IsPbxMouseDown = false;
        }

        private void PbxBitmap_MouseMove(object sender, MouseEventArgs e)//鼠标移动事件
        {
            if (this.Cursor == Cursors.Cross)
            {
                point = new Point(e.X, e.Y);
                toolTip.SetToolTip(pbxBitmap, "X:" + point.X / 2 + " " + "Y:" + point.Y / 2);
            }
            if (IsPbxMouseDown)
            {
                if (IsSet_Area)
                {
                    points[1] = new Point(e.X, e.Y);
                    graphicsDraw.Clear(Color.Transparent);
                    graphicsDraw.DrawRectangle(pen, points[0].X, points[0].Y, points[1].X - points[0].X, points[1].Y - points[0].Y);
                    pbxBitmap.Image = bitmapDraw;
                    if (tbxArea_1_X1.Enabled)
                    {
                        tbxArea_1_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_1_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_1_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_1_Y2.Text = (points[1].Y / 2).ToString();
                    }else if (tbxArea_2_X1.Enabled)
                    {
                        tbxArea_2_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_2_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_2_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_2_Y2.Text = (points[1].Y / 2).ToString();
                    }
                    else if (tbxArea_3_X1.Enabled)
                    {
                        tbxArea_3_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_3_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_3_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_3_Y2.Text = (points[1].Y / 2).ToString();
                    }
                    else if (tbxArea_4_X1.Enabled)
                    {
                        tbxArea_4_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_4_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_4_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_4_Y2.Text = (points[1].Y / 2).ToString();
                    }
                }
                else if (IsSet_Line)
                {
                    points[1] = new Point(e.X, e.Y);
                    graphicsDraw.Clear(Color.Transparent);
                    graphicsDraw.DrawLine(pen, points[0].X, points[0].Y, points[1].X, points[1].Y);
                    pbxBitmap.Image = bitmapDraw;
                    if (tbxLine_1_X1.Enabled)
                    {
                        tbxLine_1_X1.Text = (points[0].X / 2).ToString();
                        tbxLine_1_Y1.Text = (points[0].Y / 2).ToString();
                        tbxLine_1_X2.Text = (points[1].X / 2).ToString();
                        tbxLine_1_Y2.Text = (points[1].Y / 2).ToString();
                        tbxLine_1_X3.Text = ((points[0].X + points[1].X) / 4).ToString();
                        tbxLine_1_Y3.Text = ((points[0].Y + points[1].Y) / 4).ToString();
                    }
                }
            }
        }

        public Point[] Draw_Cross(Point point)//计算十字架的点坐标
        {
            Point[] points = new Point[8];
            Point temp_Point1 = new Point(point.X - 35, point.Y);
            Point temp_Point2 = new Point(point.X - 5, point.Y);
            Point temp_Point3 = new Point(point.X + 5, point.Y);
            Point temp_Point4 = new Point(point.X + 35, point.Y);
            Point temp_Point5 = new Point(point.X, point.Y - 5);
            Point temp_Point6 = new Point(point.X, point.Y - 35);
            Point temp_Point7 = new Point(point.X, point.Y + 5);
            Point temp_Point8 = new Point(point.X, point.Y + 35);
            points[0] = temp_Point1;
            points[1] = temp_Point2;
            points[2] = temp_Point3;
            points[3] = temp_Point4;
            points[4] = temp_Point5;
            points[5] = temp_Point6;
            points[6] = temp_Point7;
            points[7] = temp_Point8;
            return points;
        }

        private void PbxBitmap_MouseDown(object sender, MouseEventArgs e)//鼠标按下事件
        {
            if (IsSet_Area || IsSet_Line || IsSet_Spot)
            {
                IsPbxMouseDown = true;
                points[0] = new Point(e.X, e.Y);
            }
            if (IsSet_Spot)
            {
                pointss = Draw_Cross(e.Location);//
                graphicsDraw.Clear(Color.Transparent);//
                for (int i = 0; i < pointss.Length; i = i + 2)
                {
                    graphicsDraw.DrawLine(pen, pointss[i], pointss[i + 1]);
                }
                pbxBitmap.Image = bitmapDraw;
                if (tbxSpot_1_X.Enabled)
                {
                    tbxSpot_1_X.Text = (e.X / 2).ToString();
                    tbxSpot_1_Y.Text = (e.Y / 2).ToString();
                }else if (tbxSpot_2_X.Enabled)
                {
                    tbxSpot_2_X.Text = (e.X / 2).ToString();
                    tbxSpot_2_Y.Text = (e.Y / 2).ToString();
                }
                else if (tbxSpot_3_X.Enabled)
                {
                    tbxSpot_3_X.Text = (e.X / 2).ToString();
                    tbxSpot_3_Y.Text = (e.Y / 2).ToString();
                }
                else if (tbxSpot_4_X.Enabled)
                {
                    tbxSpot_4_X.Text = (e.X / 2).ToString();
                    tbxSpot_4_Y.Text = (e.Y / 2).ToString();
                }
            }
        }

        private void PbxBitmap_MouseLeave(object sender, EventArgs e)//鼠标离开事件
        {
            this.Cursor = Cursors.Default;
        }

        private void PbxBitmap_MouseEnter(object sender, EventArgs e)//鼠标进入时间
        {
            if (IsSet_Area || IsSet_Spot || IsSet_Line)
            {
                this.Cursor = Cursors.Cross;
            }
        }
        #endregion

        #region//清除测温目标事件
        private void btnClear_Spot_1_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_1.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.Temper_Connect, 2);
                sqlCreate.Delete_Spot(cameraId, "S2");
                tbxSpot_1_X.Text = "0";
                tbxSpot_1_Y.Text = "0";
            }
            else if(btnClear_Spot_1.Text == "取消")
            {
                Cancel_SetSpot("S2", btnAdd_Spot_1, tbxSpot_1_X, tbxSpot_1_Y, tbxSpot_1_Emiss, btnClear_Spot_1);
            }
        }

        private void btnClear_Spot_2_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_2.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.Temper_Connect, 3);
                sqlCreate.Delete_Spot(cameraId, "S3");
                tbxSpot_2_X.Text = "0";
                tbxSpot_2_Y.Text = "0";
            }
            else if (btnClear_Spot_2.Text == "取消")
            {
                Cancel_SetSpot("S3", btnAdd_Spot_2, tbxSpot_2_X, tbxSpot_2_Y, tbxSpot_2_Emiss, btnClear_Spot_2);
            }
        }

        private void btnClear_Spot_3_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_3.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.Temper_Connect, 4);
                sqlCreate.Delete_Spot(cameraId, "S4");
                tbxSpot_3_X.Text = "0";
                tbxSpot_3_Y.Text = "0";
            }
            else if (btnClear_Spot_3.Text == "取消")
            {
                Cancel_SetSpot("S4", btnAdd_Spot_3, tbxSpot_3_X, tbxSpot_3_Y, tbxSpot_3_Emiss, btnClear_Spot_3);
            }
        }

        private void btnClear_Spot_4_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_4.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.Temper_Connect, 5);
                sqlCreate.Delete_Spot(cameraId, "S5");
                tbxSpot_4_X.Text = "0";
                tbxSpot_4_Y.Text = "0";
            }
            else if (btnClear_Spot_4.Text == "取消")
            {
                Cancel_SetSpot("S5", btnAdd_Spot_4, tbxSpot_4_X, tbxSpot_4_Y, tbxSpot_4_Emiss, btnClear_Spot_4);
            }
        }

        private void btnClear_Area_1_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_1.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.Temper_Connect, 6);
                sqlCreate.Delete_Area(cameraId, "A6");
                tbxArea_1_X1.Text = "0";
                tbxArea_1_X2.Text = "0";
                tbxArea_1_Y1.Text = "0";
                tbxArea_1_Y2.Text = "0";
            }
            else if (btnClear_Area_1.Text == "取消"){
                Cancel_SetArea("A6",btnAdd_Area_1, tbxArea_1_X1, tbxArea_1_Y1, tbxArea_1_X2, tbxArea_1_Y2, tbxArea_1_Emiss, cbxMeasureType_1, btnClear_Area_1);
            }
        }

        private void btnClear_Area_2_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_2.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.Temper_Connect, 7);
                sqlCreate.Delete_Area(cameraId, "A7");
                tbxArea_2_X1.Text = "0";
                tbxArea_2_X2.Text = "0";
                tbxArea_2_Y1.Text = "0";
                tbxArea_2_Y2.Text = "0";
            }
            else if (btnClear_Area_2.Text == "取消")
            {
                Cancel_SetArea("A7",btnAdd_Area_2, tbxArea_2_X1, tbxArea_2_Y1, tbxArea_2_X2, tbxArea_2_Y2, tbxArea_2_Emiss, cbxMeasureType_2, btnClear_Area_2);
            }
        }

        private void btnClear_Area_3_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_3.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.Temper_Connect, 8);
                sqlCreate.Delete_Area(cameraId, "A8");
                tbxArea_3_X1.Text = "0";
                tbxArea_3_X2.Text = "0";
                tbxArea_3_Y1.Text = "0";
                tbxArea_3_Y2.Text = "0";
            }
            else if (btnClear_Area_3.Text == "取消")
            {
                Cancel_SetArea("A8",btnAdd_Area_3, tbxArea_3_X1, tbxArea_3_Y1, tbxArea_3_X2, tbxArea_3_Y2, tbxArea_3_Emiss, cbxMeasureType_3, btnClear_Area_3);
            }
        }

        private void btnClear_Area_4_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_4.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.Temper_Connect, 9);
                sqlCreate.Delete_Area(cameraId, "A9");
                tbxArea_4_X1.Text = "0";
                tbxArea_4_X2.Text = "0";
                tbxArea_4_Y1.Text = "0";
                tbxArea_4_Y2.Text = "0";
            }
            else if (btnClear_Area_4.Text == "取消")
            {
                Cancel_SetArea("A9",btnAdd_Area_4, tbxArea_4_X1, tbxArea_4_Y1, tbxArea_4_X2, tbxArea_4_Y2, tbxArea_4_Emiss, cbxMeasureType_4, btnClear_Area_4);
            }
        }

        private void btnClaer_Line_1_Click(object sender, EventArgs e)
        {
            if (btnClear_Line_1.Text == "清除")
            {
                DMSDK.DM_ClearLine(StaticClass.Temper_Connect, 1);
                sqlCreate.Delete_Line(cameraId, "L1");
                tbxLine_1_X1.Text = "0";
                tbxLine_1_Y1.Text = "0";
                tbxLine_1_X2.Text = "0";
                tbxLine_1_Y2.Text = "0";
                tbxLine_1_X3.Text = "0";
                tbxLine_1_Y3.Text = "0";
            }
            else if (btnClear_Line_1.Text == "取消")
            {
                Cancel_SetLine("L1",btnAdd_Line_1, tbxLine_1_X1, tbxLine_1_Y1, tbxLine_1_X2, tbxLine_1_Y2, tbxLine_1_X3, tbxLine_1_Y3, tbxLine_1_Emiss, btnClear_Line_1);
            }
        }





        #endregion

        #endregion

        private void btnClearAll_Click(object sender, EventArgs e)//全部清除 测温目标
        {
            DMSDK.DM_ClearAllArea(StaticClass.Temper_Connect);
            DMSDK.DM_ClearAllLine(StaticClass.Temper_Connect);
            DMSDK.DM_ClearAllSpot(StaticClass.Temper_Connect);
        }
    }
}
