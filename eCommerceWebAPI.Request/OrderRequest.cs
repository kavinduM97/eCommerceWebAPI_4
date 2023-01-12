using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Requests
{
    public class OrderRequest
    {
        [Required, EmailAddress]
        public string email { get; set; }

        [Required]
        public int quantity { get; set; }
    }
}
