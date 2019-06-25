using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;    //引用与sql相关的命名空间
using System.Data;

namespace 学生考勤
{
    class RegiLogin
    {
        private static string Sql = "Server=123.207.221.113;Database=Attendance;user id=sa;pwd=@wo778899";

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="Account">账号名</param>
        /// <param name="Password">密码</param>
        /// <param name="Owner">账号身份</param>
        /// <returns>返回一个表示注册是否成功的布尔值</returns>
        public static bool Regi(string Account, string Password, string Owner,string Name)
        {
            //检查账号名是否存在
            string sqlCommand = "select * from AccountOwner where account='" + Account + "'";
            if (SqlTool.ExecuteReader(Sql, sqlCommand))
            {
                //若账号已存在
                return false;
            }
            else
            {
                //账号不存在则注册写入数据库
                string sqlCommamd_w = "insert into AccountOwner values('" + Account + "','" + Password + "','" + Owner + "','" + Name + "')";
                int a = SqlTool.ExecuteNonQuery(Sql, sqlCommamd_w);
                return a >= 1;
            }
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        /// <param name="Account">账号名</param>
        /// <param name="Password">密码</param>
        /// <returns>返回一个表示登陆是否成功的布尔值</returns>
        public static bool Login(string Account, string Password)
        {
            //查询账号是否存在
            string sqlCommand = "select password from AccountOwner where account='" + Account + "'";
            if (SqlTool.ExecuteReader(Sql, sqlCommand))
            {
                //账号存在检查密码是否正确
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(Sql, sqlCommand);
                da.Fill(ds);
                bool ifSuccess = String.Equals(Password, ds.Tables[0].Rows[0][0].ToString().Trim());
                da.Dispose();
                return ifSuccess;
            }
            else
            {
                //账号不存在
                return false;
            }

        }
    }
}
