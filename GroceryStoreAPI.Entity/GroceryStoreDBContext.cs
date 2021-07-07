using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Entity
{
    public class GroceryStoreDBContext : DbContext
    {
        public GroceryStoreDBContext(DbContextOptions<GroceryStoreDBContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
