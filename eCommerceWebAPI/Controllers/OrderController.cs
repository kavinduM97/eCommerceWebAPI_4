using Azure;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Order;
using eCommerceWebAPI.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _context;
       

        public OrderController(IOrderRepository context )
        {
            _context = context;
          
        }

        [HttpPost("PlaceOrder/{id}")]
        public string PlaceOrders(int id, OrderRequest request)
        {
            // Random rnd = new Random();
            // int TrId = rnd.Next();
            var response1 = _context.PlaceOrder(id, request);
            if (response1.Result == false)
            {
                return "Please Check Product and Available quantity again";

            }

            return "OrderProduct table updated succesfully";

            /*
            var response1 = _context.PlaceOrder(id,  request);
         
            if (response1.Result == false)
            {
                return "Please Check Product and Available quantity again";
               
            }

            var response2 = _context.ProductOrderPlace(id,  request);
            if (response2.Result == false)
            {
                return "Order details are not matching, OrderProduct table was not updated";
            }

            return "OrderProduct table updated succesfully";


            */













            /*

            //var responseP = _context.PlaceOrder(id, request);

            var responseP = _context.PlaceOrder(id, request);
            if (responseP.State == false)
            {
                return BadRequest(responseP);
            }


            // var tranId = responseP.transid;
            var tranId =responseP.transid;


            var product = await _dbcontext.Products.Where(c => c.productId == id).Include(c => c.Orders).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

          //  var oid= _dbcontext.Orders.Where(u => u.trnsid == responseP.transid).FirstOrDefault();
           // var ooId = oid.orderId;
            var orderr = await _dbcontext.Orders.Where(c => c.trnsid == tranId).FirstOrDefaultAsync(); ;
            if (orderr == null)
            {
                return NotFound();
            }

            product.Orders.Add(orderr);
            await _dbcontext.SaveChangesAsync();
            return Ok("done");
            */




        }


    }
}
