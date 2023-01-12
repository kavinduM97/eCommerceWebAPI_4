using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Models
{
    public class User
    {
        [Key]
        public string email { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public string token { get; set; }
        
       
    }
}
