namespace Groceries.Service.Infrastructure.CommandHandlers.Decorators
{
    using System.Threading.Tasks;

    using FluentValidation;

    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;

    public class ValidationCommandHandlerDecorator<T> : IAsyncCommandHandler<T>
    {
        private readonly IAsyncCommandHandler<T> _handler;

        private readonly IValidator<T> _validator;

        public ValidationCommandHandlerDecorator(IAsyncCommandHandler<T> handler, IValidator<T> validator)
        {
            handler.Guard();
            validator.Guard();

            _handler = handler;
            _validator = validator;
        }

        public async Task HandleAsync(T command)
        {
            var result = _validator.Validate(command);

            if (result.IsValid)
            {
                await _handler.HandleAsync(command);
            }
        }
    }
}