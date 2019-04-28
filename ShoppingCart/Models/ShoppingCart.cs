using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class ShoppingCartItems
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
