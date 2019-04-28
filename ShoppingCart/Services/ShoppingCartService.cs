using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{
    class ShoppingCartService : IShoppingCartService
    {
        private ShoppingCartItems ShoppingCart { get; set; }

        public ShoppingCartService()
        {
            ShoppingCart.Id = Guid.NewGuid();
            ShoppingCart.Items = new List<CartItem>();
        }
        public Decimal GetTotal()
        {
            return 0;
        }

        public void AddItem(Product item)
        {
            var cartItem = ShoppingCart.Items.FirstOrDefault(s => s.Product == item);
            if (cartItem == null)
            {
                ShoppingCart.Items.Add(new CartItem() { Count = 1, Product = item, Id = ShoppingCart.Items.Count + 1 });
            }
            else
            {
                cartItem.Count += 1;
            }
        }
        public void RemoveItem(Product item)
        {
            var cartItem = ShoppingCart.Items.FirstOrDefault(s => s.Product == item);
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

        public ShoppingCartItems GetCart() {
            return ShoppingCart;
        }
    }
}
