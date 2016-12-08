namespace Web.Infrastructure.Events
{
    using System;
    using System.Threading.Tasks;

    public interface IEventHub
    {
        Task PublishAsync<TEvent>(TEvent eventDataTask);
        
        void Subscribe<TEvent>(Action<TEvent> eventHandlerTaskFactory);
        
        void Subscribe<TEvent>(Func<TEvent, Task> eventHandlerTaskFactory);
        
        void Unsubscribe<TEvent>(Action<TEvent> eventHandlerTaskFactory);
        
        void Unsubscribe<TEvent>(Func<TEvent, Task> eventHandlerTaskFactory);
    }
}