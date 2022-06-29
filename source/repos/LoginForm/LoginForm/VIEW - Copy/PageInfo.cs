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
    public partial class PageInfo : Form
    {
        private string url;
        private string mstk;
        public PageInfo(string ms)
        {
            mstk = ms;
            url = "C:/Users/LENOVO/Pictures/download.png";
            InitializeComponent();
            GUI();
            setCBB();
        }
        private void GUI()
        {
            inforuser temp = BBLQL.Instance.getInfoById(mstk);
            if (temp==null)
            {               
                pictureBox1.Image = Image.FromFile(url);
            }
            else
            {
                
                    textBox1.Text = temp.firstName;
                    textBox2.Text = temp.lastName;
                    radioButton1.Checked = temp.gender == "Male";
                    radioButton2.Checked = temp.gender == "FeMale";
                    textBox3.Text = temp.email;
                    dateTimePicker1.Value = temp.sinhnhat;
                    comboBox1.SelectedItem = new CBBuser { roleUser = temp.roleUser };
                    pictureBox1.Image = Image.FromFile(temp.urlAvatar);
                
               
             }
         
        }
        private void setCBB()
        {
            comboBox1.Items.Add(new CBBuser
            {
                roleUser = "Admin",
            });
            comboBox1.Items.Add(new CBBuser
            {
                roleUser = "User",
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(BBLQL.Instance.checkInfo(mstk))
            {
                BBLQL.Instance.updateinfor(mstk,textBox1.Text, textBox2.Text, radioButton1.Checked ? "Male" : "Female", textBox3.Text, dateTimePicker1.Value, ((CBBuser)comboBox1.SelectedItem).roleUser, url);
                MessageBox.Show("da update infor thanh cong");
            }
            else if (mstk == "")
            {
                string mtk = DateTime.Now.ToString();
                BBLQL.Instance.adduser(mtk, "macdinh", "123456");
                BBLQL.Instance.addinfor(mtk,mtk+mtk, textBox1.Text, textBox2.Text, radioButton1.Checked ? "Male" : "Female", textBox3.Text, dateTimePicker1.Value, ((CBBuser)comboBox1.SelectedItem).roleUser, url);
              
                MessageBox.Show("da tao infor thanh cong");
            }
            else
            {
                BBLQL.Instance.addinfor(GlobalAccount.id,GlobalAccount.id+GlobalAccount.id, textBox1.Text, textBox2.Text, radioButton1.Checked ? "Male" : "Female", textBox3.Text, dateTimePicker1.Value, ((CBBuser)comboBox1.SelectedItem).roleUser, url);
                MessageBox.Show("da tao infor thanh cong");
            }
           
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string i = DateTime.Now.ToString();
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    url = ofd.FileName;
                    textBox1.Text = ofd.FileName;
                }

            }
        }
        //cancel
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
