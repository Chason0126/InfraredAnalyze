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
    public partial class FrmSelectCamera : Form
    {
        public FrmSelectCamera(List<StructClass.StructIAnalyzeConfig> List, int CameraID)
        {
            InitializeComponent();
            list = new List<StructClass.StructIAnalyzeConfig>();
            list = List;
            cameraid = CameraID;
        }


        List<StructClass.StructIAnalyzeConfig> list;
        int cameraid;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Dispose();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbxCameraName.Text == "")
            {
                MessageBox.Show("请选择探测器！");
                return;
            }
            else
            {
                foreach (StructClass.StructIAnalyzeConfig structIAnalyzeConfig in list)
                {
                    if (cbxCameraName.Text == structIAnalyzeConfig.CameraName)
                    {
                        StaticClass.Temper_Ip = structIAnalyzeConfig.IP;
                        StaticClass.Temper_CameraId = structIAnalyzeConfig.CameraID;
                        StaticClass.Temper_CameraName = structIAnalyzeConfig.CameraName;
                        StaticClass.Temper_IsEnanle = structIAnalyzeConfig.Enable;
                        break;
                    }
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void FrmSelectCamera_Load(object sender, EventArgs e)
        {
            foreach (StructClass.StructIAnalyzeConfig structIAnalyzeConfig in list)
            {
                cbxCameraName.Items.Add(structIAnalyzeConfig.CameraName);
            }
            if (cbxCameraName.Items.Count > 0)
            {
                cbxCameraName.SelectedIndex = cameraid - 1;
            }
        }
    }
}
