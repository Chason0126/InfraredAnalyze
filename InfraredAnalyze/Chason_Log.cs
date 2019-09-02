using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    class Chason_Log
    {
        Thread thread_Log;
        public Chason_Log()
        {
            thread_Log = new Thread(Log_Insert);
            thread_Log.Start();
        }

        private void Log_Insert()
        {
            try
            {
                while (true)
                {
                    lock (StaticClass.obj_Lock)
                    {
                        if (StaticClass.queue_Log.Count > 0)
                    {
                            if (Write_Log(StaticClass.queue_Log.ElementAt(0)))
                            {
#if DEBUG
                                MessageBox.Show(StaticClass.queue_Log.ElementAt(0));
#endif
                                StaticClass.queue_Log.Dequeue();
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            
            
        }

        public bool Write_Log(string log)
        {
            bool IsWriteSuccess = false;
            //string path = ConfigurationSettings.AppSettings["Log_Path"] + "SM9003";//.net版本
            string path = ConfigurationManager.AppSettings["Log_Path"] + "SM7003";
            string txt = path + "\\" + "SM7003Log.txt";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(txt))
                {
                    File.CreateText(txt);
                }
                if (!IsLogInUse(txt))
                {
                    FileStream fileStream = new FileStream(txt, FileMode.Append);
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(log);
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                    IsWriteSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            return IsWriteSuccess;
        }

        private bool IsLogInUse(string path)
        {
            bool IsInuse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                IsInuse = false;
            }
            catch
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return IsInuse;
        }
    }
}
