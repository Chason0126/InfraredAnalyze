using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    class SqlCreate
    {

        static string conServer = @"server =.; integrated security = true;";
        SqlConnection con_Server = new SqlConnection(conServer);
        SqlCommand cmd;
        SqlConnection con_DB;

        public bool IsDBExist()//判断数据库存不存在
        {
            int count = 0;
            try
            {
                con_Server.Open();
                cmd = new SqlCommand("SELECT * FROM master.dbo.sysdatabases where name=SM_Infrared;", con_Server);
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
            if(!IsDBExist())
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
            }
        }

        public void Table_Create()
        {
            con_DB = new SqlConnection(@"server =.; integrated security = true;database=SM_Infrared");
            con_DB.Open();
        }
    }
}
