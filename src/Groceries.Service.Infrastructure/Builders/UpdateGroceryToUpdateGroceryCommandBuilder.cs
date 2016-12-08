namespace Groceries.Service.Infrastructure.Builders
{
    using Groceries.Contracts.Commands;
    using Groceries.Service.Infrastructure.Commands;

    using Web.Contracts.Builders;

    public class UpdateGroceryToUpdateGroceryCommandBuilder : ICommandBuilder<UpdateGrocery, UpdateGroceryCommand>
    {
        public UpdateGroceryCommand Create(UpdateGrocery command, params object[] valueObjects)
        {
            return new UpdateGroceryCommand(command.Id, command.Item, command.Quantity);
        }
    }
}