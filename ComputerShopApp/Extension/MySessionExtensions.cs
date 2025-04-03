using System.Text.Json;
using System.Text.Json.Serialization;

namespace ComputerShopApp.Extension
{
    public static class MySessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(value, options);
            session.SetString(key, json);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            T? item = default;
            string? sessionValue = session.GetString(key);
            if (sessionValue != null) item = JsonSerializer.Deserialize<T>(sessionValue, options);
            return item;
        }
    }
}
