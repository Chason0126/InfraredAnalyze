using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

namespace InfraredAnalyze
{
    class SqlCreate
    {

        static string conServer = @"server =.; integrated security = true;";
        SqlConnection con_Server = new SqlConnection(conServer);
        SqlCommand cmd;
        SqlConnection con_DB;

        public SqlCreate()
        {
            if (!IsDBExist())//判断数据库是否存在，若不存在，
            {
                DB_Create();//则创建InfraredAnalyze数据库 和创建SMInfrardeAnalyze数据表（16个）
                Table_Create();//创建InfraredAnalyze的数据表
                Insert_SMInfrared();
                for (int i = 1; i <= 16; i++)//SMInfrardeAnalyze的数据表
                {
                    Table_TemperArea_Create("SM_InfraredCamera" + i.ToString());
                    Insert_SM_InfraredCamera("SM_InfraredCamera" + i.ToString());
                }
                
            }
        }

        public bool IsDBExist()//判断数据库存不存在
        {
            int count = 0;
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("SELECT * FROM master.dbo.sysdatabases where name='SM_Infrared';", con_Server);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    count++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message);
            }
            finally
            {
                con_Server.Close();
            }
            return ((count > 0) ? true : false);
        }

        public void DB_Create()//判断并创建数据库
        {
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("CREATE DATABASE SM_Infrared;", con_Server);
                cmd.ExecuteNonQuery();
                for(int i = 1; i <= 16; i++)
                {
                    cmd = new SqlCommand("CREATE DATABASE SM_InfraredCamera" + i + ";", con_Server);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库初始化失败" + ex.Message);
            }
            finally
            {
                con_Server.Close();
            }
        }

        public void Table_Create()
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("create table SMInfraredAnalyze(CameraId int,CameraName nvarchar(MAX),IPAddress varchar(15),Port int,NodeID int,Remarks nvarchar(max),Enable bit)", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库创建异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Table_TemperArea_Create(string SM_InfraredCamera)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + SM_InfraredCamera + "'");
                con_DB.Open();
                cmd = new SqlCommand("create table TemperArea(Type nvarchar(MAX),X1 int,Y1 int,X2 int,Y2 int,X3 int,Y3 int,Emiss int,MeasureType int)", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table TemperData(CameraId int,IPAddress varchar(15),DateTime datetime,Type int,Number int,Temper decimal,Status  nvarchar(MAX))", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table AlarmConfig(Type nvarchar(MAX),Spark int,AlarmTemper decimal,Enable bit)", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库创建异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Insert_SMInfrared()//插入16个预置数据 
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                for(int i = 1; i <= 16; i++)
                {
                    cmd = new SqlCommand("insert into SMInfraredAnalyze(CameraId,CameraName,IPAddress,Port,NodeID,Remarks,Enable) values('" + i + "','" + ("探测器" + i) + "','" + ("192.168.1." + i) + "','5000','" + i + "','" + "无" + "','0')", con_DB);
                    cmd.ExecuteNonQuery();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库写入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Insert_SM_InfraredCamera(string SM_InfraredCamera)//插入16个预置数据 
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + SM_InfraredCamera + "'");
                con_DB.Open();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,X2,Y2,X3,Y3,Emiss) values ('L1','0','0','0','0','0','0','90')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,Emiss) values ('S2','0','0','90')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,Emiss) values ('S3','0','0','90')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,Emiss) values ('S4','0','0','90')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,Emiss) values ('S5','0','0','90')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,X2,Y2,Emiss,MeasureType) values ('A6','0','0','0','0','90','-1')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,X2,Y2,Emiss,MeasureType) values ('A7','0','0','0','0','90','-1')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into TemperArea(Type,X1,Y1,X2,Y2,Emiss,MeasureType) values ('A8','0','0','0','0','90','-1')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('L1','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('S2','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('S3','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('S4','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('S5','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('A6','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('A7','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into AlarmConfig(Type,Spark,AlarmTemper,Enable) values ('A8','0','50','0')", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库写入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        SqlCommand cmdCTable;
        public void Drop_AllDatabase()//数据库删除 请谨慎操作
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;");
                con_DB.Open();
                string sqlcommandText = @"ALTER DATABASE SM_Infrared SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [SM_Infrared]";//drop数据库  单纯的使用drop 如果数据库正在使用  则会删除异常
                cmdCTable = new SqlCommand(sqlcommandText, con_DB);
                cmdCTable.ExecuteNonQuery();
                for (int i = 1; i <= 16; i++)
                {
                    sqlcommandText = @"ALTER DATABASE " + ("SM_InfraredCamera" + i.ToString()) + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [ " + ("SM_InfraredCamera" + i.ToString()) + "]";//drop数据库  单纯的使用drop 如果数据库正在使用  则会删除异常
                    //sqlcommandText = @"ALTER DATABASE SM_InfraredCamera1 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [SM_InfraredCamera1]";
                    cmdCTable = new SqlCommand(sqlcommandText, con_DB);
                    cmdCTable.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public ArrayList Select_All_SMInfraredConfig()//按nodeid降序排列
        {
            ArrayList arrayList = new ArrayList();
            StaticClass.StructIAnalyzeConfig structIAnalyzeConfig = new StaticClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfraredAnalyze order by CameraId asc", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    structIAnalyzeConfig.CameraID = (int)sqlDataReader.GetValue(0);
                    structIAnalyzeConfig.CameraName = (string)sqlDataReader.GetValue(1);
                    structIAnalyzeConfig.IP = (string)sqlDataReader.GetValue(2);
                    structIAnalyzeConfig.Port = (int)sqlDataReader.GetValue(3);
                    structIAnalyzeConfig.NodeID = (int)sqlDataReader.GetValue(4);
                    structIAnalyzeConfig.Reamrks = (string)sqlDataReader.GetValue(5);
                    structIAnalyzeConfig.Enable = (bool)sqlDataReader.GetValue(6);
                    arrayList.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库创建异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        public ArrayList Select_SMInfraredConfig(int CameraID)//按CameraIp查找相机信息。
        {
            ArrayList arrayList = new ArrayList();
            StaticClass.StructIAnalyzeConfig structIAnalyzeConfig = new StaticClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfraredAnalyze where CameraID= '" + CameraID + "'", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    structIAnalyzeConfig.CameraID = (int)sqlDataReader.GetValue(0);
                    structIAnalyzeConfig.CameraName = (string)sqlDataReader.GetValue(1);
                    structIAnalyzeConfig.IP = (string)sqlDataReader.GetValue(2);
                    structIAnalyzeConfig.Port = (int)sqlDataReader.GetValue(3);
                    structIAnalyzeConfig.NodeID = (int)sqlDataReader.GetValue(4);
                    structIAnalyzeConfig.Reamrks = (string)sqlDataReader.GetValue(5);
                    structIAnalyzeConfig.Enable = (bool)sqlDataReader.GetValue(6);
                    arrayList.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库创建异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }


        public ArrayList Select_SMInfraredConfig(string IP)//按CameraIp查找相机信息。
        {
            ArrayList arrayList = new ArrayList();
            StaticClass.StructIAnalyzeConfig structIAnalyzeConfig = new StaticClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfraredAnalyze where IPAddress= '" + IP + "'", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    structIAnalyzeConfig.CameraID = (int)sqlDataReader.GetValue(0);
                    structIAnalyzeConfig.CameraName = (string)sqlDataReader.GetValue(1);
                    structIAnalyzeConfig.IP = (string)sqlDataReader.GetValue(2);
                    structIAnalyzeConfig.Port = (int)sqlDataReader.GetValue(3);
                    structIAnalyzeConfig.NodeID = (int)sqlDataReader.GetValue(4);
                    structIAnalyzeConfig.Reamrks = (string)sqlDataReader.GetValue(5);
                    structIAnalyzeConfig.Enable = (bool)sqlDataReader.GetValue(6);
                    arrayList.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据库信息失败：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        public void Delete_Node_SMInfraredConfig(int CameraId,int NodeId)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand(" delete from SMInfraredAnalyze  where CameraId ='" + CameraId + "' ", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(" update SMInfraredAnalyze set CameraId=CameraId-1 where CameraId >='" + CameraId + "' ", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(" update SMInfraredAnalyze set NodeId=NodeId-1 where NodeId >='" + NodeId + "' ", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取探测器列表信息失败：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Insert_SMInfraredAnalyze(int CameraId, string CameraName, string IPAddress, int Port, int NodeID, string Remarks, bool Enable)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("insert into SMInfraredAnalyze(CameraId,CameraName,IPAddress,Port,NodeID,Remarks,Enable) values('" + CameraId + "','" + CameraName + "','" + IPAddress + "','" + Port + "','" + NodeID + "','" + Remarks + "','" + Enable + "')", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加探测器失败：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delet_Table_SMInfraredAnalyze()
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("delete from SMInfraredAnalyze", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置操作异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Move_Node_Up(int NodeId)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfraredAnalyze set NodeId='" + (NodeId + 16) + "' where NodeId='" + NodeId + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfraredAnalyze set NodeId='" + NodeId + "' where NodeId='" + (NodeId - 1) + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfraredAnalyze set NodeId='" + (NodeId - 1) + "' where NodeId='" + (NodeId + 16) + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("探测器列表上移失败：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Move_Node_Down(int NodeId)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfraredAnalyze set NodeId='" + (NodeId + 16) + "' where NodeId='" + NodeId + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfraredAnalyze set NodeId='" + NodeId + "' where NodeId='" + (NodeId + 1) + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfraredAnalyze set NodeId='" + (NodeId + 1) + "' where NodeId='" + (NodeId + 16) + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("探测器列表下移失败：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void UpDate_IPAddress(int CameraId,string IP)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfraredAnalyze set IPAddress='"+IP+ "' where CameraId='" + CameraId + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("IP地址修改异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public  void UpDate_CameraName(int CameraId, string CameraName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfraredAnalyze set CameraName='" + CameraName + "' where CameraId='" + CameraId + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("相机名称修改异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void UpDate_CameraEnable(int CameraId,string remarks,bool Enable)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfraredAnalyze set Enable='" + Enable + "', Remarks='" + remarks + "' where CameraId='" + CameraId + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("相机名称修改异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public ArrayList Select_Spot(int CameraId,string type)
        {
            ArrayList arrayList = new ArrayList();
            DMSDK.temperSpot areaSpot = new DMSDK.temperSpot();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select type,X1,Y1,Emiss from TemperArea where Type='" + type + "'", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    areaSpot.type = (string)sqlDataReader.GetValue(0);
                    areaSpot.X1 = (int)sqlDataReader.GetValue(1);
                    areaSpot.Y1 = (int)sqlDataReader.GetValue(2);
                    areaSpot.Emiss = (int)sqlDataReader.GetValue(3);
                    arrayList.Add(areaSpot);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温点参数异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        public ArrayList Select_Area(int CameraId, string type)
        {
            ArrayList arrayList = new ArrayList();
            DMSDK.temperArea areaArea = new DMSDK.temperArea();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select type,X1,Y1,X2,Y2,Emiss,MeasureType from TemperArea where Type='" + type + "'", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    areaArea.type = (string)sqlDataReader.GetValue(0);
                    areaArea.X1 = (int)sqlDataReader.GetValue(1);
                    areaArea.Y1 = (int)sqlDataReader.GetValue(2);
                    areaArea.X2 = (int)sqlDataReader.GetValue(3);
                    areaArea.Y2 = (int)sqlDataReader.GetValue(4);
                    areaArea.Emiss = (int)sqlDataReader.GetValue(5);
                    areaArea.MeasureType=(int)sqlDataReader.GetValue(6);
                    arrayList.Add(areaArea);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温区域参数异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        public ArrayList Select_Line(int CameraId, string type)
        {
            ArrayList arrayList = new ArrayList();
            DMSDK.temperLine areaALine = new DMSDK.temperLine();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select type,X1,Y1,X2,Y2,X3,Y3,Emiss from TemperArea where Type='" + type + "'", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    areaALine.type = (string)sqlDataReader.GetValue(0);
                    areaALine.X1 = (int)sqlDataReader.GetValue(1);
                    areaALine.Y1 = (int)sqlDataReader.GetValue(2);
                    areaALine.X2 = (int)sqlDataReader.GetValue(3);
                    areaALine.Y2 = (int)sqlDataReader.GetValue(4);
                    areaALine.X3 = (int)sqlDataReader.GetValue(5);
                    areaALine.Y3 = (int)sqlDataReader.GetValue(6);
                    areaALine.Emiss = (int)sqlDataReader.GetValue(7);
                    arrayList.Add(areaALine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温线参数异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        DMSDK.temperSpot temperSpot;
        public ArrayList Select_All_Spot(int CameraId,string S)//总数据库中查询所有点的参数数据
        {
            ArrayList arrayList_All_Spot = new ArrayList();
            temperSpot = new DMSDK.temperSpot();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select Type,X1,Y1,Emiss from TemperArea where Type like 'S%' order by Type  ", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                   
                    temperSpot.type = (string)sqlDataReader.GetValue(0);
                    temperSpot.X1 = Convert.ToInt32(sqlDataReader.GetValue(1));
                    temperSpot.Y1 = (int)sqlDataReader.GetValue(2);
                    temperSpot.Emiss = (int)sqlDataReader.GetValue(3);
                   
                   
                    arrayList_All_Spot.Add(temperSpot);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温点参数异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList_All_Spot;
        }

        DMSDK.temperArea temperArea;
        public ArrayList Select_All_Area(int CameraId, string A)//总数据库中查询所有点的参数数据
        {
            ArrayList arrayList_All_Area = new ArrayList();
            temperArea = new DMSDK.temperArea();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select Type,X1,Y1,X2,Y2 ,Emiss,MeasureType from TemperArea where Type like 'A%' order by Type  ", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperArea.type = (string)sqlDataReader.GetValue(0);
                    temperArea.X1 = Convert.ToInt32(sqlDataReader.GetValue(1));
                    temperArea.Y1 = Convert.ToInt32(sqlDataReader.GetValue(2));
                    temperArea.X2 = Convert.ToInt32(sqlDataReader.GetValue(3));
                    temperArea.Y2 = Convert.ToInt32(sqlDataReader.GetValue(4));
                    temperArea.Emiss = Convert.ToInt32(sqlDataReader.GetValue(5));
                    temperArea.MeasureType = Convert.ToInt32(sqlDataReader.GetValue(6));
                    arrayList_All_Area.Add(temperArea);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温区域参数异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList_All_Area;
        }

        DMSDK.temperLine temperLine;
        public ArrayList Select_All_Line(int CameraId, string L)//总数据库中查询所有点的参数数据
        {
            ArrayList arrayList_All_Line = new ArrayList();
            temperLine = new DMSDK.temperLine();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select Type,X1,Y1,X2,Y2 ,X3,Y3,Emiss from TemperArea where Type like 'L%' order by Type  ", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperLine.type = (string)sqlDataReader.GetValue(0);
                    temperLine.X1 = (int)sqlDataReader.GetValue(1);
                    temperLine.Y1 = (int)sqlDataReader.GetValue(2);
                    temperLine.X2 = (int)sqlDataReader.GetValue(3);
                    temperLine.Y2 = (int)sqlDataReader.GetValue(4);
                    temperLine.X3 = (int)sqlDataReader.GetValue(5);
                    temperLine.Y3 = (int)sqlDataReader.GetValue(6);
                    temperLine.Emiss = (int)sqlDataReader.GetValue(7);
                    arrayList_All_Line.Add(temperLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温线参数异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList_All_Line;
        }

        public void Update_Spot(int CameraId,string type, int x1,int y1,int emiss)//更新数据库中 点参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update TemperArea set X1=" + x1 + ",Y1=" + y1 + ",Emiss=" + emiss + " where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温点数据库插入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delete_Spot(int CameraId, string type)//更新数据库中 点参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update TemperArea set X1='0',Y1='0'where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除测温点异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Update_Area(int CameraId, string type, int x1, int y1, int x2, int y2,int emiss,int measurerype)//更新数据库中 测温区域参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update TemperArea set X1=" + x1 + ",Y1=" + y1 + ", X2=" + x2 + ",Y2=" + y2 + ",Emiss=" + emiss + ", MeasureType='" + measurerype + "' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温区域数据库插入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delete_Area(int CameraId, string type)//更新数据库中 测温区域参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update TemperArea set X1='0',Y1='0', X2='0',Y2='0' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温区域数据库删除异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Update_Line(int CameraId, string type, int x1, int y1, int x2, int y2, int x3, int y3, int emiss)//更新数据库中 测温区域参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update TemperArea set X1=" + x1 + ",Y1=" + y1 + ", X2=" + x2 + ",Y2=" + y2 + ", X3=" + x3 + ",Y3=" + y3 + ",Emiss='" + emiss + "' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温线数据库插入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delete_Line(int CameraId, string type)//
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update TemperArea set X1='0',Y1='0', X2='0',Y2='0', X3='0',Y3='0' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温线数据库删除异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Insert_TemperData(int CameraId,string IPAddress,DateTime Datetime,int Type,int Number,int Temper,string Status)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("Insert Into TemperData(CameraId,IPAddress,Datetime,Type,Number,Temper,Status) Values('"+ CameraId + "','" + IPAddress + "','" + Datetime + "','" + Type + "','" + Number + "','" + Temper + "','" + Status + "')", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("测温数据插入异常：" + CameraId + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        StaticClass.StructTemperData temperData;
        public  ArrayList Select_TemperData(int CameraId,DateTime StartdateTime,DateTime EnddateTime)
        {
            ArrayList arrayList = new ArrayList();
            temperData = new StaticClass.StructTemperData();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("Select * from TemperData where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperData.CameraID = (int)sqlDataReader.GetValue(0);
                    temperData.IPAddress = (string)sqlDataReader.GetValue(1);
                    temperData.dateTime = (DateTime)sqlDataReader.GetValue(2);
                    temperData.Type = (int)sqlDataReader.GetValue(3);
                    temperData.Number = (int)sqlDataReader.GetValue(4);
                    temperData.Temper = (decimal)sqlDataReader.GetValue(5);
                    temperData.Status = (string)sqlDataReader.GetValue(6);
                    arrayList.Add(temperData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据库温度数据异常：" + CameraId + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        public ArrayList Select_TemperData(int CameraId,int type,int number, DateTime StartdateTime, DateTime EnddateTime)
        {
            ArrayList arrayList = new ArrayList();
            temperData = new StaticClass.StructTemperData();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                switch (type)
                {
                    case 0:
                        if (number != 4)
                        {
                            cmd = new SqlCommand("Select * from TemperData where number='" + (number + 1) + "' and type='" + type + "' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                        }
                        else
                        {
                            cmd = new SqlCommand("Select * from TemperData where  type='" + type + "' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                        }
                        break;
                    case 1:
                        cmd = new SqlCommand("Select * from TemperData where  type='" + type + "' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                        break;
                    case 2:
                        if (number != 3)
                        {
                            cmd = new SqlCommand("Select * from TemperData where number='" + (number + 5) + "' and type='" + type + "' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                        }
                        else
                        {
                            cmd = new SqlCommand("Select * from TemperData where  type='" + type + "' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                        }
                        break;
                    case 3:
                        cmd = new SqlCommand("Select * from TemperData where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                        break;
                }
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperData.CameraID = (int)sqlDataReader.GetValue(0);
                    temperData.IPAddress = (string)sqlDataReader.GetValue(1);
                    temperData.dateTime = (DateTime)sqlDataReader.GetValue(2);
                    temperData.Type = (int)sqlDataReader.GetValue(3);
                    temperData.Number = (int)sqlDataReader.GetValue(4);
                    temperData.Temper = (decimal)sqlDataReader.GetValue(5);
                    temperData.Status = (string)sqlDataReader.GetValue(6);
                    arrayList.Add(temperData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取数据库温度数据异常：" + CameraId + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return arrayList;
        }

        public void Update_Alarmconfig(int CameraId,string type,int spark,int alarmtemper,bool enable)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("update AlarmConfig set Spark='" + spark + "',AlarmTemper='" + alarmtemper + "', Enalbe='" + enable + "' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("报警参数数据库插入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public StaticClass.StructAlarm Select_AlarmConfig(int CameraId)//根据CameraID返回 该相机设置的报警信息
        {
            StaticClass.StructAlarm structAlarm = new StaticClass.StructAlarm();
            structAlarm.CameraId = CameraId;
            StaticClass.StructAlarmconfig structAlarmconfig = new StaticClass.StructAlarmconfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select *  from AlarmConfig", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                int i = 0;
                while (sqlDataReader.Read())
                {
                    structAlarmconfig.AreaType = (string)sqlDataReader.GetValue(0);
                    structAlarmconfig.Spark = (int)sqlDataReader.GetValue(1);
                    structAlarmconfig.AlarmTemper = (int)sqlDataReader.GetValue(2);
                    structAlarmconfig.Enable = (bool)sqlDataReader.GetValue(3);
                    structAlarm.structAlarmconfigs[i] = structAlarmconfig;
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("报警参数数据库插入异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return structAlarm;
        }
    }
}
