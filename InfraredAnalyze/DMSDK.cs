using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InfraredAnalyze
{
    class DMSDK
    {

        public struct PRESET_DM60
        {
            ushort PresetIndex;       //预置位编号
            //char PresetName[10];		//预置位名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)] public string PresetName;
        }

        public enum VIDEO_OUT_TYPE
        {
            PAL = 0,
            NTSC = 1,
        }

        public struct tagFile
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)] public string cFileName;
            // char cFileName[100];
            int RecordType; //0: 定时录像; 1: 告警录像
            int FileLength;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)] public string StartTime;
            //char StartTime[20];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)] public string EndTime;
            //char EndTime[20];
        }

        public struct tagLog
        {
            int Index;
            // char cDateTime[20];
            // char IP[24];
            // char Data[60];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)] public string cDateTime;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)] public string IP;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)] public string Data;
        }

        public struct tagResolutionInfo
        {
            public int width;
            public int height;
        }


        //激光测距得到的距离
        public struct tagDistanceInfo
        {
            int distance1;
            int distance2;
            int distance3;
        }


        public struct tagShieldRegion
        {
            int Index;
            bool Enable;
            int StartX;
            int StartY;
            int EndX;
            int EndY;
        }

        public struct tagSystemInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)] public string SDKVer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)] public string NETVer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)] public string DSPVer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)] public string FPGAVer;
            //char SDKVer[50];
            //char NETVer[50];
            //char DSPVer[50];
            //char FPGAVer[50];
        }
        /*
        *函数名称:	DM_Init
        *函数说明:	连接初始化
        *输入参数:  
        *返回值:
            */

        public struct temperAreaSpot
        {
            public int AreaId;
            public int X1;
            public int Y1;
            public int Emiss;
        }

        public struct temperLine
        {
            public string type;
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
            public int X3;
            public int Y3;
            public int Emiss;
        }

        public struct temperArea
        {
            public string type;
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
            public int Emiss;
            public int MeasureType;
        }

        public struct temperSpot
        {
            public string type;
            public int X1;
            public int Y1;
            public int Emiss;
        }

        public struct tagTemperaturePos//4*9
        {
            int type;
            int number;
            int MaxTemper;
            int MaxTemperPosX;
            int MaxTemperPosY;
            int MinTemper;
            int MinTemperPosX;
            int MinTemperPosY;
            int AveTemper;
        }


        public struct tagAlarm
        {
            public int AlarmID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagTempMessage
        {
            public int handle;//4
            public int len;//4
            //char dvrIP[16];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string dvrIP;//16
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public tagTemperature[] temperInfo;//12*32=384 一共408
            //public tagTemperature temperInfo;
        }

        public struct tagTemperature
        {
            public int type;
            public int number;
            public int temper;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagTempMessagePos
        {
            int handle;//4
            int len;//temperInfo的个数//4
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string dvrIP;//16
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public tagTemperaturePos[] temperInfo;//36*32
        }


        [DllImport("DMSDK.dll", EntryPoint = "DM_Init")]
        public static extern void DM_Init();

        /*
            *函数名称:	DM_Connect
            *函数说明:	连接DM60仪器
            *输入参数:  hwnd：		消息处理窗口句柄
			            IPAddr：	仪器IP地址
			            Port：		命令端口（默认端口80）

            *返回值:  > 0 连接到仪器后的操作句柄,用DM_Disconnect 释放。
		                <= 0 连接失败 
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Connect")]
        public static extern int DM_Connect(IntPtr intPtr, string IPAddr, int Port);

        /*
            函数名称:	DM_ConnectWithName
        *函数说明:	连接DM60仪器
        *输入参数:  hwnd：		消息处理窗口句柄
			        IPAddr：	仪器IP地址
			        Port：		命令端口（默认端口80）
			        UserName:	登陆用户名
			        Password:	登陆密码
            *返回值:  > 0 连接到仪器后的操作句柄,用DM_Disconnect 释放。
		            <= 0 连接失败			
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ConnectWithName")]
        public static extern int DM_ConnectWithName(IntPtr hintptr, string IPAddr, int Port, string UserName, string Password);

        /*
            函数名称:	DM_Disconnect
        *函数说明:	断开连接
        *输入参数:  handle：句柄
        *返回值:  >= 0： 正确；< 0： 错误			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Disconnect")]
        public static extern int DM_Disconnect(int handle);

        /*
            函数名称:	DM_SetIsotherm
        *函数说明:	设置等温区域
			        仅对 DM6x 二期或以后的有效, 更早的型号只支持1个等温区域, 分别用 DM_SetISOColor、DM_SetISOTemp 和 DM_SetISOHight
        *输入参数:  handle：		句柄
			        iIndex： 		等温编号, 支持3个等温区域（0 - 2之间）
			        iColorID:		等温色序号,  见颜色索引号, 0表示关闭
			        iIsoTemp：		等温区域中点温度 * 100
			        iHightTemp:		等温区域温度范围 * 100
        *返回值:	>0 成功, <0失败			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetIsotherm")]
        public static extern int DM_SetIsotherm(int handle, int iIndex, int iColorID, int iIsoTemp, int iHightTemp);

        /*
            函数名称:	DM_GetIsotherm
        *函数说明:	获得等温区域信息
			        仅对 DM6x 二期或以后的有效, 更早的型号只支持1个等温区域, 分别用 DM_GetISOColor、 DM_GetISOTemp 和 DM_GetISOHight
        *输入参数:  handle：		句柄
			        iIndex： 		等温编号, 支持3个等温区域（0 - 2之间）
        *返回值:			
			        iColorID:		等温色序号,  见颜色索引号, 0表示关闭
			        iIsoTemp：		等温区域中点温度 * 100
			        iHightTemp:		等温区域温度范围 * 100
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetIsotherm")]
        public static extern int DM_GetIsotherm(int handle, int iIndex, out int iColorID, out int iIsoTemp, out int iHightTemp);

        /*
            函数名称:	DM_SetAlarmInfo
        *函数说明: 报警温度设置
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        Type:			测温目标类型，0：点，1：线，2：矩形区域
			        Index:			测温目标序号，每种测温目标类型的序号都是从1开始的，区域9为全屏最热点，区域10为全屏最冷点
			        AlarmPower:		报警开关，0：关闭，1：打开
			        AlarmType:		报警触发方式，0：大于报警温度触发，1：小于报警温度触发
			        AlarmTemp：		报警温度 * 100
			        AlarmColorID:	报警颜色序号，见颜色索引号, 0表示不显示报警色
			        AlarmMessageType:	报警联动开关，0：不联动，1：联动方式1，2：联动方式2
        *返回值:
        *注：
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmInfo")]
        public static extern void DM_SetAlarmInfo(int handle, int Type, int Index, int AlarmPower, int AlarmType, int AlarmTemp, int AlarmColorID, int AlarmMessageType);

        /*
            函数名称:	DM_GetAlarmInfo
        *函数说明:  获取当前仪器的报警温度
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        Type:			测温目标类型，0：点，1：线，2：矩形区域
			        Index:			测温目标序号，每种测温目标类型的序号都是从1开始的，区域9为全屏最热点，区域10为全屏最冷点
        *输出参数:	
			        AlarmPower:		报警开关，0：关闭，1：打开
			        AlarmType:		报警触发方式，0：大于报警温度触发，1：小于报警温度触发
			        AlarmTemp：		报警温度 * 100
			        AlarmColorID:	报警颜色序号，见颜色索引号, 0表示不显示报警色
			        AlarmMessageType:	报警联动开关，0：不联动，1：联动方式1，2：联动方式2
        *注：	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAlarmInfo")]
        public static extern int DM_GetAlarmInfo(int handle, int type, int Index, out int AlarmPower, out int AlarmType, out int AlarmTemp, out int AlarmColorID, out int AlarmMessageType);

        /*
            函数名称:	DM_SetPreset
        *函数说明:	将镜头当前位置设置为 1 个预置点
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        iIndex： 		预置点编号, 支持128个预置点（0 - 127之间）
			        cPresetName:	预置点名称, 最长10个字符
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetPreset")]
        public static extern int DM_SetPreset(int handle, int iIndex, ref string cPresetName);

        /*
            函数名称:	DM_CallPreset
        *函数说明:	将镜头调焦到某预置点
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        iIndex： 		预置点编号, 支持128个预置点（0 - 127之间）
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CallPreset")]
        public static extern int DM_CallPreset(int handle, int iIndex);

        /*
            函数名称:	DM_ResetPresetName
        *函数说明:	重命名某个预置点
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        iIndex： 		预置点编号, 支持128个预置点（0 - 127之间）
			        cPresetName:	预置点的新名称, 最长10个字符
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ResetPresetName")]
        public static extern int DM_ResetPresetName(int handle, int iIndex, ref string cPresetName);

        /*
            函数名称:	DM_GetAllPreset
        *函数说明:	获得所有镜头预置点的信息
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        pPreset: 		预置点数组(0 ~ 127)
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAllPreset")]
        public static extern int DM_GetAllPreset(int handle, PRESET_DM60[] pPreset);

        /*
            函数名称:	DM_DeletePreset
        *函数说明:	删除镜头预置点
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
			        iIndex： 		预置点编号, 支持128个预置点（0 - 127之间）, -1表示全部删除
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_DeletePreset")]
        public static extern int DM_DeletePreset(int handle, int iIndex);

        /*
            函数名称:	DM_GetPos
        *函数说明:	获得镜头当前位置值
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄

        *返回值:	>-2000 成功, <=-2000失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetPos")]
        public static extern int DM_GetPos(int handle);

        /*
            函数名称:	DM_CallPos
        *函数说明:	将镜头调焦到某位置
			        仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CallPos")]
        public static extern int DM_CallPos(int handle, int iPos);

        /*
            函数名称:	DM_SetSpot
        *函数说明:	设置测温点
        *输入参数:  handle：		句柄
			        iPoint： 		点编号			支持4个测温点（1 - 4之间）
			        x,y：			点坐标，从(0, 0)开始
			        mode: 0-High/1-Low/2-Avg
			        Emiss：		辐射率  		（0 - 100）一般在90-95之间
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetSpot")]
        public static extern void DM_SetSpot(int handle, int iPoint, int x, int y, int Emiss = 90);

        /*
            函数名称:	DM_SetLine
        *函数说明:	设置测温线
        *输入参数:  handle：	句柄
			        iLine：	线编号, 目前支持一条线测温, 固定值 1
			        x1,y1：	起始点坐标，从(0, 0)开始
			        x2,y2：	结束点坐标，从(0, 0)开始
			        x3,y3：	线上测温点, 由用户指定的测温点
			        lineType,测温线类型, //0-Hori; 1-Vert; 2-Slash
			        mode, 测温模式,  0-High/1-Low/2-Avg
			        Emiss：	比幅射率  		（0 - 100）一般在10 -- 100之间

        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetLine")]
        public static extern void DM_SetLine(int handle, int iLine, int x1, int y1, int x2, int y2, int x3, int y3, int Emiss = 90);

        /*
            函数名称:	DM_SetArea
        *函数说明:	设置测温区域
        *输入参数:  handle：		句柄
			        iArea：  		区域编号		支持3个测温区域（1 - 3）
			        x1,y1： 		左上角坐标，从(0, 0)开始
			        x2,y2： 		右下角坐标，从(0, 0)开始
			        Emiss： 		辐射率  		（0 - 100）一般在90-95之间
			        MeasureType：	区域测温时的测温方式 （0： 最高 1：最低 2：平均）
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetArea")]
        public static extern void DM_SetArea(int handle, int iArea, int x1, int y1, int x2, int y2, int Emiss = 90, int MeasureType = 0);

        /*
            函数名称:	DM_ClearSpot
        *函数说明:	清除SpotID指定的测温点信息
        *输入参数:  handle：		句柄
			        SpotID： 		点编号		1 -- 4
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearSpot")]
        public static extern void DM_ClearSpot(int handle, int SpotID);

        /*
            函数名称:	DM_ClearLine
        *函数说明:	清除LineID指定的测温点信息
        *输入参数:  handle：	句柄
			        LineID：  	线编号	1
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearLine")]
        public static extern void DM_ClearLine(int handle, int LineID);

        /*
            函数名称:	DM_ClearArea
        *函数说明:	清除AreaID指定的测温区域信息
        *输入参数:  handle：	句柄
			        AreaID：  	区域编号	1 -- 3

        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearArea")]
        public static extern void DM_ClearArea(int handle, int AreaID);

        /*
            函数名称:	DM_ClearAllSpot
        *函数说明:	清除所有测温点
        *输入参数:  handle：	句柄
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearAllSpot")]
        public static extern void DM_ClearAllSpot(int handle);

        /*
            函数名称:	DM_ClearAllLine
        *函数说明:	清除所有测温线
        *输入参数:  handle：	句柄
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearAllLine")]
        public static extern void DM_ClearAllLine(int handle);

        /*
            函数名称:	DM_ClearAllArea
        *函数说明:	清除所有测温区域
        *输入参数:  handle：	句柄
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearAllArea")]
        public static extern void DM_ClearAllArea(int handle);

        /*
            函数名称:	DM_GetSpotTemp
        *函数说明:	获取测单个测温点温度
        *输入参数:  handle：		句柄
			        SpotID：		点编号(1--n)
			        Mode：			0：返回一次温度
							        1：连续返回温度, 间隔时间由仪器自动调节
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetSpotTemp")]
        public static extern void DM_GetSpotTemp(int handle, int SpotID = 1, int Mode = 0);

        /*
            函数名称:	DM_GetLineTemp
        *函数说明:	获取测单个测温线上用户设定测温点的温度
        *输入参数:  handle：		句柄
			        LineID：		线编号 (1 -- n)
			        Mode：			0：返回一次温度
							        1：连续返回温度, 间隔时间由仪器自动调节
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetLineTemp")]
        public static extern void DM_GetLineTemp(int handle, int LineID = 1, int Mode = 0);

        /*
            函数名称:	DM_GetAreaTemp
        *函数说明:	获取单个测温区域温度
        *输入参数:  handle：		句柄
			        AreaID：		区域编号(1-- n)
			        Mode：			0：返回一次温度
							        1：连续返回温度, 间隔时间由仪器自动调节。

        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAreaTemp")]
        public static extern void DM_GetAreaTemp(int handle, int AreaID, int Mode);

        /*
            函数名称:	DM_GetTemp
        *函数说明:	获取所有测温目标的测温结果
        *输入参数:  handle：		句柄
			        Mode：			0：返回一次温度
							        1：连续返回温度, 间隔时间由仪器自动调节。
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTemp")]
        public static extern void DM_GetTemp(int handle, int Mode);

        /*
            函数名称:	DM_StopTemp
        *函数说明:	停止连续测温的返回数据
        *输入参数:  handle：		句柄
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopTemp")]
        public static extern void DM_StopTemp(int handle);

        /*
            函数名称:	DM_GetTempParam
        *函数说明:	获取指定类型和编号的测温参数
        *输入参数:  handle：	句柄
			        Type：		测温数据类型 (1： 点  2： 线  3： 区域)
			        Number：	测温编号 (1 -- n)
        *返回值:>0 成功			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTempParam")]
        public static extern int DM_GetTempParam(int handle, int Type, int Number);

        /*
            函数名称:	DM_CaptureInfraredFrame
        *函数说明:	开始原始数据采集, 返回结果在消息 WM_DM_CAPTURE_FRAME 和 WM_CAPTUREINFRA_OVER 中处理
        *输入参数:  handle：	句柄
			        Path： 		保存文件的路径或文件名。如果是路径, 则系统以时间作文件名保存在此路径下；如果是文件名, 则系统直接以输入的文件名保存。
			        Frame： 	帧数
			        Time：		帧与帧之间的间隔时间(ms) >= 20ms, 且必须是20的倍数。
			        注意：Frame * Time <= 8秒
        *返回值:  采集句柄，用于消息 WM_DM_CAPTURE_FRAME 和 WM_CAPTUREINFRA_OVER 的处理

        *警告:  当该函数保存的DLV文件>=2G时，会导致InfraredSDK无法解析。

        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CaptureInfraredFrame")]
        public static extern int DM_CaptureInfraredFrame(int handle, ref string Path, int Frame, int tTime);

        /*
            函数名称:	DM_CaptureInfraredFrameStop
        *函数说明:	停止原始数据采集
        *输入参数:  handle		句柄,DM_Connect()函数的返回值
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CaptureInfraredFrameStop")]
        public static extern int DM_CaptureInfraredFrameStop(int handle);

        /*
            函数名称:	DM_RecvInfraredData
        *函数说明:  打开红外原始数据的回调
        *输入参数:  handle, 句柄
        *			InfraDataCallback, 回调函数		
        *输出参数:  无
        *返回值:正数表示成功, 负数表示失败	
        */
        public delegate void fInfraDataCallBack(int lInteptr, ulong dwDataType, ref byte pBuffer, ulong dwBufSize);
        [DllImport("DMSDK.dll", EntryPoint = "DM_RecvInfraredData")]
        public static extern long DM_RecvInfraredData(int handle, fInfraDataCallBack InfraDataCallback);

        /*
            函数名称:	DM_RecvInfraredData_EX
        *函数说明:  打开红外原始数据的回调
        *输入参数:  handle, 句柄
        *			Frame, 采集的总帧数
        *			Time,两帧的时间间隔, 单位毫秒
        *			InfraDataCallback, 回调函数		
        *输出参数:  无
        *返回值:正数表示成功, 负数表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_RecvInfraredData_EX")]
        public static extern long DM_RecvInfraredData_EX(int handle, int Frame, int Time, fInfraDataCallBack InfraDataCallback);

        /*
            函数名称:	DM_StopRecvInfraredData
        *函数说明:  停止红外原始数据的回调
        *输入参数:  lRealHandle, 句柄, 即DM_RecvInfraredData()或DM_RecvInfraredData_EX()的返回值	
        *输出参数:  
        *返回值:TRUE表示成功, FALSE表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopRecvInfraredData")]
        public static extern int DM_StopRecvInfraredData(long lRealHandle);

        /*
            函数名称:	DM_CaptureInfraredFromStream, 需要先执行 DM_RecvInfrareData_EX
        *函数说明:	开始原始数据采集, 返回结果在消息 WM_DM_CAPTURE_FRAME 和 WM_CAPTUREINFRA_OVER 中处理
        *输入参数:  handle：	DM_RecvInfraredData_EX 返回的句柄
			        Path： 		保存文件的路径或文件名。如果是路径, 则系统以时间作文件名保存在此路径下；如果是文件名, 则系统直接以输入的文件名保存。
			        Frame： 	帧数
			        Interval：	每几帧取一帧
			
        *返回值:  >=0 成功，<0失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CaptureInfraredFromStream")]
        public static extern int DM_CaptureInfraredFromStream(int handle, ref string Path, int Frame, int Interval);

        /*
            函数名称:	DM_CaptureInfraredFromStreamStop
        *函数说明:  停止从内存中保存红外原始数据
        *输入参数:  lRealHandle, 句柄, 即DM_RecvInfraredData_EX()的返回值	
        *输出参数:  
        *返回值:TRUE表示成功, FALSE表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CaptureInfraredFromStreamStop")]
        public static extern int DM_CaptureInfraredFromStreamStop(long lRealHandle);

        /*
            函数名称:	DM_SetPallette
        *函数说明:	设置色标
        *输入参数:  handle		句柄
			        Type： 		色标号 (0 -- 9) 
			        bPorarity： 		0、正相 1、反相
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetPallette")]
        public static extern void DM_SetPallette(int handle, int Type, bool bPorarity = false);

        /*
            函数名称:	DM_GetPallette
        *函数说明:	获取红外仪器的伪彩色色标
        *输入参数:  handle		句柄
        *返回值:    当前色标号			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetPallette")]
        public static extern int DM_GetPallette(int handle);

        /*
            函数名称:	DM_SetFireAlarmValue
        *函数说明:	设置消防色阈值
        *输入参数:  handle		句柄
        *返回值:    >0 设置成功
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetFireAlarmValue")]
        public static extern int DM_SetFireAlarmValue(int handle, int FireAlarmValue);

        /*
            函数名称:	DM_GetFireAlarmValue
        *函数说明:	获取消防色阈值
        *输入参数:  handle		句柄
        *返回值:    当前消防色阈值			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetFireAlarmValue")]
        public static extern int DM_GetFireAlarmValue(int handle);

        /*
            函数名称:	DM_SetVideoOutType
        *函数说明:	设置仪器的视频输出格式
        *输入参数:  handle：			句柄
			        VideoOutType：  0：PAL;1： NTSC制
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetVideoOutType")]
        public static extern int DM_SetVideoOutType(int handle, VIDEO_OUT_TYPE VideoOutType);

        /*
            函数名称:	DM_GetVideoOutType
        *函数说明:	获取仪器的当前视频输出格式(0：PAL;1： NTSC制)
        *输入参数:  handle：			句柄
        *返回值:	当前视频输出格式(0：PAL;1： NTSC制)		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetVideoOutType")]
        public static extern int DM_GetVideoOutType(int handle);

        /*
            函数名称:	DM_SetUpdateMeaTemp
        *函数说明:	设置仪器测温结果的返回速度
        *输入参数:  handle：			句柄
			        Freq  1~100, 表示每秒1~100次, 实际有效值要看仪器的反应速度
        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetUpdateMeaTemp")]
        public static extern void DM_SetUpdateMeaTemp(int handle, int Freq);

        /*
            函数名称:	DM_GetUpdateMeaTemp
        *函数说明:	获取仪器测温结果的返回速度
        *输入参数:  handle：			句柄
        *返回值:	速度, 单位为 次/秒		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetUpdateMeaTemp")]
        public static extern int DM_GetUpdateMeaTemp(int handle);

        /*
            函数名称:	DM_SetVideoMode
        *函数说明:	图像手动/自动模式切换
        *输入参数:  handle：		句柄
			        Mode：			模式(0、手动, 2、自动)
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetVideoMode")]
        public static extern void DM_SetVideoMode(int handle, int Mode);

        /*
            函数名称:	DM_AutoFocus
        *函数说明:	仪器自动聚焦
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_AutoFocus")]
        public static extern void DM_AutoFocus(int handle);

        /*
            函数名称:	DM_FocusFar
        *函数说明:	红外调焦, 拉远
        *输入参数:  handle：		句柄
			        Step： 		步长
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_FocusFar")]
        public static extern void DM_FocusFar(int handle, int step = 1);

        /*
            函数名称:	DM_FocusNear
        *函数说明:		红外调焦, 拉近
        *输入参数:  handle：		句柄
			        Step： 		步长
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_FocusNear")]
        public static extern void DM_FocusNear(int handle, int step = 1);

        /*
            函数名称:	DM_StopFocus
        *函数说明:	仪器停止调焦, 在使用DM_DurativeNear, DM_DurativeFar后有效
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopFocus")]
        public static extern void DM_StopFocus(int handle);

        /*
            函数名称:	DM_DurativeNear
        *函数说明:	仪器调焦,持续拉近, 直到调用DM_StopFocus停止调焦
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_DurativeNear")]
        public static extern void DM_DurativeNear(int handle);

        /*
            函数名称:	DM_DurativeFar
        *函数说明:	仪器调焦, 持续拉远, 直到调用DM_StopFocus停止调焦
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_DurativeFar")]
        public static extern void DM_DurativeFar(int handle);

        /*
            函数名称:	DM_StopZoom
        *函数说明:	仪器停止变倍, 在使用DM_ZoomNear, DM_ZoomFar后有效
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopZoom")]
        public static extern void DM_StopZoom(int handle);

        /*
            函数名称:	DM_ZoomNear
        *函数说明:	仪器变倍,持续拉近, 直到调用DM_StopZoom停止变倍
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ZoomNear")]
        public static extern void DM_ZoomNear(int handle);

        /*
            函数名称:	DM_ZoomFar
        *函数说明:	仪器变倍, 持续拉远, 直到调用DM_StopZoom停止变倍
        *输入参数:  handle：		句柄
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ZoomFar")]
        public static extern void DM_ZoomFar(int handle);

        /*
            函数名称:	DM_ShowTempValueOnImage
        *函数说明:	打开测温状态, 仪器上是否显示测温数据
        *输入参数:  handle：		句柄
			        bEnable：		0： 隐藏 1、显示
        *返回值:		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ShowTempValueOnImage")]
        public static extern void DM_ShowTempValueOnImage(int handle, bool bEnable);

        /*
            函数名称:	DM_GetTempValueOnImageStatus
        *函数说明:	获取仪器上是否显示测温数据
        *输入参数:  handle：		句柄
        *返回值:	0： 隐藏 1、显示	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTempValueOnImageStatus")]
        public static extern int DM_GetTempValueOnImageStatus(int handle);

        /*
            函数名称:	DM_SetISOTemp
        *函数说明:	设置仪器的等温温度值, 在测温时需要使用
        *输入参数:  handle：		句柄
			        ThermTemp：		温度 * 100
        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetISOTemp")]
        public static extern void DM_SetISOTemp(int handle, int ThermTemp);

        /*
            函数名称:	DM_GetISOTemp
        *函数说明:	获取仪器当前设置的等温温度
        *输入参数:  handle：		句柄
        *返回值:	等温温度
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetISOTemp")]
        public static extern int DM_GetISOTemp(int handle);

        /*
            函数名称:	DM_SetISOHight
        *函数说明:	设置等温高度（等温色的温度范围）, 在测温时使
        *输入参数:  handle：			句柄
                    ThermHight：		高度*100

        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetISOHight")]
        public static extern void DM_SetISOHight(int handle, int ThermHight);

        /*
            函数名称:	DM_GetISOHight
        *函数说明:	获取当前仪器的等温高度
        *输入参数:  handle：			句柄
                    ThermHight：		高度*100


        *返回值:	等温高度
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetISOHight")]
        public static extern int DM_GetISOHight(int handle);

        /*
            函数名称:	DM_SetISOColor
        *函数说明:	设置等温色, 在测温时使用
        *输入参数:  handle：			句柄
                    ColorID： 	等温色号（见颜色索引号）
        *返回值:	等温高度
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetISOColor")]
        public static extern void DM_SetISOColor(int handle, int ColorID);

        /*
            函数名称:	DM_GetISOColor
        *函数说明:	获取当前仪器的等温色
        *输入参数:  handle：句柄
        *返回值:	等温色（见颜色索引号）
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetISOColor")]
        public static extern int DM_GetISOColor(int handle);

        /*
            函数名称:	DM_SetTempUnit
        *函数说明:	设置温度单位
        *输入参数:  handle：		句柄
                    Unit：			0：℃;1 ℉;2 K
        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetTempUnit")]
        public static extern void DM_SetTempUnit(int handle, int Unit);

        /*
            函数名称:	DM_GetTempUnit
        *函数说明:	获取仪器的温度单位
        *输入参数:  handle：		句柄
        *返回值:	温度单位（0：℃;1 ℉;2 K）
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTempUnit")]
        public static extern int DM_GetTempUnit(int handle);

        /*
            函数名称:	DM_SetUpTempRange
        *函数说明:	在手动调色标时使用, 用于设置温度上限
        *输入参数:  handle	：句柄
                    Offset ：在原来色标上限的基础上增减,值 -10、-1、+1、+10 之间, 单位为度

        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetUpTempRange")]
        public static extern void DM_SetUpTempRange(int handle, int Offset);

        /*
            函数名称:	DM_SetDownTempRange
        *函数说明:	在手动调色标时使用, 用于设置温度下限
        *输入参数:  handle	：句柄
                    Offset ：在原来色标上限的基础上增减,值 -10、-1、+1、+10 之间, 单位为度

        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetDownTempRange")]
        public static extern void DM_SetDownTempRange(int handle, int Offset);


        /****************************************************************************************************************/
        /* 测温参数配置                                                                                                 */
        /****************************************************************************************************************/

        /*
            函数名称:	DM_SetMeasureClass
        *函数说明:	设置测温档位, 具体支持档位详见镜头说明
        *输入参数:  handle：		句柄
                    ParamValue ： 档位 （1-8）

        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetMeasureClass")]
        public static extern void DM_SetMeasureClass(int handle, int ParamValue);

        /*
            函数名称:	DM_GetMeasureClass
        *函数说明:	获取当前的测温档位
        *输入参数:  handle：		句柄
        *返回值:	当前的测温档位（档位值从1开始）		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetMeasureClass")]
        public static extern int DM_GetMeasureClass(int handle);

        /*
            函数名称:	DM_SetRefeType
        *函数说明:	设置参考温度类型
        *输入参数:  handle：		句柄
                    ParamValue ： 0、关闭   
                    1、	参考温度, 值由DM_SetRefeTemp设置   
                    2 - 5 ：对应 点1--4 温度
                    6 - 8 ： 对应 区域 1-3温度

        *返回值:			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetRefeType")]
        public static extern void DM_SetRefeType(int handle, int ParamValue);

        /*
            函数名称:	DM_GetRefeType
        *函数说明:	获取当前仪器的参考温度类型
        *输入参数:  handle：		句柄		
        *返回值:    仪器的参考温度类型			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRefeType")]
        public static extern int DM_GetRefeType(int handle);

        /*
            函数名称:	DM_SetRefeTemp
        *函数说明:	当选择参考类型为(1)参考温度时用到, 设置参考温度值
        *输入参数:  
                    handle：			句柄
                    ParamValue ：  温度 * 100
        *返回值:    仪器的参考温度类型			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetRefeTemp")]
        public static extern void DM_SetRefeTemp(int handle, int ParamValue);

        /*
            函数名称:	DM_GetRefeTemp
        *函数说明:	当选择参考类型为(1)参考温度时用到, 设置参考温度值
        *输入参数:  
                    handle：			句柄
                    ParamValue ：  温度 * 100
        *返回值:    仪器的参考温度类型			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRefeTemp")]
        public static extern int DM_GetRefeTemp(int handle);

        /*
            函数名称:	DM_SetAmbientTemp
        *函数说明:	环境温度设置, 当外界环境温度与设置值相差超过5时, 需要重新设定环境温度
        *输入参数:  
                    handle：		句柄
                    Temper：  	温度 * 100
        *返回值:    			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAmbientTemp")]
        public static extern void DM_SetAmbientTemp(int handle, int Temp);

        /*
            函数名称:	DM_GetAmbientTemp
        *函数说明:	获取当前仪器的环境温度
        *输入参数:  
                    handle：		句柄
        *返回值:    环境温度			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAmbientTemp")]
        public static extern int DM_GetAmbientTemp(int handle);

        /*
            函数名称:	DM_SetObjDistance
        *函数说明:	设置环境距离, 当图像目标与仪器的距离与设置值相差5m 以上时, 需要重新设定环境距离
        *输入参数:  
                    handle：		句柄
                    Distance：	 	距离 (单位：厘米)
        *返回值:    			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetObjDistance")]
        public static extern void DM_SetObjDistance(int handle, int Distance);

        /*
            函数名称:	DM_GetObjDistance
        *函数说明:	获取仪器设置的环境距离值
        *输入参数:  
                    handle：		句柄
        *返回值:    环境距离			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetObjDistance")]
        public static extern int DM_GetObjDistance(int handle);

        /*
            函数名称:	DM_SetAmbientHumidity
        *函数说明:	设置环境湿度,根据具体的外界环境来设置湿度
        *输入参数:  
                    handle：		句柄
        *返回值:    环境距离			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAmbientHumidity")]
        public static extern void DM_SetAmbientHumidity(int handle, int Humidity);

        /*
            函数名称:	DM_GetAmbientHumidity
        *函数说明:	获取仪器设置的环境湿度值
        *输入参数:  
                    handle：		句柄
        *返回值:    环境湿度 湿度范围（0-100）			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAmbientHumidity")]
        public static extern int DM_GetAmbientHumidity(int handle);

        /*
            函数名称:	DM_SetReviseParam
        *函数说明:	设置修正系数
        *输入参数:  
                    handle			句柄
                    ReviseParam		修正系数 * 100

        *返回值:   			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetReviseParam")]
        public static extern void DM_SetReviseParam(int handle, int ReviseParam);

        /*
            函数名称:	DM_GetReviseParam
        *函数说明:	获取修正系数
        *输入参数:  
                    handle			句柄
                    ReviseParam		修正系数 * 100
        *返回值:   			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetReviseParam")]
        public static extern int DM_GetReviseParam(int handle);

        /*
            函数名称:	DM_SetReviseTemp
        *函数说明:	设置修正温度
        *输入参数:  
                    handle			句柄
                    ReviseTemp		修正温度 * 100

        *返回值:   			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetReviseTemp")]
        public static extern void DM_SetReviseTemp(int handle, int ReviseTemp);

        /*
            函数名称:	DM_GetReviseTemp
        *函数说明:	获取修正温度
        *输入参数:  
                    handle			句柄
        *返回值:   	修正温度		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetReviseTemp")]
        public static extern int DM_GetReviseTemp(int handle);

        /****************************************************************************************************************/
        /* 报警设置                                                                                                     */
        /****************************************************************************************************************/

        /*
            函数名称:	DM_OpenAlarm
        *函数说明:	打开仪器端报警功能, 报警信号的输出在消息WM_TEMP_ALARM (0)中处理
        *输入参数:  handle：	句柄
        *返回值:
        **注：		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_OpenAlarm")]
        public static extern void DM_OpenAlarm(int handle);

        /*
            函数名称:	DM_CloseAlarm
        *函数说明: 关闭仪器端报警功能
        *输入参数:  handle：	句柄
        *返回值:
        *注：	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CloseAlarm")]
        public static extern void DM_CloseAlarm(int handle);

        /*
            函数名称:	DM_SetAlarmTemp
        *函数说明: 报警温度设置
        *输入参数:  handle：		句柄
                    Temper ：  	温度 * 100
        *返回值:
        *注：
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmTemp")]
        public static extern void DM_SetAlarmTemp(int handle, int Temp);

        /*
            函数名称:	DM_GetAlarmTemp
        *函数说明:  获取当前仪器的报警温度
        *输入参数:  handle：		句柄
        *返回值:	报警温度
        *注：	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAlarmTemp")]
        public static extern int DM_GetAlarmTemp(int handle);

        /*
            函数名称:	DM_SetAlarmColor
        *函数说明:  设置报警色
        *输入参数:  handle：		句柄
                    ColorID ：	（见颜色索引号）
        *返回值:	
        *注：
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmColor")]
        public static extern void DM_SetAlarmColor(int handle, int ColorID);

        /*
            函数名称:	DM_GetAlarmColor
        *函数说明:  获取当前仪器的报警色
        *输入参数:  handle：		句柄
        *返回值:	报警颜色（见颜色索引号）
        *注：
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAlarmColor")]
        public static extern int DM_GetAlarmColor(int handle);

        /*
            函数名称:	DM_OpenRemoteAlarm
        *函数说明:  打开仪器端报警功能, 报警信号的输出在消息WM_TEMP_ALARM (0)中处理
        *输入参数:  handle：		句柄
        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_OpenRemoteAlarm")]
        public static extern void DM_OpenRemoteAlarm(int handle);

        /*
            函数名称:	DM_CloseRemoteAlarm
        *函数说明:  关闭仪器端报警功能
        *输入参数:  handle：		句柄
        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CloseRemoteAlarm")]
        public static extern void DM_CloseRemoteAlarm(int handle);

        /*
            函数名称:	DM_SetRemoteAlarmTemp
        *函数说明:  报警温度设置
        *输入参数:  handle：		句柄
                    Temper ：  	温度 * 100
        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetRemoteAlarmTemp")]
        public static extern void DM_SetRemoteAlarmTemp(int handle, int Temp);

        /*
            函数名称:	DM_GetRemoteAlarmTemp
        *函数说明:  获取当前仪器的报警温度
        *输入参数:  handle：		句柄
        *返回值:	报警温度
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRemoteAlarmTemp")]
        public static extern int DM_GetRemoteAlarmTemp(int handle);

        /*
            函数名称:	DM_SetRemoteAlarmColor
        *函数说明:  设置报警色
        *输入参数:  handle：		句柄
                    ColorID ：	（见颜色索引号）
        *返回值:	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetRemoteAlarmColor")]
        public static extern void DM_SetRemoteAlarmColor(int handle, int ColorID);

        /*
            函数名称:	DM_GetRemoteAlarmColor
        *函数说明:  获取当前仪器的报警色
        *输入参数:  handle：		句柄
        *返回值:	报警颜色（见颜色索引号）
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRemoteAlarmColor")]
        public static extern int DM_GetRemoteAlarmColor(int handle);

        /*
            函数名称:	DM_IOAlarm
        *函数说明:  设置仪器报警IO口及报警使能
        *输入参数:  handle：		句柄
        *返回值:	报警颜色（见颜色索引号）
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_IOAlarm")]
        public static extern void DM_IOAlarm(int handle, int iIO, int iEnable);

        /*************************************************************************************************************8****/
        /*   系统配置设置                                                                                                 */
        /******************************************************************************************************************/

        /*
            函数名称:	DM_SetIPAddr
        *函数说明:  设置仪器的IP地址
        *输入参数:  handle：			句柄
                    IP ： IP地址   格式 ： 192.168.0.1

        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetIPAddr")]
        public static extern void DM_SetIPAddr(int handle, string IP, string SubMask, string GateWay);

        /*
            函数名称:	DM_SetMAC
        *函数说明:  设置MAC地址
        *输入参数:  handle：		句柄
                    Mac ：		地址
        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetMAC")]
        public static extern void DM_SetMAC(int handle, ref string Mac);

        /*
            函数名称:	DM_SetAutoAdjustTime
        *函数说明:  设置自动校正间隔时间
        *输入参数:  handle：		句柄
                    Time ： 		时间（单位：秒）

        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAutoAdjustTime")]
        public static extern void DM_SetAutoAdjustTime(int handle, int Time);

        /*
            函数名称:	DM_GetAutoAdjustTime
        *函数说明:  获取当前仪器的自动校正间隔时间
        *输入参数:  handle：		句柄

        *返回值:    间隔时间（秒）
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAutoAdjustTime")]
        public static extern int DM_GetAutoAdjustTime(int handle);

        /*
            函数名称:	DM_SetDateTime
        *函数说明:  调整仪器时间
        *输入参数:  handle		句柄
                    year		年
                    month		月
                    day			日
                    hour		时
                    min			分
                    sec			秒

        *返回值:    
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetDateTime")]
        public static extern void DM_SetDateTime(int handle, int year, int month, int day, int hour, int min, int sec);

        /*
            函数名称:	DM_GetDateTime
        *函数说明:  获取仪器的当前时间
        *输入参数:  handle		句柄

        *输出参数:  字符串, 格式yyyy-mm-dd hh-mm-ss
                    yyyy		年
                    mm			月
                    dd			日
                    hh			时
                    mm			分
                    ss			秒
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetDateTime")]
        public static extern void DM_GetDateTime(int handle, StringBuilder DateTime);

        /*
            函数名称:	DM_LoadDefault
        *函数说明:  恢复出厂设置
        *输入参数:  handle		句柄
        *返回值：无
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_LoadDefault")]
        public static extern void DM_LoadDefault(int handle);

        /*
            函数名称:	DM_GetSystemInfo
        *函数说明:  获取仪器的系统信息, 生成日期和版本号
        *输入参数:  handle		句柄
        *输出参数：SysInfo		日期和版本号字符串
        *返回值：无
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetSystemInfo")]
        public static extern void DM_GetSystemInfo(int handle, ref string SysInfo);

        /*
            函数名称:	DM_SetZoom
        *函数说明:	电子放大
        *输入参数:  
                    handle：　	句柄	
                    iZoom: 放大倍数
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetZoom")]
        public static extern int DM_SetZoom(int handle, int iZoom);

        /*
            函数名称:	DM_SetBright
        *函数说明:	亮度调节
        *输入参数:  handle: 句柄
        *           iBright: 亮度值
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetBright")]
        public static extern int DM_SetBright(int handle, int iBright);

        /*
            函数名称:	DM_SetContrast
        *函数说明:	增益调节
        *输入参数:  handle: 句柄
        *           iContrast: 增益值
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetContrast")]
        public static extern int DM_SetContrast(int handle, int iContrast);

        /*
            函数名称:	DM_SetOSDInfo_CameraName
        *函数说明:	设置视频中的设备名称
        *输入参数:  
                    handle：　	句柄	
                    cCameraName: 设备名称
                    iDisplayName: 是否显示设备名称，0: 否，1: 是
                    iPosX, iPosY: 设备名称显示位置的坐标
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetOSDInfo_CameraName", CharSet = CharSet.Ansi)]
        //public static extern int DM_SetOSDInfo_CameraName(int handle, string cCameraName, int iDisplayName, int iPosX, int iPosY);
        public static extern int DM_SetOSDInfo_CameraName(int handle, string cCameraName, int iDisplayName, int iPosX, int iPosY);
        /*
            函数名称:	DM_SetOSDInfo_UserDefine
        *函数说明:	设置视频中的用户自定义信息
        *输入参数:  
                    handle：　	句柄	
                    cUserDefineInfo: 用户自定义信息
                    iDisplayUserDefineInfo: 是否显示用户自定义信息，0: 否，1: 是
                    iPosX, iPosY: 用户自定义信息显示位置的坐标
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetOSDInfo_UserDefine")]
        public static extern int DM_SetOSDInfo_UserDefine(int handle, ref string cUserDefineInfo, int iDisplayUserDefineInfo, int iPosX, int iPosY);

        /*
            函数名称:	DM_SetOSDInfo_DateTime
        *函数说明:	设置视频中的时间信息
        *输入参数:  
                    handle：　	句柄	
                    iDisplayTime: 是否显示时间信息，0: 否，1: 是
                    iPosX, iPosY: 时间信息显示位置的坐标
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetOSDInfo_DateTime")]
        public static extern int DM_SetOSDInfo_DateTime(int handle, int iDisplayTime, int iPosX, int iPosY);

        /*
            函数名称:	DM_SetOSDInfo_PeaDisplay
        *函数说明:	打开/关闭视频中的智能分析轨迹显示开关
        *输入参数:  
                    handle：　	句柄	
                    iDisplayPea: 打开/关闭视频中的智能分析轨迹显示开关，0: 关闭，1: 打开
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetOSDInfo_PeaDisplay")]
        public static extern int DM_SetOSDInfo_PeaDisplay(int handle, int iDisplayPea);

        /*
            函数名称:	DM_SetEncodingInfo_Major
        *函数说明:	设置视频的编码参数
        *输入参数:  
                    handle:　	句柄	
                    BitrateType:	编码类型。0: 可变编码；1: 固定编码
                    Resolution:		分辨率。0: 320x240; 1: 384x288; 2: 640x480; 3: 720x480; 4: 720x576; 
                    Bitrate:		码流。0: 128; 1: 256; 2: 512; 3: 1024
                    FrameRate:		帧率。可选参数: 1~25，30

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetEncodingInfo_Major")]
        public static extern int DM_SetEncodingInfo_Major(int handle, int BitrateType, int Resolution, int Bitrate, int FrameRate);

        /*
            函数名称:	DM_SetEncodingInfo_Minor
        *函数说明:	设置视频的编码参数
        *输入参数:  
                    handle:　	句柄	
                    BitrateType:	编码类型。0: 可变编码；1: 固定编码
                    Resolution:		分辨率。0: 320x240; 1: 384x288; 2: 640x480; 3: 720x480; 4: 720x576; 
                    Bitrate:		码流。可选参数: 128、256、512、1024
                    FrameRate:		帧率。可选参数: 1~25，30

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetEncodingInfo_Minor")]
        public static extern int DM_SetEncodingInfo_Minor(int handle, int BitrateType, int Resolution, int Bitrate, int FrameRate);

        /*
            函数名称:	DM_SetRecordSchedule
        *函数说明:	设置录像计划
        *输入参数:  
			        handle:　	句柄	
			        iEnable:	是否启用。0: 关闭；1: 开启
			        RecordTime:		计划时间。例：ffffff-ffffff-ffffff-ffffff-ffffff-ffffff-ffffff-

        *返回值:	>0 成功 
			        <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetRecordSchedule")]
        public static extern int DM_SetRecordSchedule(int handle, int iEnable, ref string RecordTime);

        /*
            函数名称:	DM_SetStorageInfo
        *函数说明:	管理磁盘(SD卡)
        *输入参数:  
                    handle:　	句柄	
                    iEnable:	是否启用。0: 关闭；1: 开启
                    iOverWrite:	磁盘满后是否自动覆盖
                    iFullAlarm: 磁盘满是否告警
                    iFullGrade: 磁盘盘判断阈值。可选参数：20 ~ 90，表示20% ~ 90%

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetStorageInfo")]
        public static extern int DM_SetStorageInfo(int handle, int iEnable, int iOverWrite, int iFullAlarm, int iFullGrade);

        /*
            函数名称:	DM_SetNASInfo
        *函数说明:	网络存储器设置
        *输入参数:  
                    handle:　	句柄	
                    iEnable:	是否启用。0: 关闭；1: 开启
                    IP:			网络存储器的IP地址
                    ID:			网络存储器的登录用户名
                    Password:	网络存储器的登录密码
                    Path:		网络存储器中的存储路径
                    iOverWrite:	磁盘满后是否自动覆盖
                    iFullAlarm: 磁盘满是否告警
                    iFullGrade: 磁盘盘判断阈值。可选参数：20 ~ 90，表示20% ~ 90%

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetNASInfo")]
        public static extern int DM_SetNASInfo(int handle, int iEnable, ref string IP, ref string ID, ref string Password, ref string Path, int iOverWrite, int iFullAlarm, int iFullGrade);

        /*
            函数名称:	DM_SetUnexpectedInfo_Network
        *函数说明:	网络异常处理
        *输入参数:  
                    handle:　	句柄	
                    iAlarmType:		异常类型，1: 网络断开; 2: IP冲突
                    iEnable:		是否启用。0: 关闭; 1: 开启
                    iRecTime:		异常时录像时长，有效值：0 ~ 60 秒
                    iPreRecTime:	异常时预录时长，有效值：0 ~ 10 秒

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetUnexpectedInfo_Network")]
        public static extern int DM_SetUnexpectedInfo_Network(int handle, int iAlarmType, int iEnable, int iRecTime, int iPreRecTime);

        /*
            函数名称:	DM_SetUnexpectedInfo_Storage
        *函数说明:	存储器异常处理
        *输入参数:  
                    handle:　	句柄	
                    iAlarmType:		异常类型，3: 存储器满; 4: 存储器错误
                    AlarmLinkOutInfo:	告警联动方式，Rec、FTP、EMail多选
                    FtpPicNum:			告警时，上传几张图片到Ftp服务器
                    EMailPicNum:		告警时，通过EMail发送几张图片
                    iMailContentType:	发送告警信息时是否发送图片。1: 纯文本; 2: 文本+图片

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetUnexpectedInfo_Storage")]
        public static extern int DM_SetUnexpectedInfo_Storage(int handle, int iAlarmType, ref string AlarmLinkOutInfo, int iMailContentType, int iEMailPicNum, int iFtpPicNum);

        /*
            函数名称:	DM_GetZoom
        *函数说明:	获得当前电子放大倍数
        *输入参数:  
                    handle：　	句柄	
        *返回值:	>0 放大倍数
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetZoom")]
        public static extern int DM_GetZoom(int handle);

        /*
            函数名称:	DM_GetBright
        *函数说明:	获得当前亮度值
        *输入参数:  handle: 句柄
        *返回值:	正数表示亮度值, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetBright")]
        public static extern int DM_GetBright(int handle);

        /*
            函数名称:	DM_GetContrast
        *函数说明:	获得当前增益值
        *输入参数:  handle: 句柄
        *返回值:	正数表示增益值, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetContrast")]
        public static extern int DM_GetContrast(int handle);

        /*
            函数名称:	DM_GetOSDInfo_CameraName
        *函数说明:	获得视频中的设备名称信息
        *输入参数:  
                    handle：　	句柄
        *输出参数:
                    cCameraName: 设备名称
                    iDisplayName: 是否显示设备名称，0: 否，1: 是
                    iPosX, iPosY: 设备名称显示位置的坐标
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetOSDInfo_CameraName", CharSet = CharSet.Ansi)]
        //public static extern int DM_GetOSDInfo_CameraName(int handle, ref byte cCameraName, out int iDisplayName, out int iPosX, out int iPosY);
        //public static extern int DM_GetOSDInfo_CameraName(int handle, [MarshalAs(UnmanagedType.LPStr)]StringBuilder cCameraName, out int iDisplayName, out int iPosX, out int iPosY);
        public static extern int DM_GetOSDInfo_CameraName(int handle, StringBuilder cCameraName, out int iDisplayName, out int iPosX, out int iPosY);

        /*
            函数名称:	DM_GetOSDInfo_UserDefine
        *函数说明:	获得视频中的用户自定义信息
        *输入参数:  
                    handle：　	句柄
        *输出参数:
                    cUserDefineInfo: 用户自定义信息
                    iDisplayUserDefineInfo: 是否显示用户自定义信息，0: 否，1: 是
                    iPosX, iPosY: 用户自定义信息显示位置的坐标
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetOSDInfo_UserDefine")]
        public static extern int DM_GetOSDInfo_UserDefine(int handle, string cUserDefineInfo, out int iDisplayUserDefineInfo, out int iPosX, out int iPosY);

        /*
            函数名称:	DM_GetOSDInfo_DateTime
        *函数说明:	获得视频中的时间信息
        *输入参数:  
                    handle：　	句柄	
        *输出参数
                    iDisplayTime: 是否显示时间信息，0: 否，1: 是
                    iPosX, iPosY: 时间信息显示位置的坐标
        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetOSDInfo_DateTime")]
        public static extern int DM_GetOSDInfo_DateTime(int handle, out int iDisplayTime, out int iPosX, out int iPosY);

        /*
            函数名称:	DM_GetOSDInfo_PeaDisplay
        *函数说明:	获得视频中的智能分析轨迹显示状态
        *输入参数:  
                    handle：　	句柄	
        *返回值:	0: 关闭，1: 打开
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetOSDInfo_PeaDisplay")]
        public static extern int DM_GetOSDInfo_PeaDisplay(int handle);

        /*
            函数名称:	DM_GetEncodingInfo_Major
        *函数说明:	获得主码流的编码参数
        *输入参数:  
                    handle:　	句柄	
        *输出参数:
                    BitrateType:	编码类型。0: 可变编码；1: 固定编码
                    Resolution:		分辨率。0: 320x240; 1: 384x288; 2: 640x480; 3: 720x480; 4: 720x576; 
                    Bitrate:		码流。0: 128; 1: 256; 2: 512; 3: 1024
                    FrameRate:		帧率。可选参数: 1~25，30

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetEncodingInfo_Major")]
        public static extern int DM_GetEncodingInfo_Major(int handle, ref int BitrateType, ref int Resolution, ref int Bitrate, ref int FrameRate);

        /*
            函数名称:	DM_GetEncodingInfo_Minor
        *函数说明:	获得次码流的编码参数
        *输入参数:  
                    handle:　	句柄	
        *输出参数:
                    BitrateType:	编码类型。0: 可变编码；1: 固定编码
                    Resolution:		分辨率。0: 320x240; 1: 384x288; 2: 640x480; 3: 720x480; 4: 720x576; 
                    Bitrate:		码流。可选参数: 128、256、512、1024
                    FrameRate:		帧率。可选参数: 1~25，30

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetEncodingInfo_Minor")]
        public static extern int DM_GetEncodingInfo_Minor(int handle, ref int BitrateType, ref int Resolution, ref int Bitrate, ref int FrameRate);

        /*
            函数名称:	DM_GetRecordSchedule
        *函数说明:	获取录像计划
        *输入参数:  
                    handle:　	句柄	
        *输出参数:
                    iEnable:	是否启用。0: 关闭；1: 开启
                    RecordTime:		计划时间。例：ffffff-ffffff-ffffff-ffffff-ffffff-ffffff-ffffff-

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRecordSchedule")]
        public static extern int DM_GetRecordSchedule(int handle, ref int iEnable, ref string RecordTime);

        /*
            函数名称:	DM_GetStorageInfo
        *函数说明:	获得磁盘(SD卡)管理信息
        *输入参数:  
                    handle:　	句柄
        *输出参数:
                    iEnable:	是否启用。0: 关闭；1: 开启
                    iOverWrite:	磁盘满后是否自动覆盖
                    iFullAlarm: 磁盘满是否告警
                    iFullGrade: 磁盘盘判断阈值。可选参数：20 ~ 90，表示20% ~ 90%
                    cSize:		磁盘容量
                    cAvailableSpace:	磁盘可用空间

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetStorageInfo")]
        public static extern int DM_GetStorageInfo(int handle, ref int iEnable, ref int iOverWrite, ref int iFullAlarm, ref int iFullGrade, ref char cSize, ref char cAvailableSpace);

        /*
            函数名称:	DM_GetNASInfo
        *函数说明:	获得网络存储器设置信息
        *输入参数:  
                    handle:　	句柄	
        *输出参数:
                    iEnable:	是否启用。0: 关闭；1: 开启
                    IP:			网络存储器的IP地址
                    ID:			网络存储器的登录用户名
                    Password:	网络存储器的登录密码
                    Path:		网络存储器中的存储路径
                    iOverWrite:	网络存储器满后是否自动覆盖
                    iFullAlarm: 网络存储器满是否告警
                    iFullGrade: 网络存储器盘判断阈值。可选参数：20 ~ 90，表示20% ~ 90%
                    cSize:		网络存储器容量
                    cAvailableSpace:	网络存储器可用空间

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetNASInfo")]
        public static extern int DM_GetNASInfo(int handle, ref int iEnable, ref string IP, ref string ID, ref string Password, ref string Path, ref int iOverWrite, ref int iFullAlarm, ref int iFullGrade, ref string cSize, ref string cAvailableSpace);

        /*
            函数名称:	DM_GetUnexpectedInfo_Network
        *函数说明:	获得网络异常处理设置
        *输入参数:  
                    handle:　	句柄	
                    iAlarmType:		异常类型，1: 网络断开; 2: IP冲突
        *输出参数:
                    iEnable:		是否启用。0: 关闭; 1: 开启
                    iRecTime:		异常时录像时长，有效值：0 ~ 60 秒
                    iPreRecTime:	异常时预录时长，有效值：0 ~ 10 秒

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetUnexpectedInfo_Network")]
        public static extern int DM_GetUnexpectedInfo_Network(int handle, int iAlarmType, ref int iEnable, ref int iRecTime, ref int iPreRecTime);

        /*
            函数名称:	DM_GetUnexpectedInfo_Storage
        *函数说明:	获得存储器异常处理设置
        *输入参数:  
                    handle:　	句柄	
                    iAlarmType:		异常类型，3: 存储器满; 4: 存储器错误
        *输出参数:
                    AlarmLinkOutInfo:	告警联动方式，Rec、FTP、EMail多选
                    FtpPicNum:			告警时，上传几张图片到Ftp服务器
                    EMailPicNum:		告警时，通过EMail发送几张图片
                    iMailContentType:	发送告警信息时是否发送图片。1: 纯文本; 2: 文本+图片

        *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetUnexpectedInfo_Storage")]
        public static extern int DM_GetUnexpectedInfo_Storage(int handle, int iAlarmType, ref string AlarmLinkOutInfo, ref int iMailContentType, ref int iEMailPicNum, ref int iFtpPicNum);

        /********************************************************************************************************8*********/
        /*  视频处理函数                                                                                               */
        /******************************************************************************************************************/

        /*
         函数名称:	DM_PlayerInit
        *函数说明:	视频监控前的初始化。每套系统, 不管开了几个视频监控窗口, 本函数只能调用一次
        *输入参数:  hwnd, 视频显示窗口句柄
        *返回值:	=0  正确, 用DM_PlayerCleanup释放；
                    <0  错误
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayerInit")]
        public static extern int DM_PlayerInit(IntPtr hwnd);

        /*
         函数名称:	DM_OpenMonitor
        *函数说明:	打开视频监控
        *输入参数:  
                    hwnd：　	视频显示窗口句柄
                    ip ：  		仪器IP地址   
                    port ：  		端口
                    channel ：  	通道号, 目前只支持一个通道

        *返回值:	>=0 连接成功，视频监控句柄通过 WM_DM_PLAYER 消息的 wParam 返回
                    <0 连接失败
        */
        //预留, netsdk暂时不提供此接口
        [DllImport("DMSDK.dll", EntryPoint = "DM_OpenMonitor")]
        public static extern int DM_OpenMonitor(IntPtr hwnd, string ip, ushort port, int channel = 0);

        /*
            函数名称:	DM_SetOSD
        *函数说明:	设置视频的OSD
        *输入参数:  
                    handle：　	WM_DM_PLAYER 消息的 wParam 返回的句柄	
                    OSDType :   	0X00 无
                    0X01 主机时间
                    0X02 码流速率
                    0X04 主机IP地址


        *返回值:	>0 成功 
                    <0 失败
        *注： 该接口暂时保留
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetOSD")]
        public static extern int DM_SetOSD(int handle, int OSDType);

        /*
            函数名称:	DM_CloseMonitor
        *函数说明:	关闭视频监控
        *输入参数:  
                    handle：　WM_DM_PLAYER 消息的 wParam 返回的句柄
            *返回值:	>0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CloseMonitor")]
        public static extern int DM_CloseMonitor(int handle);

        /*
            函数名称:	DM_PlayerCleanup
        *函数说明:	清除int __stdcall DM_PlayerInit配置的资源
        *输入参数:  
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayerCleanup")]
        public static extern int DM_PlayerCleanup();

        /*
            函数名称:	DM_Record
        *函数说明:	开始录像
        *输入参数: 
                    handle：　WM_DM_PLAYER 消息的 wParam 返回的句柄	
                    path ：   保存文件路径

        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Record")]
        public static extern int DM_Record(int handle, ref string path);

        /*
        *函数名称:	DM_StopRecord
        *函数说明:	停止录像
        *输入参数:  handle：　	WM_DM_PLAYER 消息的 wParam 返回的句柄 
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopRecord")]
        public static extern int DM_StopRecord(int handle);

        /*
        *函数名称:	DM_Capture
        *函数说明:	采集一幅图像
        *输入参数:  handle：　	WM_DM_PLAYER 消息的 wParam 返回的句柄
                    path ： 	保存文件路径

        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Capture")]
        public static extern int DM_Capture(int handle, ref string path);

        /*
        *函数名称:	DM_PlayBack
        *函数说明:	文件回放
        *输入参数:  hwnd ：播放文件需要的窗口句柄
                    file	录像文件
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayBack")]
        public static extern int DM_PlayBack(IntPtr hwnd, ref string file);

        /*
        *函数名称:	DM_ClosePlayBack
        *函数说明:	关闭文件回放
        *输入参数:  handle	DM_PlayBack函数的返回值

        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClosePlayBack")]
        public static extern int DM_ClosePlayBack(int handle);

        /*
        *函数名称:	DM_PlayBackPause
        *函数说明:	暂停播放
        *输入参数:  handle	DM_PlayBack函数的返回值
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayBackPause")]
        public static extern int DM_PlayBackPause(int handle);

        /*
        *函数名称:	DM_PlayBackContinue
        *函数说明:	继续播放
        *输入参数:  handle	DM_PlayBack函数的返回值
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayBackContinue")]
        public static extern int DM_PlayBackContinue(int handle);

        /*
        *函数名称:	DM_PlayBackStop
        *函数说明:  停止回放
        *输入参数:  handle	DM_PlayBack函数的返回值
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayBackStop")]
        public static extern int DM_PlayBackStop(int handle);

        /*
        *函数名称:	DM_PlayBackSpeed
        *函数说明:  步进播放
        *输入参数:  
                    handle	DM_PlayBack函数的返回值
                    speed	步进速度
                    -4     1/4倍速 
                    -2     1/2倍速
                    1      正常速度
                    2      2倍速
                    4      4倍速


        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayBackSpeed")]
        public static extern int DM_PlayBackSpeed(int handle, int speed);

        /*
        *函数名称:	DM_PlayBackStep
        *函数说明:  单步播放
        *输入参数:  
                    handle	DM_PlayBack函数的返回值
        *返回值:	>=0 成功 
                    <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PlayBackStep")]
        public static extern int DM_PlayBackStep(int handle);

        /******************************************************************************************************************/
        /*  云台控制                                                                                                   */
        /******************************************************************************************************************/
        //云台协议
        public enum DALI_PTZ_PROTOCOL
        {
            PELCO_D = 1,
            PELCO_P1,
            PELCO_P2,
            PELCO_P3,
            DL_0001,
            FASTRAX,
            PANASONIC,
            ELEC,
            SAMSUNG,
            KATATEL,
            HD_600,
            T3609HD,
            LILIN,
            PELCO9760,
            KRE301,
            PIH_1016,
            PD_CONST,
            PD_NW,
            JC4116,
            SONY,
            YAAN,
            ENKEL,
            PLD,
        }

        public enum DALI_MOVEMENT_DIR
        {
            DALI_KEY_STOP,          //停止
            DALI_KEY_LEFT,      //	向左
            DALI_KEY_RIGHT,     //	向右
            DALI_KEY_UP,        //		向上
            DALI_KEY_DOWN,  //	向下
            DALI_KEY_LEFT_UP,   //	左上
            DALI_KEY_LEFT_DOWN,//	左下
            DALI_KEY_RIGHT_UP,  //	右上
            DALI_KEY_RIGHT_DOWN,//右下
            DALI_KEY_ZOOM_IN,
            DALI_KEY_ZOOM_OUT,
            DALI_KEY_FOCUS_NEAR,
            DALI_KEY_FOCUS_FAR,
        }

        public enum DALI_PRESET_DIR
        {
            DALI_KEY_PRESET_SAVE,   //保存、设置
            DALI_KEY_PRESET_CALL,   //调用
            DALI_KEY_PRESET_AUTO,   //自动
            DALI_KEY_PRESET_CLEAR,  //清除
            DALI_KEY_PRESET_ADD,    //	添加
        }

        public enum DALI_CONTROL_CMD_DIR
        {
            DALI_YUN_CONTROL,   //Yuntai
            DALI_LENS_CONTROL,  //Lens
            DALI_IRIS_CONTROL,  //Aperture
        }
        /*
        *函数名称:	DM_PTZSettings
        *函数说明:  云台初始化设置
        *输入参数:  
                    handle：	句柄
                    Protocol：	云台控制协议（参见DALI_PTZ_PROTOCOL中的定义, 目前支持Pelco_D 和 YAAN）
                    NAddrID：	云台地址

        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PTZSettings")]
        public static extern void DM_PTZSettings(int handle, DALI_PTZ_PROTOCOL Protocol = DALI_PTZ_PROTOCOL.PELCO_D, int nAddrID = 1);

        /*
        *函数名称:	DM_PTZSpeed
        *函数说明: 云台速度控制
        *输入参数:  
                    handle：			句柄
                    nSpeedTrgID：   	运动方向	
                    #define SPEED_TRG_PAN	0	//水平
                    #define SPEED_TRG_TILT	1	//垂直
                    nSpeed：			速度

        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PTZSpeed")]
        public static extern void DM_PTZSpeed(int handle, int nSpeedTrgID, int nSpeed = 1);

        /*
        *函数名称:	DM_PTZControl
        *函数说明:  云台控制
        *输入参数:  
                    handle：		句柄
                    ctrlCmd：		控制命令
                    Movement		移动方向
        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PTZControl")]
        public static extern void DM_PTZControl(int handle, DALI_CONTROL_CMD_DIR ctrlCmd, DALI_MOVEMENT_DIR Movement);

        /*
        *函数名称:	DM_PTZPreset
        *函数说明:  云台预置位控制
        *输入参数:  
                    handle：		句柄
                    Preset：		DALI_PRESET_DIR中定义
                    nPoint：		预置点
        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_PTZPreset")]
        public static extern void DM_PTZPreset(int handle, DALI_PRESET_DIR Preset, int nPoint);

        /*****************************************************************************************************************/
        /* 其他                                                                                                          */
        /*****************************************************************************************************************/

        /*
        *函数名称:	DM_GetStatus
        *函数说明:  获取仪器工作状态, 具体类型参见 enumTestDeviceStatus
        *输入参数:  handle：		句柄
        *返回值:    DM60的当前状态
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetStatus")]
        public static extern int DM_GetStatus(int handle);

        /*
        *函数名称:	DM_KBDControl
        *函数说明:  仪器模拟键盘控制
        *输入参数:  
                    handle 句柄；
                    nValue 值：
                    7		保存键 	9		向左键 
                    10		向上键  11		取消键
                    12		向右键 	13		向下键
                    14		确定键

        *返回值:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_KBDControl")]
        public static extern void DM_KBDControl(int handle, int nValue);

        /*
        *函数名称:	DM_AutoAdjust
        *函数说明:  触发仪器调零，不适用于Jxx系列2017.11.17之后的版本
        *输入参数:  
                    handle 句柄；
        *返回值:	>0 成功			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_AutoAdjust")]
        public static extern int DM_AutoAdjust(int handle);

        /*
            函数名称:	DM_AutoAdjustForJxx
        *函数说明:	触发仪器调零，适用于Jxx系列2017.11.17之后的版本
        *输入参数:  handle 句柄；
        *           
        *返回值:	>0 成功			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_AutoAdjustForJxx")]
        public static extern int DM_AutoAdjustForJxx(int handle);

        /*	函数名称: DM_Open
            *	函数说明：打开仪器前端
            *   输入参数：	handle 句柄；
            *	返回值： >=0 成功
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Open")]
        public static extern int DM_Open(int handle);


        /*	函数名称: DM_Close
            *	函数说明：关闭仪器前端
            *	输入参数：handle 句柄；
            *	返回值： >=0 成功
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Close")]
        public static extern int DM_Close(int handle);


        /*	函数名称: DM_GetRemoteAlarm
            *	函数说明：获取仪器端报警状态, 
            *	输入参数：handle：		句柄
            *	返回值：器端报警状态(0：关闭, 1：打开)
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRemoteAlarm")]
        public static extern int DM_GetRemoteAlarm(int handle);


        /*	函数名称: DM_GetVideoMode
            *	函数说明: 获取图像手动/自动模式
            *	输入参数:handle：		句柄
            *	返回值：模式(0、手动, 1、自动)
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetVideoMode")]
        public static extern int DM_GetVideoMode(int handle);

        /*	函数名称: DM_GetMAC
        *	函数说明：	获取MAC地址
        *	输入参数：handle：	句柄
        *			  Mac ：		地址
        *	返回值： N/A
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetMAC")]
        public static extern void DM_GetMAC(int handle, StringBuilder Mac);

        /*	函数名称: DM_GetRemoteLanguage
        *	函数说明：获取仪器的语言
        *	输入参数：handle		句柄
        *	返回值： 仪器的语言(0：英文, 1：中文)
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetRemoteLanguage")]
        public static extern int DM_GetRemoteLanguage(int handle);

        /*	函数名称: DM_SetRemoteLanguage
        *	函数说明：设置仪器的语言
        *	输入参数：handle		句柄
        *			  iLanguage    仪器的语言标志（0:英文, 1:中文, 2:繁体中文, 3:法文, 4:德文, 5:西班牙文, 6:意大利文, 7:韩文, 8:俄文, 9:日文, 10:瑞典语）
                                    （11:葡萄牙语, 12:土耳其语, 13:波兰语, 14:捷克语, 15:匈牙利语, 16:希腊语, 17:荷兰语）
        *	返回值： >=0 成功
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetRemoteLanguage")]
        public static extern int DM_SetRemoteLanguage(int handle, int iLanguage);

        /*	函数名称: DM_Reset
        *	函数说明：重启仪器
        *	输入参数：handle		句柄
        *	返回值： >=0 成功
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Reset")]
        public static extern int DM_Reset(int handle);

        //-----------------------------------------------------
        //2010-11-01,以下接口为兼容老的DM60机型而特意增加

        /*
        input:
        handle: 	Handle
        stream:		stream buffer pointer
        len:		buffer length
        err:		error code 
        0	//success
        1	//stop by user
        2	//stop by receive
        data:		user define, in DM_ReceiveMonitorStream
        */
        public delegate int STREAMCALL(int handle, int dataType, ref byte stream, int len, int err, IntPtr cbpara);
        //typedef int (__stdcall* STREAMCALL) (int handle, int dataType, BYTE* stream, int len, int err, void* cbpara);
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetVersion")]
        public static extern void DM_GetVersion(ref string Version);

        // 以下函数均为早期的DM60仪器所用，现已废弃
        /*
        Description：	open local alram
        Input：
        handle		Handle
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_OpenLocalAlarm")]
        public static extern int DM_OpenLocalAlarm(int handle);

        /*
        Description：	close local alram
        Input：
        handle		Handle
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CloseLocalAlarm")]
        public static extern int DM_CloseLocalAlarm(int handle);

        /*
        Description：	set local alram type
        Input：
        handle:		Handle
        type:		0: Min 1: Max 2: Diff
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetLocalAlarmType")]
        public static extern int DM_SetLocalAlarmType(int handle, int type);

        /*
        Description：	get local alram type
        Input：
        handle		Handle
        Return:
        0: Min 1: Max 2: Diff
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetLocalAlarmType")]
        public static extern int DM_GetLocalAlarmType(int handle);

        /*
        Description：	set local alram mode
        Input：
        handle:		Handle
        mode:		 0: > 1: <
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetLocalAlarmMode")]
        public static extern int DM_SetLocalAlarmMode(int handle, int mode);

        /*
        Description：	get local alram mode
        Input：
        handle:		Handle
        Return：
        0: > 1: <
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetLocalAlarmMode")]
        public static extern int DM_GetLocalAlarmMode(int handle);

        /*
        Description：	set local alram refe 
        Input：
        handle		Handle
        refe：	
        0、Close
        1、	Refe Tempature, value by DM_SetLocalAlarmTemp
        2 - 5 ：spot (1-4) tempature
        6 - 8 ：area (1-3) tempature
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetLocalAlarmRefe")]
        public static extern int DM_SetLocalAlarmRefe(int handle, int refe);

        /*
        Description：	get local alram refe
        Input：
        handle:		Handle
        Return：
        0、Close
        1、	Refe Tempature, value by DM_SetLocalAlarmTemp
        2 - 5 ：spot (1-4) tempature
        6 - 8 ：area (1-3) tempature
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetLocalAlarmRefe")]
        public static extern int DM_GetLocalAlarmRefe(int handle);

        /*
        Description：	set local alram tempature
        Input：
        handle:		Handle
        temp:		tempature
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetLocalAlarmTemp")]
        public static extern int DM_SetLocalAlarmTemp(int handle, float temp);

        /*
        Description：	get local alram tempature
        Input：
        handle:		Handle
        Return：
        tempature
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetLocalAlarmTemp")]
        public static extern float DM_GetLocalAlarmTemp(int handle);

        /*
        Description：get device system info
        Input：
        handleL:	Handle
        output：
        SysInfo:	see tagSystemInfo
        Return：
        >=0:    Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetSysInfo")]
        public static extern void DM_GetSysInfo(int handle, ref tagSystemInfo SysInfo);

        /*
        input:
        ip:			ip address
        port:		port
        channel:	channel id
        callback:	see STREAMCALL
        data:		uer data, STREAMCALL paramter
        Return:
        =0: success 1:fail
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ReceiveMonitorStream")]
        public static extern int DM_ReceiveMonitorStream(ref string ip, ushort port, int channel, STREAMCALL callback, IntPtr data);

        /*
        Input:	
        handle:	  by DLDVR_ReceiveMonitorStream return
        Return:
        =0: success <0: fail
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopMonitorStream")]
        public static extern int DM_StopMonitorStream(int handle);

        //下面几个接口尚未实现--------------------------------------------------------begin
        //hWnd: WM_DM_PLAYER 消息的 wParam 返回的句柄
        //public static extern int  DM_OpenMonitorEX(HWND hWnd, int frameRate, int fmt, int rl);
        //public static extern int  DM_SetStreamBuf(int handle, BYTE* buf, int len);
        //public static extern int  DM_EmptyStreamBuf(int handle);
        //上面几个接口尚未实现--------------------------------------------------------end

        /*
        Description：	Yuntai Control
        Input：
        handle		Handle
        cmd   	Control command
        len		command Len
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_YTControl")]
        public static extern int DM_YTControl(int handle, ref string cmd, int len);

        //tempature struct
        struct stTempDest
        {
            int iType;
            int iNumber;
            int iPointX;
            int iPointY;
            double dblTemp;
        }

        /*
        Description：	init callback of real tempature
        Input：
        handle：		Handle
        pFun：			CallBack function
        Return：
        NULL
        */
        //[DllImport("DMSDK.dll", EntryPoint = "DM_PTZPreset")]
        //public static extern void  DM_GetRealTempObject(int handle, int(__stdcall* pFun)(int, stTempDest[], int));
        // 以上函数均为早期的DM60仪器所用，现已废弃

        //以下函数属于检验检疫定制仪器所用
        /*
        Description：	enable natural termpature range
        Input：
        handle：		Handle
        iEnable：		0: disable 1: enable
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetNaturalTempRangeEnable")]
        public static extern int DM_SetNaturalTempRangeEnable(int handle, int iEnable);

        /*
        Description：	get natural termpature range status
        Input：
        handle：		Handle
        Return：
        0: disable 1: enable
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetNaturalTempRangeEnable")]
        public static extern int DM_GetNaturalTempRangeEnable(int handle);

        /*
        Description：	set termpature range
        Input：
        handle：	Handle
        HighTemp：	High tempature * 100
        LowTemp:	Low Tempature * 100
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetNaturalTempRange")]
        public static extern int DM_SetNaturalTempRange(int handle, int LowTemp, int HighTemp);

        /*
        Description：	get termpature range
        Input：
        handle：	Handle
        Return：
        >=0:     Success
        HighTemp：	High tempature * 100
        LowTemp:	Low Tempature * 100
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetNaturalTempRange")]
        public static extern int DM_GetNaturalTempRange(int handle, ref int LowTemp, ref int HighTemp);

        /*
        Description：	enable intellect Measure Tempature
        Input：
        handle：	Handle
        nIntellect:	0: diable 1: enbale
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetIntellectMeasureTemp")]
        public static extern int DM_SetIntellectMeasureTemp(int handle, int nIntellect);

        /*
        Description：	get intelleect measure tempature
        Input：
        handle：	Handle
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetIntellectMeasureTemp")]
        public static extern int DM_GetIntellectMeasureTemp(int handle);

        /*
        Description：	set Black Board Paramter
        Input：
        handle：		Handle
        nStartX:		LeftUp X value
        nStartY:		LeftUp Y value
        nEndX:			RightDown X value
        nEndY:			RightDown Y value
        dblBlackTemp:	BlackBoard tempature * 100
        dblBlackEmiss:	Emiss * 100
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetBlackBoardPara")]
        public static extern int DM_SetBlackBoardPara(int handle, int nStartX, int nStartY, int nEndX, int nEndY,
                                    int dblBlackTemp, int dblBlackEmiss);

        /*
        Description：	Get Black Board Paramter
        Input：
            handle：	Handle
        Return：
            >=0:     Success
            nStartX:		LeftUp X value
            nStartY:		LeftUp Y value
            nEndX:			RightDown X value
            nEndY:			RightDown Y value
            dblBlackTemp:	BlackBoard tempature * 100
            dblBlackEmiss:	Emiss * 100
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetBlackBoardPara")]
        public static extern int DM_GetBlackBoardPara(int handle, ref int nStartX, ref int nStartY, ref int nEndX, ref int nEndY,
                                    ref int dblBlackTemp, ref int dblBlackEmiss);

        /*
        Description：	set intellect tempature range
        Input：
        handle：	Handle
        HighTemp:	High tempature * 100
        nLowerTemp:	Lower Tempature * 100
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetIntellectTempRange")]
        public static extern int DM_SetIntellectTempRange(int handle, int HighTemp, int nLowerTemp);

        /*
        Description：	Get Intellect Tempature Range
        Input：
        handle：	Handle
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetIntellectTempRange")]
        public static extern int DM_GetIntellectTempRange(int handle);

        /*
        Description：	set shield Region
        Input：
        handle：	Handle
        nID:		Area ID
        nStatus:	0: disable 1: enable
        nStartX,nStartY:	coordinate Leftup 
        nEndX,nEndY:		coordinate RightDown 
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetShieldRegion")]
        public static extern int DM_SetShieldRegion(int handle, int nID, int nStatus, int nStartX, int nStartY, int nEndX, int nEndY);

        /*
        Description：	Get Shield Region
        Input：
        handle：	Handle
        nID:		Area ID
        Return：
        >=0:     Success
        nStatus:	0: disable 1: enable
        nStartX,nStartY:	coordinate Leftup 
        nEndX,nEndY:		coordinate RightDown 
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetShieldRegion")]
        public static extern int DM_GetShieldRegion(int handle, int nID, ref int nStatus, ref int nStartX, ref int nStartY, ref int nEndX, ref int nEndY);

        /*
        Description：	Get All Shield Region
        Input：
        handle：	Handle
        nCount:		Area Count
        Return：
        >=0:     Success
        pShieldRegion:	All shield region info
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetShieldRegionAll")]
        public static extern int DM_GetShieldRegionAll(int handle, ref int nCount, tagShieldRegion[] pShieldRegion);

        /*
        Description：	set pallette range
        Input：
        handle：		Handle
        nHighTemp:		the upper tempature * 100
        nLowTemp:		the lower tempature * 100
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetPalletteTempRange")]
        public static extern int DM_SetPalletteTempRange(int handle, int HighTemp, int nLowerTemp);

        /*
        Description：	get pallette range
        Input：
        handle：	Handle
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetPalletteTempRange")]
        public static extern int DM_GetPalletteTempRange(int handle);

        /*
        Description：	enable auto ambient tempature 
        Input：
        handle：	Handle
        nStatus:	0: disable 1: enable
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAutoAmbientTemp")]
        public static extern int DM_SetAutoAmbientTemp(int handle, int nStatus);

        /*
        Description：	enable auto ambient tempature 
        Input：
        handle：	Handle
        nStatus:	0: disable 1: enable
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAutoAmbientTempStatus")]
        public static extern int DM_GetAutoAmbientTempStatus(int handle);

        //Test funciton,dont use 
        [DllImport("DMSDK.dll", EntryPoint = "DM_Test")]
        public static extern int DM_Test(int handle, ref byte[] Test, int nLen);

        /**
        @breif Open Active Test
        @param handle : Handle
        @retval >0 success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_OpenActiveTest")]
        public static extern int DM_OpenActiveTest(int handle);

        /**
        @breif Close Active Test
        @param handle : Handle
        @retval >0 success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CloseActiveTest")]
        public static extern int DM_CloseActiveTest(int handle);

        /**
        @breif Get Lens ID
        @param handle : Handle
        @return return lens id
        @retval >0 Lens ID
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetLensID")]
        public static extern int DM_GetLensID(int handle);

        /**
        @breif Set Lens ID
        @param handle : Handle
        @param ParamValue : LENS ID
        @return return lens id
        @retval >0 Lens ID
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetLensID")]
        public static extern int DM_SetLensID(int handle, int ParamValue);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTempLimit")]
        public static extern int DM_GetTempLimit(int handle);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetTempLimit")]
        public static extern int DM_SetTempLimit(int handle, int HighTemp);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetEdgeradiiEnable")]
        public static extern int DM_SetEdgeradiiEnable(int handle, bool Enable);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetEdgeradiiEnable")]
        public static extern bool DM_GetEdgeradiiEnable(int handle);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetEdgeradii")]
        public static extern int DM_SetEdgeradii(int handle, int Edgeradii);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetEdgeradii")]
        public static extern int DM_GetEdgeradii(int handle);
        //以上函数属于检验检疫定制仪器所用

        /*
            函数名称:	DM_CheckOnline
        *函数说明:	检测主机是否在线
        *输入参数:  IPAddr：主机IP
                    Port：主机端口 
        *返回值:正数表示在线, 负数表示不在线			
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_CheckOnline")]
        public static extern int DM_CheckOnline(string IPAddr, int Port);

        /*
            函数名称:	DM_ClearAllJpeg
        *函数说明:	清除主机上的所有JPEG图片
        *输入参数:  handle
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ClearAllJpeg")]
        public static extern int DM_ClearAllJpeg(int handle);

        /*
            函数名称:	DM_BrightAdjust
        *函数说明:	亮度调节
        *输入参数:  handle
        *           step,步长, +1, 表示增加1,  -1, 表示减小1
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_BrightAdjust")]
        public static extern int DM_BrightAdjust(int handle, int step);

        /*
            函数名称:	DM_ContrastAdjust
        *函数说明:	增益调节
        *输入参数:  handle
        *           step,步长, +1, 表示增加1,  -1, 表示减小1
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ContrastAdjust")]
        public static extern int DM_ContrastAdjust(int handle, int step);

        /*
            函数名称:	DM_RemoteJpeg
        *函数说明:	远程拍照
        *输入参数:  handle
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_RemoteJpeg")]
        public static extern int DM_RemoteJpeg(int handle);

        /*
        函数名称:	DM_Zoom，与 DM_GetZoomStatus 成对，仅适用于S730
        *函数说明:	电子放大
        *输入参数:  handle
        *			value, 放大倍数。0表示1倍; 1表示2倍
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_Zoom")]
        public static extern int DM_Zoom(int handle, int value);


        /*
        函数名称:	DM_SetPalority
        *函数说明:	设置热图像模式
        *输入参数:  handle
        *			value, 1-白热； 0-黑热
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetPalority")]
        public static extern int DM_SetPalority(int handle, int value);

        /*
        函数名称:	DM_GetCapacity
        *函数说明:	获取S730机型的容量
        *输入参数:  handle
        *返回值:容量值, 若返回负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetCapacity")]
        public static extern int DM_GetCapacity(int handle);

        /*
        函数名称:	DM_GetBright_S730
        *函数说明:	获取S730机型的亮度值
        *输入参数:  handle
        *返回值:亮度值, 范围在-2048-2048, 返回其它值为失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetBright_S730")]
        public static extern int DM_GetBright_S730(int handle);

        /*
        函数名称:	DM_GetContrast_S730
        *函数说明:	获取S730机型的对比度
        *输入参数:  handle
        *返回值:对比度, 范围在0-255, 返回其它值为失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetContrast_S730")]
        public static extern int DM_GetContrast_S730(int handle);

        /*
        函数名称:	DM_GetZoomStatus，与 DM_Zoom 成对，仅适用于S730
        *函数说明:	获取图像放大倍数
        *输入参数:  handle
        *返回值:放大倍数。0-1倍； 1-2倍	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetZoomStatus")]
        public static extern int DM_GetZoomStatus(int handle);

        /*
        函数名称:	DM_GetPalority
        *函数说明:	获取热图像模式
        *输入参数:  handle
        *返回值:1-白热； 0-黑热		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetPalority")]
        public static extern int DM_GetPalority(int handle);


        /*
        函数名称:	DM_GetGFZ
        *函数说明:	获取图像冻结状态
        *输入参数:  handle
        *返回值:0-冻结； 1-非冻结
        *注意：S730和DM60-S机型适用	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetGFZ")]
        public static extern int DM_GetGFZ(int handle);

        /*
            函数名称:	DM_SetEIS
        *函数说明:	打开/关闭集成电子稳像功能
        *输入参数:  handle
        *           cmd,  1-打开   0-关闭
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetEIS")]
        public static extern int DM_SetEIS(int handle, int cmd);

        /*
            函数名称:	DM_SetFiltering
        *函数说明:	打开/关闭滤波功能
        *输入参数:  handle
        *           cmd,  1-打开   0-关闭
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetFiltering")]
        public static extern int DM_SetFiltering(int handle, int cmd);

        /*
            函数名称:	DM_SetEnhancement
        *函数说明:	打开/关闭图像增强功能
        *输入参数:  handle
        *           cmd,  1-打开   0-关闭
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetEnhancement")]
        public static extern int DM_SetEnhancement(int handle, int cmd);

        /*
            函数名称:	DM_SetFlip
        *函数说明:	打开/关闭图像垂直翻转功能
        *输入参数:  handle
        *           cmd,  1-打开   0-关闭
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetFlip")]
        public static extern int DM_SetFlip(int handle, int cmd);

        /*
            函数名称:	DM_SetMirror
        *函数说明:	打开/关闭图像水平翻转功能
        *输入参数:  handle
        *           cmd,  1-打开   0-关闭
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetMirror")]
        public static extern int DM_SetMirror(int handle, int cmd);

        /*
            函数名称:	DM_GetFiltering
        *函数说明:	获得滤波功能状态
        *输入参数:  handle
        *返回值: 1-打开   0-关闭	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetFiltering")]
        public static extern int DM_GetFiltering(int handle);

        /*
            函数名称:	DM_GetEnhancement
        *函数说明:	获得图像增强功能状态
        *输入参数:  handle
        *返回值:    1-打开   0-关闭		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetEnhancement")]
        public static extern int DM_GetEnhancement(int handle);

        /*
            函数名称:	DM_GetFlip
        *函数说明:	获得图像垂直翻转功能状态
        *输入参数:  handle
        *返回值:	1-打开   0-关闭
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetFlip")]
        public static extern int DM_GetFlip(int handle);

        /*
            函数名称:	DM_GetMirror
        *函数说明:	获得图像水平翻转功能状态
        *输入参数:  handle
        *返回值:	1-打开   0-关闭
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetMirror")]
        public static extern int DM_GetMirror(int handle);

        /*
            函数名称:	DM_GetDistance
        *函数说明:  激光测距
        *输入参数:  handle		句柄			
        *输出参数:  status,仪器状态 0-准备好  1-正常测距  2-开机自检
        *			distance, 距离, status=1时有效
        *返回值:正数表示成功, 负数表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetDistance")]
        public static extern int DM_GetDistance(int handle, out int status, ref tagDistanceInfo distance);


        /************************************************************************/
        //   回调函数定义                                                                  
        /************************************************************************/

        //解码后的视频帧回调函数定义, dwFrameRate 表示帧率(1---25)
        public delegate void fYUVDataCallBack(int handle, ulong dwFrameRate, ref byte[] pBuffer,
                                                        ulong nWidth, ulong nHeight, int err, ulong dwUser);
        //typedef void (CALLBACK* fYUVDataCallBack) (int handle, unsigned long dwFrameRate, unsigned char* pBuffer,
        //unsigned long nWidth, unsigned long nHeight, int err, unsigned long dwUser);

        /* 	功能说明:设置用于YUV视频回调的回调函数
        *	输入参数: lRealHandle, 监视句柄, 即WM_DM_PLAYER 消息的 wParam 返回的句柄
        yuvDataCallBack, 回调函数, 用于回调YUV数据
        dwUser, 用户自定义数据
        *	输出参数: 无
        *	函数返回: TRUE：成功； FALSE：失败
        *	说明:
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetYUVDataCallBack")]
        public static extern bool DM_SetYUVDataCallBack(int lRealHandle, fYUVDataCallBack yuvDataCallBack, ulong dwUser);

        /*
            函数名称:	DM_AutoCheck
        *函数说明:  激光测距自检
        *输入参数:  handle		句柄			
        *输出参数:  无
        *返回值:正数表示成功, 负数表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_AutoCheck")]
        public static extern int DM_AutoCheck(int handle);


        /*
            函数名称:	DM_VideoStable
        *函数说明:  打开/关闭电子稳像处理
        *输入参数:  lRealHandle, 监视句柄, 即WM_DM_PLAYER 消息的 wParam 返回的句柄
        *			cmd, 1-打开电子稳像 0-关闭电子稳像			
        *输出参数:  无
        *返回值:正数表示成功, 负数表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_VideoStable")]
        public static extern int DM_VideoStable(int lRealHandle, int cmd);

        /*
            函数名称:	DM_SetGFZStatus
        *函数说明:  打开/关闭图像冻结
        *输入参数:  lRealHandle, 监视句柄, 即WM_DM_PLAYER 消息的 wParam 返回的句柄
        *			cmd, 1-图像冻结 0-解除冻结			
        *输出参数:  无
        *返回值:正数表示成功, 负数表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetGFZStatus")]
        public static extern int DM_SetGFZStatus(int lRealHandle, int cmd);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetTemperatureScope")]
        public static extern bool DM_SetTemperatureScope(int handle, int dwValue1, int dwValue2);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTemperatureScope")]
        public static extern bool DM_GetTemperatureScope(int handle, out int dwValue1, out int dwValue2);

        /*
            函数名称:	DM_GetIPAddress
        *函数说明:  获取IP地址
        *输入参数: handle：	句柄			
        *输出参数:  IPAddress, IP地址
        *返回值:TRUE表示成功, FALSE表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetIPAddress")]
        public static extern int DM_GetIPAddress(int handle, StringBuilder IPAddress);

        /*
            函数名称:	DM_GetNetmask
        *函数说明:  获取子网掩码
        *输入参数: handle：	句柄			
        *输出参数:  Netmask, 子网掩码
        *返回值:TRUE表示成功, FALSE表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetNetmask")]
        public static extern int DM_GetNetmask(int handle, StringBuilder Netmask);

        /*
            函数名称:	DM_GetGateway
        *函数说明:  获取网关
        *输入参数: handle：	句柄			
        *输出参数:  Gateway, 网关
        *返回值:TRUE表示成功, FALSE表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetGateway")]
        public static extern int DM_GetGateway(int handle, StringBuilder Gateway);


        public delegate void fMessCallBack(int msg, IntPtr pBuf, int dwBufLen, uint dwUser);//
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAllMessCallBack")]
        public static extern void DM_SetAllMessCallBack(fMessCallBack messCallBack, uint dwUser = 0);
    

        /*
            函数名称:	DM_GetDM6xResolution
        *函数说明:  获取DM6x机型的主机分辨率
        *输入参数:  handle		句柄			
        *输出参数:  resolution,主机分辨率
        *返回值:>=0表示成功, 负数表示失败	
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetDM6xResolution")]
        public static extern int  DM_GetDM6xResolution(int handle,ref tagResolutionInfo resolution);

        /*
            函数名称:	DM_ControlServoMotor
        *函数说明:	控制舵机
        *输入参数:  handle：		句柄
                    int value: 		舵机目标状态（0：表示拉起, 1：表示挡下）
        *返回值:正数表示成功, 负数表示失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ControlServoMotor")]
        public static extern int  DM_ControlServoMotor(int handle, int value);

        //以下3个接口尚未实现

        //[DllImport("DMSDK.dll", EntryPoint = "DM_OpenIfrVideo")]
        //public static extern int  DM_OpenIfrVideo(IntPtr hwnd, ref string ip, ushort port, int(__stdcall* pFun)(char*, int));

        [DllImport("DMSDK.dll", EntryPoint = "DM_SaveIfr")]
        public static extern int  DM_SaveIfr(ref string strFileName);

        [DllImport("DMSDK.dll", EntryPoint = "DM_StopIfr")]
        public static extern int  DM_StopIfr();

        /*
            函数名称:	DM_ShowMenu
        *函数说明:	显示调试菜单
                    仅对 DM6x 二期或以后的有效
        *输入参数:  handle：		句柄
        *返回值:	>0 成功, <0失败		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_ShowMenu")]
        public static extern int  DM_ShowMenu(int handle);

        /*
            函数名称:	DM_GetDeviceVer
        *函数说明:	获取DM60大版本号
        *输入参数:  handle		句柄
        *返回值:    当前DM60大版本号		
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetDeviceVer")]
        public static extern int  DM_GetDeviceVer(int handle);

        /*
        Description：	Ajust tempature 
        Input：
        handle：	Handle
        nStatus:	0: Manual 1: Auto
        nTemp:		Tempature * 100
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAjustTemp")]
        public static extern int  DM_SetAjustTemp(int handle, int nStatus, int nTemp);

        /*
        Description：	Ajust Position 
        Input：
        handle：	Handle
        nStatus:	0: DisEnable 1: Enable
        IPCPos1X, IPCPos1Y:		CCD coordinate LeftUp
        IPCPos2X, IPCPos2Y:		CCD coordinate RightDown
        DMPos1X, DMPos1Y:		IR coordinate LeftUp
        DMPos2X, DMPos2Y:		IR coordinate RightDown
        Return：
        >=0:     Success
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAjustPosition")]
        public static extern int  DM_SetAjustPosition(int handle, int nStatus, int IPCResolution,
                                int IPCPos1X, int IPCPos1Y, int IPCPos2X, int IPCPos2Y,
                                int DMPos1X, int DMPos1Y, int DMPos2X, int DMPos2Y);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmSenseRadius")]
        public static extern int  DM_SetAlarmSenseRadius(int handle, int nRadius);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmRadius")]
        public static extern int  DM_SetAlarmRadius(int handle, int nRadius);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmTime")]
        public static extern int  DM_SetAlarmTime(int handle, int nTime);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetTimeOSD")]
        public static extern int  DM_SetTimeOSD(int handle, int nStatus, int StartX, int StartY, int Size = 1);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAjustTemp")]
        public static extern int  DM_GetAjustTemp(int handle,ref int nStatus,ref int nTemp);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAjustPosition")]
        public static extern int  DM_GetAjustPosition(int handle,ref int nStatus,ref int IPCResolution,
                                ref int IPCPos1X,ref int IPCPos1Y, ref int IPCPos2X,ref int IPCPos2Y,
                                ref int DMPos1X,ref int DMPos1Y,ref int DMPos2X,ref int DMPos2Y);
        /*
        return:
            >=0 时间
            <0 失败
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAlarmTime")]
        public static extern int  DM_GetAlarmTime(int handle);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetTimeOSD")]
        public static extern int  DM_GetTimeOSD(int handle,ref int nStatus,ref int StartX,ref int StartY,ref int Size);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetPointTemp")]
        public static extern int  DM_GetPointTemp(int handle, int X, int Y,ref int nTemp);

        [DllImport("DMSDK.dll", EntryPoint = "DM_RemoteUpdate")]
        public static extern int  DM_RemoteUpdate(int handle, ref string path); //Net Software

        [DllImport("DMSDK.dll", EntryPoint = "DM_UpdateConfig")]
        public static extern int  DM_UpdateConfig(int handle, ref string path); //Net Software Config
                                                                            /*
                    Type:
                        #define UPDATEFILE_TYPE_DM60_MID		0
                        #define UPDATEFILE_TYPE_DM60_FIREWARE	1
                        #define UPDATEFILE_TYPE_DM60_DATA		2
                    */
        [DllImport("DMSDK.dll", EntryPoint = "DM_UpdateDM60File")]
        public static extern int  DM_UpdateDM60File(int handle, ref string path, int Type); //Main Software

        [DllImport("DMSDK.dll", EntryPoint = "DM_DownloadMTC")]
        public static extern int  DM_DownloadMTC(int handle, ref string path); //Download MTC

        [DllImport("DMSDK.dll", EntryPoint = "DM_DownloadDLV")]
        public static extern int  DM_DownloadDLV(int handle, ref string path); //Download DLV, 保留

        //hwnd: WM_DM_PLAYER 消息的 wParam 返回的句柄
        [DllImport("DMSDK.dll", EntryPoint = "DM_OpenMonitor_Jpeg")]
        public static extern int DM_OpenMonitor_Jpeg(IntPtr hwnd, ref string ip, ushort port, int channel = 0); //JPEG Mode

        [DllImport("DMSDK.dll", EntryPoint = "DM_CloseMonitor_Jpeg")]
        public static extern int  DM_CloseMonitor_Jpeg(int handle); //JPEG Mode

        //typedef void (CALLBACK* fTempAlarmCallBack) (unsigned char* pBuffer, unsigned long dwBufSize);//long lHandle, 
        public delegate void fTempAlarmCallBack(ref byte pBuffer, ulong dwBufSize);
        [DllImport("DMSDK.dll", EntryPoint = "DM_TempAlarm")]
        public static extern long  DM_TempAlarm(long handle, fTempAlarmCallBack TempAlarmCallBack);

        //接收报警图片
        [DllImport("DMSDK.dll", EntryPoint = "DM_ReceiveAlarmJpeg")]
        public static extern int   DM_ReceiveAlarmJpeg(int handle, ref string Path);

        [DllImport("DMSDK.dll", EntryPoint = "DM_PTZPreset")]
        public static extern int  DM_GetUpgradePos(int handle);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetMeasureString")]
        public static extern bool   DM_GetMeasureString(int handle, int type, int index, ref string DMMeasureBuf);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetTCPPort")]
        public static extern int   DM_SetTCPPort(int handle, int port);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetUDPPort")]
        public static extern int   DM_SetUDPPort(int handle, int port);

        [DllImport("DMSDK.dll", EntryPoint = "DM_SetListenPort")]
        public static extern int   DM_SetListenPort(int handle, int port);

        //设置移动侦测参数
        /*
            EnableAlarm: 开启/关闭移动侦测
            Sens: 灵敏度，0: LOW; 1: MIDDLE; 2: HIGH
            MontionArea: 布防区域
            Threshold: 侦测阈值，0~100
            AlarmOut1: 告警输出开关，预留
            AlarmTime1: 告警输出时间，预留
            AlarmOut2: 告警输出开关，预留
            AlarmTime2: 告警输出时间，预留
            ActiveTime1: 是否开启，以及开启时间
            ActiveTime2: 是否开启，以及开启时间
            ActiveTimeSet: 布防时间
            AlarmLinkOutInfo: 告警联动方式，Rec、FTP、EMail多选
            FtpPicNum: 告警时，上传几张图片到Ftp服务器
            EMailPicNum: 告警时，通过EMail发送几张图片
            RecTime: 告警时，录像时长
            PreRecordTime: 启动语录时长，0 ~ 10秒
            iMailContentType:	发送告警信息时是否发送图片。1: 纯文本; 2: 文本+图片
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetMotion")]
        public static extern int  DM_SetMotion(int handle, bool EnableAlarm, int Sens, ref string MontionArea, int Threshold,
                                bool AlarmOut1, int AlarmTime1, bool AlarmOut2, int AlarmTime2, ref string ActiveTime1,
                                ref string ActiveTime2, ref string ActiveTimeSet, ref string AlarmLinkOutInfo,
                                int FtpPicNum, int EMailPicNum, int RecTime, int PreRecordTime, int iMailContentType);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetMotion")]
        public static extern int DM_GetMotion(int handle, ref bool EnableAlarm, ref int Sens, ref string MontionArea, ref int Threshold,
                                ref bool AlarmOut1, ref int AlarmTime1, ref bool AlarmOut2, ref int AlarmTime2, ref string ActiveTime1,
                                ref string ActiveTime2, ref string ActiveTimeSet, ref string AlarmLinkOutInfo,
                                ref int FtpPicNum, ref int EMailPicNum, ref int RecTime, ref int PreRecordTime, ref int iMailContentType);

        //设置灰度报警信息
        /*
            AlarmTemp: 报警温度，精确到小数点后1位，单位为℃
            AlarmColor: 跟AlarmTemp对应
            AlarmLinkOutInfo: 告警联动方式，Rec、FTP、EMail多选
            FtpPicNum: 告警时，上传几张图片到Ftp服务器
            EMailPicNum: 告警时，通过EMail发送几张图片
            RecTime: 告警时，录像时长
            PreRecordTime: 启动语录时长，0 ~ 10秒
        */
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetAlarmCfg")]
        public static extern int DM_SetAlarmCfg(int handle, bool EnableAlarm, float AlarmTemp, int AlarmColor, int AlarmTempLevel1, int AlarmTempLevel2,
                                        int AlarmTempLevel3, int AlarmTempLevel4, int AlarmTempLevel5, int AlarmPer1,
                                        int AlarmPer2, int AlarmPer3, int AlarmPer4, int AlarmPer5, int EnableAlarmLevel,
                                        int AlarmTime, ref string AlarmAreaInfo, ref string AlarmLinkOutInfo, int FtpPicNum,
                                        int EMailPicNum, int RecTime, int PreRecordTime, int iMailContentType);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetAlarmCfg")]
        public static extern int DM_GetAlarmCfg(int handle, ref bool EnableAlarm, ref int AlarmTempLevel1, ref int AlarmTempLevel2,
                                    ref int AlarmTempLevel3, ref int AlarmTempLevel4, ref int AlarmTempLevel5, ref int AlarmPer1,
                                        ref int AlarmPer2, ref int AlarmPer3, ref int AlarmPer4, ref int AlarmPer5, ref int EnableAlarmLevel,
                                        ref int AlarmTime, ref string AlarmAreaInfo, ref string AlarmLinkOutInfo, ref int FtpPicNum,
                                        ref int EMailPicNum, ref int RecTime, ref int PreRecordTime, ref int iMailContentType);

        //磁盘格式化
        [DllImport("DMSDK.dll", EntryPoint = "DM_HDDFormat")]
        public static extern int  DM_HDDFormat(int handle);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetHDDFormatStatus")]
        public static extern int  DM_GetHDDFormatStatus(int handle);

        //设置禁区
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetForbiddenZone")]
        public static extern int  DM_SetForbiddenZone(int handle, int Index, bool Enable, int Type, int Sensitive, int PointNum,
                                            ref Point pPoint, ref string AlarmLinkOutInfo, int FtpPicNum, int EMailPicNum,
                                            int RecTime, int PreRecordTime, ref string ActiveTimeSet, int iMailContentType);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetForbiddenZone")]
        public static extern int DM_GetForbiddenZone(int handle, int Index, ref bool Enable,ref int Type,ref int Sensitive,ref int PointNum,
                                            ref Point pPoint,ref string AlarmLinkOutInfo,ref int FtpPicNum, ref int EMailPicNum,
                                            ref int RecTime,  ref int PreRecordTime, ref string ActiveTimeSet, ref int iMailContentType);

        //设置警戒线
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetCordon")]
        public static extern int  DM_SetCordon(int handle, int Index, bool Enable, int Type, int Sensitive, int PointNum,
                                    ref Point pPoint, ref string AlarmLinkOutInfo, int FtpPicNum, int EMailPicNum,
                                    int RecTime, int PreRecordTime, ref string ActiveTimeSet, int iMailContentType);

        [DllImport("DMSDK.dll", EntryPoint = "DM_GetCordon")]
        public static extern int DM_GetCordon(int handle, int Index, ref bool Enable, ref int Type, ref int Sensitive, ref int PointNum,
                                    ref Point pPoint, ref string AlarmLinkOutInfo, ref int FtpPicNum, ref int EMailPicNum,
                                    ref int RecTime, ref int PreRecordTime, ref string ActiveTimeSet, ref int iMailContentType);

        /* 	功能说明:查找日志
            *	输入参数:handle:	DM_Connect返回的句柄
                        Language:	返回信息的语言种类，目前仅支持en-us(英文)
                        dwType, 日志类型
                        #define MAJOR_All		0x00	// 全部 
                        #define MAJOR_ALARM		0x1	// 报警 
                        #define MAJOR_EXCEPTION 0x2	// 异常
                        #define MAJOR_OPERATION	0x3	// 操作 
                        lpStartTime, 开始时间，格式为 yyyy-mm-dd hh:mm:ss
                        lpStopTime, 结束时间，格式为 yyyy-mm-dd hh:mm:ss
            *	输出参数: pLog:	返回的日志信息，最大1000条
            *	函数返回: >0 日志记录条数。仪器端最大支持一次检索1000条，所以当记录条数=1000时，需要缩小检索范围，以便确定是否所有符合要求的日志都以返回。
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_FindLog")]
        public static extern int DM_FindLog(int handle, ref string Language, int dwType, ref string lpStartTime, ref string lpStopTime, tagLog[] pLog);

        /* 	功能说明:开始查找录像文件
            *	输入参数:handle, DM_Connect返回的句柄
                        dwType, 文件类型
                            0: 定时录像
                            1: 告警录像
                            999: 全部；
                        dwStorageType, 存储器类型
                            0: SD卡；
                            1: 网络存储器
                        lpStartTime, 开始时间，格式为 yyyy-mm-dd hh:mm:ss
                        lpStopTime, 结束时间，格式为 yyyy-mm-dd hh:mm:ss
            *	输出参数: pFile:	返回的录像信息，最大1000条
            *	函数返回: >0 录像文件个数。
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_FindRecord")]
        public static extern int  DM_FindRecord(int handle, int dwType, int dwStorageType,ref string lpStartTime, ref string lpStopTime, tagFile[] pFile);

        /* 	功能说明:根据录像文件的名称下载单个文件
            *	输入参数:handle, DM_Connect 返回值
                        dwStorageType, 存储器类型
                            0: SD卡；
                            1: 网络存储器
                        sDVRFileName: 要下载的录像文件名，必须是DM_FindRecord 返回的文件名
                        sSavedFileName: 希望保存到本地的录像文件名
                        bShow: 是否显示下载界面
            *	输出参数:无
            *	函数返回: >= 0 表示开始下载；返回负数表示失败
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetFileByName")]
        public static extern int  DM_GetFileByName(int handle, int dwStorageType, ref string sDVRFileName, ref string sSavedFileName, bool bShow);

        /* 	功能说明:批量下载录像文件
            *	输入参数:handle, DM_Connect 返回值
                        dwStorageType, 存储器类型
                            0: SD卡；
                            1: 网络存储器
                        sTxtFileName: 一个.txt文件，准备下载的录像文件名称放入该.txt文件中，每个录像文件为一行，格式如下为：
                        20171221082614_T48219.avi
                        20171221072213_T48218.avi
                        20171221061813_T48217.avi
                        20171221051411_T48216.avi
                        sSavedPath: 将下载的录像统一保存到该目录下，必须保证该目录已存在。
                        bShow: 是否显示下载界面
            *	输出参数:无
            *	函数返回: >= 0 表示开始下载；返回负数表示失败
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_GetFilesByName")]
        public static extern int  DM_GetFilesByName(int handle, int dwStorageType, ref string sTxtFileName, ref string sSavedPath, bool bShow);

        /* 	功能说明:用户管理（尚未开放）
            *	输入参数:handle, DM_Connect 返回值
                        iOperation: 操作类型, 0: 添加用户; 1: 删除用户; 2: 修改密码
                        sUserName: 用户名
                        sPassword: 密码
                        iUserType: 用户类别, 0: 管理员; 1: 普通用户; 添加用户时使用
                        sNewPassword: 新密码, 修改密码时使用
            *	输出参数:无
            *	函数返回: >= 0 表示成功；返回负数表示失败
            *  备注：该命令只在用 admin 用户登录后才能使用
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_OperateUser")]
        public static extern int  DM_OperateUser(int handle, int iOperation, ref string sUserName, ref string sPassword, int iUserType = 0,  string  sNewPassword = "");

        /* 	功能说明: 终止本计算机中所有下载录像的进程，暂不支持
            *	输入参数:handle, DM_Connect 返回值
            *	输出参数:无
            *	函数返回: > 0 成功；<= 0 表示失败，一般是无法获得计算机进程控制权限导致
            */
        [DllImport("DMSDK.dll", EntryPoint = "DM_StopGetFile")]
        public static extern int  DM_StopGetFile();

        //以下为DM10专用
        //设置融合时可见光的裁剪位置
        [DllImport("DMSDK.dll", EntryPoint = "DM_SetCCDCutInfo")]
        public static extern int  DM_SetCCDCutInfo(int handle, int x, int y, int width, int height);
    }
}
