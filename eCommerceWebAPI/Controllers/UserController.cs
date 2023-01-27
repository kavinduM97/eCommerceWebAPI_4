using eCommerceWebAPI.Models;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerceWebAPI.Services.Users;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace eCommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly IUserRepository _userServices;
      
        public UserController(IUserRepository repository)
        {
            _userServices = repository;
        }

 

        //User Registration

        [HttpPost("register")]

        public IActionResult Register(UserRequest request)
        {

            var response = _userServices.UserRegistration(request);

            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        //User Login

        [HttpPost("login")]

        public IActionResult Login(UserRequest request)
        {

            var response = _userServices.UserLoging(request);

            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }


        [HttpGet,Authorize]
        public ActionResult <string>GetMe() 
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(UserEmail);
                
        }

    }
}