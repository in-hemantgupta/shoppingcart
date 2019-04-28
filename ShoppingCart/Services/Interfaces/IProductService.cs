using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);
        Product FindProduct(int productId);
        IList<Product> GetAllProducts();
        void RemoveProduct(Product product);
    }
}