namespace Client
{
    public interface IServer
    {
        public event EventHandler<LogEventArgs> ServerLogEvent;
        public Task Boot();
        public void Close();
        public void Log(string msg);
    }
}
