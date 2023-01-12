using Assignment.Request;
using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.ProductServices
{
    public interface IProductRepository
    {
        public List<Product> AllProduct();
        public Product OneProduct(int id);
        public ProductErrorHandler AddaProduct(ProductRequest request);
        public ProductErrorHandler UpdateProduct(int id,ProductRequest request);
        public ProductErrorHandler DeleteProduct(int id);
        public List<Product> SearchProduct(SearchProductRequest request);

    }
}
