using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Requests
{
    public class SearchProductRequest
    {
        [Required]
        public string name { get; set; }
    }
}
