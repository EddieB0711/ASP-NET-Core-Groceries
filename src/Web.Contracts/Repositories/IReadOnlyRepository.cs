namespace Web.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IReadOnlyRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null);
    }
}