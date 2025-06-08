using System.Text.Json;

namespace Entities
{
    public class JsonEventSerializer : IEventConverter
    {
        public BasicEvent<T>? Deserialize<T>(string _event)
        {
            if (string.IsNullOrWhiteSpace(_event))
                return null;
            return JsonSerializer.Deserialize<BasicEvent<T>>(_event);
        }

        public string Serialize<T>(BasicEvent<T> _event)
        {
            return JsonSerializer.Serialize(_event);
        }
    }
}
