namespace Entities
{
    public class BasicEvent<T>
    {
        public DateTime TimeStamp { get; set; }
        public List<T> Params { get; set; } = new List<T>();
        public void Append(T obj)
        {
            Params.Add(obj);
        }
    }
}
