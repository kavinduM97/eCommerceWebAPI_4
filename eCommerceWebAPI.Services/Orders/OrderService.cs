using Azure;
using eCommerceWebAPI.DataAccess;
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
        private readonly DbbContext _dbcontext = new DbbContext();
        private readonly IProductRepository _productServices;
       
        public OrderService(IProductRepository productServices)
        {
            _productServices = productServices;
         
        }
        //place order
        public OrderErrorHandler PlaceOrdersbyCart(List<int> cartIds, string email)
        {

            Random rnd = new Random();
            int TrId = rnd.Next();

            var date = DateTime.Now;
            var order = new Models.Order
            {
                Date = date,
                userEmail = email,
                Status = "Pending"
            };



            _dbcontext.Orders.Add(order);
            _dbcontext.SaveChangesAsync();



            Thread.Sleep(1000);



            var oderId = _dbcontext.Orders.Where(o => o.Date == date && o.userEmail == email && o.Status == "Pending").Select(o => o.Id).FirstOrDefault();




            List<Cart> productsList = _dbcontext.Carts.Where(c => cartIds.Contains(c.cartId)).ToList();




            if (productsList.Count > 0)
            {
                foreach (var product in productsList)
                {



                    var stock = _dbcontext.Products.Where(p => p.ProductId == product.ProductId).Select(p => p.stock).FirstOrDefault();



                    if (product.quantity > stock)
                    {
                        response = SetResponse(true, "Quantity exeed stock", 0);
                        return response;
                    }



                    Thread.Sleep(500);



                }



            }




            if (productsList.Count > 0)
            {
                foreach (var productItem in productsList)
                {



                    var orderproduct = new OrderProduct
                    {
                        ProductId = productItem.ProductId,
                        OrderId = oderId,
                    };



                    _dbcontext.OrderProducts.Add(orderproduct);
                    _dbcontext.SaveChangesAsync();
                    Thread.Sleep(3000);



                    var stock = _dbcontext.Products.Where(p => p.ProductId == productItem.ProductId).Select(p => p.stock).FirstOrDefault();



                    stock = stock - productItem.quantity;



                    //update stock



                    var product = _dbcontext.Products.Find(productItem.ProductId);



                    product.stock = stock;



                    _dbcontext.Products.Update(product);
                    _dbcontext.SaveChangesAsync();



                    Thread.Sleep(3000);



                    var cart = _dbcontext.Carts.Find(productItem.cartId); ;
                    _dbcontext.Carts.Remove(cart);
                    _dbcontext.SaveChangesAsync();
                    Thread.Sleep(2000);
                }



            }




            response = SetResponse(true, "Order placed successful", TrId);
            return response;
           



        }



        //Update order state



        public OrderErrorHandler UpdateOrderState(int orderId, string orderState)
        {
            var order = _dbcontext.Orders.Find(orderId);



            if (orderState == "accepted")
            {
                order.Status = "Accepted";
            }
            else if (orderState == "decline")
            {
                order.Status = "Decline";
            }
            else
            {
                response = SetResponse(false, "Order updated faild", 0);
            }



            _dbcontext.Orders.Update(order);
            _dbcontext.SaveChangesAsync();



            response = SetResponse(true, "Order updated successful", 0);
            return response;
        }



        //Add to cart



        public OrderErrorHandler AddToCart(int productId, string userEmail, int quantity)
        {
            //var stock = _dbcontext.Products.Where(p => p.ProductId == productId).Select(p => p.stock).FirstOrDefault();
            var product = _dbcontext.Products.Where(p => p.ProductId == productId).Select(p => new { p.name, p.price, p.stock }).FirstOrDefault();
            var totalPrice = quantity * product.price;

            if (product.stock < quantity)
            {
                response = SetResponse(false, "Quantity exeed stock", 0);
                return response;
            }



            var item = new Cart
            {
                ProductId = productId,
                userEmail = userEmail,
                quantity = quantity,
                productName= product.name,
                totalPrice = totalPrice
            };



            _dbcontext.Carts.Add(item);
            _dbcontext.SaveChangesAsync();
            Thread.Sleep(1000);
            response = SetResponse(true, "Item added to cart successfully", 0);
            return response;



        }




        // Single order

        public OrderErrorHandler PlaceOrder(int id, OrderRequest request)
        {

           

            var myProduct = _productServices.AllProduct();
            if (!myProduct.Any(u => u.ProductId == id))
            {
                response = SetResponse(false, "Product is not exists", 404);
                return response;
            }



            if (!_dbcontext.Users.Any(u => u.Email == request.email))
            {
                response = SetResponse(false, "User is not  exists", 404);
                return response;
            }

            if (request.quantity == 0)
            {
                response = SetResponse(false, "Please add a quantity to make a order", 400);
                return response;
            }


            var stock = _dbcontext.Products.Where(p => p.ProductId == id).Select(p => p.stock).FirstOrDefault();

            stock = stock - request.quantity;

            if (stock < 0)
            {
                response = SetResponse(false, "Quantity exceed stock", 400);

                return response;
            }

            //.......................................................check cart            
            var cartStock = _dbcontext.Carts.Where(c => c.ProductId == id).Select(c => new { c.productName, c.quantity, c.cartId }).FirstOrDefault();           
            if (cartStock.productName != null)           
            {                 
                if(stock == 0)                
                {   
                    var cartProduct = _dbcontext.Carts.Find(cartStock.cartId);
                    _dbcontext.Carts.Remove(cartProduct);
                    _dbcontext.SaveChangesAsync();                 
                    Thread.Sleep(2000);                
                }else 
                if(stock < cartStock.quantity)    
                {   
                    var cartProduct = _dbcontext.Carts.Find(cartStock.cartId);         
                    cartProduct.quantity = stock;
                    _dbcontext.Carts.Update(cartProduct);
                    _dbcontext.SaveChangesAsync();               
                    Thread.Sleep(2000);                }                         
            }                          
            
            //...........................................................
            var date = DateTime.Now;


            var order = new Models.Order
            {

                Date = date,

                Status = "Pending",
               

                userEmail = request.email,


            };
            _dbcontext.Orders.Add(order);
            _dbcontext.SaveChangesAsync();
            Thread.Sleep(2000);
            var oderId = _dbcontext.Orders.Where(o => o.Date == date && o.userEmail == request.email && o.Status == "Pending").Select(o => o.Id).FirstOrDefault();





            var product = _dbcontext.Products.Find(id);

            product.stock = stock;

            _dbcontext.Products.Update(product);
            _dbcontext.SaveChangesAsync();
            Thread.Sleep(2000);


            var orderproduct = new OrderProduct
            {
                ProductId = id,
                OrderId = oderId,
            };



            _dbcontext.OrderProducts.Add(orderproduct);
            _dbcontext.SaveChangesAsync();
            Thread.Sleep(3000);

            response = SetResponse(true, "Order placed succesfully", 200);

            return response;


        }


        //Get all products in cart

        public List<Cart> GetAllProductsInCart(string userEmail)
        {
            //return _context.Cart.ToList();
            return _dbcontext.Carts.Where(c => c.userEmail == userEmail).ToList();
        }


        public Cart OneCartItem(int id)
        {
            return _dbcontext.Carts.Find(id);
        }

        public OrderErrorHandler DeleteCartItem(int id, string userEmail)
        {
            var item= _dbcontext.Carts.Where(c => c.userEmail == userEmail &&c.cartId==id ).FirstOrDefault(); 

            if (item==null )
            {
                response = SetResponse(false, "No cart items belong to that user", 400);

                return response;
            }

            var myitem = OneCartItem(id);
            _dbcontext.Carts.Remove(myitem);
            _dbcontext.SaveChangesAsync();
            response = SetResponse(true, "cart item is Deleted", 200);
            return response;

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