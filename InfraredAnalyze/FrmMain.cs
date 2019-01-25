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

namespace InfraredAnalyze
{

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }


        Drawing drawing = new Drawing();
        ScreenBuffer screenBuffer = new ScreenBuffer();

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
                UCPictureBox uCPictureBox = new UCPictureBox();
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
        private void btnLoadFile_Click(object sender, EventArgs e)
        {

            ////StaticClass.m_Intptr=InfraredSDK.IFR_Init(axAnimation1.Handle, axAnimation1.Width, axAnimation1.Height);
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "InfraredFile (*.DLV;*.DLI;*.JPG)|*.DLV;*.DLI;*.JPG||";
            //openFileDialog.Title = "请选择文件";
            //openFileDialog.RestoreDirectory = true;


            //if(openFileDialog.ShowDialog()==DialogResult.OK)
            //{
            //    try
            //    {
            //        string FileName = openFileDialog.FileName;
            //        if (Path.GetExtension(FileName) == "DLV" || Path.GetExtension(FileName) == "dlv")
            //        {
            //            StaticClass.g_filetype = 1;
            //        }
            //        else if (Path.GetExtension(FileName) == "JPG" || Path.GetExtension(FileName) == "jpg")
            //        {
            //            StaticClass.g_filetype = 2;
            //        }
            //        else
            //        {
            //            StaticClass.g_filetype = 0;
            //        }
            //        FileInfo fileInfo = new FileInfo(FileName);
            //        string time = fileInfo.LastWriteTime.ToString();

            //        int seconds = (int)DateTime.UtcNow.Subtract(DateTime.Parse("1970-01-01")).TotalSeconds;
            //        StaticClass.m_Intptr = InfraredSDK.IFR_LoadFile(axAnimation1.Handle, FileName, StaticClass.g_filetype, ref seconds, ref seconds);
            //        if (StaticClass.m_Intptr != null)
            //        {
            //            InfraredSDK.IFR_GetImageWidthHeight(StaticClass.m_Intptr, out StaticClass.g_nWidth, out StaticClass.g_nHeight);
            //            InfraredSDK.IFR_ReceiveStream(StaticClass.m_Intptr, STREAMCALLBACK);
            //            InfraredSDK.IFR_Play( StaticClass.m_Intptr);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
        }

        UCPictureBox pictureBox;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            Point[] points = Calc_points(16);
            for (int i = 1; i <= 16; i++)
            {
                pictureBox = new UCPictureBox();
                // panel.Visible = false;
                pictureBox.Width1 = spcScreen.Panel1.Width / 4;
                pictureBox.Height1 = spcScreen.Panel1.Height / 4;
                spcScreen.Panel1.Controls.Add(pictureBox);
                pictureBox.Location = points[i - 1];
            }
        }

        private void Panel_DoubleClick(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            MessageBox.Show(panel.Name);
        }
       
        private void axAnimation1_MouseDownEvent(object sender, AxMSComCtl2.DAnimationEvents_MouseDownEvent e)
        {
            //Point point = new Point(e.x, e.y);
            //float temp = InfraredSDK.IFR_GetPointTemp(StaticClass.m_Intptr, point);
            //g = axAnimation1.CreateGraphics();
            //pen = new Pen(Color.Red, 2);


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

        private void btnStart_Click(object sender, EventArgs e)
        {
            DMSDK.DM_PlayerInit(screenBuffer.IntPtrHandles[0]);
            DMSDK.DM_OpenMonitor(screenBuffer.IntPtrHandles[0], ConfigurationSettings.AppSettings["IPAddre_1"])
        }
    }
}
