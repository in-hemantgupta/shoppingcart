using Logger;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public class CalculationService : ICalculationService
    {
        private ICategoryService _categoryService;
        private ILogger _logger;

        public CalculationService(ICategoryService CategoryService, ILogger logger )
        {
            _categoryService = CategoryService;
            _logger = logger;
        }

        public decimal GetTotal(ShoppingCartItems ShoppingCartItems)
        {
            decimal ItemsTotal = 0;
            ShoppingCartItems.Items.ForEach(s => { ItemsTotal += s.Product.Price * s.Count; });
            return ItemsTotal;
        }

        public decimal GetTotalDiscount(ShoppingCartItems ShoppingCartItems)
        {
            try
            {
                decimal TotalPriceAfterDiscount = 0;
                // decimal TotalPrice = 0;

                ShoppingCartItems.Items.ForEach(s =>
                {
                    if (s.Product is BuyMoreGetMoreProduct)
                    {
                        var item = s.Product as BuyMoreGetMoreProduct;

                        var eligibleFreeItems = Math.Floor(Convert.ToDecimal(s.Count / (item.NoOfFreeItems + item.NoOfItemsToBuy)));
                        //Get the number of items which are free 
                        var actualFreeItems = eligibleFreeItems * item.NoOfFreeItems;
                        //minus these free items from total
                        var itemsPriceAfterDiscount = CalculateDiscountPrice(s.Product.Discount, s.Product.Price, (s.Count - actualFreeItems));
                        TotalPriceAfterDiscount += GetBestCategoryDiscount(s, s.Product.Price * s.Count - itemsPriceAfterDiscount);
                    }
                    else
                    {
                        TotalPriceAfterDiscount += GetBestCategoryDiscount(s, CalculateDiscountPrice(s.Product.Discount, s.Product.Price, s.Count));
                    }
                    //TotalPrice += s.Product.Price * s.Count;
                });
                return TotalPriceAfterDiscount;
            }
            catch(Exception ex)
            {
                _logger.LogError("Caught some error: " + ex.Message);
                return 0;
            }
        }

        private decimal GetBestCategoryDiscount(CartItem item, decimal itemPriceAfterDiscount)
        {
            decimal bestDiscountedPrice = itemPriceAfterDiscount;
            bestDiscountedPrice = GetCategoryDiscount(item.Product.CategoryId, item, bestDiscountedPrice);
            var Category = _categoryService.FindCategory(item.Product.CategoryId);
            //calculate the discount to parent category
            while (Category.ParentId != null)
            {
                bestDiscountedPrice = GetCategoryDiscount(Category.ParentId.Value, item, bestDiscountedPrice);
                if (Category.ParentId == null)
                    break;
                Category = _categoryService.FindCategory(Category.ParentId.Value);
            }
            return bestDiscountedPrice;
        }

        private decimal GetCategoryDiscount(int CategoryId, CartItem item, decimal bestDiscountedPrice)
        {
            var CategoryDiscount = _categoryService.FindCategory(CategoryId)?.Discount;
            CategoryDiscount = CalculateDiscountPrice(CategoryDiscount.HasValue ? CategoryDiscount.Value : 1, item.Product.Price, item.Count);

            if (bestDiscountedPrice < CategoryDiscount)
            {
                bestDiscountedPrice = CategoryDiscount.Value;
            }
            return bestDiscountedPrice;
        }

        private static decimal CalculateDiscountPrice(decimal discount, decimal price, decimal count)
        {
            discount = discount > 0 ? discount : 1;
            return discount * price * count;
        }
    }
}
