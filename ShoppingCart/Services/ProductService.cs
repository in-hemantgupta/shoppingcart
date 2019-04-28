using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products { get; set; }
        public ProductService(List<Product> products)
        {
            _products = products;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _products.Add(product);
        }

        public Product FindProduct(int productId)
        {
            return _products.FirstOrDefault(cat => cat.Id == productId);
        }

        public IList<Product> GetAllProducts()
        {
            return _products;
        }
    }
}
