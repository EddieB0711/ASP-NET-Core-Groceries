namespace Web.Contracts.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IViewRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All(params string[] includes);

        TEntity Get(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    }
}