using Azure;
using eCommerceWebAPI.DataAccess;
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
    public class Orderontroller : ControllerBase
    {
        private readonly IOrderRepository _context;
        private readonly DbbContext _dbcontext = new DbbContext();
        public Orderontroller(IOrderRepository context)
        {
            _context = context; 
        }

        [HttpPost("PlaceOrder/{id}")]
        public async Task<ActionResult<Product>> PlaceOrder(int id, OrderRequest request)
        {



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
            //

            


        }


    }
}
