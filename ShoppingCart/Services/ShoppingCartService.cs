using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private ICalculationService _calculationService;

        private ShoppingCartItems ShoppingCart { get; set; }

        public ShoppingCartService(ICalculationService CalculationService)
        {
            ShoppingCart = new ShoppingCartItems();
            ShoppingCart.Id = Guid.NewGuid();
            ShoppingCart.Items = new List<CartItem>();
            _calculationService = CalculationService;
        }
        public Decimal GetTotal()
        {
            return _calculationService.GetTotal(ShoppingCart);
        }

        public void AddItem(Product item, int quantity = 1)
        {
            var cartItem = ShoppingCart.Items.FirstOrDefault(s => s.Product.Id == item.Id);
            if (cartItem == null)
            {
                ShoppingCart.Items.Add(new CartItem() { Count = quantity, Product = item, Id = ShoppingCart.Items.Count + 1 });
            }
            else
            {
                cartItem.Count += quantity;
            }
            ShoppingCart.TotalValue = GetTotal();
            ShoppingCart.TotalDiscount = _calculationService.GetTotalDiscount(ShoppingCart);
        }
        public void RemoveItem(Product item)
        {
            var cartItem = ShoppingCart.Items.FirstOrDefault(s => s.Product.Id == item.Id);
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count -= 1;
                }
                else
                {
                    ShoppingCart.Items.Remove(cartItem);
                }
            }
        }

        public ShoppingCartItems GetCart()
        {
            return ShoppingCart;
        }
    }
}
