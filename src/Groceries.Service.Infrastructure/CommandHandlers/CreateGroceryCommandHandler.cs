namespace Groceries.Service.Infrastructure.CommandHandlers
{
    using System.Threading.Tasks;

    using Groceries.Service.Infrastructure.Commands;
    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Attributes;
    using Web.Contracts.Builders;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;
    using Web.Contracts.Repositories;

    [Validate]
    public class CreateGroceryCommandHandler : IAsyncCommandHandler<CreateGroceryCommand>
    {
        private readonly IRepository<Grocery> _repo;

        private readonly IAsyncUnitOfWork _unitOfWork;

        private readonly ICommandBuilder<CreateGroceryCommand, Grocery> _builder;

        public CreateGroceryCommandHandler(
            IRepository<Grocery> repo,
            IAsyncUnitOfWork unitOfWork,
            ICommandBuilder<CreateGroceryCommand, Grocery> builder)
        {
            repo.Guard();
            unitOfWork.Guard();
            builder.Guard();

            _repo = repo;
            _unitOfWork = unitOfWork;
            _builder = builder;
        }

        public async Task HandleAsync(CreateGroceryCommand command)
        {
            _repo.Add(_builder.Create(command));
            await _unitOfWork.SaveAsync();
        }
    }
}