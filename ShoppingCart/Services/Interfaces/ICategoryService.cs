using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
  public  interface ICategoryService
    {
        void AddCategory(Category category);
        Category FindCategory(int CategoryId);
        IList<Category> GetAllCategories();
        void RemoveCategory(Category category);
    }
}