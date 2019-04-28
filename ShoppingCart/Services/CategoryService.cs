using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    class CategoryService
    {
        private List<Category> _categories { get; set; }
        public CategoryService()
        {

        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void RemoveCategory(Category category)
        {
            _categories.Add(category);
        }

        public Category FindCategory(int CategoryId)
        {
            return _categories.FirstOrDefault(cat => cat.Id == CategoryId);
        }

        public IList<Category> GetAllCategories()
        {
            return _categories;
        }
    }
}
