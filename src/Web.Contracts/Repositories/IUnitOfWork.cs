namespace Web.Contracts.Repositories
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}