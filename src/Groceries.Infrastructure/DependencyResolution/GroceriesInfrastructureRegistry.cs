namespace Groceries.Infrastructure.DependencyResolution
{
    using Groceries.Infrastructure.Builders;
    using Groceries.Infrastructure.Context;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using StructureMap;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Entities;
    using Web.Contracts.Handlers;
    using Web.Contracts.Repositories;
    using Web.Contracts.Settings;
    using Web.Infrastructure.Bus;
    using Web.Infrastructure.EntityFramework;
    using Web.Infrastructure.Settings;

    public class GroceriesInfrastructureRegistry : Registry
    {
        public GroceriesInfrastructureRegistry()
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile(
                @"appsettings.json",
                optional: true,
                reloadOnChange: true);

            var config = configBuilder.Build();

            Scan(
                s =>
                    {
                        s.AssembliesFromApplicationBaseDirectory(a => a.FullName.Contains("Web.MessageBroker"));
                        s.LookForRegistries();
                        s.TheCallingAssembly();
                        s.WithDefaultConventions();
                        s.ConnectImplementationsToTypesClosing(typeof(IAsyncCommandHandler<>));
                        s.ConnectImplementationsToTypesClosing(typeof(ICommandBuilder<,>));
                        s.ConnectImplementationsToTypesClosing(typeof(IMessageHandler<>));
                    });

            For<IAppSettings>().Use<AppSettings>().Ctor<IConfigurationRoot>().Is(config).Singleton();
            For<IAsyncCommandBus>().Use<AsyncCommandBus>();
            For(typeof(IReadOnlyRepository<>)).Use(typeof(EfReadOnlyRepository<>));
            For<DbContext>()
                .Use<GroceriesDbContext>()
                .Ctor<DbContextOptions<GroceriesDbContext>>()
                .Is(() => CreateOptions(config));
            For<IDbContext>().Use<DbContextAdapter>();
            ForConcreteType<UriServiceBuilder>();
        }

        private DbContextOptions<GroceriesDbContext> CreateOptions(IConfigurationRoot config)
        {
            var builder = new DbContextOptionsBuilder<GroceriesDbContext>();

            builder.UseSqlServer(config["Data:GroceriesConnection:ConnectionString"]);

            return builder.Options;
        }
    }
}