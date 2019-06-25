using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 学生考勤
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            注册 zc = new 注册();
            zc.form1 = this;
            zc.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (RegiLogin.Login(textBox1.Text, textBox2.Text))
            {
                //判断用户身份
                string sqlcommand = "select permission from AccountOwner where account='"+ textBox1.Text +"'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlcommand);
                da.Fill(ds);
                string permission = ds.Tables[0].Rows[0][0].ToString();
                //隐藏登录界面
                this.Hide();
                //显示用户界面
                if (String.Equals(permission, "学生"))//学生用户
                {
                    Form3 f3 = new Form3(textBox1.Text);
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    f3.form1 = this;
                    f3.Show();
                }
                else//教师用户
                {
                    Form2 f2 = new Form2(textBox1.Text);
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    f2.form1 = this;
                    f2.Show();
                }
            }
            else
                MessageBox.Show("登录失败！");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
