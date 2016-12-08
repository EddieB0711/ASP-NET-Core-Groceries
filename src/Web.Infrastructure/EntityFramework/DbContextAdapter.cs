namespace Web.Infrastructure.EntityFramework
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Web.Contracts.Entities;
    using Web.Contracts.Enums;

    public class DbContextAdapter : IAsyncDbContext
    {
        private readonly DbContext _context;

        public DbContextAdapter(DbContext context)
        {
            _context = context;
            
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public ChangePolicy AutoTrackChanges
        {
            set
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = value == ChangePolicy.On;
            }
        }

        object IDbContext.ChangeTracker => _context.ChangeTracker;

        public IQueryable<T> AsQueryable<T>() where T : class
        {
            var result = (DbSet<T>)Set<T>();
            return result;
        }

        public void Dispose() => _context.Dispose();

        public void SaveChanges() => _context.SaveChanges();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        public object Set<T>() where T : class => _context.Set<T>();
    }
}