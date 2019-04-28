using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests.Mock
{
    public class MockCartItems
    {
        private IProductService _productService;
        List<Product> _products;
        private IBuyMoreGetMoreService _buyMoreGetMoreService;

        public MockCartItems(IProductService productService, IBuyMoreGetMoreService buyMoreGetMoreService)
        {
            _productService = productService;
            _buyMoreGetMoreService = buyMoreGetMoreService;
            _products = new MockProducts(_buyMoreGetMoreService).GetProducts();
        }
        public List<CartItem> GetCartItems()
        {
            return (new List<CartItem>() {
                new CartItem() { Id=1, Count = 6, Product = _productService.FindProduct(1) },
                new CartItem() { Id=2, Count = 2, Product = _productService.FindProduct(2) },
                new CartItem() { Id=3, Count = 14, Product = _productService.FindProduct(3) },
                new CartItem() { Id=4, Count = 3, Product = _productService.FindProduct(4) },
                new CartItem() { Id=5, Count = 8, Product = _productService.FindProduct(5) },
                new CartItem() { Id=6, Count = 2, Product = _productService.FindProduct(8) },
            });
        }
    }
}
