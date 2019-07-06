using System.Collections.Generic;

using Product.Shared.Items;

namespace Product.Shared.Responses
{
    public class ProductResponse
    {
        public string Id { get; set; }

        public string Timestamp { get; set; }

        public List<ProductItem> Products { get; set; }
    }
}