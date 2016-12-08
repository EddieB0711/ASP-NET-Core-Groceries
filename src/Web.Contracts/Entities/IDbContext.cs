namespace Web.Contracts.Entities
{
    using System;
    using System.Linq;

    using Web.Contracts.Enums;

    public interface IDbContext : IDisposable
    {
        ChangePolicy AutoTrackChanges { set; }

        object ChangeTracker { get; }

        IQueryable<T> AsQueryable<T>() where T : class;

        void SaveChanges();

        object Set<T>() where T : class;
    }
}