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
                response = SetResponse(false, "User is not  exists", 404);
                return response;
            }
            var myProduct = _productServices.AllProduct();
            if (!myProduct.Any(u => u.productId == id))
            {
                response = SetResponse(false, "Product is not exists", 404);
                return response;
            }

            var stock = _context.Products.Where(p => p.productId == id).Select(p => p.stock).FirstOrDefault();

            stock = stock - request.quantity;

            if (stock < 0)
            {
                response = SetResponse(false, "Quantity exceed stock", 400);

                return response;
            }


            var date = DateTime.Now;

            var tranId = SetTransactionId();
            //int trid = Int32.Parse(tranId);
            var order = new Models.Order
            {

                date = date,

                state = 0,
                trnsid = tranId,

                email = request.email,


            };
            _context.Orders.Add(order);
            _context.SaveChangesAsync();
            Thread.Sleep(5000);

            var Rstock = _context.Products.Where(p => p.productId == id).Select(p => p.stock).FirstOrDefault();

            Rstock = stock - request.quantity;

            var product = _context.Products.Find(id);

            product.stock = stock;

            _context.Products.Update(product);
            _context.SaveChangesAsync();


            response = SetResponse(true, "Order added", tranId);
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



        private static int SetTransactionId()
        {
            DateTime d = DateTime.Now;
            int res = d.GetHashCode();


            return res;
        }


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