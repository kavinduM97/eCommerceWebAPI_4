using eCommerceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Requests
{
    public class ProductRequest
    {

        
        [Required]
        public string name { get; set; }
        [MaxLength(50)]
        public string description { get; set; }
        [Required]
        public int stock { get; set; }
        public int categoryId { get; set; }
       
        
    }
}
