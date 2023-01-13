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
        public async Task<bool> PlaceOrder(int id, OrderRequest request)
        {

            Random rnd = new Random();
            int TrId = rnd.Next();

            if (!_context.Users.Any(u => u.email == request.email))
            {
                response = SetResponse(false, "User is not  exists", 404);
                return false;
            }
            var myProduct = _productServices.AllProduct();
            if (!myProduct.Any(u => u.productId == id))
            {
                response = SetResponse(false, "Product is not exists", 404);
                return false;
            }

            var stock = _context.Products.Where(p => p.productId == id).Select(p => p.stock).FirstOrDefault();

            stock = stock - request.quantity;

            if (stock < 0)
            {
                response = SetResponse(false, "Quantity exceed stock", 400);

                return false;
            }

           
            var date = DateTime.Now;

             //var tranId = SetTransactionId();
         
            //int trid = Int32.Parse(tranId);
            var order = new Models.Order
            {

                date = date,

                state = 0,
                trnsid = TrId,

                email = request.email,


            };
             _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            Thread.Sleep(5000);

            var Rstock = await _context.Products.Where(p => p.productId == id).Select(p => p.stock).FirstOrDefaultAsync();

            Rstock = stock - request.quantity;

            var product = _context.Products.Find(id);

            product.stock = stock;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();


           // response = SetResponse(true, "Order added", TrId);
            //  return true;


            var response1 = ProductOrderPlace(id, request,TrId); 
            if (response1.Result == false)
            {
                return false;

            }

            return true;


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



        public async Task<bool> ProductOrderPlace(int id, OrderRequest request, int trId)
        {



            var tranId = trId;
          //  tranId = SetTransactionId();


            var product = await _context.Products.Where(c => c.productId == id).Include(c => c.Orders).FirstOrDefaultAsync();
            if (product == null)
            {
                return false;

            }

            //  var oid= _dbcontext.Orders.Where(u => u.trnsid == responseP.transid).FirstOrDefault();
            // var ooId = oid.orderId;
            var orderr = await _context.Orders.Where(c => c.trnsid == tranId).FirstOrDefaultAsync();
            orderr.state = (OrderState)2;
            if (orderr == null)
            {
                return false;
            }

            product.Orders.Add(orderr);
            await _context.SaveChangesAsync();
            return true;
            //
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