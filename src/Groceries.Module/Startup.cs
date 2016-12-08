namespace Groceries.Module
{
    using System;
    using System.Threading.Tasks;

    using Groceries.Module.DependencyResolution;
    using Groceries.Module.Extensions;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Web.Contracts.Bus;
    using Web.Infrastructure.StructureMapEx;

    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var container = IoC.CreateContainer(services);

            Task.WaitAll(container.GetInstance<IConfigureBus>().ConfigureAsync(container));

            return new StructureMapServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(
                c =>
                    {
                        c.MapRoute(
                            "Default",
                            "{controller}/{action}/{id?}",
                            new { controller = "Home", action = "Index" });
                    });

            //var resolver = app.ApplicationServices.GetService<IDependencyResolver>();
            //var config = new HubConfiguration { Resolver = resolver };

            app.UseSignalR();
        }
    }
}