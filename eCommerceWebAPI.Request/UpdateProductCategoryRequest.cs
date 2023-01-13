using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Requests
{
    public class UpdateProductCategoryRequest
    {

      
        [ValidateNever]
        public string updateName { get; set; }

        [ValidateNever]
        public string updateDescription { get; set; }
    }
}
