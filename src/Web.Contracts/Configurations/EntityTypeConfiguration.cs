namespace Web.Contracts.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class EntityTypeConfiguration<TConfiguration> where TConfiguration : class
    {
        public abstract void Configure(EntityTypeBuilder<TConfiguration> builder);
    }
}