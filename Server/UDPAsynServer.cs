using Client;
using Entities;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

public class UDPAsyncServer : IServer
{
    private UdpClient udpClient;
    private CancellationTokenSource _cts;
    private const int Port = 5000;


    public event EventHandler<LogEventArgs> ServerLogEvent;

    public async Task Boot()
    {
        udpClient = new UdpClient(Port);
        _cts = new CancellationTokenSource();
        Log($"Сервер запущен(UDP). port:{Port}");


        while (!_cts.Token.IsCancellationRequested)
        {
            UdpReceiveResult result = await udpClient.ReceiveAsync();
            _ = HandleClientAsync(result);
        }
    }

    private async Task HandleClientAsync(UdpReceiveResult result)
    {
        string msg = Encoding.UTF8.GetString(result.Buffer);
        Log($"Получен запрос от {result.RemoteEndPoint}: {msg}");

        Response response;
        try
        {
            Request? request = JsonSerializer.Deserialize<Request>(msg);

            if (request == null || string.IsNullOrWhiteSpace(request.text))
            {
                response = new Response { IsSuccess = false, Error = "Неверное тело запроса." };
            }
            else if (!Directory.Exists(request.text))
            {
                response = new Response { IsSuccess = false, Error = "Каталог не существует." };
            }
            else
            {
                string[] files = Directory.GetFiles(request.text);
                response = new Response
                {
                    IsSuccess = true,
                    dateTime = DateTime.Now,
                    content = new List<string>(files)
                };
            }
        }
        catch
        {
            response = new Response { IsSuccess = false, Error = "Incorrect request body." };
        }

        string responseJson = JsonSerializer.Serialize(response);
        byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);

        await udpClient.SendAsync(responseBytes, responseBytes.Length, result.RemoteEndPoint);
    }

    public void Close()
    {
        _cts?.Cancel();
        udpClient?.Close();
    }

    public void Log(string msg)
    {
        ServerLogEvent?.Invoke(this, new LogEventArgs(msg));
    }
}

