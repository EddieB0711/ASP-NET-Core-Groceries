namespace Web.Infrastructure.Events
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal abstract class SubscriptionBase<TEvent>
    {
        private readonly WeakReference _reference;

        private readonly MethodInfo _method;

        private readonly Type _delegateType;

        private readonly Type _eventType;

        public SubscriptionBase(Delegate @delegate)
        {
            _reference = new WeakReference(@delegate.Target);
            _eventType = typeof(TEvent);
            _method = @delegate.GetMethodInfo();
            _delegateType = @delegate.GetType();
        }

        public Action<TEvent> Method
        {
            get
            {
                return (Action<TEvent>)Target;
            }
        }

        public Type EventType
        {
            get
            {
                return _eventType;
            }
        }

        public bool IsAlive
        {
            get
            {
                return _reference.IsAlive;
            }
        }

        public Delegate Target
        {
            get
            {
                return TryGetDelegate();
            }
        }

        internal abstract Task FinalizedTask(TEvent eventDataTask);

        protected int DelegateGetHashCode(Delegate obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int result = obj.GetMethodInfo().GetHashCode() ^ obj.GetType().GetHashCode();
            if (obj.Target != null)
            {
                result ^= RuntimeHelpers.GetHashCode(obj);
            }

            return result;
        }

        private Delegate TryGetDelegate()
        {
            if (_method.IsStatic)
            {
                return _method.CreateDelegate(_delegateType, null);
            }

            object target = _reference.Target;

            if (target != null)
            {
                return _method.CreateDelegate(_delegateType, target);
            }

            return null;
        }
    }
}