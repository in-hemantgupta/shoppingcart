namespace ShoppingCart.Services.Interfaces
{
    interface ICalculationService
    {
        decimal GetTotal();
        decimal GetTotalDiscount();
    }
}