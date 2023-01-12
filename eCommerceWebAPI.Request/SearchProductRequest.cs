using System.ComponentModel.DataAnnotations;

namespace Assignment.Request
{
    public class SearchProductRequest
    {
        [Required]
        public string name { get; set; }
    }
}
