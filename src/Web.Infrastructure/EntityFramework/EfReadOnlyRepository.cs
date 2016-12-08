namespace Web.Infrastructure.EntityFramework
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Web.Contracts.Entities;
    using Web.Contracts.Repositories;

    public class EfReadOnlyRepository<TEntity> : ReadOnlyRepository<TEntity>, IAsyncViewRepository<TEntity>
        where TEntity : class
    {
        public EfReadOnlyRepository(IDbContext context)
            : base(context)
        {
        }

        IQueryable<TEntity> IViewRepository<TEntity>.All(params string[] includes) => All(includes);

        TEntity IViewRepository<TEntity>.Get(Expression<Func<TEntity, bool>> predicate, params string[] includes) => Get(predicate, includes);

        Task<TEntity> IAsyncViewRepository<TEntity>.GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            params string[] includes) => All(includes).FirstOrDefaultAsync(predicate);

        protected IQueryable<TEntity> All(params string[] includes)
        {
            if ((includes == null) || !includes.Any())
            {
                return DbSet.AsQueryable();
            }

            //Waiting for string based includes #6417

            //var query = DbSet.Include(includes.First());
            //query = includes.Skip(1).Aggregate(query, (current, include) => current.Include(include));
            //return query;
            return DbSet.AsQueryable();
        }

        protected TEntity Get(Expression<Func<TEntity, bool>> predicate, string[] includes) => All(includes).FirstOrDefault(predicate);
    }
}