using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace 学生考勤
{
    class User
    {
        /// <summary>
        /// 学生用户
        /// </summary>
        public class Student
        {
            public static string sqlConStr = "Server=123.207.221.113;Database=Attendance;user id=sa;pwd=@wo778899";
            public string AccountName = "";
            public string StdNum = "";

            /// <summary>
            /// 空参构造
            /// </summary>
            public Student()
            { }

            /// <summary>
            /// 有参构造
            /// </summary>
            /// <param name="AccountName">账号名</param>
            /// <param name="StdNum">学号</param>
            public Student(string AccountName, string StdNum)
            {
                this.AccountName = AccountName;
                this.StdNum = StdNum;
            }

            /// <summary>
            /// 修改密码
            /// </summary>
            /// <param name="AccountName">账号名</param>
            /// <param name="Pswd">修改后密码</param>
            /// <returns>修改是否成功</returns>
            public bool ChangePswd(string AccountName, string Pswd)
            {
                string SqlCommand = "Update Attendance set password='" + Pswd + "' where account='" + AccountName + "'";
                return SqlTool.ExecuteNonQuery(sqlConStr, SqlCommand) >= 1;
            }

            /// <summary>
            /// 查询考勤信息
            /// </summary>
            /// <returns>包含指定考勤信息的数据集</returns>
            public DataSet GetAtdInfo()
            {
                string SqlCommand = "select * from AttendanceRecord where sNum='" + this.StdNum + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }

            /// <summary>
            /// 查询考勤统计信息（某个课程的某个日期的全部考勤信息）
            /// </summary>
            /// <param name="cNum">课程号</param>
            /// <param name="date">日期</param>
            /// <returns>包含指定考勤信息的数据集</returns>
            public DataSet GetCsAtdInfo(int cNum, string date)
            {
                string SqlCommand = "select * from AttendanceRecord where cNum=" + cNum.ToString() + " and date='" + date + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }

            /// <summary>
            /// 查询课程信息
            /// </summary>
            /// <returns>包含此用户的所有课程名的数据集</returns>
            public DataSet GetCourseInfo()
            {
                string SqlCommand = "select cName from Course where cNum in (select cNum from Class where sNum='" + this.StdNum + "')";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }


        }

        /// <summary>
        /// 教师用户
        /// </summary>
        public class Teacher
        {
            public static string sqlConStr = "Server=123.207.221.113;Database=Attendance;user id=sa;pwd=@wo778899";
            public string accountName = "";
            public string tNum = "";
            /// <summary>
            /// 空参构造
            /// </summary>
            public Teacher()
            { }

            /// <summary>
            /// 含参构造
            /// </summary>
            /// <param name="accountName">账号名</param>
            /// <param name="tNum">教工号</param>
            public Teacher(string accountName, string tNum)
            {
                this.accountName = accountName;
                this.tNum = tNum;
            }

            /// <summary>
            /// 修改密码
            /// </summary>
            /// <param name="AccountName">账号名</param>
            /// <param name="Pswd">修改后密码</param>
            /// <returns>修改是否成功</returns>
            public bool ChangePswd(string AccountName, string Pswd)
            {
                string SqlCommand = "Update Attendance set password='" + Pswd + "' where account='" + AccountName + "'";
                return SqlTool.ExecuteNonQuery(sqlConStr, SqlCommand) >= 1;
            }

            /// <summary>
            /// 查询考勤信息
            /// </summary>
            /// <param name="StdNum">学号</param>
            /// <returns>包含指定考勤信息的数据集</returns>
            public DataSet GetAtdInfo()
            {
                string SqlCommand = "select * from AttendanceRecord where sNum='" + this.tNum + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }

            /// <summary>
            /// 查询考勤统计信息（某个课程的全部考勤信息）
            /// </summary>
            /// <param name="cNum">课程号</param>
            /// <returns>包含指定考勤信息的数据集</returns>
            public DataSet GetCsAtdInfo(int cNum)
            {
                string SqlCommand = "select * from AttendanceRecord where cNum=" + cNum.ToString() + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }

            /// <summary>
            /// 查询考勤统计信息（某个课程的某个日期的全部考勤信息）
            /// </summary>
            /// <param name="cNum">课程号</param>
            /// <param name="date">日期</param>
            /// <returns>包含指定考勤信息的数据集</returns>
            public DataSet GetCsAtdInfo(int cNum, string date)
            {
                string SqlCommand = "select * from AttendanceRecord where cNum=" + cNum.ToString() + " and date='" + date + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }

            /// <summary>
            /// 查询课程信息
            /// </summary>
            /// <returns>包含此用户的所有课程名的数据集</returns>
            public DataSet GetCourseInfo()
            {
                string SqlCommand = "select cName from Course where cNum in (select cNum from Class where tNum='" + this.tNum + "')";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(sqlConStr, SqlCommand);
                da.Fill(ds);
                da.Dispose();
                return ds;
            }
        }
    }
}
