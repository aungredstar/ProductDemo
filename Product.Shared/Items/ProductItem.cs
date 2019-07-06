using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product.Shared.Items
{
    public class ProductItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? SaleAmount { get; set; }
    }
}