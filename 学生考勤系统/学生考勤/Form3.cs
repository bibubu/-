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
    public partial class Form3 : Form
    {
        public Form form1;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string owner)
        {
            InitializeComponent();
            toolStripStatusLabel2.Text = owner;

        }
        /// <summary>
        /// 选择查询课程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox6_DropDown(object sender, EventArgs e)
        {
            string sqlCommand = "select cName from Course where cNum in (select cNum from Class where sNum='" + toolStripStatusLabel2.Text + "')";
            DataSet ds = new DataSet();
            SqlDataAdapter da = SqlTool.DataAdapter(User.Teacher.sqlConStr, sqlCommand);
            da.Fill(ds, "cName");
            da.Dispose();
            this.comboBox6.DataSource = ds.Tables["cName"];
            this.comboBox6.DisplayMember = "cName";
            this.comboBox6.ValueMember = "cName";
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!form1.Visible)
                Application.Exit();
        }

        /// <summary>
        /// 查看课程信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            string sqlcommand = "select s1.cNum,s2.cName from (select * from Class)s1 join (select * from Course)s2 on s1.cNum=s2.cNum where sNum='"+ toolStripStatusLabel2.Text + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlcommand);
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
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
                        string sqlCommand = "Update AccountOwner set password='" + textBox2.Text + "' where account='" + toolStripStatusLabel2.Text + "'";
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
        /// 查询考勤记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button5_Click(object sender, EventArgs e)
        {
            if(comboBox6.SelectedItem == null)
            {
                MessageBox.Show("请选择查询的课程！");
            }
            else
            {
                string sqlcommand = "select * from AttendanceRecord where sNum='"+ toolStripStatusLabel2.Text + "' and cNum in (select cNum from Course where cName='"+ comboBox6.Text + "')";
                DataSet ds = new DataSet();
                SqlDataAdapter da = SqlTool.DataAdapter(User.Student.sqlConStr, sqlcommand);
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
