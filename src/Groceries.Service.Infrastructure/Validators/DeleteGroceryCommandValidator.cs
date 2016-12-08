namespace Groceries.Service.Infrastructure.Validators
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Validators;

    using Groceries.Service.Infrastructure.Commands;
    using Groceries.Service.Infrastructure.Validators.PropertyValidators;

    public class DeleteGroceryCommandValidator : AbstractValidator<DeleteGroceryCommand>
    {
        public DeleteGroceryCommandValidator(IEnumerable<IPropertyValidator> validators)
        {
            RuleFor(c => c.Id).SetValidator(validators.OfType<IRecordExistsPropertyValidator>().First());
        }
    }
}