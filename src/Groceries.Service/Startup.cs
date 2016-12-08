namespace Groceries.Service
{
    using System;
    using System.Threading.Tasks;

    using Groceries.Service.Infrastructure.Context;
    using Groceries.Service.Infrastructure.DependencyResolution;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Nancy.Owin;

    using StructureMap;

    using Web.Contracts.Bus;
    using Web.Contracts.Settings;
    using Web.Infrastructure.StructureMapEx;

    public class Startup
    {
        private IContainer _container;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _container = new Container(new GroceriesServiceInfrastructureRegistry());

            var appSettings = _container.GetInstance<IAppSettings>();

            services.AddDbContext<GroceriesDbContext>(
                o =>
                    o.UseSqlServer(
                        appSettings.Get("Data:GroceriesConnection:ConnectionString"),
                        b => b.MigrationsAssembly("Groceries.Module")));

            _container.Populate(services);

            return new StructureMapServiceProvider(_container);
        }

        public void Configure(IApplicationBuilder builder)
        {
            builder.UseOwin(x => x.UseNancy(o => o.Bootstrapper = new NancyBootstrapper(_container)));
            Task.WaitAll(_container.GetInstance<IConfigureBus>().ConfigureAsync(_container));
        }
    }
}