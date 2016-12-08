namespace Groceries.Module.DependencyResolution
{
    using Groceries.Infrastructure.Context;
    using Groceries.Infrastructure.DependencyResolution;
    using Groceries.Infrastructure.Hubs;
    using Groceries.Module.Hubs;

    using Microsoft.AspNet.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using StructureMap;

    using Web.Contracts.Builders;
    using Web.Contracts.Bus;
    using Web.Contracts.Settings;
    using Web.Infrastructure.StructureMapEx;

    public static class IoC
    {
        public static IContainer CreateContainer(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(
                c =>
                    {
                        c.AddRegistry<GroceriesInfrastructureRegistry>();

                        c.Scan(
                            s =>
                                {
                                    s.TheCallingAssembly();
                                    s.WithDefaultConventions();
                                    s.ConnectImplementationsToTypesClosing(typeof(ICommandBuilder<,>));
                                });

                        c.For<IDependencyResolver>().Use<SturctureMapSignalRDependencyResolver>();
                        c.For<GroceriesHub>().Use<GroceriesHub>().Singleton();
                        c.Forward<GroceriesHub, IGroceryHub>();
                    });

            var appSettings = container.GetInstance<IAppSettings>();

            services.AddDbContext<GroceriesDbContext>(
                o =>
                    o.UseSqlServer(
                        appSettings.Get("Data:GroceriesConnection:ConnectionString"),
                        b => b.MigrationsAssembly("Groceries.Module")));

            services.AddMvc();

            container.Populate(services);

            var resolver = container.GetInstance<IDependencyResolver>();

            GlobalHost.DependencyResolver = resolver;
            GlobalHost.DependencyResolver.Register(typeof(GroceriesHub), () => container.GetInstance<GroceriesHub>());

            return container;
        }
    }
}