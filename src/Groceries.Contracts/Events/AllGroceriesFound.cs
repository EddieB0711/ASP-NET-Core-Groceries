namespace Groceries.Contracts.Events
{
    public class AllGroceriesFound
    {
        public AllGroceriesFound(string groceries)
        {
            Groceries = groceries;
        }

        private AllGroceriesFound()
        {
        }

        public string Groceries { get; private set; }
    }
}