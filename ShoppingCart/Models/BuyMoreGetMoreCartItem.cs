using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class BuyMoreGetMoreProduct: Product
    {
        public int NoOfItemsToBuy { get; set; }
        public int NoOfFreeItems { get; set; }
    }
}
