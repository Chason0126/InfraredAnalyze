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
            DMSDK.DM_Init();
        }

        private int tempConnect;
        private void FrmImageConfig_Load(object sender, EventArgs e)
        {
            tempConnect = DMSDK.DM_Connect(this.Handle, StaticClass.Temper_Ip, 80);//
            if (tempConnect < 0)
            {
                MessageBox.Show("连接失败，请重试！");
                return;
            }
            Init_Param();
        }

        private void Init_Param()
        {
            cbxVideoOutType.SelectedIndex = DMSDK.DM_GetVideoOutType(tempConnect);//获取视频的输出格式
            cbxVideoMode.SelectedIndex = DMSDK.DM_GetVideoMode(tempConnect);//获取图像手动自动模式
            cbxTempOnImage.SelectedIndex = DMSDK.DM_GetTempValueOnImageStatus(tempConnect);//仪器否显示测温数据
            //tbxISOTemp.Text = DMSDK.DM_GetISOTemp(tempConnect).ToString();//等温温度
            //tbxISOHight.Text = DMSDK.DM_GetISOHight(tempConnect).ToString();//等温高度
            //cbxISOColor.SelectedIndex = DMSDK.DM_GetISOColor(tempConnect);//等温色号
            cbxTempUnit.SelectedIndex = DMSDK.DM_GetTempUnit(tempConnect);//温度单位
            cbxPallette.SelectedIndex = DMSDK.DM_GetPallette(tempConnect);//获取伪彩色色标
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DMSDK.DM_SetVideoOutType(tempConnect, cbxVideoOutType.SelectedIndex);//设置视频输出格式
            if (cbxVideoMode.SelectedIndex == 0)//手动
            {
                DMSDK.DM_SetVideoMode(tempConnect, 0);
            }
            if (cbxVideoMode.SelectedIndex == 1)//自动
            {
                DMSDK.DM_SetVideoMode(tempConnect, 2);
            }
            DMSDK.DM_ShowTempValueOnImage(tempConnect, Convert.ToBoolean(cbxTempOnImage.SelectedIndex));//是否显示测温数据
            //DMSDK.DM_SetISOTemp(tempConnect, Convert.ToInt32(tbxISOTemp.Text));//等温温度
            //DMSDK.DM_SetISOHight(tempConnect, Convert.ToInt32(tbxISOHight.Text));//等温高度
            //DMSDK.DM_SetISOColor(tempConnect, cbxISOColor.SelectedIndex);//等温色号
            DMSDK.DM_SetTempUnit(tempConnect, cbxTempUnit.SelectedIndex);//温度单位
            DMSDK.DM_SetPallette(tempConnect, cbxPallette.SelectedIndex, false);//色标
            MessageBox.Show("设置完成！");
            Init_Param();
        }

        private void FrmImageConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            DMSDK.DM_Disconnect(tempConnect);
        }
    }
}
