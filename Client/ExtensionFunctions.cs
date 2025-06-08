using Entities;
using Newtonsoft.Json;

namespace Client
{
    public static class ExtensionFunctions
    {
        public static string ToString(this Response response, string? format = null)
        {
            switch (format)
            {
                case "names":
                    return string.Join(Environment.NewLine, response.content);
                default:
                    return JsonConvert.SerializeObject(response, Formatting.Indented);
            }
        }
    }
}
