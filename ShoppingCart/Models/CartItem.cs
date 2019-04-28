using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
        public void AddItem(CartItem item) { }
        public void RemoveItem(CartItem item) { }
    }
}
