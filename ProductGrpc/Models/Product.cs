using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Models
{
    public enum ProductStatus
    {
        IN_STOCK = 0,
        LOW = 1,
        NONE = 2
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
