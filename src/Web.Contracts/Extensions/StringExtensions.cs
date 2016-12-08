namespace Web.Contracts.Extensions
{
    using Newtonsoft.Json;

    public static class StringExtensions
    {
        public static T Deserialize<T>(this string value) => JsonConvert.DeserializeObject<T>(value);

        public static string Serialize<T>(this T value) => JsonConvert.SerializeObject(value);
    }
}