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

        bool isOrnot;
        string gpbName;

        public string GpbName
        {
            get => gpb.Name;
            set => gpbName = gpb.Name;
        }
        public bool IsOrnot
        {
            get => rdbIs.Checked;
            set => isOrnot = rdbIs.Checked;
        }

        private void UCIsOrNot_Load(object sender, EventArgs e)
        {
           
        }
    }
}
