using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppProject
{
    public partial class Form2 : Form
    {
        string constr = "server = 127.0.0.1,1433; uid = 12345678; pwd = 12345678; database = Project;";

        public Form2()
        {
            InitializeComponent();
        }

        private void 로그아웃ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 검색
            DataSet ds = new DataSet();

            string bookName = textBox10.Text;
            string sql;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                if (bookName == "")
                {
                    sql = "SELECT * FROM Books";
                }
                else
                {
                    sql = "SELECT * FROM Books WHERE bookName = '" + bookName + "'";
                }
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(ds, "Books");
            }

            dataGridView1.DataSource = ds.Tables[0];
            // 책 이름 = textBox10
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 책 추가
            string bookID = textBox1.Text;
            string bookName = textBox2.Text;
            string author = textBox3.Text;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = conn;
                command.CommandText = "INSERT INTO Books(bookID, bookName, author) VALUES('"
                    + bookID + "', '" + bookName + "', '" + author + "');";
                command.ExecuteNonQuery();

                button4_Click(null, null);
            }
            // 책 코드 = textBox1
            // 책 이름 = textBox2
            // 저자 = textBox3
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 책 변경
            string bookID = textBox6.Text;
            string bookName = textBox5.Text;
            string author = textBox4.Text;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = conn;
                command.CommandText = "UPDATE Books SET bookName ='" + bookName + "'WHERE bookID =" + bookID;
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE Books SET author ='" + author + "'WHERE bookID =" + bookID;
                command.ExecuteNonQuery();

                button4_Click(null, null);
            }
            // 책 코드 = textBox6
            // 새 이름 = textBox5
            // 새 저자 = textBox4
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 책 삭제
            string bookID = textBox9.Text;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                SqlCommand command = new SqlCommand();

                command.Connection = conn;
                command.CommandText = "DELETE FROM BOOKS WHERE BookID =" + bookID;
                command.ExecuteNonQuery();

                button4_Click(null, null);
            }
            // 책 코드 = textBox9S
            // 책 이름 = textBox8
            // 저자 = textBox7
        }
    }
}
