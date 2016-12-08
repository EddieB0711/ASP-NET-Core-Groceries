namespace Groceries.Module.Builders
{
    using Groceries.Infrastructure.Entities;
    using Groceries.Module.Models;

    using Web.Contracts.Builders;

    public class GroceryToGroceryViewModelBuilder : ICommandBuilder<Grocery, GroceryViewModel>
    {
        public GroceryViewModel Create(Grocery command, params object[] valueObjects)
        {
            return new GroceryViewModel { Id = command.Id, Item = command.Item, Quantity = command.Quantity };
        }
    }
}