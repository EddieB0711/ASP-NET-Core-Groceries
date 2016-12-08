namespace Web.Infrastructure.Events
{
    using System;
    using System.Threading.Tasks;

    using Nito.AsyncEx;

    internal class SyncSubscription<TEvent> : SubscriptionBase<TEvent>
    {
        public SyncSubscription(Delegate @delegate)
            : base(@delegate)
        {
        }

        public bool Equals(SyncSubscription<TEvent> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return Method == other.Method;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SyncSubscription<TEvent>);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Method != null ? DelegateGetHashCode(Method) : 0;
            }
        }

        internal override Task FinalizedTask(TEvent eventDataTask)
        {
            Method(eventDataTask);
            return TaskConstants.Completed;
        }
    }
}