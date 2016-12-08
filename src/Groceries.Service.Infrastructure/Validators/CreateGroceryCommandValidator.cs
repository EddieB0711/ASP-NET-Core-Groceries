namespace Groceries.Service.Infrastructure.Validators
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Validators;

    using Groceries.Service.Infrastructure.Commands;
    using Groceries.Service.Infrastructure.Validators.PropertyValidators;

    using Web.Contracts.Extensions;

    public class CreateGroceryCommandValidator : AbstractValidator<CreateGroceryCommand>
    {
        private readonly IEnumerable<IPropertyValidator> _validators;

        public CreateGroceryCommandValidator(IEnumerable<IPropertyValidator> validators)
        {
            validators.Guard();

            _validators = validators;

            ValidateItem();
            ValidateQuantity();
            ValidateUniqueItem();
        }

        private void ValidateItem()
        {
            var notEmptyMessage = "Item cannot be empty";
            var notNullMessage = "Item cannot be null";

            RuleFor(c => c.Item)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(notEmptyMessage)
                .NotNull()
                .WithMessage(notNullMessage);
        }

        private void ValidateQuantity()
        {
            var message = "Quantity must be greater than 0";

            RuleFor(c => c.Quantity).GreaterThan(0).WithMessage(message);
        }

        private void ValidateUniqueItem()
        {
            RuleFor(c => c.Item).SetValidator(_validators.OfType<IItemDoesNotExistPropertyValidator>().First());
        }
    }
}