namespace Groceries.Contracts.Commands
{
    public class UpdateGrocery
    {
        public UpdateGrocery(int id, string item, int quantity)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
        }

        private UpdateGrocery()
        {
        }

        public int Id { get; private set; }

        public string Item { get; private set; }

        public int Quantity { get; private set; }
    }
}