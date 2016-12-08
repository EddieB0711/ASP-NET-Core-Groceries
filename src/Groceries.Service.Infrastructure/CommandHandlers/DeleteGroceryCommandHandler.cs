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
    public class DeleteGroceryCommandHandler : IAsyncCommandHandler<DeleteGroceryCommand>
    {
        private readonly IViewRepository<Grocery> _viewRepository;

        private readonly IRepository<Grocery> _repository;

        private readonly IAsyncUnitOfWork _unitOfWork;

        public DeleteGroceryCommandHandler(
            IViewRepository<Grocery> viewRepository,
            IRepository<Grocery> repository,
            IAsyncUnitOfWork unitOfWork)
        {
            viewRepository.Guard();
            repository.Guard();
            unitOfWork.Guard();

            _viewRepository = viewRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteGroceryCommand command)
        {
            var record = _viewRepository.Get(x => x.Id == command.Id);

            _repository.Delete(record);

            await _unitOfWork.SaveAsync();
        }
    }
}