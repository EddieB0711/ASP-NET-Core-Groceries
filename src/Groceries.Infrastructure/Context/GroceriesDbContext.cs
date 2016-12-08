namespace Groceries.Infrastructure.Context
{
    using Groceries.Infrastructure.Configurations;

    using Microsoft.EntityFrameworkCore;

    using Web.Contracts.Extensions;

    public class GroceriesDbContext : DbContext
    {
        public GroceriesDbContext(DbContextOptions<GroceriesDbContext> options)
            : base(options)
        {
        }

        public GroceriesDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseConfiguration(new GroceryConfiguration());
        }
    }
}