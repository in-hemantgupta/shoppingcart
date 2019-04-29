using System;
using NUnit.Framework;
using ShoppingCart.Services;
using System.Collections.Generic;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using Logger;
using Moq;

namespace ShoppingCart.Tests
{
    [TestFixture]
    public class ShoppingCartServiceTest
    {
        IShoppingCartService ShoppingCartService;
        ICalculationService CalculationService;
        ICategoryService CategoryService;
        IProductService ProductService;
        List<Product> Products;
        List<Category> Categories;
        List<CartItem> CartItems;
        Mock.MockProducts MockProductService;
        IBuyMoreGetMoreService BuyMoreGetMoreService;
        Mock<ILogger> loggerService;

        [SetUp]
        public void Setup()
        {
            Categories = new Mock.MockCategories().Categories;
            CategoryService = new CategoryService(Categories);

            loggerService = new Mock<ILogger>();
            loggerService.Setup(l => l.LogError(It.IsAny<string>()));
            loggerService.Setup(l => l.LogInfo(It.IsAny<string>()));
            loggerService.Setup(l => l.LogWarning(It.IsAny<string>()));

            CalculationService = new CalculationService(CategoryService, loggerService.Object);
            ShoppingCartService = new ShoppingCartService(CalculationService);
            BuyMoreGetMoreService = new BuyMoreGetMoreService();
            MockProductService = new Mock.MockProducts(BuyMoreGetMoreService);
            Products = MockProductService.GetProducts();
            ProductService = new ProductService(Products);
            MockProductService = new Mock.MockProducts(BuyMoreGetMoreService);
            CartItems = new Mock.MockCartItems(ProductService, BuyMoreGetMoreService).GetCartItems();
        }

        [Test]
        public void WhenAddProduct_AnyItem_CartIdShouldNotEmpty()
        {
            ShoppingCartService.AddItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));

            Assert.That(ShoppingCartService.GetCart().Id, Is.Not.Null);
        }

        [Test]
        public void WhenAddProduct_Apple_CartShouldHaveOneItemAndItShouldBeApple()
        {
            ShoppingCartService.AddItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));

            Assert.That(ShoppingCartService.GetCart().Id, Is.Not.Null);
            Assert.That(ShoppingCartService.GetCart().Items.Count, Is.EqualTo(1));
            Assert.That(ShoppingCartService.GetCart().Items[0].Product.Name, Is.EqualTo(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple).Name));
        }

        [Test]
        public void WhenAddProduct_Apple_CartTotalValueShouldBe50()
        {
            ShoppingCartService.AddItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));

            Assert.That(ShoppingCartService.GetCart().Id, Is.Not.Null);
            Assert.That(ShoppingCartService.GetCart().Items.Count, Is.EqualTo(1));
            Assert.That(ShoppingCartService.GetCart().Items[0].Product.Name, Is.EqualTo(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple).Name));
            Assert.That(ShoppingCartService.GetCart().TotalValue, Is.EqualTo(50));
        }

        [Test]
        public void WhenAddProduct_6Apple_CartTotalValueShouldBe250()
        {
            var OfferedApple = BuyMoreGetMoreService.AddOffer(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple), 3, 1);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);

            var result = ShoppingCartService.GetCart();
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.Items[0].Count, Is.EqualTo(6));
            Assert.That(result.Items[0].Product.Name, Is.EqualTo(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple).Name));
            Assert.That(result.TotalValue - result.TotalDiscount, Is.EqualTo(246));
        }

        [Test]
        public void WhenAddProduct_5Apple_CartTotalValueShouldBe200()
        {
            var OfferedApple = BuyMoreGetMoreService.AddOffer(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple), 3, 1);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);
            ShoppingCartService.AddItem(OfferedApple);

            var result = ShoppingCartService.GetCart();
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.Items[0].Count, Is.EqualTo(5));
            Assert.That(result.Items[0].Product.Name, Is.EqualTo(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple).Name));
            Assert.That(result.TotalValue - result.TotalDiscount, Is.EqualTo(200));
        }

        [Test]
        public void WhenRemoveProduct_Apple_CartShouldNotHaveApple()
        {
            ShoppingCartService.AddItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));
            ShoppingCartService.RemoveItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));

            var result = ShoppingCartService.GetCart();
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void WhenRemoveProduct_1AppleFrom2Apples_CartShouldHave1Apple()
        {
            ShoppingCartService.AddItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));
            ShoppingCartService.AddItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));
            ShoppingCartService.RemoveItem(MockProductService.CreateProduct(Mock.MockProducts.Products.Apple));

            var result = ShoppingCartService.GetCart();
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items[0].Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenAddProducts_MultipleProducts_CartTotalDiscountValueShouldBe359()
        {
            CartItems.ForEach(item =>
            {
                ShoppingCartService.AddItem(item.Product, item.Count);
            });

            var result = ShoppingCartService.GetCart();
            Assert.That(result.Id, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(6));
            Assert.That(result.TotalValue, Is.EqualTo(1650));
            Assert.That(result.TotalDiscount, Is.EqualTo(359));
        }
    }
}
