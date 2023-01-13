using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Requests
{
    public class UpdateProductRequest
    {
        [ValidateNever]
        public string updateName { get; set; }

        [ValidateNever]
        public string updateDescription { get; set; }

        [Required]
        public int updateStock { get; set; }

        [Required]
        public int updateCategoryId { get; set; }
    }
}
