namespace Groceries.Infrastructure.CommandHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Commands;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;

    public class DeleteGroceryCommandHandler : IAsyncCommandHandler<DeleteGroceryCommand>
    {
        private readonly IMessageContext _bus;

        private readonly ICommandBuilder<DeleteGroceryCommand, DeleteGrocery> _builder;

        public DeleteGroceryCommandHandler(
            IMessageContext bus,
            ICommandBuilder<DeleteGroceryCommand, DeleteGrocery> builder)
        {
            bus.Guard();
            builder.Guard();

            _bus = bus;
            _builder = builder;
        }

        public Task HandleAsync(DeleteGroceryCommand command)
        {
            return _bus.SendAsync(_builder.Create(command));
        }
    }
}