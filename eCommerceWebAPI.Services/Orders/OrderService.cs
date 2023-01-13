using Azure;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.DataAccess.Migrations;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Order;
using eCommerceWebAPI.Services.Productcategories;
using eCommerceWebAPI.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
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
        public  OrderErrorHandler PlaceOrder(int id, OrderRequest request)
        {

            Random rnd = new Random();
            int TrId = rnd.Next();

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

      
            var order = new Models.Order
            {

                date = date,

                state = 0,
                trnsid = TrId,

                email = request.email,


            };
             _context.Orders.Add(order);
             _context.SaveChangesAsync();
            Thread.Sleep(5000);



            var sstock = _context.Products.Where(p => p.productId == id).Select(p => p.stock).FirstOrDefault();

            sstock = sstock - request.quantity;

            var product = _context.Products.Find(id);

            product.stock = sstock;

            _context.Products.Update(product);
             _context.SaveChangesAsync();
           Thread.Sleep(5000);


            var response1 = ProductOrderPlace(id, request,TrId); 
            if (response1.Result == false)
            {
                response = SetResponse(false, "ProductOrder table is not updated", 400);

                return response;

            }

            response = SetResponse(true, "ProductOrder is updated", 200);

            return response;


        }

        public async Task<bool> ProductOrderPlace(int id, OrderRequest request, int trId)
        {



            var tranId = trId;
        


            var product = await _context.Products.Where(c => c.productId == id).Include(c => c.Orders).FirstOrDefaultAsync();
            if (product == null)
            {
                return false;

            }

      
            var myorder = await _context.Orders.Where(c => c.trnsid == tranId).FirstOrDefaultAsync();
            myorder.state = (OrderState)2;
            if (myorder == null)
            {
                return false;
            }

            product.Orders.Add(myorder);
            await _context.SaveChangesAsync();
            return true;
            
        }





        private static int SetTransactionId()
        {
            Random rnd = new Random();
            int num = rnd.Next();


            return num;
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