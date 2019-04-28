using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests.Mock
{
    public class MockProducts
    {
        IBuyMoreGetMoreService _service;
        public MockProducts(IBuyMoreGetMoreService iBuyMoreGetMoreService)
        {
            _service = iBuyMoreGetMoreService;
        }
        List<Product> products = new List<Product>();
        public MockProducts()
        {
            products.AddRange(new List<Product>() {
                _service.AddOffer(new Product() { Id=1, Price = 50, CategoryId = 3,  Name = "Apple" }, 3, 1)
        });
        }
    }
}
