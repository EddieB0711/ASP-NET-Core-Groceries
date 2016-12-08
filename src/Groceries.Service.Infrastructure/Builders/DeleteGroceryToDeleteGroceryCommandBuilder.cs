namespace Groceries.Service.Infrastructure.Builders
{
    using Groceries.Contracts.Commands;
    using Groceries.Service.Infrastructure.Commands;

    using Web.Contracts.Builders;

    public class DeleteGroceryToDeleteGroceryCommandBuilder : ICommandBuilder<DeleteGrocery, DeleteGroceryCommand>
    {
        public DeleteGroceryCommand Create(DeleteGrocery command, params object[] valueObjects)
        {
            return new DeleteGroceryCommand(command.Id);
        }
    }
}