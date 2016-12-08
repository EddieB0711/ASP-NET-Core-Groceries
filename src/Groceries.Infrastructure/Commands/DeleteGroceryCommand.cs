namespace Groceries.Infrastructure.Commands
{
    public class DeleteGroceryCommand
    {
        public DeleteGroceryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}