using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }
        int Count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Count++;
            Tmglbl.Text = ""+Count+"%";
            panel3.Width += 7;
            if(panel3.Width >= 830)
            {
                timer1.Stop();
                /*Form3 form = new Form3();
                form.Show();
                this.Hide();*/
                Exam obj = new Exam();
                obj.Show();
                this.Hide();
            }
        }
    }
}
