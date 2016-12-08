namespace Web.Contracts.Settings
{
    using System;

    public interface IAppSettings
    {
        string Get(string setting);

        string GetOrDefault(string setting, Func<string> defaultValue);

        string GetOrDefault(string setting, string defaultValue);
    }
}