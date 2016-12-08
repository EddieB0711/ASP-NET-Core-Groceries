namespace Groceries.Service.Infrastructure.MessageHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Service.Infrastructure.Commands;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class DeleteGroceryMessageHandler : IMessageHandler<DeleteGrocery>
    {
        private readonly IAsyncCommandBus _bus;

        private readonly ICommandBuilder<DeleteGrocery, DeleteGroceryCommand> _builder;

        public DeleteGroceryMessageHandler(
            IAsyncCommandBus bus,
            ICommandBuilder<DeleteGrocery, DeleteGroceryCommand> builder)
        {
            bus.Guard();
            builder.Guard();

            _bus = bus;
            _builder = builder;
        }

        public Task HandleAsync(DeleteGrocery command)
        {
            return _bus.SendAsync(_builder.Create(command));
        }
    }
}