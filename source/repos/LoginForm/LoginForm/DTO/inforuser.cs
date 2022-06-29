using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginForm.DTO
{
    public class inforuser
    {

        [Key]
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public DateTime sinhnhat { get; set; }
        public string roleUser { get; set; }
        public string idaccount { get; set; }
        public string urlAvatar { get; set; }

        [ForeignKey("idaccount")]
        public virtual accountUser user { get; set; }
    }
}
