using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        StaticClass.StructIAnalyzeConfig structIAnalyzeConfig;
        ArrayList arrayList;
        public SqlInsert()
        {
            threadSqlInsert = new Thread(TemperDataInsert);
            threadSqlInsert.Start();
        }

        ~SqlInsert()
        {
            threadSqlInsert.Abort();
        }

        public void InsertTemperDataToArrayList(DMSDK.tagTempMessage tempMessage)
        {
            TemperDataArrList.Add(tempMessage);
            Interlocked.Increment(ref TemperDataLength);
        }

        private void TemperDataInsert()
        {
            while (true)
            {
                if (TemperDataLength > 0)
                {
                    tempMessage = new DMSDK.tagTempMessage();
                    tagTemperature = new DMSDK.tagTemperature();
                    structIAnalyzeConfig = new StaticClass.StructIAnalyzeConfig();
                    tempMessage = (DMSDK.tagTempMessage)TemperDataArrList[0];
                    int handle = tempMessage.handle;
                    arrayList = sqlCreate.Select_SMInfraredConfig(tempMessage.dvrIP);//根据IP地址判断 
                    structIAnalyzeConfig = (StaticClass.StructIAnalyzeConfig)arrayList[0];
                    int CameraID = structIAnalyzeConfig.CameraID;
                    for (int i=0;i<tempMessage.len;i++)
                    {
                        tagTemperature = tempMessage.temperInfo[i];
                        byte Alarmid = (byte)tagTemperature.number;
                        if ((StaticClass.intPtrs_AlarmId[CameraID - 1] & (0x80>>Alarmid)) != 0) //应该这样，你试试
                        {
                            sqlCreate.Insert_TemperData(CameraID, tempMessage.dvrIP, DateTime.Now, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "测温告警");
                        }else if ((StaticClass.intPtrs_AlarmId[CameraID - 1] & (0x80 >> Alarmid)) == 0)
                        {
                            sqlCreate.Insert_TemperData(CameraID, tempMessage.dvrIP, DateTime.Now, tagTemperature.type, tagTemperature.number, tagTemperature.temper, "无报警");
                        }
                    }
                    TemperDataArrList.RemoveAt(0);
                    Interlocked.Decrement(ref TemperDataLength);
                }
                Thread.Sleep(200);
            }
        }
    }
}
