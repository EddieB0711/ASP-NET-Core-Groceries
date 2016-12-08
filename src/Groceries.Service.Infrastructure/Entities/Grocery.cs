namespace Groceries.Service.Infrastructure.Entities
{
    using Web.Contracts.Entities;

    public class Grocery : ILiteral<int>
    {
        public Grocery(int id, string item, int quantity)
        {
            Id = id;
            Item = item;
            Quantity = quantity;
        }

        private Grocery()
        {
        }

        public int Id { get; private set; }

        public string Item { get; private set; }

        public int Quantity { get; private set; }

        public void Update(string item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}