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
using Microsoft.DirectX.DirectDraw;

namespace InfraredAnalyze
{

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }


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
            pictureBox.Draw_Tag(pictureBox.Tagg.ToString());
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

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        public int STREAMCALLBACK(IntPtr hIntPtr, IntPtr stream, int len, long nTime)
        {
            StaticClass.g_stream = stream;
            StaticClass.g_Len = len;
            StaticClass.g_nTime = (int)nTime;
            StaticClass.g_Intptr = hIntPtr;
            return Play(hIntPtr, stream, len, (int)nTime);
            //return 1;
        }



        public int Play(IntPtr intPtr, IntPtr stream, int len, int nTime)
        {
            #region
            //Device displayDevice = null;//定义DirectDraw设备
            //Surface surfaceLP = null;//离屏表面（内存块）
            //displayDevice = new Device();
            //displayDevice.SetCooperativeLevel(axAnimation1, CooperativeLevelFlags.Normal);//设置显示窗口为 ax控件 显示方式为  全屏
            //displayDevice.SetDisplayMode(StaticClass.g_nWidth, StaticClass.g_nHeight, 24, 0, true);//设置显示模式  大小  位色 等等
            //ColorKey key = new ColorKey();
            //key.ColorSpaceHighValue = key.ColorSpaceLowValue = 0;//设置透明色  黑色
            //surfaceLP.SetColorKey(ColorKeyFlags.SourceDraw, key);//设置页面透明色
            //SurfaceDescription surfaceDescription = new SurfaceDescription();//目标表面
            //surfaceDescription.SurfaceCaps.PrimarySurface = surfaceDescription.SurfaceCaps.Complex = true;
            //surfaceDescription.BackBufferCount = 1;//为目标建立一个后表面
            //surfaceLP = new Surface(surfaceDescription, displayDevice);
            #endregion//Device
            #region
            Bitmap bitmapLP = new Bitmap(StaticClass.g_nWidth, StaticClass.g_nHeight);

            IntPtr LP = bitmapLP.GetHbitmap();
            #endregion

            if (intPtr == null)
            {
                return -1;
            }
            //InfraredSDK.IFR_DoHistogramProcess(intPtr, stream);
            //int nTmp = -1;
            //nTmp = InfraredSDK.IFR_GetDrawBuffer(intPtr, stream,ref  LP);
            //float fTemp, Mintemp, MaxTemp;

            //Point temp_Point = new Point(100, 100);
            //fTemp = InfraredSDK.IFR_GetPointTemp(StaticClass.m_Intptr,temp_Point);
            //InfraredSDK.IFR_Flip(StaticClass.m_Intptr);
            //Graphics graphics = axAnimation1.CreateGraphics();
            //Pen pen = new Pen(Color.Red, 2);
            //Font font = new Font("微软雅黑", 9);
            //Brush brush = new SolidBrush(Color.Red);
            //Rectangle rectangle = new Rectangle(20, 20, 30, 40);
            //graphics.DrawRectangle(pen, rectangle);
            //graphics.DrawString(fTemp.ToString(), font, brush, temp_Point.X + 2, temp_Point.Y - 30);
            //Point[] points = drawing.Draw_Cross(temp_Point);
            //for (int i = 0; i < points.Length; i = i + 2)
            //{
            //    graphics.DrawLine(pen, points[i], points[i + 1]);
            //}

            return 1;
        }
        Graphics g;
        Pen pen;
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
      
    }
}
