using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace 学生考勤
{
    public partial class Form2 : Form
    {
        public bool isStart = false;
        public string c_date = "";
        public string c_time = "";
        public Form form1;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string owner)
        {
            InitializeComponent();
            toolStripStatusLabel2.Text = owner;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!form1.Visible)
                Application.Exit();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                if (string.Equals(textBox2.Text, textBox3.Text))
                {
                    if (RegiLogin.Login(toolStripStatusLabel2.Text, textBox1.Text))
                    {
                        string sqlCommand = "Update AccountOwner set password='" + textBox2.Text + "' where account='"+ toolStripStatusLabel2.Text + "'";
                        if (SqlTool.ExecuteNonQuery(User.Student.sqlConStr, sqlCommand) == 1)
                        {
                            MessageBox.Show("修改成功,请重新登录！");
                            form1.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("修改失败！");
                    }
                    else
                        MessageBox.Show("原密码错误！");
                }
                else
                    MessageBox.Show("两次密码不一致！");
            }
            else
                MessageBox.Show("请将信息填写完整！");
        }

        /// <summary>
        /// 查询指定考勤信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "")
            {
                MessageBox.Show("请选择日期时间！");
            }
            else if(comboBox6.Text == "")
            {
                MessageBox.Show("请选择查询的课程！");
            }
            else if(comboBox7.Text == "")
            {
                MessageBox.Show("请选择查询的时段！");
            }
            else
            {
                string date = comboBox2.Text + "/" + comboBox3.Text + "/" + comboBox4.Text;
                string time = comboBox7.Text;
                DataSet ds = new DataSet();
                string sqlCommand = "select * from AttendanceRecord where date='"+ date +"' and time='"+ time +"' and cNum in (select cNum from Course where cName='"+ comboBox6.Text +"')";
                SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlCommand);
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        /// <summary>
        /// 查看指定课程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            if (comboBox5.Text == "")
            {
                MessageBox.Show("请选择要查询的课程！");
            }
            else
            {
                DataSet ds = new DataSet();
                string sqlCommand = "select * from Class where cNum in (select cNum from Course where cName='"+ comboBox5.Text +"')";
                SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlCommand);
                da.Fill(ds,"course");
                da.Dispose();
                dataGridView2.DataSource = ds.Tables["course"];
            }
        }

        /// <summary>
        /// 导入新的课程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            if(textBox5.Text == "")
            {
                MessageBox.Show("请输入课程号！");
            }
            else if(textBox6.Text == "")
            {
                MessageBox.Show("请输入课程名！");
            }
            else if(textBox4.Text == null)
            {
                MessageBox.Show("请输入学生信息文件位置！");
            }
            else
            {
                string path = @textBox4.Text;
                //确定文件路径是否存在问题
                bool path_isRight = true;
                try
                {
                    StreamReader SR = new StreamReader(path, Encoding.Default);
                }
                catch(Exception Ex)
                {
                    path_isRight = false;
                    MessageBox.Show(Ex.Message+"*"+path+"*");
                }
                //文件路径确认无误
                if (path_isRight)
                {
                    StreamReader sr = new StreamReader(path, Encoding.Default);  //path为文件路径
                    string line = "";
                    //确定此课程信息是否已经存在
                    string sqlCmd = "select * from Course where cNum='" + textBox5.Text + "'";
                    //课程信息已存在
                    if (SqlTool.ExecuteReader(User.Student.sqlConStr, sqlCmd))
                    {
                        //更新信息
                        string sqlcmd = "updata Course set cName='" + textBox6.Text + "' where cNum='" + textBox5.Text + "'";
                    }
                    else
                    {
                        //写入课程号,课程名和教师教工号
                        String sqlCommand1 = "insert into Course values('" + textBox5.Text + "','" + textBox6.Text + "','" + toolStripStatusLabel2.Text + "')";
                        SqlTool.ExecuteNonQuery(User.Student.sqlConStr, sqlCommand1);
                    }
                    //写入对应课程的学生信息
                    string sqlCommand2;
                    while ((line = sr.ReadLine()) != null)//按行读取 line为每行的数据
                    {
                        sqlCommand2 = "insert into Class values('" + textBox5.Text + "','" + line.Trim() + "')";
                        SqlTool.ExecuteNonQuery(User.Student.sqlConStr, sqlCommand2);
                    }
                    MessageBox.Show("导入成功，导入信息如表中所示！");
                    //导入成功后显示导入的信息
                    string sqlCommand3 = "select * from Class where cNum='" + textBox5.Text + "'";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlCommand3);
                    da.Fill(ds);
                    da.Dispose();
                    dataGridView2.DataSource = ds.Tables[0];
                }
            }
        }

        /// <summary>
        /// combox状态栏下拉时查询账号涉及的课程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox5_DropDown(object sender, EventArgs e)
        {
            string sqlCommand = "select cName from Course where tNum='" + toolStripStatusLabel2.Text + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = SqlTool.DataAdapter(User.Teacher.sqlConStr, sqlCommand);
            da.Fill(ds,"cName");
            da.Dispose();
            this.comboBox5.DataSource = ds.Tables["cName"];
            this.comboBox5.DisplayMember = "cName";
            this.comboBox5.ValueMember = "cName";
        }

        /// <summary>
        /// 开始考勤，显示二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if(isStart)
            {
                MessageBox.Show("正在考勤中！");
            }
            else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("请选择要考勤的课程！");
            }
            else
            {
                isStart = true;
                //生成初始化考勤记录（全为缺勤）
                string cNum = "";
                //List<string> sNum = new List<string>();
                //查询课程号
                string sqlCommand_cNum = "select cNum from Course where cName='" + comboBox1.Text + "' and tNum='"+ toolStripStatusLabel2.Text + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlCommand_cNum);
                da.Fill(ds);
                da.Dispose();
                cNum = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
                //查询对应课程的学生学号
                string sqlCommand_sNum = "select sNum from Class where cNum='" + cNum + "'";
                DataSet ds_sNum = new DataSet();
                SqlDataAdapter Da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlCommand_sNum);
                Da.Fill(ds_sNum);
                //获取时间
                string localtime = DateTime.Now.ToLocalTime().ToString();        // 2019/6/14 20:12:12
                string date = localtime.Split(' ')[0];
                string time = localtime.Split(' ')[1].Split(':')[0];
                c_date = date;
                c_time = time;
                foreach (DataRow dr in ds_sNum.Tables[0].Rows)
                {
                    string sqlCmd = "insert into AttendanceRecord values('"+ cNum +"','"+ dr[0].ToString() +"','"+ "缺勤" +"','"+ date +"','"+ time +"')";
                    SqlTool.ExecuteNonQuery(User.Student.sqlConStr, sqlCmd);
                }
                MessageBox.Show("考勤开始,请扫描屏幕二维码进行签到！");
                
                //显示二维码
                string url = string.Format(@"http://123.207.221.113:8080/home/qiandao.png");
                System.Net.WebRequest webreq = System.Net.WebRequest.Create(url);
                System.Net.WebResponse webres = webreq.GetResponse();
                using (System.IO.Stream stream = webres.GetResponseStream())
                {
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
        }

        /// <summary>
        /// 选择考勤课程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_DropDown(object sender, EventArgs e)
        {
            string sqlCommand = "select cName from Course where tNum='" + toolStripStatusLabel2.Text + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = SqlTool.DataAdapter(User.Teacher.sqlConStr, sqlCommand);
            da.Fill(ds, "cName");
            da.Dispose();
            this.comboBox1.DataSource = ds.Tables["cName"];
            this.comboBox1.DisplayMember = "cName";
            this.comboBox1.ValueMember = "cName";
        }

        /// <summary>
        /// 停止考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Button2_Click(object sender, EventArgs e)
        {
            if (isStart)
            {
                isStart = false;
                MessageBox.Show("已停止考勤！");
                pictureBox1.Image = null;
                //查询本次考勤信息
                string sqlCommand = "select * from AttendanceRecord where date='" + c_date + "' and time='" + c_time + "' and cNum in (select cNum from Course where cName='"+ comboBox1.Text +"')";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlCommand);
                da.Fill(ds);
                da.Dispose();
                //创建窗口显示本次考勤信息
                本次考勤信息 msg = new 本次考勤信息(ds);
                msg.Show();
            }
            else
            {
                MessageBox.Show("未开始考勤！");
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择查询课程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox6_DropDown(object sender, EventArgs e)
        {
            string sqlCommand = "select cName from Course where tNum='" + toolStripStatusLabel2.Text + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = SqlTool.DataAdapter(User.Teacher.sqlConStr, sqlCommand);
            da.Fill(ds, "cName");
            da.Dispose();
            this.comboBox6.DataSource = ds.Tables["cName"];
            this.comboBox6.DisplayMember = "cName";
            this.comboBox6.ValueMember = "cName";
        }
    }
}
