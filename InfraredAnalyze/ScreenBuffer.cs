using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    class ScreenBuffer
    {
        public ArrayList VideoBuffer = new ArrayList();
        public IntPtr[] intPtrs_Pbx = new IntPtr[16];
        public IntPtr[] intPtrs_UCpbx = new IntPtr[16];

        public ScreenBuffer()//构造函数 初始化 缓冲区
        {
            try
            {
                for (int i = 0; i < 16; i++)
                {
                    UCPictureBox uCPictureBox = new UCPictureBox();
                    uCPictureBox.Number = i.ToString();
                    VideoBuffer.Add(uCPictureBox);
                    intPtrs_Pbx[i] = uCPictureBox.IntPtrHandle;
                    intPtrs_UCpbx[i] = uCPictureBox.Handle;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
