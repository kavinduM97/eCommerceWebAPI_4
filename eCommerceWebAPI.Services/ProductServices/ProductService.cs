using Assignment.Request;
using Azure;
using Azure.Core;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.Services.Productcategories;
using eCommerceWebAPI.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCommerceWebAPI.Services.ProductServices
{
    public class ProductService : IProductRepository
    {
       
        private readonly IProductcategoryRepository _productcategoriesServices;
        private ProductErrorHandler response;
        private readonly DbbContext _context= new DbbContext();

        public ProductService(IProductcategoryRepository repository)
        {
            _productcategoriesServices = repository;
           


        }
        public ProductErrorHandler AddaProduct(ProductRequest request)
        {

            if (_context.Products.Any(p => p.name == request.name && p.categoryId == request.categoryId))
            {
                response = SetResponse(false, "Product already exists", null);
                return response;
            }
            var myProductcategories = _productcategoriesServices.AllProductcategories();
            if (!myProductcategories.Any(u => u.categoryId == request.categoryId))
            {
                response = SetResponse(false, "Product category is not matching", null);
                return response;
            }

            try
            {
                var Product = new Product
                {

                    name = request.name,
                    description = request.description,
                    stock = request.stock,
                    categoryId = request.categoryId

                };
                _context.Products.Add(Product);
                _context.SaveChangesAsync();
                response = SetResponse(true, "Product added", Product);
            }
            catch (Exception ex)
            {
                response = SetResponse(false, "Something went wrong", null);

            }

            return response;
        }























        public List<Product> AllProduct()
        {
            return _context.Products.ToList();
        }

      

        public Product OneProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public  ProductErrorHandler UpdateProduct(int id,UpdateProductRequest request)


        {
            var product =  OneProduct(id);





            if (request.updateName == null && request.updateDescription == null && request.updateStock == null && request.updateCategoryId == null)
            {
                response = SetResponse(false, "Nothing to Update", null);
                return response;



            }



            if (request.updateName != null)
            {
                if (request.updateCategoryId > 0)
                {
                    if (_context.Products.Any(p => p.name == request.updateName && p.categoryId == request.updateCategoryId))
                    {
                        response = SetResponse(false, "Product already exists in the category",  null);
                        return response;
                    }
                }
                else
                {
                    if (_context.Products.Any(p => p.name == request.updateName && p.categoryId == product.categoryId))
                    {
                        response = SetResponse(false, "Product already exists in the category", null);
                        return response;
                    }
                }
                product.name = request.updateName;
            }



            if (request.updateDescription != null)
            {
                product.description = request.updateDescription;
            }



            if (request.updateStock > -1)
            {
                product.stock = request.updateStock;
            }



            if (request.updateCategoryId > 0)
            {
                if (_context.Products.Any(p => p.name == product.name && p.categoryId == request.updateCategoryId))
                {
                    response = SetResponse(false, "Product already exists in the category", null);
                    return response;
                }
                product.categoryId = request.updateCategoryId;
            }



            _context.Products.Update(product);
            _context.SaveChangesAsync();



            product = OneProduct(id);



            response = SetResponse(true, "Category updated successfully", product);
            return response;
        }
        public  ProductErrorHandler DeleteProduct(int id)
        {

            var myproductServices = AllProduct();
            if (!myproductServices.Any(u => u.productId == id))
            {
                response = SetResponse(false, "Product is already not in the system", null);
                return response;
            }

               var product = OneProduct(id);

            _context.Products.Remove(product);
           _context.SaveChangesAsync();
            response = SetResponse(true, "Product is Deleted", product);
            return response;
        }

        public List<Product> SearchProduct(SearchProductRequest request)
        {
            var categoryId = _context.Categories.Where(c => c.name == request.name).Select(c => c.categoryId).FirstOrDefault();
            var productName = _context.Products.Where(p => p.name == request.name).Select(p => p.name).FirstOrDefault();


            if (categoryId != 0 && productName != null)
            {
                return _context.Products.Where(p => p.categoryId == categoryId || p.name == productName).ToList();
            }

            if (categoryId != 0)
            {
                return _context.Products.Where(p => p.categoryId == categoryId).ToList();
            }

            return _context.Products.Where(p => p.name == request.name).ToList();

        }

        private ProductErrorHandler SetResponse(bool state, string message, Product obj)
        {
            ProductErrorHandler response = new ProductErrorHandler
            {
                State = state,
                Message = message,
                obj = obj,
            };

            return response;
        }
    }
}
