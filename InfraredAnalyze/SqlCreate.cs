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
                DB_Create();//则创建InfraredAnalyze数据库
                Table_Create();//创建SMInfrardeAnalyze数据表
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
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=" + SM_InfraredCamera + "");
                con_DB.Open();
                cmd = new SqlCommand("create table TemperArea(Type nvarchar(MAX),X1 int,Y1 int,X2 int,Y2 int,X3 int,Y3 int,Emiss int,MeasureType int)", con_DB);
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
        ArrayList arrayList;
        public ArrayList Select_All_SMInfraredConfig()//按nodeid降序排列
        {
            arrayList = new ArrayList();
            StaticClass.StructIAnalyzeConfig structIAnalyzeConfig = new StaticClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfraredAnalyze order by NodeID asc", con_DB);
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

        public ArrayList Select_SMInfraredConfig(int CameraID)
        {
            arrayList = new ArrayList();
            StaticClass.StructIAnalyzeConfig structIAnalyzeConfig = new StaticClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfraredAnalyze where CameraID= " + CameraID + "", con_DB);
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

        int num;
        public int Select_Num_SMInfraredConfig()
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
                con_DB.Open();
                cmd = new SqlCommand("select count(*) from SMInfraredAnalyze", con_DB);
                num = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库查询异常：" + ex.Message);
            }
            finally
            {
                con_DB.Close();
            }
            return num;
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

        public ArrayList Select_Spot(int CameraId,string type)
        {
            arrayList = new ArrayList();
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
            arrayList = new ArrayList();
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
            arrayList = new ArrayList();
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
        ArrayList arrayList_All_Spot;
        public ArrayList Select_All_Spot(int CameraId,string S)//总数据库中查询所有点的参数数据
        {
            arrayList_All_Spot = new ArrayList();
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
                    temperSpot.X1 = (int)sqlDataReader.GetValue(1);
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
        ArrayList arrayList_All_Area;
        public ArrayList Select_All_Area(int CameraId, string A)//总数据库中查询所有点的参数数据
        {
            arrayList_All_Area = new ArrayList();
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
        ArrayList arrayList_All_Line;
        public ArrayList Select_All_Line(int CameraId, string L)//总数据库中查询所有点的参数数据
        {
            arrayList_All_Line = new ArrayList();
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
    }
}
