namespace Groceries.Service.Infrastructure.Commands
{
    public class CreateGroceryCommand
    {
        public CreateGroceryCommand(int id, string item, int quantity)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
        }

        public int Id { get; private set; }

        public string Item { get; private set; }

        public int Quantity { get; private set; }
    }
}