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
                cmd = new SqlCommand("create table TemperArea(AreaId int,Type nvarchar(MAX),X1 int,Y1 int,X2 int,Y2 int,X3 int,Y3 int,Emiss int,MeasureType int)", con_DB);
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

        public ArrayList Select_Spot(int CameraId)
        {
            arrayList = new ArrayList();
            DMSDK.temperAreaSpot areaSpot = new DMSDK.temperAreaSpot();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_InfraredCamera" + CameraId + "");
                con_DB.Open();
                cmd = new SqlCommand("select AreaId,X1,Y1,Emiss from TemperArea where Type='S' order by AreaId", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    areaSpot.AreaId = (int)sqlDataReader.GetValue(0);
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

        public StructType ConverBytesToStructure<StructType>(byte[] bytesBuffer)
        {
            // 检查长度  
            if (bytesBuffer.Length != Marshal.SizeOf(typeof(StructType)))
            {
                throw new ArgumentException("bytesBuffer参数和structObject参数字节长度不一致。");
            }
            //分配一个未托管类型变量  
            IntPtr bufferHandler = Marshal.AllocHGlobal(bytesBuffer.Length);
            //逐个复制，也可以直接用copy()方法  
            for (int index = 0; index < bytesBuffer.Length; index++)
            {
                Marshal.WriteByte(bufferHandler, index, bytesBuffer[index]);
            }
            //从非托管类型转化为托管类型变量  
            StructType structObject = (StructType)Marshal.PtrToStructure(bufferHandler, typeof(StructType));
            //释放非托管类型变量  
            Marshal.FreeHGlobal(bufferHandler);
            return structObject;
        }

        public static object BytesToStruct(byte[] bytes, Type strType)
        {
            //获取结构体的大小（以字节为单位）  
            int size = Marshal.SizeOf(strType);
            //简单的判断（可以去掉）  
            if (size > bytes.Length)
            {
                return null;
            }

            //从进程的非托管堆中分配内存给structPtr  
            IntPtr strPtr = Marshal.AllocHGlobal(size);

            //将数据从一维托管数组bytes复制到非托管内存指针strPtr  
            Marshal.Copy(bytes, 0, strPtr, size);

            //将数据从非托管内存块封送到新分配的指定类型的托管对象  
            //将内存空间转换为目标结构体  
            object obj = Marshal.PtrToStructure(strPtr, strType);

            //释放以前使用 AllocHGlobal 从进程的非托管内存中分配的内存  
            Marshal.FreeHGlobal(strPtr);
            return obj;
        }
    }
}
