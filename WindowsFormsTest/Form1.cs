using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
namespace WindowsFormsTest
{
    public partial class Form1 : Form
    {
        DataSet ds;//用于存放配置文件信息
        double total = 0.0d;//用于总计
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds.ReadXml(Application.StartupPath + "\\CashAcceptType.xml");
            //将读取到的记录绑定到下拉列表框中
            foreach (DataRowView dr in ds.Tables[0].DefaultView)
            {
                cbxType.Items.Add(dr["name"].ToString());
            }
            cbxType.SelectedIndex = 0;
        }
    }
}
