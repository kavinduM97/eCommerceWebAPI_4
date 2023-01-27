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
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }



    }
}
