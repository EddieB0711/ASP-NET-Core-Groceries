namespace Groceries.Service.Infrastructure.Builders
{
    using Groceries.Service.Infrastructure.Commands;
    using Groceries.Service.Infrastructure.Entities;

    using Web.Contracts.Builders;

    public class CreateGroceryCommandToEntityBuilder : ICommandBuilder<CreateGroceryCommand, Grocery>
    {
        public Grocery Create(CreateGroceryCommand command, params object[] valueObjects)
        {
            return new Grocery(command.Id, command.Item, command.Quantity);
        }
    }
}