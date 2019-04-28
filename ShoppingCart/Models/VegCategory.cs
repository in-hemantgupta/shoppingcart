using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class VegCategory : ProduceCategory
    {
        public WeightUnit unit { get; set; }
    }
}
