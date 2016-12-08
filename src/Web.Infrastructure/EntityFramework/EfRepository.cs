namespace Web.Infrastructure.EntityFramework
{
    using Web.Contracts.Entities;
    using Web.Contracts.Repositories;

    public class EfRepository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        public EfRepository(IDbContext context)
            : base(context)
        {
        }

        public void Attach(TEntity entity) => DbSet.Attach(entity);

        void IRepository<TEntity>.Add(TEntity entity) => Add(entity);

        void IRepository<TEntity>.Delete(TEntity entity) => Delete(entity);

        protected void Add(TEntity entity) => DbSet.Add(entity);

        protected void Delete(TEntity entity) => DbSet.Remove(entity);
    }
}