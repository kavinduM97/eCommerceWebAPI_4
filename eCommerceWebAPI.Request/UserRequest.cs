using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Requests 
{
    public class UserRequest
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,MinLength(6)]
        public string Password { get; set; }
    }
}
