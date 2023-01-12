using Assignment.Request;
using Azure.Core;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Productcategories;
using eCommerceWebAPI.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eCommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productServices;
        private readonly IProductcategoryRepository _productcategoriesServices;
        public ProductController(IProductRepository repository, IProductcategoryRepository productcategoriesServices)
        {
            _productServices = repository;
            _productcategoriesServices = productcategoriesServices;
        }

        [HttpGet]
        public IActionResult Getproducts()
        {


            var myProduct = _productServices.AllProduct();
            if (myProduct is null)
            {
                return NotFound();
            }
            return Ok(myProduct);

        }

        [HttpGet("{id}")]

        public IActionResult Getproduct(int id)
        {

            var myProductcat = _productServices.OneProduct(id);
            if (myProductcat is null)
            {
                return NotFound();
            }

            return Ok(myProductcat);

        }


        [HttpPost("addproduct")]
        public IActionResult AddProduct(ProductRequest request)
        {
            var response = _productServices.AddaProduct(request);


            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, UpdateProductRequest request)
        {

            var response = _productServices.UpdateProduct( id,request);
            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }


    
    [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            var response = _productServices.DeleteProduct(id);
            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        [HttpPost("SearchProduct"),AllowAnonymous]
        public IActionResult SearchProduct(SearchProductRequest request)
        {
            var response = _productServices.SearchProduct(request);

            return response.Count == 0 ? BadRequest("Product is not existed") : Ok(response);
        }
    }
}
