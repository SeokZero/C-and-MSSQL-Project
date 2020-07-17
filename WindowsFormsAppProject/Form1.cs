using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppProject
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection();
        string constr = "server = 127.0.0.1,1433; uid = 12345678; pwd = 12345678; database = Project;";

        public Form1()
        {
            InitializeComponent();
        }

        // textBox1 = 이름
        // textBox2 = 비밀번호

        private void button1_Click(object sender, EventArgs e)
        {
            // DB연동
            try
            {
                conn.ConnectionString = constr;
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            string name = textBox1.Text;
            string password = textBox2.Text;

            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string sql = "SELECT password FROM Users Where name = '" + name + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Connection = conn;

                cmd.CommandText = "SELECT password FROM Users Where name = '" + name + "'";

                object scalarValue = cmd.ExecuteScalar();

                if (scalarValue != null) // 이름, 비번이 맞는지 확인
                {
                    string Pwd = scalarValue.ToString().Trim();

                    if (Pwd == password)
                    {
                        cmd.CommandText = "SELECT isAdmin FROM Users Where name = '" + name + "'";
                        scalarValue = cmd.ExecuteScalar();
                        string n = scalarValue.ToString();
                        if (n == "1") // 사서면 true, 학생이면 false
                        {
                            this.Visible = false;
                            Form2 form2 = new Form2();
                            form2.Show();
                        }
                        else
                        {
                            this.Visible = false;
                            Form3 form3 = new Form3();
                            form3.setName(name);
                            form3.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("정보가 잘못되었습니다.");
                }
            }

            
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
