using LiveCharts;
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
using LiveCharts.Wpf;

namespace LoginForm
{
    public partial class thongkeviewvideo : Form
    {
        public thongkeviewvideo()
        {
            InitializeComponent();
        }

        private void thongkeviewvideo_Load(object sender, EventArgs e)
        {
            viewBindingSource.DataSource = new List<View>();
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title="Month",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul","Aug","Sep","Oct","Nov","Dec"}
            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis { 
            Title="View",
            LabelFormatter=value=>value.ToString()
            });
            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BBLQL.Instance.adddatetimeintovdtc("a",DateTime.Now);
            BBLQL.Instance.adddatetimeintovdtc("b", DateTime.Now);
            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            var years = BBLQL.Instance.getNams().Distinct();
            foreach(int year in years.ToList())
            {
                List<int> values = new List<int>();
                values = BBLQL.Instance.thongkeviewtheonam(year).ToList();
                series.Add(new LineSeries() { Title = year.ToString() ,Values=new ChartValues<int>(values)});
            }
            cartesianChart1.Series = series;
        }
    }
}
