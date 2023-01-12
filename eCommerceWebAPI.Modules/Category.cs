using eCommerceWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Models
{
    public class Category
    {
        public int categoryId { get; set; }
       
        public string name { get; set; }
       
        public string description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();


    }
}
