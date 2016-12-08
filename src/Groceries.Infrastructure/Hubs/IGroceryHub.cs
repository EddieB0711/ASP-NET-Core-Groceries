namespace Groceries.Infrastructure.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Groceries.Infrastructure.Entities;

    public interface IGroceryHub
    {
        Task Polling();

        void AddItems(IList<Grocery> records);
    }
}