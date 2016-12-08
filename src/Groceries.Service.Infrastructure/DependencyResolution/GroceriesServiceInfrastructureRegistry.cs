namespace Groceries.Service.Infrastructure.DependencyResolution
{
    using FluentValidation;
    using FluentValidation.Validators;

    using Groceries.Service.Infrastructure.CommandHandlers.Decorators;
    using Groceries.Service.Infrastructure.Context;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;

    using StructureMap;

    using Web.Contracts.Attributes;
    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Entities;
    using Web.Contracts.Extensions;
    using Web.Contracts.Handlers;
    using Web.Contracts.Repositories;
    using Web.Contracts.Settings;
    using Web.Infrastructure.Bus;
    using Web.Infrastructure.EntityFramework;
    using Web.Infrastructure.Events;
    using Web.Infrastructure.Settings;

    public class GroceriesServiceInfrastructureRegistry : Registry
    {
        public GroceriesServiceInfrastructureRegistry()
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
                        s.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                        s.ConnectImplementationsToTypesClosing(typeof(IMessageHandler<>));
                        s.AddAllTypesOf<IPropertyValidator>();
                    });

            For<IAppSettings>().Use<AppSettings>().Ctor<IConfigurationRoot>().Is(config).Singleton();
            For<IAsyncCommandBus>().Use<AsyncCommandBus>();
            For(typeof(IRepository<>)).Use(typeof(EfRepository<>));
            For(typeof(IReadOnlyRepository<>)).Use(typeof(EfReadOnlyRepository<>));
            For(typeof(IViewRepository<>)).Use(typeof(EfReadOnlyRepository<>));
            For<DbContext>()
                .Use<GroceriesDbContext>()
                .Ctor<DbContextOptions<GroceriesDbContext>>()
                .Is(() => CreateOptions(config));
            For<IDbContext>().Use<DbContextAdapter>();
            Forward<IDbContext, IAsyncDbContext>();
            For<IAsyncUnitOfWork>().Use<AsyncEfUnitOfWork>();
            For<IAsyncEventAggregator>().Use<AsyncEventAggregator>().Singleton();

            For(typeof(IAsyncCommandHandler<>))
                .DecorateAllWith(
                    typeof(ValidationCommandHandlerDecorator<>),
                    filter => filter.ReturnedType.ContainsAttribute<Validate>());
        }

        private DbContextOptions<GroceriesDbContext> CreateOptions(IConfigurationRoot config)
        {
            var builder = new DbContextOptionsBuilder<GroceriesDbContext>();

            builder.UseSqlServer(config["Data:GroceriesConnection:ConnectionString"]);
            builder.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));

            return builder.Options;
        }
    }
}