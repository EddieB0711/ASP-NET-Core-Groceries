namespace Groceries.MessageBroker
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;

    using NServiceBus;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class FindAllGroceriesMessage : IHandleMessages<FindAllGroceries>
    {
        private readonly IMessageHandler<FindAllGroceries> _handler;

        public FindAllGroceriesMessage(IMessageHandler<FindAllGroceries> handler)
        {
            handler.Guard();

            _handler = handler;
        }

        public Task Handle(FindAllGroceries message, IMessageHandlerContext context)
        {
            return _handler.HandleAsync(message);
        }
    }
}