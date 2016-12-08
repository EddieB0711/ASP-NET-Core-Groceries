namespace Groceries.Contracts.Commands
{
    public class CreateGrocery
    {
        public CreateGrocery(int id, string item, int quantity)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
        }

        private CreateGrocery()
        {
        }

        public int Id { get; private set; }

        public string Item { get; private set; }

        public int Quantity { get; private set; }
    }
}