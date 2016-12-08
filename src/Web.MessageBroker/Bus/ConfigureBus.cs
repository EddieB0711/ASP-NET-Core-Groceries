namespace Web.MessageBroker.Bus
{
    using System.Threading.Tasks;

    using NServiceBus;
    using NServiceBus.Features;
    using NServiceBus.Persistence.Legacy;

    using StructureMap;

    using Web.Contracts.Bus;
    using Web.Contracts.Extensions;
    using Web.Contracts.Settings;

    public class ConfigureBus : IConfigureBus
    {
        private readonly IAppSettings _appSettings;

        public ConfigureBus(IAppSettings appSettings)
        {
            appSettings.Guard();

            _appSettings = appSettings;
        }

        public async Task ConfigureAsync(IContainer container)
        {
            var endpointName = _appSettings.Get("NServiceBus:Endpoint:Name");
            var config = new EndpointConfiguration(endpointName);
            var conventions = config.Conventions();

            conventions.DefiningCommandsAs(
                t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.EndsWith("Commands"));

            conventions.DefiningMessagesAs(
                t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.EndsWith("Messages"));

            conventions.DefiningEventsAs(t => !string.IsNullOrWhiteSpace(t.Namespace) && t.Namespace.EndsWith("Events"));

            config.UseContainer<StructureMapBuilder>(c => c.ExistingContainer(container));
            config.UseSerialization<JsonSerializer>();
            config.UsePersistence<MsmqPersistence>();
            config.DisableFeature<TimeoutManager>();
            config.EnableInstallers();

            var endpoint = await Endpoint.Create(config);
            var endpointInstance = await endpoint.Start();

            container.Configure(x => x.For<IMessageSession>().Use(endpointInstance));
        }
    }
}