using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 原来想直接从探测器取报警状态的 但是其返回的报警消息结构体只含有 告警号 即 只有一个编号  没有类型 导致同时设置了区域1与点1 无法判断是谁给的报警 所以需要用编号唯一标识该区域
/// </summary>
namespace InfraredAnalyze
{
    class SqlInsert
    {
        Thread threadSqlInsert;
        int queueCount = 0;
        Queue<Time_tempMessage> queue_TemperMessage = new Queue<Time_tempMessage>();
        SqlCreate sqlCreate = new SqlCreate();
        DMSDK.tagTempMessage tempMessage;
        DMSDK.tagTemperature tagTemperature;
        StructClass.realTimeTemper realTimeTemper;
        StructClass.realTimeTemper[] realTimeTempers;
        StructClass.realTimeStructTemper realTimeStructTemper;

        StructClass.StructNumber_Datetime structNumber_Datetime;
        StructClass.StructCameraId_Datetime structCameraId_Datetime;
        StructClass.StructNumber_Datetime[] structNumber_Datetimes = new StructClass.StructNumber_Datetime[8];
        static object Lock_obj = new object();
        
        struct Time_tempMessage //给传过来的时间加上一个时间戳 数据的时间应该按 采集过来的时间算 不能按插入数据库的时间来算
        {
            public DateTime dateTime;
            public DMSDK.tagTempMessage tempMessage;
        }

        struct DateTime_CameraID_Number
        {
            public int CameraID;
            public DateTime dateTime;
            public int number;
        }

        Time_tempMessage time_TempMessage;
        DateTime_CameraID_Number tempDt_Cid_Num;//同一时间 同一个相机的同一个区域 只能被插入一次数据

        public SqlInsert()
        {
            try
            {
               
                threadSqlInsert = new Thread(TemperDataInsert);
                threadSqlInsert.Start();
                for (int i = 0; i < 16; i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        structNumber_Datetimes[j].number = j;
                    }
                    structCameraId_Datetime.CameraID = i + 1;
                    structCameraId_Datetime.structNumber_DateTime = structNumber_Datetimes;
                    StaticClass.intPtrs_DateTime_CameraID.Add(structCameraId_Datetime);
                }//初始化
            }
            catch (Exception ex)
            {
                 ex.StackTrace.ToString();
            }
        }

        ~SqlInsert()
        {
            threadSqlInsert.Abort();
        }

        public void InsertTemperDataToArrayList(DateTime dateTime, DMSDK.tagTempMessage tempMessage)
        {
            lock (Lock_obj)
            {
                time_TempMessage.dateTime = dateTime;
                time_TempMessage.tempMessage = tempMessage;
                queue_TemperMessage.Enqueue(time_TempMessage);
                Interlocked.Increment(ref queueCount);
            }
        }

        private void TemperDataInsert()//线程 处理插入数据
        {
            time_TempMessage = new Time_tempMessage();//加上时间戳
            tempMessage = new DMSDK.tagTempMessage();
            tagTemperature = new DMSDK.tagTemperature();
            while (true)
            {
                try
                {
                    if (queueCount > 0)
                    {
                        lock (Lock_obj)
                        {
                            time_TempMessage = queue_TemperMessage.ElementAt(0);
                            tempMessage = time_TempMessage.tempMessage;
                            DateTime dateTime = time_TempMessage.dateTime;
                            int index = Array.IndexOf(StaticClass.intPtrs_Ip, tempMessage.dvrIP);
                            int CameraId = index + 1;

                            var List_number = new ArrayList() { 0, 1, 2, 3, 4, 5, 6, 7 };//清残留
                            for (int i = 0; i < tempMessage.len; i++)
                            {
                                tagTemperature = tempMessage.temperInfo[i];
                                StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[tagTemperature.number].temper = tagTemperature.temper;
                                StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[tagTemperature.number].type = tagTemperature.type;
                                StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[tagTemperature.number].number = tagTemperature.number;
                                List_number.Remove(tagTemperature.number);
                                tempDt_Cid_Num.dateTime = dateTime;
                                tempDt_Cid_Num.CameraID = CameraId;
                                tempDt_Cid_Num.number= tagTemperature.number;
                                //byte Alarmid = (byte)tagTemperature.number;
                                //if ((StaticClass.intPtrs_AlarmId[CameraID - 1] & (0x80>>Alarmid)) != 0) //应该这样
                                //{
                                //    sqlCreate.Insert_TemperData(CameraID, tempMessage.dvrIP, DateTime.Now, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "测温告警");
                                //}else if ((StaticClass.intPtrs_AlarmId[CameraID - 1] & (0x80 >> Alarmid)) == 0)
                                //{
                                //    sqlCreate.Insert_TemperData(CameraID, tempMessage.dvrIP, DateTime.Now, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "无报警");
                                //}
                                //StructClass.StructDataFilter structDataFilter = sqlCreate.Select_TemperData_Top1(CameraID);
                                //if ((dateTime.ToString() != structDataFilter.dateTime.ToString()) || (structDataFilter.num != tagTemperature.number))
                                //if (dateTime.ToString() != sqlCreate.Select_TemperData_Top1(CameraId, tagTemperature.number, StaticClass.DataBaseName).ToString())//防止一秒进来很多数据
                                //if (dateTime.ToString()== StaticClass.intPtrs_DateTime_CameraID[CameraId].structNumber_DateTime[tagTemperature.number].dateTime.ToString())//这个队列数据与最新插入记录相同 
                                //{
                                //    if(tagTemperature.number == StaticClass.intPtrs_DateTime_CameraID[CameraId].structNumber_DateTime[tagTemperature.number].number)
                                //    {
                                       
                                //    }
                                //}
                                if (dateTime.ToString() != StaticClass.intPtrs_DateTime_CameraID[CameraId].structNumber_DateTime[tagTemperature.number].dateTime.ToString())//数据过滤  一秒一个 
                                {
                                    if (StaticClass.intPtrs_Status[CameraId - 1] == 1)//火警
                                    {
                                        if (StaticClass.FireCount == 1)//首警
                                        {
                                            sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "首警", StaticClass.DataBaseName);
                                        }
                                        else if (StaticClass.FireCount > 1)//火警
                                        {
                                            sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "火警", StaticClass.DataBaseName);
                                        }
                                    }
                                    else if (StaticClass.intPtrs_Status[CameraId - 1] == 2) //故障 2（故障的时候会停止回调函数  应该不会有数据进来了）
                                    {
                                        sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "故障", StaticClass.DataBaseName);
                                    }
                                    else if (StaticClass.intPtrs_Status[CameraId - 1] == 3)//先火警再故障
                                    {
                                        sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "故障", StaticClass.DataBaseName);
                                    }
                                    else if (StaticClass.intPtrs_Status[CameraId - 1] == 4)//正常
                                    {
                                        sqlCreate.Insert_TemperData(CameraId, tempMessage.dvrIP, dateTime, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "无报警", StaticClass.DataBaseName);
                                    }
                                    StaticClass.intPtrs_DateTime_CameraID[CameraId].structNumber_DateTime[tagTemperature.number].dateTime = dateTime;
                                }
                            }
                            foreach(int i in List_number)
                            {
                                StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[i].temper = 0;
                                StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[i].type = 0;
                                StaticClass.intPtrs_RealtimeTemper[index].realTimeTemper[i].number = 0;
                            }
                            queue_TemperMessage.Dequeue();
                            Interlocked.Decrement(ref queueCount);
                        }
                    }
                }
                catch (Exception ex)
                {
                    StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "温度数据插入异常" + ex.Message + ex.StackTrace);
                }
                Thread.Sleep(2);//20*16=320 1000/320
                StaticClass.QueueLength = queueCount;
            }
        }
    }
}
