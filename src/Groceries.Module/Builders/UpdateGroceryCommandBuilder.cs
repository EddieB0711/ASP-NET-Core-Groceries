namespace Groceries.Module.Builders
{
    using Groceries.Infrastructure.Commands;
    using Groceries.Module.Models;

    using Web.Contracts.Builders;

    public class UpdateGroceryCommandBuilder : ICommandBuilder<GroceryViewModel, UpdateGroceryCommand>
    {
        public UpdateGroceryCommand Create(GroceryViewModel command, params object[] valueObjects)
        {
            return new UpdateGroceryCommand(command.Id, command.Item, command.Quantity);
        }
    }
}