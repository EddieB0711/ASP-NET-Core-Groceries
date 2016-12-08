namespace Groceries.MessageBroker
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Events;

    using NServiceBus;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class AllGroceriesFoundMessage : IHandleMessages<AllGroceriesFound>
    {
        private readonly IMessageHandler<AllGroceriesFound> _handler;

        public AllGroceriesFoundMessage(IMessageHandler<AllGroceriesFound> handler)
        {
            handler.Guard();

            _handler = handler;
        }

        public Task Handle(AllGroceriesFound message, IMessageHandlerContext context)
        {
            return _handler.HandleAsync(message);
        }
    }
}