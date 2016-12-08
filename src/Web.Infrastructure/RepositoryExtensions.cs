namespace Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Web.Contracts.Extensions;
    using Web.Contracts.Repositories;

    public static class RepositoryExtensions
    {
        public static IList<T> EnsureNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            try
            {
                enumerable.Guard();
                return enumerable.ToList();
            }
            catch (Exception)
            {
                return Enumerable.Empty<T>().ToList();
            }
        }

        public static async Task<IList<T>> EnsureNotEmptyAsync<T>(this IQueryable<T> queryable)
        {
            try
            {
                queryable.Guard();
                return await queryable.ToListAsync();
            }
            catch (Exception)
            {
                return Enumerable.Empty<T>().ToList();
            }
        }

        public static T Get<T>(this IReadOnlyRepository<T> repo, Expression<Func<T, bool>> predicate) where T : class
        => repo.Find(predicate).FirstOrDefault();

        public static async Task<T> Get<T>(this IAsyncReadOnlyRepository<T> repo, Expression<Func<T, bool>> predicate)
            where T : class => (await repo.FindAsync(predicate)).FirstOrDefault();
    }
}