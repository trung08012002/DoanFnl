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
    public partial class Comment : UserControl
    {
        public Comment()
        {
            InitializeComponent();
        }
        public event EventHandler ButtonClick;
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }
    }
}
