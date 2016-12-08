namespace Groceries.Service.Infrastructure.MessageHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Service.Infrastructure.Commands;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class CreateGroceryMessageHandler : IMessageHandler<CreateGrocery>
    {
        private readonly IAsyncCommandBus _bus;

        private readonly ICommandBuilder<CreateGrocery, CreateGroceryCommand> _builder;

        public CreateGroceryMessageHandler(
            IAsyncCommandBus bus,
            ICommandBuilder<CreateGrocery, CreateGroceryCommand> builder)
        {
            bus.Guard();
            builder.Guard();

            _bus = bus;
            _builder = builder;
        }

        public Task HandleAsync(CreateGrocery command)
        {
            return _bus.SendAsync(_builder.Create(command));
        }
    }
}