using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            try
            {
                DateTime dateTime = Convert.ToDateTime(DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作系统日期时间格式异常，请先重置时间格式后重新打开软件！" + ex.Message + ex.StackTrace);
                Environment.Exit(0);
            }
        }

        #region//拖动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Frm_Move()
        {
            int WM_SYSCOMMAND = 0x0112;
            int SC_MOVE = 0xF010;
            int HTCAPTION = 0x0002;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void tabPage1_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void tabPage2_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnConfig_Click(object sender, EventArgs e)//设置按钮
        {
            if (tabMain.SelectedTab == tabPage1)
            {
                tabMain.SelectedTab = tabPage2;
            }
            else
            {
                tabMain.SelectedTab = tabPage1;
            }
        }

        ToolTip toolTip;
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnClose, "退出");
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnMin, "最小化");
            btnMin.BackColor = Color.Yellow;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Transparent;
        }

        private void btnConfig_MouseEnter(object sender, EventArgs e)
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnConfig, "设置");
            btnConfig.BackColor = Color.Green;
        }

        private void btnConfig_MouseLeave(object sender, EventArgs e)
        {
            btnConfig.BackColor = Color.Transparent;
        }

        Point point;
        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point tempPoint = MousePosition;
                tempPoint.Offset(-point.X, -point.Y);
                this.Location = tempPoint;
            }
        }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
            Frm_Move();
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

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            //btnLogin.BackColor = Color.Yellow;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.WhiteSmoke;
        }

        SqlCreate sqlCreate = new SqlCreate();

        private void cbxProjName_Init()
        {
            cbxProjName.Items.Clear();
            cbxProjName.Text = "";
            StaticClass.List_SMInfrared_Config = sqlCreate.Select_SMInfrared_Config();//获取所有项目名称 16个
            foreach (StructClass.StructSMInfrared_Config structSMInfrared_Config in StaticClass.List_SMInfrared_Config)
            {
                if (structSMInfrared_Config.Enable)
                {
                    cbxProjName.Items.Add(structSMInfrared_Config.ProjName);
                }
            }
        }

        private void tvwProj_Init()
        {
            tvwProj.Nodes.Clear();
            StaticClass.List_SMInfrared_Config = sqlCreate.Select_SMInfrared_Config();
            foreach (StructClass.StructSMInfrared_Config structSMInfrared_Config in StaticClass.List_SMInfrared_Config)
            {
                if (structSMInfrared_Config.Enable)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = structSMInfrared_Config.ProjName;
                    tvwProj.Nodes.Add(treeNode);
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            tabMain.Region = new Region(new RectangleF(tabPage1.Left, tabPage1.Top, tabPage1.Width, tabPage1.Height));//把上面隐藏
            cbxProjName_Init();
            tvwProj_Init();
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPage1)
            {
                cbxProjName_Init();
            }else if(tabMain.SelectedTab == tabPage2)
            {
                tvwProj_Init();
            }
        }

        private void btnAddProj_Click(object sender, EventArgs e)//新增项目 
        {
            if (tvwProj.Nodes.Count == 16)
            {
                MessageBox.Show("项目数量已达上限，最多支持16个项目！");
                return;
            }
            StaticClass.List_SMInfrared_Config = sqlCreate.Select_SMInfrared_Config();
            for(int i = 0; i <= 16; i++)
            {
                StructClass.StructSMInfrared_Config structSMInfrared_Config = StaticClass.List_SMInfrared_Config[i];
                if (structSMInfrared_Config.Enable == false)
                {
                    sqlCreate.UpDate_SMInfrared_Config(structSMInfrared_Config.ProjName, structSMInfrared_Config.DataBaseName, true);
                    break;
                }
            }
            cbxProjName_Init();
            tvwProj_Init();
        }

        private bool Match_ProjName_Leave()//正则表达式
        {
            bool revalue = true;
            Regex regex = new Regex(@"^[\u4e00-\u9fa5_a-zA-Z0-9]+$");
            Match match = regex.Match(tbxProjName.Text);
            if (!match.Success)
            {
                MessageBox.Show("请输入合法的名称,只包含（汉字、英文、数字、_）！");
                revalue = false;
                tbxProjName.Focus();
                tbxProjName.SelectAll();
            }
            foreach(TreeNode treeNode in tvwProj.Nodes)
            {
                if(treeNode.Text== tbxProjName.Text)
                {
                    MessageBox.Show("名称重复！");
                    revalue = false;
                    tbxProjName.Focus();
                    tbxProjName.SelectAll();
                }
            }
            return revalue;
        }

        private void btnRename_Click(object sender, EventArgs e)//重命名
        {
            try
            {
                if (SelectNode_Index == -1)
                {
                    MessageBox.Show("请点击选择需要设置的项目！");
                    tvwProj.Enabled = true;
                    btnConfig.Enabled = true;
                    btnAddProj.Enabled = true;
                    btnDelProj.Enabled = true;
                    btnCancelReName.Visible = false; ;
                    return;
                }
                StaticClass.List_SMInfrared_Config = sqlCreate.Select_SMInfrared_Config();
                StructClass.StructSMInfrared_Config structSMInfrared_Config = StaticClass.List_SMInfrared_Config[SelectNode_Index];
                if (btnRename.Text == "重命名")
                {
                    tbxProjName.Text = tvwProj.SelectedNode.Text;
                    tbxProjName.Visible = true;
                    btnRename.Text = "确认";

                    btnCancelReName.Visible = true;
                    tvwProj.Enabled = false;
                    btnConfig.Enabled = false;
                    btnAddProj.Enabled = false;
                    btnDelProj.Enabled = false;
                }
                else if (btnRename.Text == "确认")
                {
                    if (!Match_ProjName_Leave())
                    {
                        return;
                    }
                    sqlCreate.UpDate_SMInfrared_Config(tbxProjName.Text, structSMInfrared_Config.DataBaseName, true);
                    tbxProjName.Text = "";
                    tbxProjName.Visible = false;
                    btnRename.Text = "重命名";
                    btnCancelReName.Visible = false;
                    tvwProj.Enabled = true;
                    btnConfig.Enabled = true;
                    btnAddProj.Enabled = true;
                    btnDelProj.Enabled = true;
                }
                cbxProjName_Init();
                tvwProj_Init();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int SelectNode_Index = -1;
        private void tvwProj_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                tvwProj.SelectedNode = tvwProj.GetNodeAt(e.X, e.Y);
                if (tvwProj.SelectedNode != null)
                {
                    SelectNode_Index = tvwProj.SelectedNode.Index;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelProj_Click(object sender, EventArgs e)
        {
            if (tvwProj.SelectedNode == null)
            {
                MessageBox.Show("请点击选择需要设置的项目！");
                return;
            }
            FrmPwd frmPwd = new FrmPwd();
            frmPwd.PwdLevel = -1;
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                StaticClass.List_SMInfrared_Config = sqlCreate.Select_SMInfrared_Config();
                foreach (StructClass.StructSMInfrared_Config structSMInfrared_Config in StaticClass.List_SMInfrared_Config)
                {
                    if (structSMInfrared_Config.ProjName == tvwProj.SelectedNode.Text)
                    {
                        sqlCreate.UpDate_SMInfrared_Config("项目" + structSMInfrared_Config.ProjId.ToString(), structSMInfrared_Config.DataBaseName, false);
                        break;
                    }
                }
            }
            cbxProjName_Init();
            tvwProj_Init();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCreate = new SqlCreate();
                if (cbxProjName.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择项目！");
                    return;
                }
                StaticClass.List_SMInfrared_Config = sqlCreate.Select_SMInfrared_Config();
                foreach (StructClass.StructSMInfrared_Config structSMInfrared_Config in StaticClass.List_SMInfrared_Config)
                {
                    if (structSMInfrared_Config.ProjName == cbxProjName.Text)
                    {
                        StaticClass.ProjName = cbxProjName.Text;
                        StaticClass.DataBaseName = structSMInfrared_Config.DataBaseName;
                    }
                }
                if (StaticClass.ProjName == null || StaticClass.DataBaseName == null)
                {
                    MessageBox.Show("系统登陆异常！");
                    return;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelReName_Click(object sender, EventArgs e)//取消
        {
            tbxProjName.Text = "";
            tbxProjName.Visible = false;
            btnRename.Text = "重命名";
            btnCancelReName.Visible = false;
            tvwProj.Enabled = true;
            btnConfig.Enabled = true;
            btnAddProj.Enabled = true;
            btnDelProj.Enabled = true;
            SelectNode_Index = -1;
        }

    }
}
