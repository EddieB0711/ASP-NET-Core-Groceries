namespace Web.Contracts.Handlers
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}