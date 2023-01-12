using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceWebAPI.Services.Productcategories
{
    public interface IProductcategoryRepository
    {
        public List<Category> AllProductcategories();
        public Category OneProductcategory(int id);
        public CategoryErrorHandler AddaProductCaterory(ProductCatergoryRequest request);
        public CategoryErrorHandler UpdateProductCatergory(int id, ProductCatergoryRequest request);
        public CategoryErrorHandler DeleteProductCatergory(int id);
    }
}
