using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmPwdManage : Form
    {
        public FrmPwdManage()
        {
            InitializeComponent();
        }

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

        private void tbxSystemPwd_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9]+$");
            Match match = regex.Match(tbxAdminPwd.Text);
            if (!match.Success)
            {
                MessageBox.Show("只能包含数字与字母！请重新输入！");
                tbxAdminPwd.Focus();
                tbxAdminPwd.SelectAll();
            }
        }

        private void tbxUserPwd_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9]+$");
            Match match = regex.Match(tbxUserPwd.Text);
            if (!match.Success)
            {
                MessageBox.Show("只能包含数字与字母！请重新输入！");
                tbxUserPwd.Focus();
                tbxUserPwd.SelectAll();
            }
        }

        SqlCreate sqlCreate = new SqlCreate();
        List<StructClass.StructPwd> structPwds = new List<StructClass.StructPwd>();
        private void GetPwd()
        {
            try
            {
                structPwds = sqlCreate.Select_Pwd();
                tbxAdminPwd.Text = structPwds[1].pwd;
                tbxUserPwd.Text = structPwds[2].pwd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        private void FrmPwdManage_Load(object sender, EventArgs e)
        {
            GetPwd();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCreate.Update_Pwd(1, tbxAdminPwd.Text);
                sqlCreate.Update_Pwd(2, tbxUserPwd.Text);
                MessageBox.Show("修改成功！");
                GetPwd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
