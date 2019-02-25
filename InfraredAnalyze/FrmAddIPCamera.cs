using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace InfraredAnalyze
{
    public partial class FrmAddIPCamera : Form
    {
        public FrmAddIPCamera()
        {
            InitializeComponent();
        }

        SqlCreate sqlCreate = new SqlCreate();
        private int num;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        Point point;

        public int Num { get => num; set => num = value; }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        private void tbxCameraName_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[\u4e00-\u9fa5_a-zA-Z0-9]+$");
            Match match = regex.Match(tbxCameraName.Text);
            if(!match.Success)
            {
                MessageBox.Show("请输入合法的名称！");
                tbxCameraName.Focus();
                tbxCameraName.SelectAll();
            }        
        }

        private void tbxRemarks_Leave(object sender, EventArgs e)
        {
            if(tbxRemarks.Text=="")
            {
                tbxRemarks.Text = "无";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)//确认添加
        {
            try
            {
                string CameraName = tbxCameraName.Text;
                int CameraId = num + 1;
                string IPAddress = ipAddressText.IPAdd.ToString();
                int Port = 5000;
                int NodeID = num + 1;
                string Remarks = tbxRemarks.Text;
                bool Enable = false;
                if (rdbEnable.Checked == true)
                {
                    Enable = true;
                }
                sqlCreate.Insert_SMInfraredAnalyze(CameraId, CameraName, IPAddress, Port, NodeID, Remarks, Enable);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmAddIPCamera_Load(object sender, EventArgs e)
        {
            int count = num + 1;
            label7.Text = "(编号：" + count + ")";
            tbxCameraName.Text = "探测器" + count;
        }
    }
}
