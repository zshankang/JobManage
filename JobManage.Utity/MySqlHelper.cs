using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace JobManage.Utity
{
    /// <summary>
    /// MySql 工具类
    /// </summary>
    public class MySqlHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        /// <summary>
        /// 执行Sql，返回受影响条数
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static int ExceuteSql(string sqlStr)
        {
            int result = 0;
            if (string.IsNullOrEmpty(sqlStr))
            {
                return result;
            }
            try
            {
                using(MySqlConnection SqlConnection = new MySqlConnection(connStr))
                {
                    MySqlCommand command = new MySqlCommand(sqlStr, SqlConnection);
                    if (SqlConnection.State == System.Data.ConnectionState.Closed)
                        SqlConnection.Open();
                    result = command.ExecuteNonQuery();
                    SqlConnection.Close();
                    SqlConnection.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }

            return result;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sqlStr)
        {
            DataSet ds = null;
            try
            {
                if (!string.IsNullOrEmpty(sqlStr))
                {
                    using (MySqlConnection SqlConnection = new MySqlConnection(connStr))
                    {
                        ds = new DataSet();
                        MySqlCommand command = new MySqlCommand(sqlStr, SqlConnection);
                        MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(command);
                        if (SqlConnection.State == ConnectionState.Closed)
                            SqlConnection.Open();
                        sqlDataAdapter.Fill(ds);
                        SqlConnection.Close();
                        SqlConnection.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                
            }
            return ds;
        }
        /// <summary>
        /// 获取第一行第一列
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static object GetReader(string sqlStr)
        {
            object ds = null;
            try
            {
                if (!string.IsNullOrEmpty(sqlStr))
                {
                    using (MySqlConnection SqlConnection = new MySqlConnection(connStr))
                    {
                        MySqlCommand command = new MySqlCommand(sqlStr);
                        if (SqlConnection.State == ConnectionState.Closed)
                            SqlConnection.Open();
                        ds = command.ExecuteScalar();
                        if (SqlConnection.State == ConnectionState.Closed)
                            SqlConnection.Open();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
               
            }
            return ds;
        }

    }
}
