using System.Collections.Generic;

using Product.Shared.Items;

namespace Product.Shared.Requests
{
    public class ProductRequest
    {
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public ProductItem Product { get; set; }
    }
}