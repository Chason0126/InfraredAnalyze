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

        public SqlCreate()//构造函数 判断存不存在  不存在就新建数据库
        {
            if (!IsDBExist_SMInfrared_Config())
            {//不存在
                CreatDB_SMInfrared_Config();
                CreatTable_SMInfrared_Config();
                Insert_SMInfrared_Config();
            }
            for(int i = 1; i <= 16; i++)
            {
                if (!IsDBExist_SMInfraredX("SMInfrared"+i.ToString()))//不存在
                {
                    Create_DataBase_SMInfraredX("SMInfrared" + i.ToString());
                    Create_Table_SMInfraredX("SMInfrared" + i.ToString());
                    Insert_SMInfraredX("SMInfrared" + i.ToString());
                }
            }
         
        }

        #region//创建SMInfrared_Config
        /// <summary>
        /// 判断数据库SMInfrared_Config是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsDBExist_SMInfrared_Config()
        {
            int count = 0;
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("SELECT * FROM master.dbo.sysdatabases where name='SMInfrared_Config';", con_Server);
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

        /// <summary>
        /// 创建数据库SMInfrared_Config
        /// </summary>
        public void CreatDB_SMInfrared_Config()
        {
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("CREATE DATABASE SMInfrared_Config;", con_Server);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "创建数据库SMInfrared_Config失败！请检查数据库状态：" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_Server.Close();
            }
        }

        /// <summary>
        /// 创建数据表SMInfrared_Config
        /// </summary>
        public void CreatTable_SMInfrared_Config()
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SMInfrared_Config");
                con_DB.Open();
                cmd = new SqlCommand("create table SMInfrared_Config(ProjId int,ProjName nvarchar(MAX),DataBaseName nvarchar(MAX),Enable bit)", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "创建数据表SMInfrared_Config异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        /// <summary>
        /// 查询所有的项目名称
        /// </summary>
        /// <returns></returns>
        public List<StructClass.StructSMInfrared_Config> Select_SMInfrared_Config()
        {
            StructClass.StructSMInfrared_Config structSMInfrared_Config = new StructClass.StructSMInfrared_Config();
            List<StructClass.StructSMInfrared_Config> list = new List<StructClass.StructSMInfrared_Config>();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SMInfrared_Config");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfrared_Config order by ProjId", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    structSMInfrared_Config.ProjId = (int)sqlDataReader.GetValue(0);
                    structSMInfrared_Config.ProjName = (string)sqlDataReader.GetValue(1);
                    structSMInfrared_Config.DataBaseName = (string)sqlDataReader.GetValue(2);
                    structSMInfrared_Config.Enable = (bool)sqlDataReader.GetValue(3);
                    list.Add(structSMInfrared_Config);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取项目数据失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 插入预置项目16个 即最多16个项目
        /// </summary>
        public void Insert_SMInfrared_Config()
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SMInfrared_Config");
                con_DB.Open();
                for (int i = 1; i <= 16; i++)
                {
                    cmd = new SqlCommand("insert into SMInfrared_Config(ProjId,ProjName,DataBaseName,Enable) values('" + i + "','项目" + i + "','SMInfrared" + i + "','0')", con_DB);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "SMInfrared_Config初始化失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        /// <summary>
        /// 设置项目 新增 或者 删除
        /// </summary>
        public void UpDate_SMInfrared_Config(string ProjName, string DataBaseName, bool Enable)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database=SMInfrared_Config");
                con_DB.Open();
                cmd = new SqlCommand("Update SMInfrared_Config set ProjName='" + ProjName + "',Enable='" + Enable + "' where DataBaseName='" + DataBaseName + "'", con_DB);
                cmd.ExecuteNonQuery();
                if (Enable == false)
                {
                    Drop_AllDatabase(DataBaseName);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "设置项目失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }
        #endregion

        #region//创建SMInfraredX
        /// <summary>
        /// 判断SMInfraredX是否存在
        /// </summary>
        /// <param name="DBInfrared"></param>
        /// <returns></returns>
        public bool IsDBExist_SMInfraredX(string DBName)//判断数据库存不存在
        {
            int count = 0;
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("SELECT * FROM master.dbo.sysdatabases where name='" + DBName + "';", con_Server);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    count++;
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "数据库异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_Server.Close();
            }
            return ((count > 0) ? true : false);
        }

        /// <summary>
        /// 创建SM_InfraredX数据库
        /// </summary>
        public void Create_DataBase_SMInfraredX(string DBName)
        {
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("CREATE DATABASE " + DBName + "", con_Server);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "创建SM_InfraredX数据库失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_Server.Close();
            }
        }

        /// <summary>
        /// 创建SM_InfraredX数据库中的数据表
        /// </summary>
        public void Create_Table_SMInfraredX(string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("create table SMInfrared_ProjConfig(CameraId int,CameraName nvarchar(MAX),IPAddress varchar(15),Port int,NodeID int,Remarks nvarchar(max),Enable bit)", con_DB);//16台相机表
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table  SMInfrared_WarningRecords(CameraId int,IPAddress varchar(15),DateTime datetime,Type nvarchar(max),Message nvarchar(max))", con_DB);//告警记录表
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("create table SMInfrared_Pwd(Level int,Pwd nvarchar(max))", con_DB);//密码表
                cmd.ExecuteNonQuery();
                for(int i = 1; i <= 16; i++)
                {
                    cmd = new SqlCommand("create table Camera" + i + "_TemperArea(Type nvarchar(MAX),X1 int,Y1 int,X2 int,Y2 int,X3 int,Y3 int,Emiss int,MeasureType int)", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("create table Camera" + i + "_TemperData(CameraId int,IPAddress varchar(15),DateTime datetime,Type varchar(2),Temper decimal,Status  nvarchar(MAX))", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("create table Camera" + i + "_AlarmConfig(AreaId int,Type nvarchar(MAX),Spark int,AlarmTemper decimal,Enable bit)", con_DB);
                    cmd.ExecuteNonQuery();
                }
               
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "创建SM_InfraredX数据库中的数据表失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        /// <summary>
        /// 初始化SMInfrared数据表
        /// </summary>
        /// <param name="DBName"></param>
        public void Insert_SMInfraredX(string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                for (int i = 1; i <= 16; i++)
                {
                    cmd = new SqlCommand("insert into SMInfrared_ProjConfig(CameraId,CameraName,IPAddress,Port,NodeID,Remarks,Enable) values('" + i + "','" + ("探测器" + i) + "','" + ("192.168.1." + i) + "','5000','" + i + "','" + "无" + "','0')", con_DB);//插入16台预置相机参数
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,X2,Y2,X3,Y3,Emiss) values ('L1','0','0','0','0','0','0','90')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,Emiss) values ('S2','0','0','90')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,Emiss) values ('S3','0','0','90')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,Emiss) values ('S4','0','0','90')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,Emiss) values ('S5','0','0','90')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,X2,Y2,Emiss,MeasureType) values ('A6','0','0','0','0','90','-1')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,X2,Y2,Emiss,MeasureType) values ('A7','0','0','0','0','90','-1')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_TemperArea(Type,X1,Y1,X2,Y2,Emiss,MeasureType) values ('A8','0','0','0','0','90','-1')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('1','L1','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('2','S2','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('3','S3','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('4','S4','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('5','S5','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('6','A6','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('7','A7','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("insert into Camera" + i + "_AlarmConfig(AreaId,Type,Spark,AlarmTemper,Enable) values ('8','A8','0','50','0')", con_DB);
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("insert into SMInfrared_Pwd(Level, Pwd) values(1,'adsensor')", con_DB);//插入密码数据
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into SMInfrared_Pwd(Level, Pwd) values(2,'sm7003')", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into SMInfrared_Pwd(Level, Pwd) values(3,'123456')", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "SM_InfraredX数据库初始化失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }
        #endregion


        #region//删除数据库
        /// <summary>
        /// 删除数据库
        /// </summary>
        public void Drop_AllDatabase(string DBName)//数据库删除 请谨慎操作 相当危险
        {
            SqlCommand cmdCTable;
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;");
                con_DB.Open();
                string sqlcommandText = @"ALTER DATABASE " + DBName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [" + DBName + "]";//drop数据库  单纯的使用drop 如果数据库正在使用  则会删除异常
                cmdCTable = new SqlCommand(sqlcommandText, con_DB);
                cmdCTable.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "数据库删除失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }
        #endregion

        #region//获取密码数据
        /// <summary>
        /// 获取密码数据
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<StructClass.StructPwd> Select_Pwd(string DBName)//查询密码信息
        {
            List<StructClass.StructPwd> structPwds = new List<StructClass.StructPwd>();
            try
            {
                StructClass.StructPwd structPwd = new StructClass.StructPwd();
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfrared_Pwd order by level asc", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    structPwd.level = (int)sqlDataReader.GetValue(0);
                    structPwd.pwd = (string)sqlDataReader.GetValue(1);
                    structPwds.Add(structPwd);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取密码数据异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return structPwds;
        }
        #endregion

        #region//更新密码
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="level">权限等级</param>
        /// <param name="pwd">修改值</param>
        /// <param name="DBName">项目名称</param>
        public void Update_Pwd(int level,string pwd,string DBName)//更新密码信息
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfrared_Pwd set  Pwd='" + pwd + "' where level='" + level + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "密码修改失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }
        #endregion

        #region//获取项目Config
        /// <summary>
        /// 获取项目探测器配置参数  全部16个
        /// </summary>
        /// <param name="DBName">项目名称</param>
        /// <returns></returns>
        public List<StructClass.StructIAnalyzeConfig> Select_All_SMInfrared_ProjConfig(string DBName)
        {
            List<StructClass.StructIAnalyzeConfig> list = new List<StructClass.StructIAnalyzeConfig>();
            StructClass.StructIAnalyzeConfig structIAnalyzeConfig = new StructClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfrared_ProjConfig order by CameraId asc", con_DB);
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
                    list.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "数据库创建异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }
        #endregion

        #region//按相机ID获取某个探测器的配置信息
        /// <summary>
        /// 按CameraId 查询某台探测器的配置
        /// </summary>
        /// <param name="CameraID">相机编号</param>
        /// <param name="DBName">数据库名称</param>
        /// <returns></returns>
        public List<StructClass.StructIAnalyzeConfig> Select_SMInfrared_ProjConfig(int CameraID,string DBName)//查找相机信息。
        {
            List<StructClass.StructIAnalyzeConfig> list = new List<StructClass.StructIAnalyzeConfig>();
            StructClass.StructIAnalyzeConfig structIAnalyzeConfig = new StructClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfrared_ProjConfig where CameraID= '" + CameraID + "'", con_DB);
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
                    list.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "数据库创建异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 按IP获取探测器配置信息
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<StructClass.StructIAnalyzeConfig> Select_SMInfrared_ProjConfig(string IP,string DBName)//按CameraIp查找相机信息。
        {
            List<StructClass.StructIAnalyzeConfig> list = new List<StructClass.StructIAnalyzeConfig>();
            StructClass.StructIAnalyzeConfig structIAnalyzeConfig = new StructClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfrared_ProjConfig where IPAddress= '" + IP + "'", con_DB);
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
                    list.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取数据库信息失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 获取一个项目所有的探测器信息
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<StructClass.StructIAnalyzeConfig> Select_SMInfrared_ProjConfig(string DBName)//按CameraIp查找相机信息。
        {
            List<StructClass.StructIAnalyzeConfig> list = new List<StructClass.StructIAnalyzeConfig>();
            StructClass.StructIAnalyzeConfig structIAnalyzeConfig = new StructClass.StructIAnalyzeConfig();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select * from SMInfrared_ProjConfig ", con_DB);
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
                    list.Add(structIAnalyzeConfig);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取数据库信息失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 删除节点  后期谁想对画面或者树视图进行修改 删除 排序 可以修改
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="NodeId"></param>
        /// <param name="DBName"></param>
        public void Delete_Node_SMInfraredConfig(int CameraId,int NodeId,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand(" delete from SMInfrared_ProjConfig  where CameraId ='" + CameraId + "' ", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(" update SMInfrared_ProjConfig set CameraId=CameraId-1 where CameraId >='" + CameraId + "' ", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(" update SMInfrared_ProjConfig set NodeId=NodeId-1 where NodeId >='" + NodeId + "' ", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "删除探测器列表信息失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        #region//未启用
        /// <summary>
        /// 添加探测器  没启用
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="CameraName"></param>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        /// <param name="NodeID"></param>
        /// <param name="Remarks"></param>
        /// <param name="Enable"></param>
        /// <param name="DBName"></param>
        public void Insert_SMInfraredAnalyze(int CameraId, string CameraName, string IPAddress, int Port, int NodeID, string Remarks, bool Enable,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("insert into SMInfrared_ProjConfig(CameraId,CameraName,IPAddress,Port,NodeID,Remarks,Enable) values('" + CameraId + "','" + CameraName + "','" + IPAddress + "','" + Port + "','" + NodeID + "','" + Remarks + "','" + Enable + "')", con_DB);
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

        public void Move_Node_Up(int NodeId,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set NodeId='" + (NodeId + 16) + "' where NodeId='" + NodeId + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set NodeId='" + NodeId + "' where NodeId='" + (NodeId - 1) + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set NodeId='" + (NodeId - 1) + "' where NodeId='" + (NodeId + 16) + "'", con_DB);
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

        public void Move_Node_Down(int NodeId, string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set NodeId='" + (NodeId + 16) + "' where NodeId='" + NodeId + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set NodeId='" + NodeId + "' where NodeId='" + (NodeId + 1) + "'", con_DB);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set NodeId='" + (NodeId + 1) + "' where NodeId='" + (NodeId + 16) + "'", con_DB);
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

        public void UpDate_IPAddress(int CameraId,string IP,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set IPAddress='" + IP+ "' where CameraId='" + CameraId + "'", con_DB);
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
        #endregion

        /// <summary>
        /// 修改相机名称
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="CameraName"></param>
        /// <param name="DBName"></param>
        public void UpDate_CameraName(int CameraId, string CameraName,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set CameraName='" + CameraName + "' where CameraId='" + CameraId + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "相机名称修改异常" + ex.Message + ex.StackTrace);              
            }
            finally
            {
                con_DB.Close();
            }
        }

        /// <summary>
        /// 是否启用相机
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="remarks"></param>
        /// <param name="Enable"></param>
        /// <param name="DBName"></param>
        public void UpDate_CameraEnable(int CameraId,string remarks,bool Enable,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update SMInfrared_ProjConfig set Enable='" + Enable + "', Remarks='" + remarks + "' where CameraId='" + CameraId + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "相机启用修改异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        /// <summary>
        /// 插入告警信息
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="Ip"></param>
        /// <param name="dateTime"></param>
        /// <param name="Type"></param>
        /// <param name="Message"></param>
        /// <param name="DBName"></param>
        public void Insert_WarningRecords(int CameraId, string Ip, DateTime  dateTime,string Type,string Message,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("insert into SMInfrared_WarningRecords(CameraId,IPAddress,DateTime,Type,Message) values('" + CameraId + "','" + Ip + "','" + dateTime.ToString("G") + "','" + Type + "','" + Message + "') ", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "报警信息插入异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        /// <summary>
        /// 查询告警信息
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="Ip"></param>
        /// <param name="Type"></param>
        /// <param name="StartdateTime"></param>
        /// <param name="EnddateTime"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<StructClass.StructRecordsData> Select_WarningRecords(int CameraId, string Type, DateTime StartdateTime, DateTime EnddateTime,string DBName)
        {
            List<StructClass.StructRecordsData> list = new List<StructClass.StructRecordsData>();
            StructClass.StructRecordsData structRecords = new StructClass.StructRecordsData();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                if (CameraId == 0)//全部探测器
                {
                    if (Type == "全部")//全部类型
                    {
                        cmd = new SqlCommand("Select top 1000 * from SMInfrared_WarningRecords where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select * from SMInfrared_WarningRecords where type like '" + Type+"%' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                    }
                }
                else
                {
                    if (Type == "全部")//全部类型
                    {
                        cmd = new SqlCommand("Select top 1000 * from SMInfrared_WarningRecords where cameraId='" + CameraId + "' and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                    }
                    else
                    {
                        cmd = new SqlCommand("Select top 1000 * from SMInfrared_WarningRecords where cameraId='" + CameraId + "'  and convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "'  and type like '" + Type + "%'  order by datetime desc", con_DB);
                    }
                }
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    structRecords.CameraID = (int)sqlDataReader.GetValue(0);
                    structRecords.IPAddress = (string)sqlDataReader.GetValue(1);
                    structRecords.dateTime = (DateTime)sqlDataReader.GetValue(2);
                    structRecords.Type = (string)sqlDataReader.GetValue(3);
                    structRecords.Message = (string)sqlDataReader.GetValue(4);
                    list.Add(structRecords);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "报警信息查询异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 获取数据库中测温的信息
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="type"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<DMSDK.temperSpot> Select_Spot(int CameraId,string type,string DBName)
        {
            List<DMSDK.temperSpot> list = new List<DMSDK.temperSpot>();
            DMSDK.temperSpot areaSpot = new DMSDK.temperSpot();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select type,X1,Y1,Emiss from Camera" + CameraId + "_TemperArea where Type='" + type + "'", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    areaSpot.type = (string)sqlDataReader.GetValue(0);
                    areaSpot.X1 = (int)sqlDataReader.GetValue(1);
                    areaSpot.Y1 = (int)sqlDataReader.GetValue(2);
                    areaSpot.Emiss = (int)sqlDataReader.GetValue(3);
                    list.Add(areaSpot);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取测温点参数失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 获取数据库测温区域信息
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="type"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<DMSDK.temperArea> Select_Area(int CameraId, string type,string DBName)
        {
            List<DMSDK.temperArea> list = new List<DMSDK.temperArea>();
            DMSDK.temperArea areaArea = new DMSDK.temperArea();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select type,X1,Y1,X2,Y2,Emiss,MeasureType from Camera" + CameraId + "_TemperArea where Type='" + type + "'", con_DB);
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
                    list.Add(areaArea);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取测温区域参数失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 获取数据库测温线信息
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="type"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<DMSDK.temperLine> Select_Line(int CameraId, string type,string DBName)
        {
            List<DMSDK.temperLine> list = new List<DMSDK.temperLine>();
            DMSDK.temperLine areaALine = new DMSDK.temperLine();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select type,X1,Y1,X2,Y2,X3,Y3,Emiss from Camera" + CameraId + "_TemperArea where Type='" + type + "'", con_DB);
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
                    list.Add(areaALine);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取测温线参数失败" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        DMSDK.temperSpot temperSpot;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="S"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<DMSDK.temperSpot> Select_All_Spot(int CameraId,string S,string DBName)//总数据库中查询所有点的参数数据
        {
            List<DMSDK.temperSpot> list = new List<DMSDK.temperSpot>();
            temperSpot = new DMSDK.temperSpot();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select Type,X1,Y1,Emiss from Camera" + CameraId + "_TemperArea where Type like 'S%' order by Type  ", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                   
                    temperSpot.type = (string)sqlDataReader.GetValue(0);
                    temperSpot.X1 = Convert.ToInt32(sqlDataReader.GetValue(1));
                    temperSpot.Y1 = (int)sqlDataReader.GetValue(2);
                    temperSpot.Emiss = (int)sqlDataReader.GetValue(3);
                   
                    list.Add(temperSpot);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温点参数异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        DMSDK.temperArea temperArea;//好像重复了
        public List<DMSDK.temperArea> Select_All_Area(int CameraId, string A,string DBName)//总数据库中查询所有点的参数数据
        {
            List<DMSDK.temperArea> list = new List<DMSDK.temperArea>();
            temperArea = new DMSDK.temperArea();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select Type,X1,Y1,X2,Y2 ,Emiss,MeasureType from Camera" + CameraId + "_TemperArea where Type like 'A%' order by Type  ", con_DB);
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
                    list.Add(temperArea);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温区域参数异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        DMSDK.temperLine temperLine;
        public List<DMSDK.temperLine> Select_All_Line(int CameraId, string L,string DBName)//总数据库中查询所有点的参数数据
        {
            List<DMSDK.temperLine> list = new List<DMSDK.temperLine>();
            temperLine = new DMSDK.temperLine();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select Type,X1,Y1,X2,Y2 ,X3,Y3,Emiss from Camera" + CameraId + "_TemperArea where Type like 'L%' order by Type  ", con_DB);
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
                    list.Add(temperLine);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温线参数异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        public void Update_Spot(int CameraId,string type, int x1,int y1,int emiss,string DBName)//更新数据库中 点参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update Camera" + CameraId + "_TemperArea set X1=" + x1 + ",Y1=" + y1 + ",Emiss=" + emiss + " where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温点数据更新异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delete_Spot(int CameraId, string type,string DBName)//更新数据库中 点参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update  Camera" + CameraId + "_TemperArea set X1='0',Y1='0'where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "删除测温点异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Update_Area(int CameraId, string type, int x1, int y1, int x2, int y2,int emiss,int measurerype,string DBName)//更新数据库中 测温区域参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update  Camera" + CameraId + "_TemperArea set X1=" + x1 + ",Y1=" + y1 + ", X2=" + x2 + ",Y2=" + y2 + ",Emiss=" + emiss + ", MeasureType='" + measurerype + "' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温区域数据更新异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delete_Area(int CameraId, string type,string DBName)//更新数据库中 测温区域参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update  Camera" + CameraId + "_TemperArea set X1='0',Y1='0', X2='0',Y2='0',MeasureType='-1' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温区域数据库删除异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Update_Line(int CameraId, string type, int x1, int y1, int x2, int y2, int x3, int y3, int emiss,string DBName)//更新数据库中 测温区域参数的设置
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update Camera" + CameraId + "_TemperArea set X1=" + x1 + ",Y1=" + y1 + ", X2=" + x2 + ",Y2=" + y2 + ", X3=" + x3 + ",Y3=" + y3 + ",Emiss='" + emiss + "' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温线数据库插入异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Delete_Line(int CameraId, string type,string DBName)//
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update Camera" + CameraId + "_TemperArea set X1='0',Y1='0', X2='0',Y2='0', X3='0',Y3='0' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温线数据库删除异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Insert_TemperData(int CameraId,string IPAddress,DateTime Datetime,int Type,int Number,int Temper,string Status,string DBName)
        {
            try
            {
                string type = "";
                switch (Number)
                {
                    case 0:
                        type = "L1";
                        break;
                    case 1:
                        type = "S2";
                        break;
                    case 2:
                        type = "S3";
                        break;
                    case 3:
                        type = "S4";
                        break;
                    case 4:
                        type = "S5";
                        break;
                    case 5:
                        type = "A6";
                        break;
                    case 6:
                        type = "A7";
                        break;
                    case 7:
                        type = "A8";
                        break;
                }
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("Insert Into Camera" + CameraId + "_TemperData(CameraId,IPAddress,Datetime,Type,Temper,Status) Values('" + CameraId + "','" + IPAddress + "','" + Datetime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + type + "','" + Temper + "','" + Status + "')", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "测温数据插入异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public DateTime Select_TemperData_Top1(int CameraId, int num,string DBName)
        {
            DateTime dateTime = new DateTime();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select top 1 * from Camera" + CameraId + "_TemperData where number ='" + num + "' order by datetime desc ", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    dateTime = (DateTime)sqlDataReader.GetValue(2);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "根据时间判断插入频率：" + "探测器编号：" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return dateTime;
        }

        StructClass.StructTemperData temperData;
        /// <summary>
        /// 时间降序
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="StartdateTime"></param>
        /// <param name="EnddateTime"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public  List<StructClass.StructTemperData> Select_TemperData(int CameraId,DateTime StartdateTime,DateTime EnddateTime,string DBName)
        {
            List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
            temperData = new StructClass.StructTemperData();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("Select * from Camera" + CameraId + "_TemperData where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperData.CameraID = (int)sqlDataReader.GetValue(0);
                    temperData.IPAddress = (string)sqlDataReader.GetValue(1);
                    temperData.dateTime = (DateTime)sqlDataReader.GetValue(2);
                    temperData.Type = (string)sqlDataReader.GetValue(3);
                    temperData.Temper = (decimal)sqlDataReader.GetValue(4);
                    temperData.Status = (string)sqlDataReader.GetValue(5);
                    list.Add(temperData);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取数据库温度数据异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 时间升序
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="StartdateTime"></param>
        /// <param name="EnddateTime"></param>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public List<StructClass.StructTemperData> Select_TemperData_ASC(int CameraId, DateTime StartdateTime, DateTime EnddateTime, string DBName)
        {
            List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
            temperData = new StructClass.StructTemperData();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("Select * from Camera" + CameraId + "_TemperData where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperData.CameraID = (int)sqlDataReader.GetValue(0);
                    temperData.IPAddress = (string)sqlDataReader.GetValue(1);
                    temperData.dateTime = (DateTime)sqlDataReader.GetValue(2);
                    temperData.Type = (string)sqlDataReader.GetValue(3);
                    temperData.Temper = (decimal)sqlDataReader.GetValue(4);
                    temperData.Status = (string)sqlDataReader.GetValue(5);
                    list.Add(temperData);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取数据库温度数据异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        public List<StructClass.StructTemperData> Select_TemperData(int CameraId,string type, DateTime StartdateTime, DateTime EnddateTime,string DBName)
        {
            List<StructClass.StructTemperData> list = new List<StructClass.StructTemperData>();
            temperData = new StructClass.StructTemperData();
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                if (type == "全部")
                {
                    cmd = new SqlCommand("Select  * from Camera" + CameraId + "_TemperData where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' order by datetime desc", con_DB);
                }
                else
                {
                    cmd = new SqlCommand("Select  * from Camera" + CameraId + "_TemperData where convert(varchar(10),dateTime,120)>='" + StartdateTime.ToString("yyyy-MM-dd") + "' AND convert(varchar(10),dateTime,120)<='" + EnddateTime.ToString("yyyy-MM-dd") + "' and type='" + type + "' order by datetime desc", con_DB);
                }

                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    temperData.CameraID = (int)sqlDataReader.GetValue(0);
                    temperData.IPAddress = (string)sqlDataReader.GetValue(1);
                    temperData.dateTime = (DateTime)sqlDataReader.GetValue(2);
                    temperData.Type = (string)sqlDataReader.GetValue(3);
                    temperData.Temper = (decimal)sqlDataReader.GetValue(4);
                    temperData.Status = (string)sqlDataReader.GetValue(5);
                    list.Add(temperData);
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "获取数据库温度数据异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return list;
        }

        /// <summary>
        /// 删除历史数据
        /// </summary>
        /// <param name="CameraId"></param>
        /// <param name="dateTime"></param>
        /// <param name="DBName"></param>
        public void Delete_HisData(int CameraId,string dateTime,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                string datetime = dateTime + "%";
                cmd = new SqlCommand("delete  from Camera" + CameraId + "_TemperData where convert(varchar,dateTime,120) like '" + datetime + "'", con_DB);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "数据库维护：删除历史数据失败：" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        public void Update_Alarmconfig(int CameraId,string type,int spark,int alarmtemper,bool enable,string DBName)
        {
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("update Camera" + CameraId + "_AlarmConfig set Spark='" + spark + "',AlarmTemper='" + alarmtemper + "', Enable='" + enable + "' where Type='" + type + "'", con_DB);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "报警参数数据库插入异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
        }

        StructClass.StructAlarm structAlarm;
        StructClass.StructAlarmconfig structAlarmconfig;
        StructClass.StructAlarmconfig[] structAlarmconfigs;
        public StructClass.StructAlarm Select_AlarmConfig(int CameraId,string DBName)//根据CameraID返回 该相机设置的报警信息
        {
            structAlarm = new StructClass.StructAlarm();
            structAlarmconfigs = new StructClass.StructAlarmconfig[8];
            structAlarm.structAlarmconfigs = structAlarmconfigs;
            structAlarm.CameraId = CameraId;
            try
            {
                con_DB = new SqlConnection(@"server =.; integrated security = true;database='" + DBName + "'");
                con_DB.Open();
                cmd = new SqlCommand("select *  from Camera" + CameraId + "_AlarmConfig order by AreaId", con_DB);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                int i = 0;
                while (sqlDataReader.Read())//每个探测器8个数据
                {
                    structAlarmconfig.AreaType = (int)sqlDataReader.GetValue(0);
                    structAlarmconfig.Spark = (int)sqlDataReader.GetValue(2);
                    structAlarmconfig.AlarmTemper = Convert.ToInt32(sqlDataReader.GetValue(3));
                    structAlarmconfig.Enable = (bool)sqlDataReader.GetValue(4);
                    structAlarm.structAlarmconfigs[i] = structAlarmconfig;
                    i++;
                }
            }
            catch (Exception ex)
            {
                StaticClass.queue_Log.Enqueue(DateTime.Now.ToString() + "读取报警参数数据库异常" + ex.Message + ex.StackTrace);
            }
            finally
            {
                con_DB.Close();
            }
            return structAlarm;
        }
    }
}
