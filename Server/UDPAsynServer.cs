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

    private readonly string logFilePath = "events_log.json";

    private async Task HandleClientAsync(UdpReceiveResult result)
    {
        string msg = Encoding.UTF8.GetString(result.Buffer);
        Log($"Получен запрос от {result.RemoteEndPoint}: {msg}");

        Response response;

        try
        {
            Request? request = JsonSerializer.Deserialize<Request>(msg);

            if (request == null || string.IsNullOrWhiteSpace(request.text) || string.IsNullOrWhiteSpace(request.eventType))
            {
                response = new Response { IsSuccess = false, Error = "Неверное тело запроса." };
            }
            else
            {
                Type? typeArg = Type.GetType(request.eventType);
                if (typeArg == null)
                {
                    response = new Response { IsSuccess = false, Error = "Не удалось определить тип события." };
                }
                else
                {
                    Type targetType = typeof(BasicEvent<>).MakeGenericType(typeArg);

                    IEventConverter converter = request.convertingType switch
                    {
                        (int)DataType.Json => new JsonEventSerializer(),
                        (int)DataType.Xml => new XmlEventSerializer(),
                        (int)DataType.Csv => new CsvEventSerializer(),
                        _ => throw new NotSupportedException("Неизвестный тип сериализации")
                    };

                    var method = converter.GetType().GetMethod("Deserialize")!.MakeGenericMethod(typeArg);
                    object? deserialized = method.Invoke(converter, new object[] { request.text });


                    string json = JsonSerializer.Serialize(deserialized, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });


                    await File.AppendAllTextAsync(logFilePath, json + Environment.NewLine);

                    response = new Response
                    {
                        IsSuccess = true,
                    };
                }
            }
        }
        catch (Exception ex)
        {
            response = new Response
            {
                IsSuccess = false,
                Error = $"Ошибка обработки запроса: {ex.Message}"
            };
        }

        byte[] responseBytes = JsonSerializer.SerializeToUtf8Bytes(response);
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

