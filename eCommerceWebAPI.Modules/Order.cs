using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public DateTime date { get; set; }
        public OrderState state { get; set; }
        public int trnsid { get; set; }
        public string email { get; set; }
        public List<Product> Products { get; set; }


    }

    public enum OrderState
    {
        Pending,
        Purchase,
        Completed
    }

}
