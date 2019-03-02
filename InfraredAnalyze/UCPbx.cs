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
    public partial class UCPbx : UserControl
    {
        public UCPbx()
        {
            InitializeComponent();
        }
        private int id;

        public int Id { get => id; set => id = value; }
    }
}
