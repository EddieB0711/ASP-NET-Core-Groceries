namespace Web.MessageBroker.Bus
{
    using System;
    using System.Threading.Tasks;

    using NServiceBus;

    using Web.Contracts.Extensions;

    public class MessageContext : Contracts.Bus.IMessageContext
    {
        private readonly IMessageSession _bus;

        public MessageContext(IMessageSession bus)
        {
            bus.Guard();

            _bus = bus;
        }

        public void Publish<T>(T @event)
        {
            Task.WaitAll(_bus.Publish(@event));
        }

        public void Publish<T>(Action<T> messageBuilder)
        {
            Task.WaitAll(_bus.Publish(messageBuilder));
        }

        public void Send<T>(T command)
        {
            Task.WaitAll(_bus.Send(command));
        }

        public Task PublishAsync<T>(T @event)
        {
            return _bus.Publish(@event);
        }

        public Task PublishAsync<T>(Action<T> messageBuilder)
        {
            return _bus.Publish(messageBuilder);
        }

        public Task SendAsync<T>(T command)
        {
            return _bus.Send(command);
        }
    }
}