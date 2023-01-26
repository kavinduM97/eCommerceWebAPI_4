
using Azure.Core;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.Order
{
    public interface IOrderRepository
    {
        //   public Task<List<Product>> PlaceOrder(List<int> productIds, string email);
        public OrderErrorHandler PlaceOrdersbyCart(List<int> cartIds, string email);
        public OrderErrorHandler AddToCart(int productId, string userEmail, int quantity);

        public OrderErrorHandler UpdateOrderState(int orderId, string orderState);
        public OrderErrorHandler PlaceOrder(int id, OrderRequest request); 
        public OrderErrorHandler DeleteCartItem(int id,string userEmail);
        public List<Cart> GetAllProductsInCart(string userEmail);
        //public Task<ActionResult<List<Product>>> UpdateTables(int id, UpdateOrderRequestscs responseP);
        // public  Task<bool> ProductOrderPlace(int id, OrderRequest request, int trId);

    }
}
