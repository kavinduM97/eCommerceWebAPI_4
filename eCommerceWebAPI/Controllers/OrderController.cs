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
        [Route("placeorderbyCart/{email}")]
        public IActionResult PlaceOrdersbyCart( List<int> cartIds, string email)
        {



            var Response = _orderService.PlaceOrdersbyCart(cartIds, email);



            return Response.State == false ? BadRequest(Response) : Ok(Response);



        }

        //add to cart



        //[HttpPost("PlaceOrder"), AllowAnonymous]
        [HttpPost("AddToCart/{productId}/{userEmail}/{quantity}")]
        public IActionResult AddtoCart(int productId, string userEmail, int quantity)
        {



            var Response = _orderService.AddToCart(productId, userEmail, quantity);



            return Response.State == false ? BadRequest(Response) : Ok(Response);



        }

        //get cart

        [HttpGet("GetAllProductsInCart/{userEmail}")]
        // [Authorize(Roles = "Admin || Customer")]
        public IActionResult GetAllProductsInCart(string userEmail)
        {

            var Response = _orderService.GetAllProductsInCart(userEmail);

            return Response.Count == 0 ? BadRequest("No any Product in cart") : Ok(Response);

        }



        //Delete cart item


        [HttpDelete("DeleteFromCart/{id}/{userEmail}")]
        public IActionResult DeleteCart(int id,string userEmail)
        {

            var response = _orderService.DeleteCartItem(id,userEmail);
            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

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
