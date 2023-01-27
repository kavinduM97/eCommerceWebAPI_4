using eCommerceWebAPI.Models;
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
    public class ProductcategoryServices : IProductcategoryRepository
    {
        private readonly DbbContext _dbcontext = new DbbContext();
        private CategoryErrorHandler response;

      
        public List<Category> AllProductcategories()
        {
            return _dbcontext.Categories.ToList();

        }

        public Category OneProductcategory(int id)
        {
            return _dbcontext.Categories.Find(id);

        }

        public  CategoryErrorHandler AddaProductCaterory(ProductCatergoryRequest request)
        {

            var myProductcategories = AllProductcategories();


            if (myProductcategories.Any(u => u.Name == request.name))
            {
                response = SetResponse(false, "Product catergory is already in the system", null);
                return response;
            }
            var category = new Category
            {
              
                Name = request.name,
                Description = request.description,
            };


            _dbcontext.Categories.Add(category);
            _dbcontext.SaveChangesAsync();
            response = SetResponse(true, "Product catergory is added", category);
            return response;



        }

        public  CategoryErrorHandler UpdateProductCatergory(int id, UpdateProductCategoryRequest request)
       
        {

            var myProductcategories = AllProductcategories();
            var productcatergory = OneProductcategory(id);

        

            if (request.updateName == null && request.updateDescription == null)
            {
                response = SetResponse(false, "Nothing to Update", null);
                return response;

            }
            if (_dbcontext.Categories.Any(c => c.Name == request.updateName))
            {
                if (productcatergory.Name != request.updateName)
                {
                    response = SetResponse(false, "Category already exists", null);
                    return response;
                }


            }

            if (request.updateName != null)
            {
                productcatergory.Name = request.updateName;
            }

            if (request.updateDescription != null)
            {
                productcatergory.Description = request.updateDescription;
            }

            _dbcontext.Categories.Update(productcatergory);
            _dbcontext.SaveChangesAsync();

            productcatergory = OneProductcategory(id);

            response = SetResponse(true, "Category updated successfully", productcatergory);
            return response;

        }

        public  CategoryErrorHandler DeleteProductCatergory(int id)
        {

            var myproductcategoriesServices = AllProductcategories();
            if (!myproductcategoriesServices.Any(u => u.catergoryId == id))
            {

                response = SetResponse(false, "Product catergory is already not in the system", null);
                return response;

            }
            var productcatergory = OneProductcategory(id);

            _dbcontext.Categories.Remove(productcatergory);
            _dbcontext.SaveChangesAsync();
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
