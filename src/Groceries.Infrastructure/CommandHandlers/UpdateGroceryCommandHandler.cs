namespace Groceries.Infrastructure.CommandHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Commands;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;

    public class UpdateGroceryCommandHandler : IAsyncCommandHandler<UpdateGroceryCommand>
    {
        private readonly IMessageContext _bus;

        private readonly ICommandBuilder<UpdateGroceryCommand, UpdateGrocery> _builder;

        public UpdateGroceryCommandHandler(
            IMessageContext bus,
            ICommandBuilder<UpdateGroceryCommand, UpdateGrocery> builder)
        {
            bus.Guard();
            builder.Guard();

            _bus = bus;
            _builder = builder;
        }

        public Task HandleAsync(UpdateGroceryCommand command)
        {
            return _bus.SendAsync(_builder.Create(command));
        }
    }
}