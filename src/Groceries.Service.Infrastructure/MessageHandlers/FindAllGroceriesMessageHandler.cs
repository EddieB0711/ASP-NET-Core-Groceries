namespace Groceries.Service.Infrastructure.MessageHandlers
{
    using System.Threading.Tasks;

    using Groceries.Contracts.Commands;
    using Groceries.Contracts.Events;
    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Repositories;

    public class FindAllGroceriesMessageHandler : IMessageHandler<FindAllGroceries>
    {
        private readonly IReadOnlyRepository<Grocery> _readOnlyRepository;

        private readonly IMessageContext _bus;

        private readonly ICommandBuilder<FindAllGroceries, AllGroceriesFound> _builder;

        public FindAllGroceriesMessageHandler(
            IReadOnlyRepository<Grocery> readOnlyRepository,
            IMessageContext bus,
            ICommandBuilder<FindAllGroceries, AllGroceriesFound> builder)
        {
            readOnlyRepository.Guard();
            bus.Guard();
            builder.Guard();

            _readOnlyRepository = readOnlyRepository;
            _bus = bus;
            _builder = builder;
        }

        public Task HandleAsync(FindAllGroceries command)
        {
            var records = _readOnlyRepository.Find();

            return _bus.PublishAsync(_builder.Create(command, records));
        }
    }
}