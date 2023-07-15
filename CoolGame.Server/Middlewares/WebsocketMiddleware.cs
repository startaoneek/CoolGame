using System.Net.WebSockets;
using CoolGame.Server.Websockets;

namespace CoolGame.Server.Middlewares;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebsocketManager _webSocketManager;

    public WebSocketMiddleware(RequestDelegate next, IWebsocketManager webSocketManager)
    {
        _next = next;
        _webSocketManager = webSocketManager;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/ws")
        {
            if (context.WebSockets.IsWebSocketRequest)
            {        
                var segmentParam = context.Request.Query["segment"].ToString();

                if (int.TryParse(segmentParam, out int segment))
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    _webSocketManager.AddSocket(webSocket, segment);

                    await ProcessWebSocketAsync(webSocket);
                }
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
        else
        {
            await _next(context);
        }
    }

    private async Task ProcessWebSocketAsync(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            // Handle received message...

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                break;
            }
        }

        string id = _webSocketManager.GetId(webSocket);
        await _webSocketManager.RemoveSocket(id);
    }
}