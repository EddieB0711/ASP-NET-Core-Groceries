namespace Web.MessageBroker.DependencyResolution
{
    using StructureMap;

    using Web.Contracts.Bus;
    using Web.MessageBroker.Bus;

    public class MessageBrokerRegistry : Registry
    {
        public MessageBrokerRegistry()
        {
            For<IConfigureBus>().Use<ConfigureBus>();
            For<IMessageContext>().Use<MessageContext>();
        }
    }
}