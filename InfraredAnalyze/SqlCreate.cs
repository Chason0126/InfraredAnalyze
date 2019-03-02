using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;

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

    }
}
