using eCommerceWebAPI.ErrorHandler;
using eCommerceWebAPI.Models;
using eCommerceWebAPI.Requests;

namespace eCommerceWebAPI.Services.Productcategories
{
    public class ProductcategoriesService : IProductcategoryRepository
    {
        public void AddaProductCaterory(ProductCatergoryRequest request)
        {
            throw new NotImplementedException();
        }

        public List<Category> AllProductcategories()
        {
            var productcategories = new List<Category>();
            var productcategory1 = new Category
            {
                categoryId = 1,
                name = "Electrical",
                description = "This is and 15w"
            };
            productcategories.Add(productcategory1);


            var productcategory2 = new Category
            {
                categoryId = 2,
                name = "Hand Craft",
                description = "Hela Handcraft"
            };
            productcategories.Add(productcategory2);


            var productcategory3 = new Category
            {
                categoryId = 3,
                name = "Food",
                description = "Fast Food"
            };
            productcategories.Add(productcategory3);
            return productcategories;
        }

        public void DeleteProductCatergory(int id)
        {
            throw new NotImplementedException();
        }

        public Category OneProductcategory(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductCatergory(int id, ProductCatergoryRequest request)
        {
            throw new NotImplementedException();
        }

        CategoryErrorHandler IProductcategoryRepository.AddaProductCaterory(ProductCatergoryRequest request)
        {
            throw new NotImplementedException();
        }

        CategoryErrorHandler IProductcategoryRepository.DeleteProductCatergory(int id)
        {
            throw new NotImplementedException();
        }

        CategoryErrorHandler IProductcategoryRepository.UpdateProductCatergory(int id, ProductCatergoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
