namespace Web.Infrastructure.Settings
{
    using System;

    using Microsoft.Extensions.Configuration;

    using Web.Contracts.Extensions;
    using Web.Contracts.Settings;

    public class AppSettings : IAppSettings
    {
        private readonly IConfigurationRoot _config;

        public AppSettings(IConfigurationRoot config)
        {
            config.Guard();

            _config = config;
        }

        public string Get(string setting) => _config[setting];

        public string GetOrDefault(string setting, Func<string> defaultValue)
        {
            var value = Get(setting);
            return value ?? defaultValue();
        }

        public string GetOrDefault(string setting, string defaultValue)
        {
            var value = Get(setting);
            return value ?? defaultValue;
        }
    }
}