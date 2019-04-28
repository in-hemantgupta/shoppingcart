using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    interface IShoppingCartService
    {
        decimal GetTotal();
        void AddItem(Product item);
        void RemoveItem(Product item);

        ShoppingCartItems GetCart();
    }
}