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

        public string Number
        {
            get => number = label1.Text;
            set => number = value;
        }

        public void Draw_Tag(string tag)
        {
            label1.Text = "编号：" + tag;
        }

        private void UCPictureBox_DoubleClick(object sender, EventArgs e)
        {
            FullScreen_DispalyEventHandler fullScreen_Display = new FullScreen_DispalyEventHandler(frmMain.Full_Screen_Display);
            fullScreen_Display(this);
        }
    }
}
