using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests.Mock
{
    public class MockCategories
    {
        public List<Category> Categories;

        public MockCategories()
        {
            Categories = new List<Category>();
            Categories.AddRange(new List<Category>() {
                new ProduceCategory
                {
                    Discount = 0.1M,
                    DiscountName = "10% Off",
                    Id = 1,
                    Name = "Produce"
                },
             new DairyCategory
                {
                    Discount = 0.15M,
                    DiscountName = "15% Off",
                    Id = 2,
                    Name = "Dairy"
                },
                new FruitCategory
                {
                    Discount = 0.18M,
                    DiscountName = "18% Off",
                    Id = 3,
                    Name = "Fruits",
                    ParentId = 1,
                    unit = WeightUnit.Kilogram
                },
             new VegCategory
                {
                    Discount = 0.05M,
                    DiscountName = "5% Off",
                    Id = 4,
                    Name = "Veg",
                    ParentId = 1,
                    unit = WeightUnit.Kilogram
                },
             new MilkCategory
                {
                    Discount = 0.20M,
                    DiscountName = "20% Off",
                    Id = 5,
                    Name = "Milk",
                    ParentId = 2,
                    unit = VolumeUnit.Liters
                },
            new CheeseCategory
                {
                    Discount = 0.20M,
                    DiscountName = "20% Off",
                    Id = 6,
                    Name = "Milk",
                    ParentId = 2,
                    unit =  WeightUnit.Kilogram
                },

            });
        }

    }
}
