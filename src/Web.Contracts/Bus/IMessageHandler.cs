namespace Web.Contracts.Bus
{
    using System.Threading.Tasks;

    public interface IMessageHandler<in T>
    {
        Task HandleAsync(T command);
    }
}