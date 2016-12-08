namespace Groceries.Service.Infrastructure.Configurations
{
    using Groceries.Service.Infrastructure.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Web.Contracts.Configurations;

    public class GroceryConfiguration : EntityTypeConfiguration<Grocery>
    {
        public override void Configure(EntityTypeBuilder<Grocery> builder)
        {
            builder.ToTable("Grocery");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id).UseSqlServerIdentityColumn();

            builder.Property(g => g.Item).HasColumnName("Item");
        }
    }
}