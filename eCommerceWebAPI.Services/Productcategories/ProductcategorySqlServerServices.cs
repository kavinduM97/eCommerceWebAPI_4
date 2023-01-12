﻿using eCommerceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceWebAPI.DataAccess;
using eCommerceWebAPI.Requests;
using eCommerceWebAPI.ErrorHandler;
using Azure;

namespace eCommerceWebAPI.Services.Productcategories
{
    public class ProductcategorySqlServerServices : IProductcategoryRepository
    {
        private readonly DbbContext _context = new DbbContext();
        private CategoryErrorHandler response;


        public List<Category> AllProductcategories()
        {
            return _context.Categories.ToList();

        }

        public Category OneProductcategory(int id)
        {
            return _context.Categories.Find(id);

        }

        public  CategoryErrorHandler AddaProductCaterory(ProductCatergoryRequest request)
        {

            var myProductcategories = AllProductcategories();


         /*   if (myProductcategories.Any(u => u.name == request.name))
            {
                response = SetResponse(false, "Product catergory is already in the system", null);
                return response;
            }*/
            var category = new Category
            {
              
                name = request.name,
                description = request.description,
            };


           _context.Categories.Add(category);
           _context.SaveChangesAsync();
            response = SetResponse(true, "Product catergory is added", category);
            return response;



        }

        public  CategoryErrorHandler UpdateProductCatergory(int id, ProductCatergoryRequest request)
       
        {

            var myproductcategories = AllProductcategories();

            if (!myproductcategories.Any(u => u.categoryId == id))
            {

                response = SetResponse(false, "Product catergory is not in the system", null);
                return response;

            }
          /*  if (myproductcategories.Any(u => u.name  == request.name)) 
            {
                response = SetResponse(false, "Product catergory is already in the system", null);
                return response;
            }*/

            var productcatergory = OneProductcategory(id);

            productcatergory.name = request.name;
            productcatergory.description = request.description;
 
            _context.Categories.Update(productcatergory);
             _context.SaveChangesAsync();
            response = SetResponse(true, "Product catergory is Updated", productcatergory);
            return response;

        }

        public  CategoryErrorHandler DeleteProductCatergory(int id)
        {

            var myproductcategoriesServices = AllProductcategories();
            if (!myproductcategoriesServices.Any(u => u.categoryId == id))
            {

                response = SetResponse(false, "Product catergory is already not in the system", null);
                return response;

            }
            var productcatergory = OneProductcategory(id);

            _context.Categories.Remove(productcatergory);
             _context.SaveChangesAsync();
            response = SetResponse(true, "Product catergory is Deleted", productcatergory);
            return response;
        }

        private CategoryErrorHandler SetResponse(bool state, string message, Category obj)
        {
            CategoryErrorHandler response = new CategoryErrorHandler
            {
                State = state,
                Message = message,
                obj = obj,
               };

            return response;
        }
    }

}