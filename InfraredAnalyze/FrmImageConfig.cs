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
    public partial class FrmImageConfig : Form
    {
        public FrmImageConfig()
        {
            InitializeComponent();
        }

        private void FrmImageConfig_Load(object sender, EventArgs e)
        {
            Init_Param();
        }

        private void Init_Param()
        {
            cbxVideoOutType.SelectedIndex = DMSDK.DM_GetVideoOutType(StaticClass.Temper_Connect);//获取视频的输出格式
            cbxVideoMode.SelectedIndex = DMSDK.DM_GetVideoMode(StaticClass.Temper_Connect);//获取图像手动自动模式
            cbxTempOnImage.SelectedIndex = DMSDK.DM_GetTempValueOnImageStatus(StaticClass.Temper_Connect);//仪器否显示测温数据
            //tbxISOTemp.Text = DMSDK.DM_GetISOTemp(StaticClass.Temper_Connect).ToString();//等温温度
            //tbxISOHight.Text = DMSDK.DM_GetISOHight(StaticClass.Temper_Connect).ToString();//等温高度
            //cbxISOColor.SelectedIndex = DMSDK.DM_GetISOColor(StaticClass.Temper_Connect);//等温色号
            cbxTempUnit.SelectedIndex = DMSDK.DM_GetTempUnit(StaticClass.Temper_Connect);//温度单位
            cbxPallette.SelectedIndex = DMSDK.DM_GetPallette(StaticClass.Temper_Connect);//获取伪彩色色标
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DMSDK.DM_SetVideoOutType(StaticClass.Temper_Connect, cbxVideoOutType.SelectedIndex);//设置视频输出格式
            if (cbxVideoMode.SelectedIndex == 0)//手动
            {
                DMSDK.DM_SetVideoMode(StaticClass.Temper_Connect, 0);
            }
            if (cbxVideoMode.SelectedIndex == 1)//自动
            {
                DMSDK.DM_SetVideoMode(StaticClass.Temper_Connect, 2);
            }
            DMSDK.DM_ShowTempValueOnImage(StaticClass.Temper_Connect, Convert.ToBoolean(cbxTempOnImage.SelectedIndex));//是否显示测温数据
            //DMSDK.DM_SetISOTemp(StaticClass.Temper_Connect, Convert.ToInt32(tbxISOTemp.Text));//等温温度
            //DMSDK.DM_SetISOHight(StaticClass.Temper_Connect, Convert.ToInt32(tbxISOHight.Text));//等温高度
            //DMSDK.DM_SetISOColor(StaticClass.Temper_Connect, cbxISOColor.SelectedIndex);//等温色号
            DMSDK.DM_SetTempUnit(StaticClass.Temper_Connect, cbxTempUnit.SelectedIndex);//温度单位
            DMSDK.DM_SetPallette(StaticClass.Temper_Connect, cbxPallette.SelectedIndex, false);//色标
            MessageBox.Show("设置完成！");
            Init_Param();
        }
    }
}
