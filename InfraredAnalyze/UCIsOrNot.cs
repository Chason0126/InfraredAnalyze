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
    public partial class UCIsOrNot : UserControl
    {
        public UCIsOrNot()
        {
            InitializeComponent();
        }

        string ucText;
        bool isOrNot;

        public bool IsOrNot { get => rdbIs.Checked; set => rdbIs.Checked = value; }
        public string UcText { get => gpb.Text; set => gpb.Text = value; }

        private void rdbIs_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbIs.Checked==true)
            {
                IsOrNot = true;
            }
        }

        private void rbdNot_CheckedChanged(object sender, EventArgs e)
        {
            if(rbdNot.Checked==true)
            {
                IsOrNot = false;
            }
        }
    }
}
