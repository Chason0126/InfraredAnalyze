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
    public partial class FrmAlarmConfig : Form
    {
        public FrmAlarmConfig()
        {
            InitializeComponent();
        }

        private void FrmAlarmConfig_Load(object sender, EventArgs e)
        {
            worker.WorkerSupportsCancellation = true;
            Init_Param();
        }

        int AlarmPower;
        int AlarmType;
        int AlarmTemp;
        int AlarmColorID;
        int AlarmMessageType;
        private void Init_Param()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 4; j++)
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

        SqlCreate sqlCreate = new SqlCreate();
        FrmIsRunning isRunning;
        BackgroundWorker worker = new BackgroundWorker();
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            isRunning = new FrmIsRunning(worker);
            worker.DoWork += new DoWorkEventHandler(showIsRunning);
            worker.RunWorkerAsync();
            isRunning.ShowDialog();
            MessageBox.Show("设置完成！");
        }

        private void showIsRunning(object sender,DoWorkEventArgs e)
        {
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
                            sqlCreate.Update_Alarmconfig(StaticClass.Temper_CameraId, "S" + (j + 1), AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower));//向数据库写入 告警设置信息 供 判断告警时使用
                            break;
                        case 2://设置区域温度告警
                            AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeArea_" + j + "", false)[0])).SelectedIndex;
                            AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempArea_" + j + "", false)[0])).Text) * 100;
                            AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorArea_" + j + "", false)[0])).SelectedIndex;
                            AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerArea_" + j + "", false)[0])).SelectedIndex;
                            AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeArea_" + j + "", false)[0])).SelectedIndex;
                            DMSDK.DM_SetAlarmInfo(StaticClass.Temper_Connect, i, j + 5, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测区域 编号 +5 从6开始算起
                            sqlCreate.Update_Alarmconfig(StaticClass.Temper_CameraId, "A" + (j + 5), AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower));
                            break;
                        case 1://设置线 温度告警
                            AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeLine_1", false)[0])).SelectedIndex;
                            AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempLine_1", false)[0])).Text) * 100;
                            AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorLine_1", false)[0])).SelectedIndex;
                            AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerLine_1", false)[0])).SelectedIndex;
                            AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeLine_1", false)[0])).SelectedIndex;
                            DMSDK.DM_SetAlarmInfo(StaticClass.Temper_Connect, i, j, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测温线  仅一条
                            sqlCreate.Update_Alarmconfig(StaticClass.Temper_CameraId, "L1", AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower));
                            break;
                    }
                }
            }
            Init_Param();
        }
    }
}
