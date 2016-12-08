namespace Groceries.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Groceries.Infrastructure.Entities;

    public interface IGroceryService
    {
        Task<IEnumerable<Grocery>> GetGroceriesAsync();

        Task<Grocery> GetGroceryAsync(int id);
    }
}