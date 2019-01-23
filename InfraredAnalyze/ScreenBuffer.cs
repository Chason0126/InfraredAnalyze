using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraredAnalyze
{
    class ScreenBuffer
    {
        // List<UCPictureBox> screenBuffer = new List<UCPictureBox>();
        ArrayList screenBuffer = new ArrayList();
        public ScreenBuffer()//构造函数 初始化 缓冲区
        {
            for (int i = 1; i <= 16; i++)
            {
                UCPictureBox uCPictureBox = new UCPictureBox();
                uCPictureBox.Number = i.ToString();
                screenBuffer.Add(uCPictureBox);
            }
        }
    }
}
