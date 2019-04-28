using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public decimal Discount { get; set; }
        public string DiscountName { get; set; }
    }

    public enum WeightUnit { Kilogram, Gram }
    public enum VolumeUnit { Liters, Mililiters }
    public enum NumbersUnit { Dozen }
}
