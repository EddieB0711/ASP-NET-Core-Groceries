namespace Web.Contracts.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAsyncViewRepository<TEntity> : IViewRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    }
}