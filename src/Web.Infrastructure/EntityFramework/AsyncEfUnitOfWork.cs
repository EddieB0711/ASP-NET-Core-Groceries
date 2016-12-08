namespace Web.Infrastructure.EntityFramework
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Web.Contracts.Entities;
    using Web.Contracts.Enums;
    using Web.Contracts.Repositories;

    public class AsyncEfUnitOfWork : IAsyncUnitOfWork
    {
        private readonly IAsyncDbContext _context;

        public AsyncEfUnitOfWork(IAsyncDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();

            GC.SuppressFinalize(this);
        }

        public void TrackChanges(ChangePolicy changePolicy)
        {
            _context.AutoTrackChanges = changePolicy;
        }
    }
}