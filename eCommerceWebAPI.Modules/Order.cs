using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string userEmail { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }


    }

    

}
