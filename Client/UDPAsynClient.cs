using Entities;
using Server;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Client
{
    public class UDPAsyncClient : IClient
    {
        private UdpClient client;
        private IPEndPoint serverEndPoint;

        public event EventHandler<EventArgs> ResponseGet;
        public UDPAsyncClient() => client = new UdpClient();
        public Task Boot()
        {
            client = new UdpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            return Task.CompletedTask;
        }

        public async Task Send(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.text))
                throw new ArgumentException("Request text is empty", nameof(request.text));

            string json = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(json);
            try
            {
                await client.SendAsync(data, data.Length, serverEndPoint);
            }
            catch (Exception)
            {
                client.Dispose();
                throw new ConnectionLostException();
            }
        }

        public async Task<Response?> Get()
        {
            try
            {
                UdpReceiveResult result = await client.ReceiveAsync();
                string responseString = Encoding.UTF8.GetString(result.Buffer);
                var response = JsonSerializer.Deserialize<Response>(responseString);
                ResponseGet?.Invoke(this, EventArgs.Empty);
                return response;
            }
            catch
            {
                throw new ConnectionLostException();
            }
        }

        public void Stop()
        {
            client?.Dispose();
        }
    }
}
