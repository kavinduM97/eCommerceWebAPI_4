using eCommerceWebAPI.Models;


namespace eCommerceWebAPI.ErrorHandler
{
    public class OrderErrorHandler
    {
        public bool State { get; set; }
      
        public int transid { get; set; }
        public string Message { get; set; }
    }
}
