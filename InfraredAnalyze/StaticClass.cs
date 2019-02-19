using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraredAnalyze
{
    class StaticClass
    {
        public  static int g_filetype = 1;
        public static DateTime nTimeStart;
        public static DateTime nTimeEnd;
        public static IntPtr m_Intptr;
        
        public static int PLAY_MODE = 0;
        public static int STOP_MODE = 1;
        public static int PAUSE_MODE = 2;

        public static int g_nWidth;
        public static int g_nHeight;

        public static IntPtr g_stream;
        public static int g_Len;
        public static int g_nTime;
        public static IntPtr g_Intptr;

        public  struct StructIAnalyzeConfig
        {
           public int CameraID;
           public string CameraName;
           public string IP;
           public int Port;
           public int NodeID;
           public string Reamrks;
           public bool Enable;
        }

        public struct StructSM7003Tag
        {
            public int CameraID;
            public string IP;
            public int Port;
            public string Reamrks;
            public bool Enable;
        }

        public static int[] intPtrs_Operate = new int[16];

    }
}
