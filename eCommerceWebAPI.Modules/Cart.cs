using eCommerceWebAPI.Models;
using System.ComponentModel.DataAnnotations;


namespace eCommerceWebAPI.Models
{
    public class Cart
    {
        [Key]
        public int cartId { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public int quantity { get; set; }
        public string userEmail { get; set; }
        public User User { get; set; }
    }
}
