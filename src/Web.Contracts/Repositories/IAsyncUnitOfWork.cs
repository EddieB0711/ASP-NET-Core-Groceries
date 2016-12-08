namespace Web.Contracts.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAsyncUnitOfWork : IDisposable
    {
        Task SaveAsync();

        Task SaveAsync(CancellationToken cancellationToken);
    }
}