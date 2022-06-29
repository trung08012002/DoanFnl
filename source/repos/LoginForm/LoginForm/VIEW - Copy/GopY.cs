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

namespace LoginForm
{
    public partial class GopY : Form
    {
        public GopY()
        {
            InitializeComponent();
            DisplayCmt();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-455C16V\SQLEXPRESS;Initial Catalog=Quizdb;Integrated Security=True");
        private void DisplayCmt()
        {
            Con.Open();
            string Query = "select * from Commenttbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ListcmtDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void NewcmtTbn_Click(object sender, EventArgs e)
        {
            if (Totb.Text == "" || TitileTb.Text == "" || CmtTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Commenttbl (ID_Member,Title,Content,Date_Sent,Send_To) values (@CmM,@CmT,@CmC,@CmD,@CmS)", Con);
                    cmd.Parameters.AddWithValue("@CmM", Fromtb.Text);
                    cmd.Parameters.AddWithValue("@CmS", Totb.Text);
                    cmd.Parameters.AddWithValue("@CmT", TitileTb.Text);
                    cmd.Parameters.AddWithValue("@CmC", CmtTb.Text);
                    cmd.Parameters.AddWithValue("@CmD", DateTime.Now.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Comment saved");
                    Con.Close();
                    /* Reset();
                     DisplayMembers();*/
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void ListcmtDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Key = 0;
            Fromtb.Text = ListcmtDGV.SelectedRows[0].Cells[1].Value.ToString();
            TitileTb.Text = ListcmtDGV.SelectedRows[0].Cells[2].Value.ToString();
            CmtTb.Text = ListcmtDGV.SelectedRows[0].Cells[3].Value.ToString();
            Totb.Text = ListcmtDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (Fromtb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ListcmtDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}

