using Entities;
using Server;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Client
{
    public class TCPAsyncClient : IClient
    {
        private TcpClient client { get; set; }
        private NetworkStream stream { get; set; }
        public event EventHandler<EventArgs> ResponseGet;
        public TCPAsyncClient() => client = new TcpClient();
        public async Task Boot()
        {
            client = new TcpClient();
            try
            {
                await client.ConnectAsync("127.0.0.1", 5000);

                stream = client.GetStream();
            }
            catch (Exception)
            {
                throw new ServerOfflineException();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task Send(Entities.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.text))
                throw new ArgumentException("Request text is empty", nameof(request.text));

            string json = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(json);
            try
            {
                if (stream.CanWrite)
                    await stream.WriteAsync(data, 0, data.Length);
            }
            catch
            {
                client.Close();
                throw new ConnectionLostException();
            }
        }

        public async Task<Entities.Response?> Get()
        {
            byte[] buffer = new byte[16348];
            int bytesRead;
            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string rawData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Response? result;
            try
            {
                result = JsonSerializer.Deserialize<Entities.Response>(rawData);
            }
            catch
            {
                client.Close();
                throw new ConnectionLostException();
            }
            return result;
        }
        public void Stop()
        {
            client.Close();
        }
    }
}
