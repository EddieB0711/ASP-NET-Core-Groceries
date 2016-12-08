namespace Web.Infrastructure.Events
{
    public interface IAsyncEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : AsyncEventBase, new();
    }
}