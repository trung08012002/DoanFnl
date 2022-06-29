using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.DTO
{
    public class inforuserView
    {
        public inforuserView()
        {
            STT = "0";
        }
        public string STT { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public DateTime sinhnhat { get; set; }
    
        public string roleUser { get; set; }
    }
}
