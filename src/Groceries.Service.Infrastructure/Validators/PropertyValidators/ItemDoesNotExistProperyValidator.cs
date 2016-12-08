namespace Groceries.Service.Infrastructure.Validators.PropertyValidators
{
    using FluentValidation.Validators;

    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Extensions;
    using Web.Contracts.Repositories;

    public class ItemDoesNotExistProperyValidator : PropertyValidator, IItemDoesNotExistPropertyValidator
    {
        private readonly IViewRepository<Grocery> _viewRepository;

        public ItemDoesNotExistProperyValidator(IViewRepository<Grocery> viewRepository)
            : base("Item already exists: {Command}")
        {
            viewRepository.Guard();

            _viewRepository = viewRepository;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return true;
            }

            var item = context.PropertyValue.ToString();
            var record = _viewRepository.Get(x => x.Item == item);

            return record == null;
        }
    }
}