using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    class CalculationService : ICalculationService
    {
        private ShoppingCartItems _shoppingCartItems;
        private CategoryService _categoryService;
        public CalculationService(ShoppingCartItems items, CategoryService CategoryService)
        {
            _shoppingCartItems = items;
            _categoryService = CategoryService;
        }

        public decimal GetTotal()
        {
            decimal ItemsDiscount = 0; decimal ItemsTotal = 0;
            _shoppingCartItems.Items.ForEach(s => { ItemsDiscount += s.Product.Discount * s.Product.Price; ItemsTotal = s.Product.Price; });
            return ItemsTotal;
        }

        public decimal GetTotalDiscount()
        {
            decimal TotalPriceAfterDiscount = 0;
            decimal TotalPrice = 0;

            _shoppingCartItems.Items.ForEach(s =>
            {
                if (s.Product is BuyMoreGetMoreProduct)
                {
                    var item = s.Product as BuyMoreGetMoreProduct;

                    var eligibleFreeItems = Math.Floor(Convert.ToDecimal(s.Count / (item.NoOfFreeItems + item.NoOfItemsToBuy)));
                    //Get the number of items which are free 
                    var actualFreeItems = eligibleFreeItems * item.NoOfFreeItems;
                    //minus these free items from total
                    var itemsPriceAfterDiscount = CalculateDiscountPrice(s.Product.Discount, s.Product.Price, (s.Count - actualFreeItems));
                    TotalPriceAfterDiscount += GetBestCategoryDiscount(s, itemsPriceAfterDiscount);
                }
                else
                {
                    TotalPriceAfterDiscount += CalculateDiscountPrice(s.Product.Discount, s.Product.Price, s.Count);
                }
                TotalPrice += s.Product.Price;
            });
            return TotalPrice - TotalPriceAfterDiscount;
        }

        private decimal GetBestCategoryDiscount(CartItem item, decimal itemPriceAfterDiscount)
        {
            decimal bestDiscountedPrice = itemPriceAfterDiscount;
            bestDiscountedPrice = GetCategoryDiscount(item.Product.CategoryId, item, bestDiscountedPrice);
            var ParentCategory = _categoryService.FindCategory(item.Product.CategoryId);
            //calculate the discount to parent category
            while (ParentCategory != null)
            {
                bestDiscountedPrice = GetCategoryDiscount(ParentCategory.Id, item, bestDiscountedPrice);
                if (ParentCategory.ParentId == null)
                    break;
                ParentCategory = _categoryService.FindCategory(ParentCategory.ParentId.Value);
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
