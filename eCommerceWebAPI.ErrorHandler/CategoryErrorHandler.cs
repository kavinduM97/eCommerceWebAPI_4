using eCommerceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.ErrorHandler
{
    public class CategoryErrorHandler
    {
        public bool State { get; set; }

        public Category obj { get; set; }
        public string Message { get; set; }
    }
}
