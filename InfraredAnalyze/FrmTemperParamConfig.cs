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
    public partial class FrmTemperParamConfig : Form
    {
        public FrmTemperParamConfig()
        {
            InitializeComponent();
            DMSDK.DM_Init();
            this.Disposed += FrmTemperParamConfig_Disposed;
        }

      

        decimal RefeTemp;
        int tempConnect;
        private void cbxRefeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRefeType.SelectedIndex == 1)
            {
                grpRefeTemp.Enabled = true;
                tbxRefeTemp.Text = RefeTemp.ToString();
            }
            else
            {
                grpRefeTemp.Enabled = false;
            }
        }

        private void Init_Param()
        {
            cbxMeasureClass.SelectedIndex = DMSDK.DM_GetMeasureClass(tempConnect) - 1;//测温档位

            cbxRefeType.SelectedIndex = DMSDK.DM_GetRefeType(tempConnect);//参考温度类型

            RefeTemp = (IntToDecimals(DMSDK.DM_GetRefeTemp(tempConnect)) / 100);//自定义参考温度的值

            tbxAmbientTemp.Text = (IntToDecimals(DMSDK.DM_GetAmbientTemp(tempConnect)) / 100).ToString();//环境温度

            tbxObjDistance.Text = (IntToDecimals(DMSDK.DM_GetObjDistance(tempConnect)) / 100).ToString();//环境距离

            tbxAmbientHumidity.Text = (IntToDecimals(DMSDK.DM_GetAmbientHumidity(tempConnect)) / 100).ToString();//环境湿度

            tbxReviseParam.Text = (IntToDecimals(DMSDK.DM_GetReviseParam(tempConnect)) / 100).ToString();//修正系数

            tbxReviseTemp.Text = (IntToDecimals(DMSDK.DM_GetReviseTemp(tempConnect)) / 100).ToString();//修正温度
        }

        private void FrmTemperParamConfig_Load(object sender, EventArgs e)
        {

            tempConnect = DMSDK.DM_Connect(this.Handle, StaticClass.Temper_Ip, 80);//
            if (tempConnect < 0)
            {
                MessageBox.Show("连接失败，请重试！");
                return;
            }
            Init_Param();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DMSDK.DM_SetMeasureClass(tempConnect, cbxMeasureClass.SelectedIndex + 1);//设置测温档位
            DMSDK.DM_SetRefeType(tempConnect, cbxRefeType.SelectedIndex);//设置参考温度类型
            if (cbxRefeType.SelectedIndex == 1)
            {
                DMSDK.DM_SetRefeTemp(tempConnect,(int)(Convert.ToDecimal(tbxRefeTemp.Text) * 100));//自定义温度时
            }
            DMSDK.DM_SetAmbientTemp(tempConnect, (int)(Convert.ToDecimal(tbxAmbientTemp.Text) * 100));//设置环境温度
            DMSDK.DM_SetObjDistance(tempConnect, (int)(Convert.ToDecimal(tbxObjDistance.Text)*100));//设置环境距离
            DMSDK.DM_SetAmbientHumidity(tempConnect, (int)(Convert.ToDecimal(tbxAmbientHumidity.Text) * 100));//设置环境湿度
            DMSDK.DM_SetReviseParam(tempConnect, (int)(Convert.ToDecimal(tbxReviseParam.Text) * 100));//修正系数
            DMSDK.DM_SetReviseTemp(tempConnect, (int)(Convert.ToDecimal(tbxReviseTemp.Text) * 100));//修正温度
            Init_Param();
            MessageBox.Show("修改完成！");
        }

        private decimal IntToDecimals(int temper) => Math.Round(Convert.ToDecimal(temper), 2);

        private void FrmTemperParamConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
        }

        private void FrmTemperParamConfig_Disposed(object sender, EventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
        }
    }
}
