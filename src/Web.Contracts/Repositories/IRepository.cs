namespace Web.Contracts.Repositories
{
    using System;

    public interface IRepository<in TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);
    }
}