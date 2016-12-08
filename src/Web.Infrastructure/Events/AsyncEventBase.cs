namespace Web.Infrastructure.Events
{
    using System.Threading;

    using Web.Contracts.Extensions;

    public abstract class AsyncEventBase
    {
        protected SynchronizationContext Context { get; private set; }

        protected internal void ApplyContext(SynchronizationContext syncContext)
        {
            syncContext.Guard();
            Context = syncContext;
        }
    }
}