using System.Xml.Serialization;

namespace Entities
{
    public class XmlEventSerializer : IEventConverter
    {
        public BasicEvent<T>? Deserialize<T>(string _event)
        {
            if (string.IsNullOrWhiteSpace(_event))
                return null;

            var serializer = new XmlSerializer(typeof(EventWrapper<T>));
            using (var reader = new StringReader(_event))
            {
                var result = serializer.Deserialize(reader) as EventWrapper<T>;
                return result?.Event;
            }
        }

        public string Serialize<T>(BasicEvent<T> _event)
        {
            if (_event == null)
                throw new ArgumentNullException(nameof(_event));

            var wrapper = new EventWrapper<T> { Event = _event };
            var serializer = new XmlSerializer(typeof(EventWrapper<T>));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, wrapper);
                return writer.ToString();
            }
        }
        public class EventWrapper<T>
        {
            public BasicEvent<T> Event { get; set; }
        }
    }
}
