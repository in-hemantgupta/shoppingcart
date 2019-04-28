using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public class BuyMoreGetMoreService : IBuyMoreGetMoreService
    {
        public BuyMoreGetMoreProduct AddOffer(Product product, int buyQty, int freeQty)
        {
            return new BuyMoreGetMoreProduct()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Discount = product.Discount,
                ExpiryDate = product.ExpiryDate,
                Name = product.Name,
                NoOfFreeItems = freeQty,
                NoOfItemsToBuy = buyQty,
                Price = product.Price
            };
        }
    }
}
