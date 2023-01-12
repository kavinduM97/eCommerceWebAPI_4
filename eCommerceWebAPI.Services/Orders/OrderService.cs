using Azure;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.DataAccess.Migrations;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Order;
using eCommerceWebAPI.Services.Productcategories;
using eCommerceWebAPI.Services.ProductServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.Orders
{
    public class OrderService : IOrderRepository
    {
        private OrderErrorHandler response;
        private readonly DbbContext _context = new DbbContext();
        private readonly IProductRepository _productServices;

        public OrderService(IProductRepository productServices)
        {
            _productServices = productServices;
        }
        public OrderErrorHandler PlaceOrder(int id, OrderRequest request)
        {

            if (!_context.Users.Any(u => u.email == request.email))
            {
                response = SetResponse(false, "User is not  exists", 09);
                return response;
            }
            var myProduct = _productServices.AllProduct();
            if (!myProduct.Any(u => u.productId == id))
            {
                response = SetResponse(false, "Product is not exists", 099);
                return response;
            }

            var date = DateTime.Now;

            //var tranId = SetTransactionId(request.email, id);
            //int trid = Int32.Parse(tranId);
            var order = new Models.Order
            {

                date = date,

                state = 0,
                trnsid = 102,

                email = request.email,


            };
            _context.Orders.Add(order);
            _context.SaveChangesAsync();
            Thread.Sleep(5000);



            response = SetResponse(true, "Order added", 102);
            return response;


        }


        /*public OrderErrorHandler UpdateTables(int id, UpdateOrderRequestscs request) {

            var product =  _context.Products .Where(c => c.productId == request.productid).Include(c => c.Orders).FirstOrDefaultAsync();
            if (product == null)
            {
                response = SetResponse(false, "", null);
                return response;
            }

            var orderr= _context.Orders.FindAsync(request.trnsid);
            if (orderr == null)
            {
                response = SetResponse(false, "", null);
                return response;
            }

            product.Orders.Add(orderr);
            _context.SaveChangesAsync();

            response = SetResponse(true, "", null);
            return response;

        }*/


      /* private static string SetTransactionId(string email, int id)
        {
            var mySecretString = email + id;

            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(mySecretString)));
        }*/


        private OrderErrorHandler SetResponse(bool state, string message, int ttransid)
        {
            OrderErrorHandler response = new OrderErrorHandler
            {
                State = state,
                Message = message,

                transid = ttransid,
            };

            return response;
        }
    }
}








