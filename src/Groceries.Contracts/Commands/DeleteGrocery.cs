namespace Groceries.Contracts.Commands
{
    public class DeleteGrocery
    {
        public DeleteGrocery(int id)
        {
            Id = id;
        }

        private DeleteGrocery()
        {
        }

        public int Id { get; private set; }
    }
}