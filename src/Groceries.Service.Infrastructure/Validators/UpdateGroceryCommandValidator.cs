namespace Groceries.Service.Infrastructure.Validators
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentValidation;
    using FluentValidation.Validators;

    using Groceries.Service.Infrastructure.Commands;
    using Groceries.Service.Infrastructure.Validators.PropertyValidators;

    using Web.Contracts.Extensions;

    public class UpdateGroceryCommandValidator : AbstractValidator<UpdateGroceryCommand>
    {
        public UpdateGroceryCommandValidator(IEnumerable<IPropertyValidator> validators)
        {
            validators.Guard();

            RuleFor(c => c.Id).SetValidator(validators.OfType<IRecordExistsPropertyValidator>().First());
        }
    }
}