using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InfraredAnalyze
{
    enum RunningStatus
    {
        未启用,火警, 故障,先火警再故障 ,正常,停止
    }
    static class  StaticClass
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

       
        public static IntPtr[] intPtrs_UCPbx = new IntPtr[16];
        public static int[] intPtrs_Connect = new int[16];
        public static int[] intPtrs_Operate = new int[16];
        public static int[] intPtrs_Status = new int[16];//状态显示数组 
        public static string[] intPtrs_Ip = new string[16];//存储IP地址  用于在线检测 报警提示 IP地址
        public static string[] intPtrs_CameraName = new string[16];//存储相机名称
        public static int[] intPtrs_NodeId = new int[16];//存储nodeid地址  用于显示顺序
        public static bool[] intPtrs_Enable = new bool[16];//记录启用的探测器
        public static bool[] intPtrs_IsOnline = new bool[16];//记录在不在线
        public static byte[] intPtrs_AlarmId = new byte[16];//单台仪器可以是使用 多台的话 要自己判断 
        public static bool[] Is_CallBack = new bool[16];//纪录每台仪器是否回调获取数据，设备重连时，会出现回调失败的情况
        public static int SelectedNode = 0;
        public static List<StructClass.StructAlarm> intPtrs_AlarmConfig = new List<StructClass.StructAlarm>(16);//用于保存获取数据库中每台探测器的报警设置(存Structalarm)  引用类型 需要初始化
        public static List<StructClass.realTimeStructTemper> intPtrs_RealtimeTemper = new List<StructClass.realTimeStructTemper>(16);//用于存储每个探测器的每个测温区域的实时温度 （）
        public static List<StructClass.alarmStructCount> intPtrs_structCameraAlarmCounts = new List<StructClass.alarmStructCount>(16);//告警次数 记录list
        public static int FireCount = 0;
        public static int ErrCount = 0;
        public static ArrayList arrayList_FireData = new ArrayList();
        public static ArrayList arrayList_ErrData = new ArrayList();

        public static int Temper_Monitor;//临时操作（设置时使用）
        public static int Temper_Connect;
        public static string Temper_Ip;
        public static int Temper_CameraId;
        public static string Temper_CameraName;
        public static bool Temper_IsEnanle;
    }
}
