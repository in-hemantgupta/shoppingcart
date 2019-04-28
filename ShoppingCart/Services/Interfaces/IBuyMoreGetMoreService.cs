using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface IBuyMoreGetMoreService
    {
        BuyMoreGetMoreProduct AddOffer(Product product, int buyQty, int freeQty);
    }
}