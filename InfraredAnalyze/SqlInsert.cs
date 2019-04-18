using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    class SqlInsert
    {
        Thread threadSqlInsert;
        int TemperDataLength = 0;
        ArrayList TemperDataArrList = new ArrayList();
        SqlCreate sqlCreate = new SqlCreate();
        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagTemperature tagTemperature;
        StructClass.realTimeTemper realTimeTemper;
        StructClass.realTimeTemper[] realTimeTempers;
        StructClass.realTimeStructTemper realTimeStructTemper;
        Time_tempMessage time_TempMessage;

        struct Time_tempMessage //给传过来的时间加上一个时间戳 数据的时间应该按 采集过来的时间算 不能按插入数据库的时间来算
        {
            public DateTime dateTime;
            public DMSDK.tagTempMessage tempMessage;
        }

        public SqlInsert()
        {
            threadSqlInsert = new Thread(TemperDataInsert);
            threadSqlInsert.Start();
        }

        ~SqlInsert()
        {
            threadSqlInsert.Abort();
        }

        public void InsertTemperDataToArrayList(DateTime dateTime,DMSDK.tagTempMessage tempMessage)
        {
            time_TempMessage.dateTime = dateTime;
            time_TempMessage.tempMessage = tempMessage;


            TemperDataArrList.Add(time_TempMessage);//将数据放到动态数组里


            Interlocked.Increment(ref TemperDataLength);
        }

        private void TemperDataInsert()
        {
            time_TempMessage = new Time_tempMessage();//加上时间戳
            tempMessage = new DMSDK.tagTempMessage();
            tagTemperature = new DMSDK.tagTemperature();
            while (true)
            {
                try
                {
                    if (TemperDataLength > 0)
                    {
                        if (TemperDataArrList[0] == null)
                        {
                            TemperDataArrList.RemoveAt(0);
                            Interlocked.Decrement(ref TemperDataLength);
                        }
                        time_TempMessage = (Time_tempMessage)TemperDataArrList[0];
                        tempMessage = time_TempMessage.tempMessage;
                        DateTime dateTime = time_TempMessage.dateTime;
                        int index = Array.IndexOf(StaticClass.intPtrs_Ip, tempMessage.dvrIP);
                        int CameraId = index + 1;
                        StaticClass.Is_CallBack[index] = true;
                        for (int i = 0; i < tempMessage.len; i++)
                        {
                            tagTemperature = tempMessage.temperInfo[i];
                            StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[tagTemperature.number].temper = tagTemperature.temper;
                            StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[tagTemperature.number].type = tagTemperature.type;
                            StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[tagTemperature.number].number = tagTemperature.number;
                            //byte Alarmid = (byte)tagTemperature.number;
                            //if ((StaticClass.intPtrs_AlarmId[CameraID - 1] & (0x80>>Alarmid)) != 0) //应该这样，你试试
                            //{
                            //    sqlCreate.Insert_TemperData(CameraID, tempMessage.dvrIP, DateTime.Now, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "测温告警");
                            //}else if ((StaticClass.intPtrs_AlarmId[CameraID - 1] & (0x80 >> Alarmid)) == 0)
                            //{
                            //    sqlCreate.Insert_TemperData(CameraID, tempMessage.dvrIP, DateTime.Now, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "无报警");
                            //}
                            //StructClass.StructDataFilter structDataFilter = sqlCreate.Select_TemperData_Top1(CameraID);
                            //if ((dateTime.ToString() != structDataFilter.dateTime.ToString()) || (structDataFilter.num != tagTemperature.number))
                            if (dateTime.ToString() != sqlCreate.Select_TemperData_Top1(CameraId, tagTemperature.number).ToString())//
                            {
                                if (StaticClass.intPtrs_Status[CameraId - 1] == 1)//火警
                                {
                                    if (StaticClass.FireCount == 1)//首警
                                    {
                                        sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "首警");
                                    }
                                    else if (StaticClass.FireCount > 1)//火警
                                    {
                                        sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "火警");
                                    }
                                }
                                else if (StaticClass.intPtrs_Status[CameraId - 1] == 2) //故障 2（故障的时候会停止回调函数  应该不会有数据进来了）
                                {
                                    sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "故障");
                                }
                                else if (StaticClass.intPtrs_Status[CameraId - 1] == 3)//先火警再故障
                                {
                                    sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "故障");
                                }
                                else if (StaticClass.intPtrs_Status[CameraId - 1] == 4)//正常
                                {
                                    sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "无报警");
                                }
                            }
                        }
                        TemperDataArrList.RemoveAt(0);
                        Interlocked.Decrement(ref TemperDataLength);
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("温度数据插入异常！" + ex.Message);
                }
                Thread.Sleep(5);
            }
        }
    }
}
