using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        int AlarmPower;
        int AlarmType;
        int AlarmTemp;
        int AlarmColorID;
        int AlarmMessageType;

        private void Init_Param()
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        switch (i)
                        {
                            case 0://点
                                DMSDK.DM_GetAlarmInfo(StaticClass.tempConnect, i, j + 1, out AlarmPower, out AlarmType, out AlarmTemp, out AlarmColorID, out AlarmMessageType);
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeSpot_" + j + "", false)[0])).SelectedIndex = AlarmType;
                                ((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempSpot_" + j + "", false)[0])).Text = (AlarmTemp / 100).ToString();
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorSpot_" + j + "", false)[0])).SelectedIndex = AlarmColorID;
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerSpot_" + j + "", false)[0])).SelectedIndex = AlarmPower;
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeSpot_" + j + "", false)[0])).SelectedIndex = AlarmMessageType;
                                break;
                            case 2://区域
                                DMSDK.DM_GetAlarmInfo(StaticClass.tempConnect, i, j + 5, out AlarmPower, out AlarmType, out AlarmTemp, out AlarmColorID, out AlarmMessageType);
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeArea_" + j + "", false)[0])).SelectedIndex = AlarmType;
                                ((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempArea_" + j + "", false)[0])).Text = (AlarmTemp / 100).ToString();
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorArea_" + j + "", false)[0])).SelectedIndex = AlarmColorID;
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerArea_" + j + "", false)[0])).SelectedIndex = AlarmPower;
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeArea_" + j + "", false)[0])).SelectedIndex = AlarmMessageType;
                                break;
                            case 1://线
                                DMSDK.DM_GetAlarmInfo(StaticClass.tempConnect, i, j, out AlarmPower, out AlarmType, out AlarmTemp, out AlarmColorID, out AlarmMessageType);
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeLine_1", false)[0])).SelectedIndex = AlarmType;
                                ((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempLine_1", false)[0])).Text = (AlarmTemp / 100).ToString();
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorLine_1", false)[0])).SelectedIndex = AlarmColorID;
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerLine_1", false)[0])).SelectedIndex = AlarmPower;
                                ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeLine_1", false)[0])).SelectedIndex = AlarmMessageType;
                                break;
                        }
                    }
                }
                tbxAlarmTime.Text = ConfigurationManager.AppSettings["AlarmCount"];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        SqlCreate sqlCreate = new SqlCreate();
        private void FrmAlarmConfig_Load(object sender, EventArgs e)
        {
            if (StaticClass.tempConnect < 0)
            {
                MessageBox.Show("连接失败，请重新连接探测器！");
                foreach(Control control in  grpAlarmInfo.Controls)
                {
                    control.Enabled = false;
                }
                return;
            }
            Init_Param();
        }

        FrmIsRunning isRunning;
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(showDialog);
            thread.Start();
            showIsRunning();
            thread.Abort();
            MessageBox.Show("设置完成！");
        }

        private void showDialog()
        {
            isRunning = new FrmIsRunning();
            isRunning.ShowDialog();
        }

        private void showIsRunning()
        {
            try
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
                                DMSDK.DM_SetAlarmInfo(StaticClass.tempConnect, i, j + 1, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测温点 编号+1 从1开始算
                                sqlCreate.Update_Alarmconfig(StaticClass.Temper_CameraId, "S" + (j + 1), AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower), StaticClass.DataBaseName);//向数据库写入 告警设置信息 供 判断告警时使用
                                break;
                            case 2://设置区域温度告警
                                AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeArea_" + j + "", false)[0])).SelectedIndex;
                                AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempArea_" + j + "", false)[0])).Text) * 100;
                                AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorArea_" + j + "", false)[0])).SelectedIndex;
                                AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerArea_" + j + "", false)[0])).SelectedIndex;
                                AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeArea_" + j + "", false)[0])).SelectedIndex;
                                DMSDK.DM_SetAlarmInfo(StaticClass.tempConnect, i, j + 5, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测区域 编号 +5 从6开始算起
                                sqlCreate.Update_Alarmconfig(StaticClass.Temper_CameraId, "A" + (j + 5), AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower), StaticClass.DataBaseName);
                                break;
                            case 1://设置线 温度告警
                                AlarmType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmTypeLine_1", false)[0])).SelectedIndex;
                                AlarmTemp = Convert.ToInt32(((TextBox)(grpAlarmInfo.Controls.Find("tbxAlarmTempLine_1", false)[0])).Text) * 100;
                                AlarmColorID = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmColorLine_1", false)[0])).SelectedIndex;
                                AlarmPower = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmPowerLine_1", false)[0])).SelectedIndex;
                                AlarmMessageType = ((ComboBox)(grpAlarmInfo.Controls.Find("cbxAlarmMessageTypeLine_1", false)[0])).SelectedIndex;
                                DMSDK.DM_SetAlarmInfo(StaticClass.tempConnect, i, j, AlarmPower, AlarmType, AlarmTemp, AlarmColorID, AlarmMessageType);//设置测温线  仅一条
                                sqlCreate.Update_Alarmconfig(StaticClass.Temper_CameraId, "L1", AlarmType, AlarmTemp, Convert.ToBoolean(AlarmPower), StaticClass.DataBaseName);
                                break;
                        }
                    }
                }
                Regex regex = new Regex(@"^[1-9]\d*$");
                Match match = regex.Match(tbxAlarmTime.Text);
                if (!match.Success)
                {
                    MessageBox.Show("报警时间必须为正整数，且在1-60之间！");
                    tbxAlarmTime.Focus();
                    return;
                }
                if (Convert.ToInt32(tbxAlarmTime.Text) > 60)
                {
                    MessageBox.Show("报警时间必须为正整数，且在1-60之间！");
                    tbxAlarmTime.Focus();
                    return;
                }

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//保存配置文件中当前屏幕数量
                config.AppSettings.Settings["AlarmCount"].Value = tbxAlarmTime.Text;
                ConfigurationManager.RefreshSection("appSettings");
                config.Save(ConfigurationSaveMode.Modified);

                Thread.Sleep(3000);
                Init_Param();
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmAlarmConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

    }
}
