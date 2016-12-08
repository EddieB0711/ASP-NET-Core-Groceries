namespace Groceries.Module.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Owin.Builder;

    using Owin;

    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder builder, Action<IAppBuilder> config)
        {
            builder.UseOwin(
                pipeline =>
                    {
                        pipeline(
                            next =>
                                {
                                    var appBuilder = new AppBuilder();
                                    appBuilder.Properties["builder.DefaultApp"] = next;
                                    config(appBuilder);

                                    return appBuilder.Build<Func<IDictionary<string, object>, Task>>();
                                });
                    });

            return builder;
        }

        public static void UseSignalR(this IApplicationBuilder builder)
        {
            builder.UseAppBuilder(b => b.MapSignalR());
        }
    }
}