namespace Web.Infrastructure.Events
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    public class AsyncEventAggregator : IAsyncEventAggregator
    {
        private readonly ConcurrentDictionary<Type, AsyncEventBase> _events =
            new ConcurrentDictionary<Type, AsyncEventBase>();

        private readonly SynchronizationContext _syncContext = SynchronizationContext.Current;

        public TEventType GetEvent<TEventType>() where TEventType : AsyncEventBase, new()
        {
            lock (_events)
            {
                AsyncEventBase existingEvent;

                if (_events.TryGetValue(typeof(TEventType), out existingEvent))
                {
                    return (TEventType)existingEvent;
                }

                TEventType newEvent = new TEventType();
                newEvent.ApplyContext(_syncContext);
                _events[typeof(TEventType)] = newEvent;
                return newEvent;
            }
        }
    }
}