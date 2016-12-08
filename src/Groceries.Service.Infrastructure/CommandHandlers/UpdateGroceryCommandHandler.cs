namespace Groceries.Service.Infrastructure.CommandHandlers
{
    using System.Threading.Tasks;

    using Groceries.Service.Infrastructure.Commands;
    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Attributes;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;
    using Web.Contracts.Repositories;

    [Validate]
    public class UpdateGroceryCommandHandler : IAsyncCommandHandler<UpdateGroceryCommand>
    {
        private readonly IViewRepository<Grocery> _viewRepository;

        private readonly IAsyncUnitOfWork _unitOfWork;

        public UpdateGroceryCommandHandler(IViewRepository<Grocery> viewRepository, IAsyncUnitOfWork unitOfWork)
        {
            viewRepository.Guard();
            unitOfWork.Guard();

            _viewRepository = viewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(UpdateGroceryCommand command)
        {
            var record = _viewRepository.Get(x => x.Id == command.Id);

            record.Update(command.Item, command.Quantity);

            await _unitOfWork.SaveAsync();
        }
    }
}