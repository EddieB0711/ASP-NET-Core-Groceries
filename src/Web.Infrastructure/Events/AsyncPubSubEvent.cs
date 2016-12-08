namespace Web.Infrastructure.Events
{
    using System;
    using System.Threading.Tasks;

    using Web.Contracts.Extensions;

    public abstract class AsyncPubSubEvent<TEvent> : AsyncEventBase
    {
        private readonly IEventHub _hub;

        protected AsyncPubSubEvent()
            : this(new AsyncEventHub())
        {
        }

        protected AsyncPubSubEvent(IEventHub hub)
        {
            hub.Guard();
            _hub = hub;
        }

        public void Subscribe(Action<TEvent> action)
        {
            _hub.Subscribe(action);
        }

        public void SubscribeAsync(Func<TEvent, Task> action)
        {
            _hub.Subscribe(action);
        }

        public void Unsubscribe(Action<TEvent> action)
        {
            _hub.Unsubscribe(action);
        }

        public void UnsubscribeAsync(Func<TEvent, Task> action)
        {
            _hub.Unsubscribe(action);
        }

        public async Task PublishAsync(TEvent eventData)
        {
            eventData.Guard();

            await _hub.PublishAsync(eventData);
        }
    }
}