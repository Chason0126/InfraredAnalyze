using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace InfraredAnalyze
{
    public partial class IpAddressTextBox : UserControl
    {
        public IpAddressTextBox()
        {
            InitializeComponent();
        }

        private IPAddress iPAdd;

        public IPAddress IPAdd
        {
            get
            {
                try
                {
                    if (tbx1.Text == ""|| tbx2.Text=="" || tbx3.Text == "" || tbx4.Text == "")
                    {
                        MessageBox.Show("请输入完整的IP地址！");
                        if (tbx1.Text == "")
                        {
                            tbx1.Text = "0";
                        }
                        if (tbx2.Text == "")
                        {
                            tbx2.Text = "0";
                        }
                        if (tbx3.Text == "")
                        {
                            tbx3.Text = "0";
                        }
                        if (tbx4.Text == "")
                        {
                            tbx4.Text = "0";
                        }
                    }
                    iPAdd = IPAddress.Parse(tbx1.Text.ToString() + "." + tbx2.Text.ToString() + "." + tbx3.Text.ToString() + "." + tbx4.Text.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请输入正确的IP地址！"+ex.Message);
                }
                return iPAdd;
            }
            set => iPAdd = value;
        }

        public bool TextBox_Changed(TextBox textBox)
        {
            if (textBox.Text.Length == 3)
            {
                if (Convert.ToInt32(textBox.Text) > 255)
                {
                    MessageBox.Show(""+ textBox.Text + "不是一个有效项，请指定一个介于0-255之间的值");
                    textBox.Text = "255";
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TextBox_KeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (e.KeyChar < '0' || e.KeyChar > '9')
                {
                    e.Handled = true;
                }
            }
        }

        private void IpAddressTextBox_Load(object sender, EventArgs e)
        {
            tbx1.Focus();
        }

        private void tbx1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox_Changed(sender as TextBox))
            {
                tbx2.Focus();
            }
        }

        private void tbx1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(e);
        }

        private void tbx2_TextChanged(object sender, EventArgs e)
        {
            if (TextBox_Changed(sender as TextBox))
            {
                tbx3.Focus();
            }
        }

        private void tbx2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(e);
        }

        private void tbx3_TextChanged(object sender, EventArgs e)
        {
            if (TextBox_Changed(sender as TextBox))
            {
                tbx4.Focus();
            }
        }

        private void tbx3_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(e);
        }

        private void tbx4_TextChanged(object sender, EventArgs e)
        {
            TextBox_Changed(sender as TextBox);
        }

        private void tbx4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox_KeyPress(e);
        }

        private void tbx1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                tbx2.Focus();
            }
        }

        private void tbx2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                tbx3.Focus();
            }
            else if(e.KeyCode==Keys.Left)
            {
                tbx1.Focus();
            }
        }

        private void tbx3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                tbx4.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                tbx2.Focus();
            }
        }

        private void tbx4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                tbx3.Focus();
            }
        }
    }
}
