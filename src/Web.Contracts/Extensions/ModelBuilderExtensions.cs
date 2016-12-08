namespace Web.Contracts.Extensions
{
    using Microsoft.EntityFrameworkCore;

    using Web.Contracts.Configurations;

    public static class ModelBuilderExtensions
    {
        public static ModelBuilder UseConfiguration<TEntity>(
            this ModelBuilder builder,
            EntityTypeConfiguration<TEntity> config) where TEntity : class
        {
            builder.Entity<TEntity>(config.Configure);

            return builder;
        }
    }
}