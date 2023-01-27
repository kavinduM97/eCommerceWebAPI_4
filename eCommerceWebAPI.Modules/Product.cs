using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public string categoryName { get; set; }
        public decimal price { get; set; }
        public int categoryId { get; set; }
        public Category category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
