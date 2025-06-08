using System.Globalization;

namespace Entities
{
    public class CsvEventSerializer : IEventConverter
    {
        public BasicEvent<T>? Deserialize<T>(string _event)
        {
            if (string.IsNullOrWhiteSpace(_event))
                return null;

            var parts = _event.Split(',', 2); // timestamp,paramsString
            if (parts.Length != 2)
                throw new FormatException("Invalid CSV format");

            var timeStamp = DateTime.Parse(parts[0], CultureInfo.InvariantCulture);

            var paramStrings = parts[1].Split(';', StringSplitOptions.RemoveEmptyEntries);
            var paramList = paramStrings.Select(s => (T)Convert.ChangeType(s, typeof(T))).ToList();

            return new BasicEvent<T>
            {
                TimeStamp = timeStamp,
                Params = paramList
            };
        }

        public string Serialize<T>(BasicEvent<T> _event)
        {
            if (_event == null)
                throw new ArgumentNullException(nameof(_event));

            string timeStamp = _event.TimeStamp.ToString("o", CultureInfo.InvariantCulture);
            string paramString = string.Join(";", _event.Params.Select(p => p?.ToString()?.Replace(";", "\\;")));

            return $"{timeStamp},{paramString}";
        }
    }
}
