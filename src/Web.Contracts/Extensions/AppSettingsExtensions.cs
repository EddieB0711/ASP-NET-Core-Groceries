namespace Web.Contracts.Extensions
{
    using System;

    using Web.Contracts.Settings;

    public static class AppSettingsExtensions
    {
        public static T GetOrDefault<T>(this IAppSettings settings, string setting, Func<T> defaultValue)
        {
            var value = settings.GetOrDefault(setting, () => null);
            return value == null ? defaultValue() : ConvertIt<T>(value);
        }

        public static T GetOrDefault<T>(this IAppSettings settings, string setting, T defaultValue)
        {
            var value = settings.GetOrDefault(setting, () => null);
            return value == null ? defaultValue : ConvertIt<T>(value);
        }

        private static T ConvertIt<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}