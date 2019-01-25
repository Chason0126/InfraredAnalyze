using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    public partial class UCPictureBox : UserControl
    {
        public UCPictureBox()
        {
            InitializeComponent();
            pbxScreen.DoubleClick += new EventHandler(UCPictureBox_DoubleClick);
        }

        FrmMain frmMain = new FrmMain();
        public delegate void FullScreen_DispalyEventHandler(UCPictureBox pictureBox);
        private string number;
        private int width;
        private int height;

        public string Number
        {
            get => number = label1.Text;
            set => number = value;
        }
        public int Width1 { get => width; set => width = value; }
        public int Height1 { get => height; set => height = value; }
        public IntPtr IntPtrHandle
        {
            get => intPtrHandle = pbxScreen.Handle;
            set => intPtrHandle = value;
        }

        private IntPtr intPtrHandle;

        public void Draw_Tag(string tag)
        {
            label1.Text = "编号：" + tag;
        }

        private void UCPictureBox_DoubleClick(object sender, EventArgs e)
        {
            FullScreen_DispalyEventHandler fullScreen_Display = new FullScreen_DispalyEventHandler(frmMain.Full_Screen_Display);
            fullScreen_Display(this);
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        Point point;
        private void UCPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X,e.Y);
        }

        private void UCPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                //Point temp_Poit = MousePosition;
                //temp_Poit.Offset(-point.X, -point.Y);
                //this.Location = temp_Poit;
            }
        }
    }
}
