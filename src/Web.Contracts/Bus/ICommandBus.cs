namespace Web.Contracts.Bus
{
    public interface ICommandBus
    {
        void Send<TCommand>(TCommand command);
    }
}