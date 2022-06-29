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
    public partial class mediaplayer : Form
    {
         private string resume;
        public string idvideotrochoi;
        trietree tree;
        public string paragraph;
        public mediaplayer(string id)
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.idvideotrochoi = id;
            this.paragraph = BBLQL.Instance.addParagraphGUI(idvideotrochoi);
            GUI();
        }

       
        

        private void button2_Click(object sender, EventArgs e)
        {

            if (resume == "Start")
            {
                axWindowsMediaPlayer1.URL = BBLQL.Instance.gettrochoivideobyid(this.idvideotrochoi).video.url;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                resume = "Resume";
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
           
        }

        
        private void GUI()
        {   
            string text= BBLQL.Instance.addParagraphGUI(idvideotrochoi);
            richTextBox1.Text = text;
            var rand=new Random();
            quizholeitem temp=BBLQL.Instance.quizhole(text, rand.Next(text.Length/20));
            Control[]list=new Control[temp.chuoi.Length];
            
            for (int i=0;i<temp.chuoi.Length;i++)
            {
                if (temp.indexhole.Contains(i) == false)
                {
                    Label  a = new Label();
                    a.Size = new Size(40, 20);
                    a.Text = temp.chuoi[i];
                    a.Location = new Point(groupBox1.Location.X+(i%30)*(a.Width+5),groupBox1.Location.Y+5+(i/30)*a.Height);
                    list[i]=a;                    
                }
                
                else
                {
                    TextBox b = new TextBox();
                    b.Size = new Size(40, 20);
                    b.Location = new Point(groupBox1.Location.X + (i % 30) * (b.Width + 5), groupBox1.Location.Y + 5 + (i / 30) * b.Height);
                    list[i] = b;

                }
            }
            groupBox1.Controls.AddRange(list);
            axWindowsMediaPlayer1.URL = BBLQL.Instance.gettrochoivideobyid(this.idvideotrochoi).video.url;
            resume = "Start";
            /* tabControl1.Controls.Add();*/
            comment1.ButtonClick += new EventHandler(this.customEvent_Handler);

        }
        private void customEvent_Handler(object sender,EventArgs e )
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.tree = new trietree(this.paragraph);
            reloadParagraph();            
            string paragraph = richTextBox1.Text;
            string[] pars = textBox2.Text.Split(',');
            foreach (string par in pars)
            {             
                
                List<int> indexes = BBLQL.Instance.searchparagraph(paragraph, par, tree);
                if (indexes != null)
                {
                    foreach (int index in indexes)
                    {

                        richTextBox1.SelectionStart = index;
                        richTextBox1.SelectionLength = par.Length;
                        richTextBox1.SelectionBackColor = Color.Yellow;

                    }
                }
            }
              
        }
        private void reloadParagraph()
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor= Color.White;
        }

     
    }
}
