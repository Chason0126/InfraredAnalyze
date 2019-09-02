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
    public partial class FrmPwd : Form
    {
        public FrmPwd()
        {
            InitializeComponent();
        }

        private int pwdLevel;

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

        public string Pwd { get => tbxPwd.Text; }//只能Get
        public int PwdLevel { get => pwdLevel; set => pwdLevel = value; }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        List<StructClass.StructPwd> structPwds = new List<StructClass.StructPwd>();
        private void GetPwd()
        {
            SqlCreate sqlCreate = new SqlCreate();
            structPwds = sqlCreate.Select_Pwd(StaticClass.DataBaseName);
        }

        private void FrmPwd_Load(object sender, EventArgs e)
        {
            if (pwdLevel != -1)
            {
                GetPwd();
                if (pwdLevel == 1)
                {
                    lblPwdLevel.Text = "权限等级：系统管理员";
                }
                if (pwdLevel == 2)
                {
                    lblPwdLevel.Text = "权限等级：管理员";
                }
                if (pwdLevel == 3 || pwdLevel == 0)
                {
                    lblPwdLevel.Text = "权限等级：普通用户";
                }
            }else if (pwdLevel == -1)
            {
                lblPwdLevel.Text = "权限等级：系统管理员";
            }
            tbxPwd.Focus();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (pwdLevel == 1 && tbxPwd.Text == structPwds[0].pwd)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (pwdLevel == 2 && (tbxPwd.Text == structPwds[0].pwd || tbxPwd.Text == structPwds[1].pwd))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if ((pwdLevel == 3 || pwdLevel == 0) && (tbxPwd.Text == structPwds[0].pwd || tbxPwd.Text == structPwds[1].pwd || tbxPwd.Text == structPwds[2].pwd))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (pwdLevel == -1 && tbxPwd.Text == "adsensor")//缺省状态
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("密码错误！") == DialogResult.OK)
                    {
                        tbxPwd.Focus();
                        tbxPwd.SelectAll();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbxPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirm.PerformClick();
            }
        }
    }
}
