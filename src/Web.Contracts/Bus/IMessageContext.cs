namespace Web.Contracts.Bus
{
    using System;
    using System.Threading.Tasks;

    public interface IMessageContext
    {
        void Publish<T>(T @event);

        void Publish<T>(Action<T> messageBuilder);

        void Send<T>(T command);

        Task PublishAsync<T>(T @event);

        Task PublishAsync<T>(Action<T> messageBuilder);

        Task SendAsync<T>(T command);
    }
}