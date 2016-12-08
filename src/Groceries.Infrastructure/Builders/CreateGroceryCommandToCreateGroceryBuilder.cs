namespace Groceries.Infrastructure.Builders
{
    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Commands;

    using Web.Contracts.Builders;

    public class CreateGroceryCommandToCreateGroceryBuilder : ICommandBuilder<CreateGroceryCommand, CreateGrocery>
    {
        public CreateGrocery Create(CreateGroceryCommand command, params object[] valueObjects)
        {
            return new CreateGrocery(command.Id, command.Item, command.Quantity);
        }
    }
}