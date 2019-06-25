using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 学生考勤
{
    public partial class 注册 : Form
    {
        public Form form1;
        public 注册()
        {
            InitializeComponent();
        }

        private void 注册_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")//注册信息不为空
            {
                if (String.Equals(textBox2.Text, textBox3.Text))//两次注册密码一致
                {
                    if (checkBox1.Checked == true)//学生注册
                    {
                        if (RegiLogin.Regi(textBox1.Text, textBox2.Text, checkBox1.Text, textBox4.Text))
                        {
                            MessageBox.Show("注册成功！");
                            //跳转到登录页面
                            form1.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("账号名已存在！");
                    }
                    else if (checkBox2.Checked == true)//教师注册
                    {
                        if (RegiLogin.Regi(textBox1.Text, textBox2.Text, checkBox2.Text, textBox4.Text))
                        {
                            MessageBox.Show("注册成功！");
                            //跳转到登录页面
                            form1.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("账号名已存在！");
                    }
                    else
                        MessageBox.Show("请选择用户身份！");
                }
                else
                    MessageBox.Show("两次输入的密码不一致！");
            }
            else
                MessageBox.Show("请完善注册信息！");
        }

        //显示密码
        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else if (checkBox3.Checked == false)
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }

        //学生身份单项选中
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox2.Checked = false;
        }

        //教师身份单项选中
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkBox1.Checked = false;
        }
    }
}
