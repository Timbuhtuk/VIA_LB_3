
using Entities;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Client
{
    public class TCPAsyncServer : IServer
    {
        public TcpListener listener { get; private set; }
        private CancellationTokenSource _cts;
        private const int _PORT = 5000;
        private const int _BUFFER_SIZE = 16384;

        public event EventHandler<LogEventArgs> ServerLogEvent;

        public async Task Boot()
        {
            listener = new TcpListener(IPAddress.Any, _PORT);
            listener.Start();
            _cts = new CancellationTokenSource();
            Log($"Сервер запущен(TCP). port:{_PORT}");

            while (!_cts.Token.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }
        private async Task HandleClientAsync(TcpClient client)
        {
            var stream = client.GetStream();
            byte[] buffer = new byte[_BUFFER_SIZE];
            int bytesRead;
            Log($"new client {client.Client.LocalEndPoint}");
            try
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0 && !_cts.Token.IsCancellationRequested)
                {
                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Log($"New request - {msg}");
                    if (client.Connected)
                    {
                        Request? request;
                        byte[] responseBuffer = new byte[16348];
                        Response response;
                        try
                        {
                            request = JsonSerializer.Deserialize<Request>(msg);


                            if (!Directory.Exists(request.text))
                                response = new Response() { IsSuccess = false, Error = "Directory not exists." };
                            else
                            {
                                string[] files = Directory.GetFiles(request.text);

                                response = new Response() { IsSuccess = true, dateTime = DateTime.Now, content = new List<string>(files) };
                            }
                        }
                        catch
                        {
                            response = new Response() { IsSuccess = false, Error = "Incorrect request body." };
                        }

                        string responseJson = JsonSerializer.Serialize(response);

                        responseBuffer = Encoding.UTF8.GetBytes(responseJson);

                        await client.GetStream().WriteAsync(responseBuffer, 0, Encoding.UTF8.GetByteCount(responseJson));
                    }
                }
                stream.Close();
            }
            catch (Exception)
            {
                Log($"Client disconnected: {client.Client.LocalEndPoint}");
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }
        public void Close()
        {
            _cts?.Cancel();
            listener.Stop();
            listener.Dispose();
        }
        public void Log(string msg)
        {
            ServerLogEvent.Invoke(this, new LogEventArgs(msg));
        }

    }
}
