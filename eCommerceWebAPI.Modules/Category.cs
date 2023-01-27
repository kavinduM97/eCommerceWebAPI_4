using eCommerceWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Models
{
    public class Category
    {
        [Key]
        public int catergoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }


    }
}
