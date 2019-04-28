using ShoppingCart.Models;

namespace ShoppingCart.Services
{
   public interface IShoppingCartService
    {
        decimal GetTotal();
        void AddItem(Product item, int quantity = 1);
        void RemoveItem(Product item);

        ShoppingCartItems GetCart();
    }
}