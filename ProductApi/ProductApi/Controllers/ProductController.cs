using System;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;

using Product.Shared.Items;
using Product.Shared.Requests;
using Product.Shared.Responses;

namespace ProductApi.Controllers
{
    public class ProductController : BaseController
    {
        [HttpGet]
        public ProductResponse GetProducts()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var response = new ProductResponse
            {
                Id = Guid.NewGuid().ToString(),
                Timestamp = unixTimestamp.ToString()
            };

            var ents = new List<ProductItem>();

            using (var db = new ProductEntities())
            {
                var results = db.PRODUCTS.ToList<PRODUCT>();

                if (results.Any())
                {
                    foreach (var e in results)
                    {
                        var ent = new ProductItem
                        {
                            Id = e.ID,
                            Name = e.NAME,
                            Quantity = e.QUANTITY,
                            SaleAmount = e.SALE_AMOUNT
                        };

                        ents.Add(ent);
                    }
                }
            }

            response.Products = ents;

            return response;
        }

        [HttpPost]
        public ProductResponse SaveProduct(ProductRequest request)
        {
            ProductItem item = request.Product;
            var response = new ProductResponse();

            using (var db = new ProductEntities())
            {
                var model = new PRODUCT
                {
                    NAME = item.Name,
                    QUANTITY = item.Quantity,
                    SALE_AMOUNT = item.SaleAmount
                };

                if (item.Id == null)
                    db.PRODUCTS.Add(model);
                else
                {
                    model.ID = (int)item.Id;
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                    
                db.SaveChanges();
            }

            return response;
        }

        [HttpPost]
        public ProductResponse Delete(ProductRequest request)
        {
            ProductItem item = request.Product;

            var response = new ProductResponse();

            using (var db = new ProductEntities())
            {
                var model = new PRODUCT
                {
                    ID = (int)item.Id
                };

                var entry = db.Entry(model);

                if (entry.State == System.Data.Entity.EntityState.Detached)
                    db.PRODUCTS.Attach(model);

                db.PRODUCTS.Remove(model);
                db.SaveChanges();
            }

            return response;
        }
    }
}
