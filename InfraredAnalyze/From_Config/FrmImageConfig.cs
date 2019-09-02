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
            if (StaticClass.tempConnect < 0)
            {
                MessageBox.Show("连接失败，请重新连接！");
                foreach (Control control in this.Controls)
                {
                    control.Enabled = false;
                }
                return;
            }
            Init_Param();
        }

        private void Init_Param()
        {
            cbxVideoOutType.SelectedIndex = DMSDK.DM_GetVideoOutType(StaticClass.tempConnect);//获取视频的输出格式
            cbxVideoMode.SelectedIndex = DMSDK.DM_GetVideoMode(StaticClass.tempConnect);//获取图像手动自动模式
            cbxTempOnImage.SelectedIndex = DMSDK.DM_GetTempValueOnImageStatus(StaticClass.tempConnect);//仪器否显示测温数据
            //tbxISOTemp.Text = DMSDK.DM_GetISOTemp(StaticClass.tempConnect).ToString();//等温温度
            //tbxISOHight.Text = DMSDK.DM_GetISOHight(StaticClass.tempConnect).ToString();//等温高度
            //cbxISOColor.SelectedIndex = DMSDK.DM_GetISOColor(StaticClass.tempConnect);//等温色号
            cbxTempUnit.SelectedIndex = DMSDK.DM_GetTempUnit(StaticClass.tempConnect);//温度单位
            cbxPallette.SelectedIndex = DMSDK.DM_GetPallette(StaticClass.tempConnect);//获取伪彩色色标
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DMSDK.DM_SetVideoOutType(StaticClass.tempConnect, cbxVideoOutType.SelectedIndex);//设置视频输出格式
            if (cbxVideoMode.SelectedIndex == 0)//手动
            {
                DMSDK.DM_SetVideoMode(StaticClass.tempConnect, 0);
            }
            if (cbxVideoMode.SelectedIndex == 1)//自动
            {
                DMSDK.DM_SetVideoMode(StaticClass.tempConnect, 2);
            }
            DMSDK.DM_ShowTempValueOnImage(StaticClass.tempConnect, Convert.ToBoolean(cbxTempOnImage.SelectedIndex));//是否显示测温数据
            //DMSDK.DM_SetISOTemp(StaticClass.tempConnect, Convert.ToInt32(tbxISOTemp.Text));//等温温度
            //DMSDK.DM_SetISOHight(StaticClass.tempConnect, Convert.ToInt32(tbxISOHight.Text));//等温高度
            //DMSDK.DM_SetISOColor(StaticClass.tempConnect, cbxISOColor.SelectedIndex);//等温色号
            DMSDK.DM_SetTempUnit(StaticClass.tempConnect, cbxTempUnit.SelectedIndex);//温度单位
            DMSDK.DM_SetPallette(StaticClass.tempConnect, cbxPallette.SelectedIndex, false);//色标
            MessageBox.Show("设置完成！");
            Init_Param();
        }

        private void FrmImageConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

       
    }
}
