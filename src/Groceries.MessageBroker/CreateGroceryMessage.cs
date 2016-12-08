namespace Groceries.MessageBroker
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;

    using NServiceBus;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class CreateGroceryMessage : IHandleMessages<CreateGrocery>
    {
        private readonly IMessageHandler<CreateGrocery> _handler;

        public CreateGroceryMessage(IMessageHandler<CreateGrocery> handler)
        {
            handler.Guard();

            _handler = handler;
        }

        public Task Handle(CreateGrocery message, IMessageHandlerContext context)
        {
            return _handler.HandleAsync(message);
        }
    }
}