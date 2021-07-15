using System;
using System.Collections.Generic;
using System.Linq; 

namespace ProductGrpc.Data
{
    public class ProductContextSeed
    {
        public static void Async(ProductContext productContext) 
        {
            if (!productContext.Product.Any())
            {
                var products = new List<Models.Product> 
                { 
                    new Models.Product
                    {
                        Id = 1,
                        Name = "Product_1",
                        Description = "Description of product 1",
                        CreatedTime = DateTime.UtcNow,
                        Price = 59,
                        Status = Models.ProductStatus.IN_STOCK
                    },
                    new Models.Product
                    {
                        Id = 2,
                        Name = "Product_2",
                        Description = "Description of product 2",
                        CreatedTime = DateTime.UtcNow,
                        Price = 61,
                        Status = Models.ProductStatus.IN_STOCK
                    },
                    new Models.Product
                    {
                        Id = 3,
                        Name = "Product_3",
                        Description = "Description of product 3",
                        CreatedTime = DateTime.UtcNow,
                        Price = 63,
                        Status = Models.ProductStatus.IN_STOCK
                    }
                };
                productContext.AddRange(products);
                productContext.SaveChanges();
            }
        }
    }
}
