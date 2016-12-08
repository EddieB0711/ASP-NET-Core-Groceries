namespace Web.Infrastructure.Events
{
    using System;
    using System.Threading.Tasks;

    internal class AsyncSubscription<TEvent> : SubscriptionBase<TEvent>
    {
        public AsyncSubscription(Delegate @delegate)
            : base(@delegate)
        {
        }

        public new Func<TEvent, Task> Method
        {
            get
            {
                return (Func<TEvent, Task>)Target;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (!(obj is AsyncSubscription<TEvent>))
            {
                return false;
            }

            return Equals((AsyncSubscription<TEvent>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Method != null ? DelegateGetHashCode(Method) : 0;
            }
        }

        internal override async Task FinalizedTask(TEvent eventDataTask)
        {
            await Method(eventDataTask);
        }

        private bool Equals(AsyncSubscription<TEvent> other)
        {
            return Method.Equals(other.Method);
        }
    }
}