using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace CoolGame.Server.Websockets;

public class WebsocketManager : IWebsocketManager
{
    private readonly ILogger<WebsocketManager> _logger;
    private readonly ConcurrentDictionary<string, GameClient> _sockets = new ConcurrentDictionary<string, GameClient>();

    public WebsocketManager(ILogger<WebsocketManager> logger)
    {
        _logger = logger;
    }

    public async Task SendMessageToSegmentAsync<T>(T payload, int segment)
    {
        _logger.LogInformation($"Sending message to segment: {segment}");
        WebsocketMessage<T> message = new WebsocketMessage<T>
        {
            Type = payload.GetType().Name,
            Payload = payload
        };
        
        string messageData = JsonSerializer.Serialize(message);
        foreach (var pair in _sockets.Values.Where(s => s.Segment == segment))
        {
            if (pair.Socket.State == WebSocketState.Open)
                await SendMessageAsync(pair.Socket, messageData);
        }
    }

    private async Task SendMessageAsync(WebSocket socket, string message)
    {
        if (socket.State != WebSocketState.Open)
            return;

        await socket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public void AddSocket(WebSocket socket, int segment)
    {
        _logger.LogInformation($"Connected client from segment: {segment}");
        _sockets.TryAdd(Guid.NewGuid().ToString(), new GameClient
        {
            Segment = segment,
            Socket = socket
        });
    }

    public async Task RemoveSocket(string id)
    {
        GameClient client;
        _sockets.TryRemove(id, out client);

        await client.Socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
            statusDescription: "Closed by the WebSocketManager",
            cancellationToken: CancellationToken.None);
    }


    public string GetId(WebSocket socket)
    {
        return _sockets.FirstOrDefault(p => p.Value.Socket == socket).Key;
    }
}