using Microsoft.EntityFrameworkCore; 

namespace ProductGrpc.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Models.Product> Product { get; set; }
    }
}
