namespace Web.Infrastructure.Events
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;

    using Web.Contracts.Extensions;

    public sealed class AsyncEventHub : IEventHub
    {
        private readonly ConcurrentDictionary<Type, ConcurrentBag<object>> _hub;

        private readonly Task _completedTask;

        public AsyncEventHub()
        {
            _hub = new ConcurrentDictionary<Type, ConcurrentBag<object>>();
            _completedTask = Task.FromResult(0);
        }

        public async Task PublishAsync<TEvent>(TEvent eventDataTask)
        {
            ConcurrentBag<object> subscribers;
            if (!_hub.TryGetValue(typeof(TEvent), out subscribers) || subscribers == null || !subscribers.Any())
            {
                return;
            }

            foreach (var subscriber in CreateConcurrentTaskList(eventDataTask, subscribers))
            {
                await subscriber;
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            action.Guard();

            var subscription = new SyncSubscription<TEvent>(action);
            InternalSubscribe(subscription);
        }

        public void Subscribe<TEvent>(Func<TEvent, Task> action)
        {
            action.Guard();

            var subscription = new AsyncSubscription<TEvent>(action);
            InternalSubscribe(subscription);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> action)
        {
            var subscription = new SyncSubscription<TEvent>(action);
            InternalUnsubscribe(subscription);
        }

        public void Unsubscribe<TEvent>(Func<TEvent, Task> action)
        {
            var subscription = new AsyncSubscription<TEvent>(action);
            InternalUnsubscribe(subscription);
        }

        private void InternalSubscribe<TEvent>(SubscriptionBase<TEvent> subscription)
        {
            var subscribers = _hub.GetOrAdd(subscription.EventType, type => new ConcurrentBag<object>());
            object subscriber;

            if (!subscribers.TryGet(subscription, subscription.Equals, out subscriber))
            {
                subscribers.Add(subscription);
            }
        }

        private void InternalUnsubscribe<TEvent>(SubscriptionBase<TEvent> subscription)
        {
            var eventType = typeof(TEvent);

            ConcurrentBag<object> subscribers;
            if (!_hub.TryGetValue(eventType, out subscribers) || subscribers == null)
            {
                return;
            }

            var listset = new ConcurrentBag<object>(subscribers.Where(x => !x.Equals(subscription)));

            _hub[eventType] = listset;
        }

        private ConcurrentBag<Task> CreateConcurrentTaskList<TEvent>(
            TEvent eventDataTask,
            ConcurrentBag<object> subscribers)
        {
            return
                new ConcurrentBag<Task>(
                    new ConcurrentBag<SubscriptionBase<TEvent>>(subscribers.Cast<SubscriptionBase<TEvent>>()).Select(
                        p => ConcurentTask(eventDataTask, p)));
        }

        private Task ConcurentTask<TEvent>(TEvent eventDataTask, SubscriptionBase<TEvent> p)
        {
            if (p.IsAlive)
            {
                return p.FinalizedTask(eventDataTask);
            }

            return _completedTask;
        }
    }
}