namespace Entities
{
    public class Response
    {
        public DateTime dateTime { get; set; }
        public List<string> content { get; set; }
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

    }
}
