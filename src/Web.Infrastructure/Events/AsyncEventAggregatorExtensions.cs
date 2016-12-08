namespace Web.Infrastructure.Events
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class AsyncEventAggregatorExtensions
    {
        public static Task<TResult> FromException<TResult>(this TaskFactory factory, Exception exception)
        {
            var tcs = new TaskCompletionSource<TResult>(factory.CreationOptions);
            tcs.SetException(exception);
            return tcs.Task;
        }

        public static bool TryGet<TValue>(
            this ConcurrentBag<TValue> table,
            TValue searchForValue,
            Func<TValue, bool> predicate,
            out TValue foundValue) where TValue : class
        {
            try
            {
                foundValue = table.FirstOrDefault(predicate);
                return foundValue != null;
            }
            catch (Exception)
            {
                foundValue = default(TValue);
                return false;
            }
        }

        public static bool TryRemove<TKey, TValue>(
            this ConditionalWeakTable<TKey, TValue> table,
            TKey key,
            out TValue value) where TKey : class where TValue : class
        {
            try
            {
                return table.TryGetValue(key, out value) && table.Remove(key);
            }
            catch (Exception)
            {
                value = default(TValue);
                return false;
            }
        }
    }
}