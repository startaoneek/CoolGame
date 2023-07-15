using System.Net.WebSockets;

namespace CoolGame.Server.Websockets;

public class GameClient
{
    public int Segment { get; set; }
    public WebSocket Socket { get; set; }
}