namespace Groceries.Module.Builders
{
    using Groceries.Infrastructure.Commands;
    using Groceries.Module.Models;

    using Web.Contracts.Builders;

    public class CreateGroceryCommandBuilder : ICommandBuilder<GroceryViewModel, CreateGroceryCommand>
    {
        public CreateGroceryCommand Create(GroceryViewModel command, params object[] valueObjects)
        {
            return new CreateGroceryCommand(command.Id, command.Item, command.Quantity);
        }
    }
}