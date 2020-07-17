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
    public partial class Form3 : Form
    {
        string constr = "server = 127.0.0.1,1433; uid = 12345678; pwd = 12345678; database = Project;";
        string name;

        public Form3()
        {
            InitializeComponent();
        }

        public void setName(string name)
        {
            this.name = name;
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
                    bookName = "'" + bookName + "'";
                    sql = "SELECT * FROM Books WHERE bookName=" + bookName;
                }
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(ds, "Books");
            }

            dataGridView1.DataSource = ds.Tables[0];
            // 책 이름 = textBox10
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 책 대여
            DataSet ds = new DataSet();

            string bookID = textBox1.Text;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string sql = "SELECT bookName FROM Books WHERE bookID = '" + bookID + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Connection = conn;

                cmd.CommandText = "SELECT bookName FROM Books WHERE bookID = '" + bookID + "'";
                object scalarValue = cmd.ExecuteScalar();
                string bookName = scalarValue.ToString();

                cmd.CommandText = "SELECT UserID FROM Users WHERE name = '" + name + "'";
                scalarValue = cmd.ExecuteScalar();
                string userID = scalarValue.ToString();

                SqlCommand command = new SqlCommand();
                DateTime time = new DateTime();
                time = DateTime.Now;
                time.AddDays(14);
                command.Connection = conn;
                command.CommandText = "INSERT INTO BorrowList(bookID, userID, bookName, borrowDate, returnDate) VALUES('"
                    + bookID + "', '" + userID + "', '" + bookName + "', '" + DateTime.Now.ToString("yyyy-mm-dd" + "', '" + time.ToString() + "'");
                command.ExecuteNonQuery();
            }
            // 책 코드 = textBox1
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 책 반납
            // 책 코드 = textBox6
            // 책 이름 = textBox5
            // 저자 = textBox4
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 조회
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string sql = "SELECT UserID FROM Users WHERE name = '" + name + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandText = "SELECT UserID FROM Users WHERE name = '" + name + "'";
                object scalarValue = cmd.ExecuteScalar();
                string userID = scalarValue.ToString();

                sql = "SELECT * FROM BorrowList WHERE userID = '" + userID + "'";

                adapter.Fill(ds, "BorrowList");
            }

            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
