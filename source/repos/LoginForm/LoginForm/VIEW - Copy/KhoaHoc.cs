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
namespace LoginForm
{
    public partial class KhoaHoc : Form
    {
        private string urlavatar;
        private string urlvideo;
        public KhoaHoc()
        {
            InitializeComponent();
         
        }

        private void KhoaHoc_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using(OpenFileDialog ofd =new OpenFileDialog() { Filter= "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV" })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                   
                    urlvideo=ofd.FileName;
                    axWindowsMediaPlayer1.URL = urlvideo;
                                      
                }
            }
        }
        public void LoadData(string linkvideo = "",string linkimage="", string tittle="",string author="",string transcript="",string titlevideo="")
        {
            QLENG db = new QLENG();
            textBox1.Text = tittle;
            textBox2.Text = author;
            textBox3.Text=linkvideo;
            textBox4.Text = titlevideo;
            richTextBox1.Text = transcript;
            axWindowsMediaPlayer1.URL = linkvideo;
            pictureBox1.Image=Image.FromFile(linkimage);
            dataGridView1.DataSource = db.Videotrochois.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    BBLQL.Instance.deletekhoahoctrochoi(row.Cells["idkhoahoc"].Value.ToString());
                }
            }
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string i = DateTime.Now.ToString();
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    urlavatar = ofd.FileName;                   
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string idkhoahocvideo = DateTime.Now.ToString();
            BBLQL.Instance.addKhoaHocVideo(idkhoahocvideo, textBox1.Text, urlvideo, textBox2.Text, textBox4.Text, richTextBox1.Text,urlavatar);
            LoadData(urlvideo, urlavatar);
        }
    }
}
