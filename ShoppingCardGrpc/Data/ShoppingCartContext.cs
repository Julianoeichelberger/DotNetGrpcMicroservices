using Microsoft.EntityFrameworkCore;
using ShoppingCardGrpc.Models;

namespace ShoppingCardGrpc.Data
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options) { }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

    }
}
