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
    public partial class UCStatusPanel : UserControl
    {
        public UCStatusPanel()
        {
            InitializeComponent();
            //lblStatus.Text = status;
            //if (status == "Disconnect")
            //{
            //    splitContainer1.Panel2.BackColor = Color.Gray;
            //}
        }
        private string status;

        public string Status
        {
            get => lblStatus.Text;
            set => lblStatus.Text = value;
        }
        
    }
}
