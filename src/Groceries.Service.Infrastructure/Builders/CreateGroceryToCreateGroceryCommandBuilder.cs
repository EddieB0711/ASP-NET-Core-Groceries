namespace Groceries.Service.Infrastructure.Builders
{
    using Groceries.Contracts.Commands;
    using Groceries.Service.Infrastructure.Commands;

    using Web.Contracts.Builders;

    public class CreateGroceryToCreateGroceryCommandBuilder : ICommandBuilder<CreateGrocery, CreateGroceryCommand>
    {
        public CreateGroceryCommand Create(CreateGrocery command, params object[] valueObjects)
        {
            return new CreateGroceryCommand(command.Id, command.Item, command.Quantity);
        }
    }
}