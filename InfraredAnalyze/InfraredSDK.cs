using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.DirectX.PrivateImplementationDetails;
using Microsoft.DirectX;

namespace InfraredAnalyze
{
    public class InfraredSDK
    {

        public enum Nine_POINT_AVE
        {
            Open,
            Close,
        };

        public enum TEMP_UNIT_TYPE
        {
            C,
            F,
            K,
        };

        public struct PointInfo
        {
            public short x, y;         //The upper left corner coordinates of rectangle
            public short width, high;
            public int m_nStatue;
            public int nEmissivity;        //Emissivity*100
            public int nDistance;
            public float fTemp;            //Temperature
            public int posx, posy;			//the location of minimum temperature or the location of maximum temperature
        }

        public struct LineInfo
        {
            short x1, y1;           //Start
            short x2, y2;           //End
            int m_nStatue;
            int nEmissivity;        //Emissivity*100
            int nDistance;
            ushort wdCx;          //Coordinates of points on the line
            ushort wdCy;          //Coordinates of points on the line
            float fTemp;			//Temperature of points on the line
        }


        public struct RectInfo
        {
            short x, y;         //The upper left corner coordinates of rectangle
            short width, high;
            int m_nStatue;
            int nEmissivity;        //Emissivity*100
            int nDistance;
            float fTemp;            //Temperature
            int posx, posy;			//the location of minimum temperature or the location of maximum temperature
        }
        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Init")]
        public static extern IntPtr IFR_Init(IntPtr WndWindow, int nWidth, int nHeight);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Free")]
        public static extern void IFR_Free(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetSDKVersion")]
        public static extern int IFR_GetSDKVersion(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadInfraredData")]
        public static extern int IFR_LoadInfraredData(IntPtr intPtr, ref byte InfraredData);//C#中调用前需定义byte 变量名 = new byte(); 

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetDeviceModel")]
        public static extern int IFR_GetDeviceModel(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetDeviceSoftInfo")]
        public static extern int IFR_GetDeviceSoftInfo(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetMeasureTempVersion")]
        public static extern int IFR_SetMeasureTempVersion(IntPtr intPtr, int MTVer);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetEmissDis")]
        public static extern int IFR_SetEmissDis(IntPtr intPtr, float fEmissivity, float fDistance);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetAmbHum")]
        public static extern int IFR_SetAmbHum(IntPtr intPtr, float fAmbTemp, float fHumidity);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetAtmoTemp")]
        public static extern int IFR_SetAtmoTemp(IntPtr intPtr, float fAtmoTemp);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetCorrectParam")]
        public static extern int IFR_SetCorrectParam(IntPtr intPtr, float fQuotiety, float fTemp, int nGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetAllK")]
        public static extern void IFR_SetAllK(IntPtr intPtr, float fAllK);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadGrayToTempLine")]
        public static extern int IFR_LoadGrayToTempLine(IntPtr intPtr, IntPtr pLine, int nLen);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadDelicacyLine")]
        public static extern int IFR_LoadDelicacyLine(IntPtr intPtr, IntPtr pLine, int nLen);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadZeroLine")]
        public static extern int IFR_LoadZeroLine(IntPtr intPtr, IntPtr pLine, int nLen);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GrayToTemp")]
        public static extern float IFR_GrayToTemp(IntPtr intPtr, int nGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_TempToGray")]
        public static extern int IFR_TempToGray(IntPtr intPtr, float fTemp);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Set9PointMeasure")]
        public static extern int IFR_Set9PointMeasure(IntPtr intPtr, Nine_POINT_AVE Flag);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetPointGray")]
        public static extern int IFR_GetPointGray(IntPtr intPtr, Point Spot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetPointTemp")]
        public static extern float IFR_GetPointTemp(IntPtr intPtr, Point Spot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineMinMaxGray")]
        public static extern int IFR_GetLineMinMaxGray(IntPtr intPtr, Point Spot1, Point Spot2, ref int MinGray, ref Point MinSpot, ref int MaxGray, ref Point MaxSpot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineMinMaxTemp")]
        public static extern int IFR_GetLineMinMaxTemp(IntPtr intPtr, Point Spot1, Point Spot2, ref float MinTemp, ref Point MinSpot, ref float MaxTemp, ref Point MaxSpot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineAverageTemp")]
        public static extern float IFR_GetLineAverageTemp(IntPtr intPtr, Point Spot1, Point Spot2);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineLen")]
        public static extern int IFR_GetLineLen(IntPtr intPtr, Point Spot1, Point Spot2);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineGray")]
        public static extern int IFR_GetLineGray(IntPtr intPtr, Point Spot1, Point Spot2, ref int pGray);


        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineTemp")]
        public static extern int IFR_GetLineTemp(IntPtr intPtr, Point Spot1, Point Spot2, ref float pTemp);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectMinMaxGray")]
        public static extern int IFR_GetRectMinMaxGray(IntPtr intPtr, Point Spot1, Point Spot2, ref int MinGray, ref Point MinSpot, ref int MaxGray, ref int MaxSpot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectMinMaxTemp")]
        public static extern int IFR_GetRectMinMaxTemp(IntPtr intPtr, Point Spot1, Point Spot2, ref float MinTemp, ref Point MinSpot, ref float MaxTemp, ref Point MaxSpot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectAverageTemp")]
        public static extern float IFR_GetRectAverageTemp(IntPtr intPtr, Point Spot1, Point Spot2);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectLen")]
        public static extern int IFR_GetRectLen(IntPtr intPtr, Point Spot1, Point Spot2);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectGray")]
        public static extern int IFR_GetRectGray(IntPtr intPtr, Point Spot1, Point Spot2, ref int pGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectTemp")]
        public static extern int IFR_GetRectTemp(IntPtr intPtr, Point Spot1, Point Spot2, ref float pTemp);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCircleMinMaxGray")]
        public static extern int IFR_GetCircleMinMaxGray(IntPtr intPtr, Point Spot1, int r, ref int MinGray, ref Point MinSpot, ref int MaxGray, ref Point MaxSpot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCircleMinMaxTemp")]
        public static extern int IFR_GetCircleMinMaxTemp(IntPtr intPtr, Point Spot1, int r, ref float MinTemp, ref Point MinSpot, ref float MaxTemp, ref Point MaxSpot);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCircleAverageTemp")]
        public static extern float IFR_GetCircleAverageTemp(IntPtr intPtr, Point Spot, int r);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCircleLen")]
        public static extern int IFR_GetCircleLen(IntPtr intPtr, Point Spot1, int r);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCircleGray")]
        public static extern int IFR_GetCircleGray(IntPtr intPtr, Point Spot1, int r, ref int pGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCircleTemp")]
        public static extern int IFR_GetCircleTemp(IntPtr intPtr, Point Spot1, int r, ref float pTemp);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetTempUnit")]
        public static extern int IFR_SetTempUnit(IntPtr intPtr, TEMP_UNIT_TYPE TempUnitType);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetTempUnit")]
        public static extern TEMP_UNIT_TYPE IFR_GetTempUnit(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetMeasureTempVersion")]
        public static extern int IFR_GetMeasureTempVersion(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetEmissDis")]
        public static extern int IFR_GetEmissDis(IntPtr intPtr, ref float fEmissivity, ref float fDistance);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetAmbHum")]
        public static extern int IFR_GetAmbHum(IntPtr intPtr, ref float fAmbTemp, ref float fHumidity);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCorrectParam")]
        public static extern int IFR_GetCorrectParam(IntPtr intPtr, ref float fQuotiety, ref float fTemp, ref int nGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetDeviceScheme")]
        public static extern int IFR_GetDeviceScheme(IntPtr intPtr, ref float fMinTemp, ref float fMaxTemp);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetPointInfo")]
        public static extern int IFR_GetPointInfo(IntPtr intPtr, PointInfo[] pointInfo);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetLineInfo")]
        public static extern int IFR_GetLineInfo(IntPtr intPtr, ref LineInfo lineInfo);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetRectInfo")]
        public static extern int IFR_GetRectInfo(IntPtr intPtr, ref RectInfo lineInfo);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetColorRuler")]
        public static extern int IFR_SetColorRuler(IntPtr intPtr, ref char cRulerFile);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetImageFlipMode")]
        public static extern void IFR_SetImageFlipMode(IntPtr intPtr, int nMode);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SwapColorRulerBuffer")]
        public static extern void IFR_SwapColorRulerBuffer(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_DoHistogramProcess")]
        public static extern void IFR_DoHistogramProcess(IntPtr intPtr, IntPtr pIfr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_DoIfrProcess")]
        public static extern void IFR_DoIfrProcess(IntPtr intPtr, IntPtr pIfr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetHistogramMinGray")]
        public static extern void IFR_SetHistogramMinGray(IntPtr intPtr, int nGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetHistogramMinGray")]
        public static extern int IFR_GetHistogramMinGray(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetHistogramMaxGray")]
        public static extern void IFR_SetHistogramMaxGray(IntPtr intPtr, int nGray);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetHistogramMaxGray")]
        public static extern int IFR_GetHistogramMaxGray(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetHistogramTblLength")]
        public static extern int IFR_GetHistogramTblLength(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetHistogramTblData")]
        public static extern void IFR_GetHistogramTblData(IntPtr intPtr, ref char pwdTbl);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_ShowImage")]
        public static extern void IFR_ShowImage(IntPtr intPtr, IntPtr pIfr);

        
        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetDrawBuffer")]//离屏表面设备句柄
        public static extern int IFR_GetDrawBuffer(IntPtr intPtr, IntPtr pIfr,ref IntPtr LP);
   
        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Flip")]
        public static extern void IFR_Flip(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetHistogramLevel")]
        public static extern void IFR_SetHistogramLevel(IntPtr intPtr,int nLevel);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetHistogramGain")]
        public static extern void IFR_SetHistogramGain(IntPtr intPtr, int nGain);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetHistogramLevel")]
        public static extern int IFR_GetHistogramLevel(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetHistogramGain")]
        public static extern int IFR_GetHistogramGain(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetAGCStatus")]
        public static extern void IFR_SetAGCStatus(IntPtr intPtr,int nMode);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetAGCStatus")]
        public static extern int IFR_GetAGCStatus(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_ResetAGCStatus")]
        public static extern void IFR_ResetAGCStatus(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetFilterType")]
        public static extern void IFR_SetFilterType(IntPtr intPtr,int wdType);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetEnhanceType")]
        public static extern void IFR_SetEnhanceType(IntPtr intPtr, int wdType);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetMaxContrastCtrl")]
        public static extern void IFR_SetMaxContrastCtrl(IntPtr intPtr, int nValue);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetMaxContrastCtrl")]
        public static extern int IFR_GetMaxContrastCtrl(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetHistogramComboModeGain")]
        public static extern void IFR_SetHistogramComboModeGain(IntPtr intPtr,int nGain);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetHistogramComboModeLevel")]
        public static extern void IFR_SetHistogramComboModeLevel(IntPtr intPtr, int nLevel);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadFile")]
        public static extern IntPtr IFR_LoadFile(IntPtr WndWindow, string fName, int iFileType, ref int nBeginTime, ref int nEndTime);

        public delegate int FILESTREAMCALLBACK(IntPtr intPtr, IntPtr stream, int len, long nTime);
        // typedef int (WINAPI* FILESTREAMCALLBACK) (HANDLE hHandle, void* stream, int len, long nTime);
        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_ReceiveStream")]
        public static extern int IFR_ReceiveStream(IntPtr intPtr, FILESTREAMCALLBACK GetStream);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Play")]
        public static extern int IFR_Play(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Pause")]
        public static extern int IFR_Pause(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Stop")]
        public static extern int IFR_Stop(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Next")]
        public static extern int IFR_Next(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_Prior")]
        public static extern int IFR_Prior(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GotoTime")]
        public static extern int IFR_GotoTime(IntPtr intPtr,long nTime);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_PlaySpeed")]
        public static extern int IFR_PlaySpeed(IntPtr intPtr, long nTime);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_CloseFile")]
        public static extern int IFR_CloseFile(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_MemSaveToJPG")]
        public static extern int IFR_MemSaveToJPG(IntPtr intPtr,string cFileName,int Q);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_InfraredFileToJPG")]
        public static extern int IFR_InfraredFileToJPG(string cRulerFileName, int nSrcFileType, string cSrcFileName, string cDstFileName, int Q);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_InfraredFileToBMP")]
        public static extern int IFR_InfraredFileToBMP(string cRulerFileName, int nSrcFileType, string cSrcFileName, string cDstFileName);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetImageWidthHeight")]
        public static extern int IFR_GetImageWidthHeight(IntPtr intPtr, out int nWidth, out int nHeight);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadInfraredParamFromStream")]
        public static extern int IFR_LoadInfraredParamFromStream(IntPtr intPtr, ref byte pBuf);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadInfraredParamFromDLVStream")]
        public static extern int IFR_LoadInfraredParamFromDLVStream(IntPtr intPtr, ref byte pBuf);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_StreamDiff")]
        public static extern int IFR_StreamDiff(IntPtr intPtr, IntPtr stream, IntPtr stream1, IntPtr stream2);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_MemSaveToDLI")]
        public static extern int IFR_MemSaveToDLI(IntPtr intPtr,string cFileName);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_MemSaveToBMP")]
        public static extern int IFR_MemSaveToBMP(IntPtr intPtr, string cFileName);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_MemSaveToBuff")]
        public static extern int IFR_MemSaveToBuff(IntPtr intPtr, IntPtr pBuff, int BuffLen);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetUNCompensateMode")]
        public static extern void IFR_SetUNCompensateMode(IntPtr intPtr,  int nMode);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetBendiLimmit")]
        public static extern void IFR_SetBendiLimmit(IntPtr intPtr, int swBendi);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_ExistSound")]
        public static extern bool IFR_ExistSound(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetSoundFile")]
        public static extern int IFR_GetSoundFile(IntPtr intPtr,string strSoundFile);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetFileLen")]
        public static extern int IFR_GetFileLen(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetFileBuff")]
        public static extern void IFR_GetFileBuff(IntPtr intPtr,ref byte p_Buff);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetColorID")]
        public static extern void IFR_GetColorID(IntPtr intPtr, ref int nColorID);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetFileVer")]
        public static extern void IFR_GetFileVer(IntPtr intPtr, ref int FileVersion,ref int Version);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_OutputDIB")]
        public static extern int IFR_OutputDIB(IntPtr intPtr, ref byte pHDIB, ref int iSize, int ImageType);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCCDImageWidthHeight")]
        public static extern void IFR_GetCCDImageWidthHeight(IntPtr intPtr, ref int CCDImageWidth, ref int CCDImageHeight);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_OutputCCDDIB")]
        public static extern int IFR_OutputCCDDIB(IntPtr intPtr,ref IntPtr pHDIB, ref int iSize, int ImageType);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_ExistCCD")]
        public static extern int IFR_ExistCCD(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SaveCCDFile")]
        public static extern void IFR_SaveCCDFile(IntPtr intPtr,string FileName,ref int ImageType);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_ExistRemark")]
        public static extern int IFR_ExistRemark(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SaveRemarkFile")]
        public static extern int IFR_SaveRemarkFile(IntPtr intPtr,string FileName);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_LoadBuffer")]
        public static extern IntPtr IFR_LoadBuffer(IntPtr intPtr, ref byte IRBuff,int BuffLen);

        //[DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetIsothermTemp")]
        // public static extern int IFR_SetIsothermTemp(IntPtr intPtr, bool IsOpen, float fUpTemp, float fDownTemp, COLORREF c_IsothermColor, int sequence = 0);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_SetIRInvert")]
        public static extern void IFR_SetIRInvert(IntPtr intPtr,bool b_Invert);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetDeviceDate")]
        public static extern void IFR_GetDeviceDate(IntPtr intPtr, string s_DeviceType, string s_SystemVersion, string s_DeviceID, string s_SystemCreateTime, ref byte i_nAperture, ref int i_nAperLeast, ref int i_nAperMost, ref byte i_nExternLenType);


        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetBoardCoord")]
        public static extern void IFR_GetBoardCoord(IntPtr intPtr, ref int x, ref int y, ref int width, ref int height);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetCurrentFrameIndex")]
        public static extern int IFR_GetCurrentFrameIndex(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GetTotalFrame")]
        public static extern int IFR_GetTotalFrame(IntPtr intPtr);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GotoFrame")]
        public static extern int IFR_GotoFrame(IntPtr intPtr,int nIndex);

        [DllImport("InfraredSDK.dll", EntryPoint = "IFR_GotoFrame")]
        public static extern int IFR_GotoFrame(IntPtr intPtr, string FileName, int nBeginFrame, int nEndFrame, int nInterval);
    }
}
