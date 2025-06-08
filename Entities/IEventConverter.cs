namespace Entities
{
    public interface IEventConverter
    {
        public string Serialize<T>(BasicEvent<T> _event);
        public BasicEvent<T>? Deserialize<T>(string _event);
    }
}
