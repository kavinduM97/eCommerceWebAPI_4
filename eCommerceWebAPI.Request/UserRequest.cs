using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Requests 
{
    public class UserRequest
    {
        [Required,EmailAddress]
        public string email { get; set; }
        [Required,MinLength(6)]
        public string password { get; set; }
    }
}
