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
        public List<Product> GetProducts()
        {
            return (new List<Product>() {
                _service.AddOffer(CreateProduct( Products.Apple), 3, 1),
                CreateProduct( Products.Orange),
                _service.AddOffer(CreateProduct( Products.Potato), 5, 2),
                CreateProduct( Products.Tomato),
                _service.AddOffer(CreateProduct( Products.CowMilk), 3, 1),
                CreateProduct( Products.SoyaMilk),
                _service.AddOffer(CreateProduct( Products.Cheddar), 2, 1),
                CreateProduct( Products.Gauda)
        });
        }

        public enum Products { Apple, Orange, Potato, Tomato, CowMilk, SoyaMilk, Cheddar, Gauda }

        public Product CreateProduct(Products product)
        {
            switch (product)
            {
                case Products.Apple:
                    return new Product() { Id = 1, Price = 50, CategoryId = 3, Name = "Apple" };
                case Products.Orange:
                    return new Product() { Id = 2, Price = 80, CategoryId = 3, Name = "Orange", Discount = .2M };
                case Products.Potato:
                    return new Product() { Id = 3, Price = 30, CategoryId = 4, Name = "Potato" };
                case Products.Tomato:
                    return new Product() { Id = 4, Price = 70, CategoryId = 4, Name = "Tomato", Discount = .1M };
                case Products.CowMilk:
                    return new Product() { Id = 5, Price = 50, CategoryId = 5, Name = "Cow Milk" };
                case Products.SoyaMilk:
                    return new Product() { Id = 6, Price = 40, CategoryId = 5, Name = "Soya Milk", Discount = .1M };
                case Products.Cheddar:
                    return new Product() { Id = 7, Price = 50, CategoryId = 6, Name = "Cheddar" };
                case Products.Gauda:
                    return new Product() { Id = 8, Price = 80, CategoryId = 6, Name = "Gauda", Discount = .1M };
                default:
                    return null;
            }
        }
    }
}
