using eCommerceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.ErrorHandler
{
    public class ProductErrorHandler
    {
        public bool State { get; set; }
       
        public Product obj { get; set; }
        public string Message { get; set; }
    }
}
