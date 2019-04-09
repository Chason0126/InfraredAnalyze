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
        }

        decimal RefeTemp;
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
            cbxMeasureClass.SelectedIndex = DMSDK.DM_GetMeasureClass(StaticClass.Temper_Connect) - 1;//测温档位

            cbxRefeType.SelectedIndex = DMSDK.DM_GetRefeType(StaticClass.Temper_Connect);//参考温度类型

            RefeTemp = (IntToDecimals(DMSDK.DM_GetRefeTemp(StaticClass.Temper_Connect)) / 100);//自定义参考温度的值

            tbxAmbientTemp.Text = (IntToDecimals(DMSDK.DM_GetAmbientTemp(StaticClass.Temper_Connect)) / 100).ToString();//环境温度

            tbxObjDistance.Text = (IntToDecimals(DMSDK.DM_GetObjDistance(StaticClass.Temper_Connect)) / 100).ToString();//环境距离

            tbxAmbientHumidity.Text = (IntToDecimals(DMSDK.DM_GetAmbientHumidity(StaticClass.Temper_Connect)) / 100).ToString();//环境湿度

            tbxReviseParam.Text = (IntToDecimals(DMSDK.DM_GetReviseParam(StaticClass.Temper_Connect)) / 100).ToString();//修正系数

            tbxReviseTemp.Text = (IntToDecimals(DMSDK.DM_GetReviseTemp(StaticClass.Temper_Connect)) / 100).ToString();//修正温度
        }

        private void FrmTemperParamConfig_Load(object sender, EventArgs e)
        {
            Init_Param();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DMSDK.DM_SetMeasureClass(StaticClass.Temper_Connect, cbxMeasureClass.SelectedIndex + 1);//设置测温档位
            DMSDK.DM_SetRefeType(StaticClass.Temper_Connect, cbxRefeType.SelectedIndex);//设置参考温度类型
            if (cbxRefeType.SelectedIndex == 1)
            {
                DMSDK.DM_SetRefeTemp(StaticClass.Temper_Connect,(int)(Convert.ToDecimal(tbxRefeTemp.Text) * 100));//自定义温度时
            }
            DMSDK.DM_SetAmbientTemp(StaticClass.Temper_Connect, (int)(Convert.ToDecimal(tbxAmbientTemp.Text) * 100));//设置环境温度
            DMSDK.DM_SetObjDistance(StaticClass.Temper_Connect, (int)(Convert.ToDecimal(tbxObjDistance.Text)*100));//设置环境距离
            DMSDK.DM_SetAmbientHumidity(StaticClass.Temper_Connect, (int)(Convert.ToDecimal(tbxAmbientHumidity.Text) * 100));//设置环境湿度
            DMSDK.DM_SetReviseParam(StaticClass.Temper_Connect, (int)(Convert.ToDecimal(tbxReviseParam.Text) * 100));//修正系数
            DMSDK.DM_SetReviseTemp(StaticClass.Temper_Connect, (int)(Convert.ToDecimal(tbxReviseTemp.Text) * 100));//修正温度
            Init_Param();
            MessageBox.Show("修改完成！");
        }

        private decimal IntToDecimals(int temper) => Math.Round(Convert.ToDecimal(temper), 2);

       
    }
}
