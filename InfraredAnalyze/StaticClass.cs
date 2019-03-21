using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraredAnalyze
{
    enum RunningStatus
    {
        未启用,温度告警, 故障, 正常, 离线
    }
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
            public int NodeID;
            public string Reamrks;
            public bool Enable;
        }

        public static IntPtr[] intPtrs_UCPbx = new IntPtr[16];
        public static int[] intPtrs_Connect = new int[16];
        public static int[] intPtrs_Operate = new int[16];
        public static int[] intPtrs_Status = new int[16];//状态显示数组 0火警 1故障 2正常3离线
        public static string[] intPtrs_Ip = new string[16];//存储IP地址  用于在线检测
        public static string[] intPtrs_CameraName = new string[16];//存储相机名称
        public static int[] intPtrs_NodeId = new int[16];//存储nodeid地址  用于显示顺序
        public static bool[] intPtrs_Enable = new bool[16];//记录启用的探测器
        public static byte[] intPtrs_AlarmId = new byte[16];//
        public static IntPtr[] intPtrs_UCStatusPanel = new IntPtr[16];//状态显示控件的panel

        public struct StructTemperData
        {
            public int CameraID;
            public string IPAddress;
            public DateTime dateTime;
            public int Type;
            public int Number;
            public decimal Temper;
            public string Status;
        }

    }
}
