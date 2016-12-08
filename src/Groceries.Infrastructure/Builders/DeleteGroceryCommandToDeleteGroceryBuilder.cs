namespace Groceries.Infrastructure.Builders
{
    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Commands;

    using Web.Contracts.Builders;

    public class DeleteGroceryCommandToDeleteGroceryBuilder : ICommandBuilder<DeleteGroceryCommand, DeleteGrocery>
    {
        public DeleteGrocery Create(DeleteGroceryCommand command, params object[] valueObjects)
        {
            return new DeleteGrocery(command.Id);
        }
    }
}