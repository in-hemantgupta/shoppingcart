using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
   public interface ICalculationService
    {
        decimal GetTotal(ShoppingCartItems ShoppingCartItems);
        decimal GetTotalDiscount(ShoppingCartItems ShoppingCartItems);
    }
}