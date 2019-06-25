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
    public partial class 本次考勤信息 : Form
    {
        public DataSet ds;
        public 本次考勤信息()
        {
            InitializeComponent();
        }
        public 本次考勤信息(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void 本次考勤信息_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.ds.Tables[0];
        }
    }
}
