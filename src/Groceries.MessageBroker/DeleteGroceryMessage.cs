namespace Groceries.MessageBroker
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;

    using NServiceBus;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class DeleteGroceryMessage : IHandleMessages<DeleteGrocery>
    {
        private readonly IMessageHandler<DeleteGrocery> _handler;

        public DeleteGroceryMessage(IMessageHandler<DeleteGrocery> handler)
        {
            handler.Guard();

            _handler = handler;
        }

        public Task Handle(DeleteGrocery message, IMessageHandlerContext context)
        {
            return _handler.HandleAsync(message);
        }
    }
}