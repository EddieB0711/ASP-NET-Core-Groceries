namespace Groceries.Infrastructure.MessageHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Groceries.Contracts.Events;
    using Groceries.Infrastructure.Entities;
    using Groceries.Infrastructure.Hubs;

    using Nito.AsyncEx;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class AllGroceriesFoundMessageHandler : IMessageHandler<AllGroceriesFound>
    {
        private readonly IGroceryHub _hub;

        public AllGroceriesFoundMessageHandler(IGroceryHub hub)
        {
            hub.Guard();

            _hub = hub;
        }

        public Task HandleAsync(AllGroceriesFound command)
        {
            var records = command.Groceries.Deserialize<IList<Grocery>>();

            _hub.AddItems(records);

            return TaskConstants.Completed;
        }
    }
}