namespace Groceries.Module.Builders
{
    using Groceries.Infrastructure.Commands;
    using Groceries.Module.Models;

    using Web.Contracts.Builders;

    public class DeleteGroceryCommandBuilder : ICommandBuilder<GroceryViewModel, DeleteGroceryCommand>
    {
        public DeleteGroceryCommand Create(GroceryViewModel command, params object[] valueObjects)
        {
            return new DeleteGroceryCommand(command.Id);
        }
    }
}