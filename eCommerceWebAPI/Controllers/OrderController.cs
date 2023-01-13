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
        public IActionResult PlaceOrders(int id, OrderRequest request)
        {
            var response = _context.PlaceOrder(id, request);
         

            if (response.State == true)
            {

                return Ok(response);
            }

            return BadRequest(response);

        }


    }
}
