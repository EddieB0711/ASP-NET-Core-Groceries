namespace Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using LinqKit;

    using Microsoft.EntityFrameworkCore;

    using Web.Contracts.Entities;
    using Web.Contracts.Repositories;

    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>, IAsyncReadOnlyRepository<TEntity>
        where TEntity : class
    {
        public ReadOnlyRepository(IDbContext context)
        {
            Context = context;
        }

        protected DbSet<TEntity> DbSet => (DbSet<TEntity>)Context.Set<TEntity>();

        protected IDbContext Context { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        async Task<IList<TEntity>> IAsyncReadOnlyRepository<TEntity>.FindAsync(
            Expression<Func<TEntity, bool>> predicate) => await FindAsync(predicate);

        IList<TEntity> IReadOnlyRepository<TEntity>.Find(Expression<Func<TEntity, bool>> predicate) => Find(predicate);

        protected IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => (predicate == null ? Find() : Find().Where(predicate).AsExpandable()).EnsureNotEmpty();

        protected async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var task = (predicate == null ? Find() : Find().Where(predicate).AsExpandable()).EnsureNotEmptyAsync();
            return await task;
        }

        protected IQueryable<TEntity> Find() => Context.AsQueryable<TEntity>();

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            Context?.Dispose();
            Context = null;
        }
    }
}