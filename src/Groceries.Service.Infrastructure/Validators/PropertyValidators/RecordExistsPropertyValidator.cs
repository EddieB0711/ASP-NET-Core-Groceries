namespace Groceries.Service.Infrastructure.Validators.PropertyValidators
{
    using FluentValidation.Validators;

    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Extensions;
    using Web.Contracts.Repositories;

    public class RecordExistsPropertyValidator : PropertyValidator, IRecordExistsPropertyValidator
    {
        private readonly IViewRepository<Grocery> _viewRepository;

        public RecordExistsPropertyValidator(IViewRepository<Grocery> viewRepository)
            : base("Item does not exist: {Command}")
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

            var id = (int)context.PropertyValue;
            var record = _viewRepository.Get(x => x.Id == id);

            return record != null;
        }
    }
}