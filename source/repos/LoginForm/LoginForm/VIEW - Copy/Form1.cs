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
using LoginForm.BBL;
using LoginForm.DTO;
namespace LoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetLevels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GetLevels()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select LvName from Levelsstbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LvName", typeof(string));
            dt.Load(rdr);
            LvCb.ValueMember = "LvName";
            LvCb.DataSource = dt;

            Con.Close();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-455C16V\SQLEXPRESS;Initial Catalog=Quizdb;Integrated Security=True");
        public static string memberName = "", lvName = "";
        private void button1_Click(object sender, EventArgs e)
        {
           /* if(BBLQL.Instance.checkuser(txtUserName.Text,txtPassword.Text))
            {
                Form3 f = new Form3();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("The Username or Password you entered is incorrect, try again", "Warning!");
                txtUserName.Text="";
                txtPassword.Text="";
                txtUserName.Focus();
            }*/
           if(txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("enter info");
            }

            else
            {
                Con.Open();

                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Memberstbl where MName = '"+txtUserName.Text+"' and MPass ='"+txtPassword.Text+"' ", Con);
                DataTable dt = new DataTable();
                  sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    memberName = txtUserName.Text;
                    lvName = LvCb.SelectedValue.ToString();
                    Load obj = new Load();
                    obj.Show();
                    this.Hide();
                }
                Con.Close();   
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            /*Form2 f = new Form2();
            this.Hide();
            f.ShowDialog();*/
            Members obj = new Members();
            obj.Show();
            this.Hide();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
