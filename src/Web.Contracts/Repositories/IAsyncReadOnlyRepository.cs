namespace Web.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAsyncReadOnlyRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}