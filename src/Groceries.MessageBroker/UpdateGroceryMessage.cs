namespace Groceries.MessageBroker
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;

    using NServiceBus;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class UpdateGroceryMessage : IHandleMessages<UpdateGrocery>
    {
        private readonly IMessageHandler<UpdateGrocery> _handler;

        public UpdateGroceryMessage(IMessageHandler<UpdateGrocery> handler)
        {
            handler.Guard();

            _handler = handler;
        }

        public Task Handle(UpdateGrocery message, IMessageHandlerContext context)
        {
            return _handler.HandleAsync(message);
        }
    }
}