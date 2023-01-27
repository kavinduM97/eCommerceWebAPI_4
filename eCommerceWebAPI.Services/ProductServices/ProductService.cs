using eCommerceWebAPI.Requests;
using Azure;
using Azure.Core;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;

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
        private readonly DbbContext _dbcontext= new DbbContext();

        public ProductService(IProductcategoryRepository repository)
        {
            _productcategoriesServices = repository;
           


        }
        public ProductErrorHandler AddaProduct(ProductRequest request)
        {

            if (_dbcontext.Products.Any(p => p.name == request.name && p.categoryId == request.categoryId))
            {
                response = SetResponse(false, "Product already exists", null);
                return response;
            }
            var myProductcategories = _productcategoriesServices.AllProductcategories();
            if (!myProductcategories.Any(u => u.catergoryId == request.categoryId))
            {
                response = SetResponse(false, "Product category is not matching", null);
                return response;
            }

            try
            {

                var categoryName = _dbcontext.Categories.Where(c => c.catergoryId == request.categoryId).Select(c => c.Name).FirstOrDefault();
                var Product = new Product
                {

                    name = request.name,
                    description = request.description,
                    stock = request.stock,
                    categoryId = request.categoryId,
                    price = request.price,
                    categoryName= categoryName

                };
                _dbcontext.Products.Add(Product);
                _dbcontext.SaveChangesAsync();
                Thread.Sleep(2000);
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
            return _dbcontext.Products.ToList();
        }

      

        public Product OneProduct(int id)
        {
            return _dbcontext.Products.Find(id);
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
                    if (_dbcontext.Products.Any(p => p.name == request.updateName && p.categoryId == request.updateCategoryId))
                    {
                        response = SetResponse(false, "Product already exists in the category",  null);
                        return response;
                    }
                }
                else
                {
                    if (_dbcontext.Products.Any(p => p.name == request.updateName && p.categoryId == product.categoryId))
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
                if (_dbcontext.Products.Any(p => p.name == product.name && p.   categoryId == request.updateCategoryId))
                {
                    response = SetResponse(false, "Product already exists in the category", null);
                    return response;
                }
                product.categoryId = request.updateCategoryId;
            }



            _dbcontext.Products.Update(product);
            _dbcontext.SaveChangesAsync();



            product = OneProduct(id);



            response = SetResponse(true, "Category updated successfully", product);
            return response;
        }
        public  ProductErrorHandler DeleteProduct(int id)
        {

            var myproductServices = AllProduct();
            if (!myproductServices.Any(u => u.categoryId == id))
            {
                response = SetResponse(false, "Product is already not in the system", null);
                return response;
            }

               var product = OneProduct(id);

            _dbcontext.Products.Remove(product);
            _dbcontext.SaveChangesAsync();
            response = SetResponse(true, "Product is Deleted", product);
            return response;
        }

        public List<Product> SearchProduct(SearchProductRequest request)
        {
            var categoryId = _dbcontext.Categories.Where(c => c.Name == request.name).Select(c => c.catergoryId).FirstOrDefault();
            var productName = _dbcontext.Products.Where(p => p.name == request.name).Select(p => p.name).FirstOrDefault();


            if (categoryId != 0 && productName != null)
            {
                return _dbcontext.Products.Where(p => p.categoryId == categoryId || p.name == productName).ToList();
            }

            if (categoryId != 0)
            {
                return _dbcontext.Products.Where(p => p.categoryId == categoryId).ToList();
            }

            return _dbcontext.Products.Where(p => p.name == request.name).ToList();

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
