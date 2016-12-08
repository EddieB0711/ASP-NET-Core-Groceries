namespace Groceries.Service.Infrastructure.Context
{
    using Groceries.Service.Infrastructure.Configurations;

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