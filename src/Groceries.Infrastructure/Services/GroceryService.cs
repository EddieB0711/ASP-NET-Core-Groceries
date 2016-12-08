namespace Groceries.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Groceries.Infrastructure.Builders;
    using Groceries.Infrastructure.Entities;

    using Web.Contracts.Extensions;

    public class GroceryService : IGroceryService
    {
        private readonly UriServiceBuilder _builder;

        public GroceryService(UriServiceBuilder builder)
        {
            builder.Guard();

            _builder = builder;
        }

        public async Task<IEnumerable<Grocery>> GetGroceriesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_builder.Create());
                var records = (await response.Content.ReadAsStringAsync()).Deserialize<IList<Grocery>>();

                return records;
            }
        }

        public async Task<Grocery> GetGroceryAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_builder.Create(id));
                var record = (await response.Content.ReadAsStringAsync()).Deserialize<Grocery>();

                return record;
            }
        }
    }
}