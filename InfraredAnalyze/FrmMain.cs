using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX.PrivateImplementationDetails;
using System.Configuration;
using System.Collections;

namespace InfraredAnalyze
{

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        SqlCreate sqlCreate = new SqlCreate();
        ScreenBuffer screenBuffer = new ScreenBuffer();
        Drawing drawing = new Drawing();

        #region//窗体右上角按钮功能
        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)//关闭窗体 退出程序
        {
            this.Close();
        }

        private void btnWindow_MouseEnter(object sender, EventArgs e)
        {
            btnWindow.BackColor = Color.Green;
        }

        private void btnWindow_MouseLeave(object sender, EventArgs e)
        {
            btnWindow.BackColor = Color.Transparent;
        }

        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Yellow;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Transparent;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnWindow_Click(object sender, EventArgs e)//窗口全屏、恢复按钮 
        {
            if (this.WindowState==FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnWindow.BackgroundImage = Properties.Resources.窗口化;
            }
            else if (this.WindowState==FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnWindow.BackgroundImage = Properties.Resources.最大化;
            }
        }
        #endregion

        #region//双击头部 变换窗体模式  最大化或者为 窗体化
        private void pnlHeader_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnWindow.BackgroundImage = Properties.Resources.窗口化;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnWindow.BackgroundImage = Properties.Resources.最大化;
            }
        }
        #endregion

        #region//按住头部 拖动窗体
        Point point;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point temp_Point = MousePosition;
                temp_Point.Offset(-point.X, -point.Y);
                this.Location = temp_Point;
            }
        }
        #endregion

        #region//拖动鼠标 改变窗体大小
        public enum MouseDirection
        {
            Herizontal,
            Vertical,
            Declining,
            None
        }

        MouseDirection mouseDirection = new MouseDirection();
        bool IsMouseDown = false;

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            mouseDirection = MouseDirection.None;
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= this.Width - 5 && e.Location.Y > this.Height - 5)
            {
                this.Cursor = Cursors.SizeNWSE;
                mouseDirection = MouseDirection.Declining;
            }
            else if (e.Location.X > this.Width - 5)
            {
                this.Cursor = Cursors.SizeWE;
                mouseDirection = MouseDirection.Herizontal;
            }
            else if (e.Location.Y > this.Height - 5)
            {
                this.Cursor = Cursors.SizeNS;
                mouseDirection = MouseDirection.Vertical;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
            ResizeWindow();
        }

        private void ResizeWindow()
        {
            if (!IsMouseDown)
            {
                return;
            }
            //if (mouseDirection == MouseDirection.Declining)
            //{
            //    this.Cursor = Cursors.SizeNWSE;
            //    this.Width = MousePosition.X - this.Left;
            //    this.Height = MousePosition.Y - this.Top;
            //}
            //if (mouseDirection == MouseDirection.Herizontal)
            //{
            //    this.Cursor = Cursors.SizeWE;
            //    this.Width = MousePosition.X - this.Left;
            //}
            //if (mouseDirection == MouseDirection.Vertical)
            //{
            //    this.Cursor = Cursors.SizeNS;
            //    this.Height = MousePosition.Y - this.Top;
            //}
            //else
            //{
            //    this.Cursor = Cursors.Arrow;
            //}
        }
        #endregion//拖动 鼠标 改变窗体大小

        #region//根据数量 分割主窗体 显示监控画面
        public void Refresh_Screen(int num)
        {
            tlpScreen.Controls.Clear();
            tlpScreen.RowCount = tlpScreen.ColumnCount = num;
            for (int i = 1; i <= num * num; i++)
            {
                UCPictureBox uCPictureBox = FromHandle(screenBuffer.intPtrs_UCpbx[0]) as UCPictureBox;
                uCPictureBox.Height = spcScreen.Panel1.Height / num;
                uCPictureBox.Width = spcScreen.Panel1.Width / num;
                tlpScreen.Controls.Add(uCPictureBox);
                uCPictureBox.Draw_Tag(i.ToString());
            }
        }
        #endregion

        #region//消除控件加载时的闪烁问题  但是会卡住呢
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
        #endregion

        #region//全屏显示
        public void Full_Screen_Display(UCPictureBox pictureBox)
        {
            //tlpScreen.Controls.Clear();
            tlpScreen.RowCount = tlpScreen.ColumnCount = 1;
            pictureBox.Height = spcScreen.Panel1.Height;
            pictureBox.Width = spcScreen.Panel1.Width;
            tlpScreen.Controls.Add(pictureBox);
            pictureBox.Draw_Tag(pictureBox.Number.ToString());
        }
        #endregion

        #region//计算分割的点
        Point[] points;
        /// <summary>
        /// 返回分割好的坐标点数组
        /// </summary>
        /// <param name="num">输入需要分割显示的数量</param>
        /// <returns></returns>
        public Point[] Calc_points(int num)
        {
            switch (num)
            {
                case 1:
                    points = new Point[1];
                    points[0] = new Point(0, 0);
                    break;
                case 4:
                    points = new Point[4];
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, spcScreen.Panel1.Width / 2);
                    points[2] = new Point(spcScreen.Panel1.Height / 2, 0);
                    points[3] = new Point(spcScreen.Panel1.Width / 2, spcScreen.Panel1.Height / 2);
                    break;
                case 9:
                    points = new Point[9];
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, spcScreen.Panel1.Width / 3);
                    points[2] = new Point(0, spcScreen.Panel1.Width / 3 * 2);

                    points[3] = new Point(spcScreen.Panel1.Height / 3, 0);
                    points[4] = new Point(spcScreen.Panel1.Height / 3, spcScreen.Panel1.Width / 3);
                    points[5] = new Point(spcScreen.Panel1.Height / 3, spcScreen.Panel1.Width / 3 * 2);

                    points[6] = new Point(spcScreen.Panel1.Height / 3 * 2, 0);
                    points[7] = new Point(spcScreen.Panel1.Height / 3 * 2, spcScreen.Panel1.Width / 3);
                    points[8] = new Point(spcScreen.Panel1.Height / 3 * 2, spcScreen.Panel1.Width / 3 * 2);
                    break;
                case 16:
                    points = new Point[16];
                    points[0] = new Point(0, 0);
                    points[1] = new Point(0, spcScreen.Panel1.Width / 4);
                    points[2] = new Point(0, spcScreen.Panel1.Width / 4 * 2);
                    points[3] = new Point(0, spcScreen.Panel1.Width / 4 * 3);

                    points[4] = new Point(spcScreen.Panel1.Height / 4, 0);
                    points[5] = new Point(spcScreen.Panel1.Height / 4, spcScreen.Panel1.Width / 4);
                    points[6] = new Point(spcScreen.Panel1.Height / 4, spcScreen.Panel1.Width / 4 * 2);
                    points[7] = new Point(spcScreen.Panel1.Height / 4, spcScreen.Panel1.Width / 4 * 3);

                    points[8] = new Point(spcScreen.Panel1.Height / 2, 0);
                    points[9] = new Point(spcScreen.Panel1.Height / 2, spcScreen.Panel1.Width / 4);
                    points[10] = new Point(spcScreen.Panel1.Height / 2, spcScreen.Panel1.Width / 4 * 2);
                    points[11] = new Point(spcScreen.Panel1.Height / 2, spcScreen.Panel1.Width / 4 * 3);

                    points[12] = new Point(spcScreen.Panel1.Height / 4 * 3, 0);
                    points[13] = new Point(spcScreen.Panel1.Height / 4 * 3, spcScreen.Panel1.Width / 4);
                    points[14] = new Point(spcScreen.Panel1.Height / 4 * 3, spcScreen.Panel1.Width / 4 * 2);
                    points[15] = new Point(spcScreen.Panel1.Height / 4 * 3, spcScreen.Panel1.Width / 4 * 3);
                    break;
            }
            return points;
        }

        #endregion

        #region//从数据库中加载探测器类表并转换为树视图
        public void LoadTreeView()
        {
            ArrayList temp_arrayList = sqlCreate.Select_All_SMInfraredConfig();
            try
            {
                tvwSensor.Nodes.Clear();
                if(temp_arrayList.Count>0)
                {
                    foreach(StaticClass.StructIAnalyzeConfig structIAnalyzeConfig in temp_arrayList)
                    {
                        TreeNode temp_Node = new TreeNode();
                        StaticClass.StructSM7003Tag structSM7003Tag = new StaticClass.StructSM7003Tag();
                        temp_Node.Text = structIAnalyzeConfig.CameraName;
                        structSM7003Tag.CameraID = structIAnalyzeConfig.CameraID;
                        structSM7003Tag.IP = structIAnalyzeConfig.IP;
                        structSM7003Tag.Port = structIAnalyzeConfig.Port;
                        structSM7003Tag.Reamrks = structIAnalyzeConfig.Reamrks;
                        structSM7003Tag.Enable = structIAnalyzeConfig.Enable;
                        if(structSM7003Tag.Enable==false)
                        {
                            temp_Node.ForeColor = Color.Gray;
                            temp_Node.NodeFont = new Font("微软雅黑", 9, FontStyle.Strikeout);
                        }else
                        {
                           
                        }
                        temp_Node.Tag = structSM7003Tag;
                        tvwSensor.Nodes.Add(temp_Node);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载探测器列表失败！" + ex.Message);
            }
        }
        #endregion

        #region// 树视图点击事件
        Point tvwPoint;
        private void tvwSensor_MouseDown(object sender, MouseEventArgs e)
        {
            tvwPoint = new Point(e.X, e.Y);
            try
            {
                if(e.Button==MouseButtons.Left)
                {
                    tvwSensor.SelectedNode = tvwSensor.GetNodeAt(tvwPoint);
                    if(tvwSensor.SelectedNode==null)
                    {
                        tvwSensor.SelectedNode = null;//取消选中的树视图节点
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("探测器列表异常！" + ex.Message);
            }
        }
        #endregion

        #region//树视图双击事件
        private void tvwSensor_DoubleClick(object sender, EventArgs e)
        {
            TreeNode treeNode = tvwSensor.GetNodeAt(tvwPoint);
            FrmCameraConfig frmCameraConfig = new FrmCameraConfig();
            if (treeNode != null)
            {
                StaticClass.StructSM7003Tag sM7003Tag = (StaticClass.StructSM7003Tag)treeNode.Tag;//将所双击的树视图的节点的 cameraID赋值给弹出窗体的ID属性
                frmCameraConfig.IPCameraID = sM7003Tag.CameraID;
                frmCameraConfig.ShowDialog();
            }
        }
        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            UCPictureBox uCPictureBox = FromHandle(screenBuffer.intPtrs_UCpbx[0]) as UCPictureBox;
            uCPictureBox.Width1 = spcScreen.Panel1.Width;
            uCPictureBox.Height1 = spcScreen.Panel1.Height;
            tlpScreen.Controls.Add(uCPictureBox);
            uCPictureBox.Location = new Point(0, 0);
            uCPictureBox.Draw_Tag("0");//编号顺序
        }

        private void Panel_DoubleClick(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            MessageBox.Show(panel.Name);
        }
       
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Refresh_Screen(3);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Refresh_Screen(2);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Refresh_Screen(1);
        }

        private void cmsShowNum_4_Click(object sender, EventArgs e)
        {
            Refresh_Screen(4);
        }

        private void 单画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(1);
        }

        private void 四画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(2);
        }

        private void 九画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(3);
        }

        private void 十六画面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh_Screen(4);
        }

        int OpenHandle;
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Tag.ToString() == "Start")
                {
                    string IP = ConfigurationManager.AppSettings["IPAddre_2"];
                    DMSDK.DM_Init();
                    DMSDK.DM_PlayerInit(screenBuffer.intPtrs_Pbx[0]);
                    OpenHandle = DMSDK.DM_OpenMonitor(screenBuffer.intPtrs_Pbx[0], IP, 5000);//6.0默认9989端口 ,7.0默认80端口 
                    if (OpenHandle >= 0)
                    {
                        btnStart.BackgroundImage = Properties.Resources.Pause;
                        btnStart.Tag = "Pause";
                    }
                    else
                    {
                        MessageBox.Show("连接失败！请检查参数后重试。");
                    }
                }
                else if (btnStart.Tag.ToString() == "Pause")
                {
                    btnStart.Tag = "Start";
                    btnStart.BackgroundImage = Properties.Resources.start;
                    DMSDK.DM_CloseMonitor(OpenHandle);
                    DMSDK.DM_PlayerCleanup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
