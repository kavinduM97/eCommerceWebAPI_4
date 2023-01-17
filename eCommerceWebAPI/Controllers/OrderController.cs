using Azure;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Order;
using eCommerceWebAPI.Services.Orders;
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
        private readonly IOrderRepository _orderService;
       

        public OrderController(IOrderRepository context )
        {
            _orderService = context;
          
        }

        [HttpPost]
        [Route("placeorderbyCart")]
        public IActionResult PlaceOrdersbyCart([FromBody] List<int> cartIds, string email)
        {



            var Response = _orderService.PlaceOrdersbyCart(cartIds, email);



            return Response.State == false ? BadRequest(Response) : Ok(Response);



        }

        //add to cart



        //[HttpPost("PlaceOrder"), AllowAnonymous]
        [HttpPost("AddToCart/{productId}/{userEmail}")]
        public IActionResult AddtoCart(int productId, string userEmail, int quantity)
        {



            var Response = _orderService.AddToCart(productId, userEmail, quantity);



            return Response.State == false ? BadRequest(Response) : Ok(Response);



        }


        [HttpPut("UpdateOrderState/{orderId}/{orderState}")]
        public IActionResult UpdateOrdersState(int orderId, string orderState)
        {



            var Response = _orderService.UpdateOrderState(orderId, orderState);



            return Response.State == false ? BadRequest(Response) : Ok(Response);



        }

        [HttpPost("PlaceOrder/{id}")]
        public IActionResult PlaceOrders(int id, OrderRequest request)
        {
            var response = _orderService.PlaceOrder(id, request);


            if (response.State == true)
            {

                return Ok(response);
            }

            return BadRequest(response);

        }


    }
}
