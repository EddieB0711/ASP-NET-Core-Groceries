namespace Web.Contracts.Entities
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAsyncDbContext : IDbContext
    {
        Task SaveChangesAsync();

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}