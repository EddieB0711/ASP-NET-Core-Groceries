namespace Web.Infrastructure.EntityFramework
{
    using System;

    using Web.Contracts.Entities;
    using Web.Contracts.Enums;
    using Web.Contracts.Repositories;

    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;

        public EfUnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();

            GC.SuppressFinalize(this);
        }

        public void Save() => _context.SaveChanges();

        public void TrackChanges(ChangePolicy changePolicy) => _context.AutoTrackChanges = changePolicy;
    }
}