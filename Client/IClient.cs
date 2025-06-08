using Entities;

namespace Server
{
    public interface IClient
    {
        public Task Boot();
        public Task Send(Request request);
        public Task<Response> Get();
        public void Stop();
    }
}
