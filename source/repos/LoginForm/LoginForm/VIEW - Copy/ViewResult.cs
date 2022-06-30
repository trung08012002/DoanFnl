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
    public partial class ViewResult : Form
    {
        public ViewResult()
        {
            InitializeComponent();
            GetLevels();
            GetMembers();
            DisplayResult();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-455C16V\SQLEXPRESS;Initial Catalog=Quizdb;Integrated Security=True");

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
        private void GetMembers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select MName from Memberstbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MName", typeof(string));
            dt.Load(rdr);
            MbNameCb.ValueMember = "MName";
            MbNameCb.DataSource = dt;

            Con.Close();
        }
        private void DisplayResult()
        {
            Con.Open();
            string Query = "select * from Resultstbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ResultDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FilterByLv()
        {
            Con.Open();
            string Query = "select * from Resultstbl where RLv = '"+LvCb.SelectedValue.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ResultDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FilterByName()
        {
            Con.Open();
            string Query = "select * from Resultstbl where Rmem = '" + MbNameCb.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); 
            sda.Fill(ds);
            ResultDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void LvCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByLv();
        }

        private void MbNameCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByName();
        }
    }
}
