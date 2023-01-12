using eCommerceWebAPI.Models;

namespace eCommerceWebAPI.ErrorHandler
{
    public class UserErrorHandler
    {
        public bool State { get; set; }
        public string User { get; set; }
        public User Detail { get; set; }
        public string Message { get; set; }
    }
}
