namespace Web.Contracts.Handlers
{
    using System.Threading.Tasks;

    public interface IAsyncCommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}