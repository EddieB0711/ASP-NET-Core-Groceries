namespace Groceries.Module.Hubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Entities;
    using Groceries.Infrastructure.Hubs;
    using Groceries.Module.Models;

    using Microsoft.AspNet.SignalR;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class GroceriesHub : Hub, IGroceryHub
    {
        private readonly ICommandBuilder<Grocery, GroceryViewModel> _builder;

        private readonly IMessageContext _bus;

        public GroceriesHub(ICommandBuilder<Grocery, GroceryViewModel> builder, IMessageContext bus)
        {
            builder.Guard();
            bus.Guard();

            _builder = builder;
            _bus = bus;
        }

        public Task Polling()
        {
            return _bus.SendAsync(new FindAllGroceries());
        }

        public void AddItems(IList<Grocery> records)
        {
            var models = records.Select(x => _builder.Create(x));

            Clients.All.newItem(models);
        }
    }
}