namespace CoolGame.Server.Websockets;

public class WebsocketMessage<T>
{
    public string Type { get; set; }
    
    public T Payload { get; set; }
}