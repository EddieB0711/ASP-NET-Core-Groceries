namespace Web.Infrastructure.Bus
{
    using System.Threading.Tasks;

    using StructureMap;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;

    public class AsyncCommandBus : IAsyncCommandBus
    {
        private readonly IContainer _container;

        public AsyncCommandBus(IContainer container)
        {
            _container = container;
        }

        public async Task SendAsync<TCommand>(TCommand command)
        {
            command.Guard();

            var handler = _container.TryGetInstance<IAsyncCommandHandler<TCommand>>();

            if (handler != null)
            {
                await handler.HandleAsync(command);
            }
        }
    }
}