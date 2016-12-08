namespace Web.Contracts.Bus
{
    using System.Threading.Tasks;

    public interface IAsyncCommandBus
    {
        Task SendAsync<TCommand>(TCommand command);
    }
}