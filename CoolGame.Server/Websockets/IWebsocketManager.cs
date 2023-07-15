using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace CoolGame.Server.Websockets;

public interface IWebsocketManager
{
    Task SendMessageToSegmentAsync<T>(T payload, int segment);

    public void AddSocket(WebSocket socket, int segment);

    public Task RemoveSocket(string id);
    string GetId(WebSocket webSocket);
}