
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Models
{
    public class Product
    {
        public int productId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int stock { get; set; }
        public int categoryId { get; set; }
   
        public Category category { get; set; }

        public List<Order> Orders { get; set; }


    }
}
