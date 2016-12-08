namespace Groceries.Infrastructure.Builders
{
    using Groceries.Contracts.Commands;
    using Groceries.Infrastructure.Commands;

    using Web.Contracts.Builders;

    public class UpdateGroceryCommandToUpdateGroceryBuilder : ICommandBuilder<UpdateGroceryCommand, UpdateGrocery>
    {
        public UpdateGrocery Create(UpdateGroceryCommand command, params object[] valueObjects)
        {
            return new UpdateGrocery(command.Id, command.Item, command.Quantity);
        }
    }
}