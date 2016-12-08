namespace Groceries.Infrastructure.Builders
{
    using System;

    using Web.Contracts.Extensions;
    using Web.Contracts.Settings;

    public class UriServiceBuilder
    {
        private readonly IAppSettings _appSettings;

        public UriServiceBuilder(IAppSettings appSettings)
        {
            appSettings.Guard();

            _appSettings = appSettings;
        }

        public Uri Create(params object[] addons)
        {
            var scheme = _appSettings.GetOrDefault("GroceriesManager:Api:Scheme", "http");
            var host = _appSettings.GetOrDefault("GroceriesManager:Api:Host", "localhost");
            var port = _appSettings.GetOrDefault("GroceriesManager:Api:Port", 5000);
            var path = _appSettings.GetOrDefault("GroceriesManager:Api:Path", "/") + string.Join("/", addons);

            return new UriBuilder(scheme, host, port, path).Uri;
        }
    }
}