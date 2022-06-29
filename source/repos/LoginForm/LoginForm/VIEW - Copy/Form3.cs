using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginForm.BBL;
using LoginForm.DTO;
namespace LoginForm
{
    public partial class Form3 : Form
    {

        DataTable dt;
        public Form3()
        {
            InitializeComponent();
            GUI();
        }  

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                PageInfo pif = new PageInfo(BBLQL.Instance.getinforbyEmail(dataGridView1.Rows[0].Cells["email"].Value.ToString()).id);
                this.Hide();
                pif.ShowDialog();
            }

        }
        private void GUI()
        {
            inforuserView a = new inforuserView();
            dt = new DataTable();
            foreach (var i in a.GetType().GetProperties())
            {
                if(i.Name!="STT")
                {
                    comboBox1.Items.Add(i.Name);
                }
                dt.Columns.Add(i.Name);
                
            }
          
            dataGridView1.DataSource = dt;
        }
        private void setSTT()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count >0)
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    BBLQL.Instance.removeAccount(BBLQL.Instance.getinforbyEmail(dataGridView1.Rows[0].Cells["email"].Value.ToString()).id);
                    BBLQL.Instance.removeInfor(BBLQL.Instance.getinforbyEmail(dataGridView1.Rows[0].Cells["email"].Value.ToString()).id);
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (dt.Rows.Count == 0)
                {
                   dt= filterDataTable(BBLQL.Instance.sortByField(comboBox1.Text).ToList());
                }
                dt.DefaultView.RowFilter = String.Format("{0} = '{1}'", comboBox1.Text, textBox1.Text);
                setSTT();
                dataGridView1.DataSource = dt;
                
               
            }
            else
            {

                dataGridView1.DataSource = BBLQL.Instance.sortByField(comboBox1.Text).ToList();
            }
        }
        DataTable filterDataTable(List<inforuserView> list)
        {
            foreach(inforuserView i in list)
            {
                
                dt.Rows.Add(i.STT,i.firstName, i.lastName, i.email, i.gender, i.sinhnhat, i.roleUser);
                         
            }
           
            return dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PageInfo pif = new PageInfo("");
            this.Hide();
            pif.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=BBLQL.Instance.sortByField(comboBox1.Text).ToList();
        }

        

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            setSTT();
        }
    }
}
