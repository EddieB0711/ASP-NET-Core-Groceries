namespace Groceries.Service.Infrastructure.MessageHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Service.Infrastructure.Commands;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;

    public class UpdateGroceryMessageHandler : IMessageHandler<UpdateGrocery>
    {
        private readonly IAsyncCommandBus _bus;

        private readonly ICommandBuilder<UpdateGrocery, UpdateGroceryCommand> _builder;

        public UpdateGroceryMessageHandler(
            IAsyncCommandBus bus,
            ICommandBuilder<UpdateGrocery, UpdateGroceryCommand> builder)
        {
            bus.Guard();
            builder.Guard();

            _bus = bus;
            _builder = builder;
        }

        public Task HandleAsync(UpdateGrocery command)
        {
            return _bus.SendAsync(_builder.Create(command));
        }
    }
}