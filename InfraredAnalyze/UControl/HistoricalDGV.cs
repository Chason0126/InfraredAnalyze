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
    public partial class HistoricalDGV : UserControl
    {
        public HistoricalDGV(List<StructClass.StructTemperData> list)
        {
            InitializeComponent();
            this.list = list;
        }

        List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();

        private void HistoricalDGV_Load(object sender, EventArgs e)
        {
            dgvHisData.Rows.Clear();
            foreach (StructClass.StructTemperData structTemperData in list)
            {
                dgvHisData.Rows.Add(structTemperData.CameraID, structTemperData.IPAddress, structTemperData.dateTime, structTemperData.Type, structTemperData.Temper, structTemperData.Status);
            }
        }
    }
}
