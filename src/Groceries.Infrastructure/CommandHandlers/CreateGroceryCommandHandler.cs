namespace Groceries.Infrastructure.CommandHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Commands;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;

    public class CreateGroceryCommandHandler : IAsyncCommandHandler<CreateGroceryCommand>
    {
        private readonly IMessageContext _bus;

        private readonly ICommandBuilder<CreateGroceryCommand, CreateGrocery> _builder;

        public CreateGroceryCommandHandler(
            IMessageContext bus,
            ICommandBuilder<CreateGroceryCommand, CreateGrocery> builder)
        {
            bus.Guard();
            builder.Guard();

            _bus = bus;
            _builder = builder;
        }

        public async Task HandleAsync(CreateGroceryCommand command)
        {
            await _bus.SendAsync(_builder.Create(command));
        }
    }
}