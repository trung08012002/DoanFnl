using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            /*Application.Run(new KhoaHoc());*/

            /*Application.Run(new Form2());*/
            Application.Run(new Form1());
            /* Application.Run(new mediaplayer("6/20/2022 9:34:43 AM"));*/

            /*Application.Run(new PageInfo("6/22/2022 5:46:46 PM"));*/
        }
    }
}
