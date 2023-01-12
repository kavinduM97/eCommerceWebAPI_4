using Azure.Core;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Productcategories;
using eCommerceWebAPI.Services.ProductServices;
using eCommerceWebAPI.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace eCommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductCategoriesController : ControllerBase
    {

        private readonly IProductcategoryRepository _productcategoriesServices;
            public ProductCategoriesController ( IProductcategoryRepository repository)
        {
            _productcategoriesServices = repository;
        }
        [HttpGet]
        public IActionResult Getproductcategories()
        {


            var myProductcategories = _productcategoriesServices.AllProductcategories();
            if (myProductcategories is null)
            {
                return NotFound();
            }
            return Ok(myProductcategories);

              

            

        }
        [HttpGet("{id}")]

        public IActionResult Getproductcategory(int id)
        {
             
            var myProductcategories = _productcategoriesServices.OneProductcategory(id);
            if (myProductcategories is null)
            {
                return NotFound();
            }

            return Ok(myProductcategories);

        }

        [HttpPost("addproductCaterory")]
        public IActionResult AddProductCaterory(ProductCatergoryRequest request)
        {
            
                var response = _productcategoriesServices.AddaProductCaterory(request);


                if (response.State == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);

            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductCatergory(int id, UpdateProductCategoryRequest request)
        {
            var response = _productcategoriesServices.UpdateProductCatergory(id,request);


            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductCatergory(int id)
        {
            var response = _productcategoriesServices.DeleteProductCatergory(id);


            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }





    }
}
