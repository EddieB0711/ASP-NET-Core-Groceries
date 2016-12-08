namespace Web.Infrastructure.Bus
{
    using StructureMap;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;

    public class CommandBus : ICommandBus
    {
        private readonly IContainer _container;

        public CommandBus(IContainer container)
        {
            container.Guard();

            _container = container;
        }

        public void Send<TCommand>(TCommand command)
        {
            var handler = _container.GetInstance<ICommandHandler<TCommand>>();

            handler.Handle(command);
        }
    }
}