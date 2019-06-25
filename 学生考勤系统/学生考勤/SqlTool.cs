using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;    //引用与sql相关的命名空间
using System.Data;

namespace 学生考勤
{
    /// <summary>
    /// 数据库工具类，包括数据库的增删查改功能
    /// </summary>
    public class SqlTool
    {
        /// <summary>
        /// 根据数据库连接字符串和数据库命令字符串进行数据库操作
        /// </summary>
        /// <param name="sqlConStr">数据库连接字符串</param>
        /// <param name="sqlCommand">数据库命令字符串</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sqlConStr, string sqlCommand)
        {
            SqlConnection Con = new SqlConnection(sqlConStr);
            SqlCommand sqlCmd = new SqlCommand(sqlCommand, Con);
            try
            {
                Con.Open();
                int ret = sqlCmd.ExecuteNonQuery();
                Con.Close();
                return ret;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 查询数据库中是否有满足条件的数据
        /// </summary>
        /// <param name="sqlConStr">数据库连接字符串</param>
        /// <param name="sqlCommand">数据库命令字符串</param>
        /// <returns>表示所查询的数据是否存在的一个布尔值</returns>
        public static bool ExecuteReader(string sqlConStr, string sqlCommand)
        {
            SqlConnection Con = new SqlConnection(sqlConStr);
            SqlCommand sqlCmd = new SqlCommand(sqlCommand, Con);
            try
            {
                Con.Open();
                SqlDataReader dr = sqlCmd.ExecuteReader();
                bool ifExist = dr.HasRows;
                dr.Close();
                Con.Close();
                return ifExist;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 返回一个进行数据库操作后的SqlDataAdapter对象
        /// </summary>
        /// <param name="sqlConStr">数据库连接字符串</param>
        /// <param name="sqlCommand">数据库命令字符串</param>
        /// <returns>一个进行数据库操作后的SqlDataAdapter对象</returns>
        public static SqlDataAdapter DataAdapter(string sqlConStr, string sqlCommand)
        {
            SqlConnection Con = new SqlConnection(sqlConStr);
            try
            {
                Con.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand, Con);
                Con.Close();
                return da;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
