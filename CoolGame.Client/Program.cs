using System.Net.WebSockets;
using System.Text;
using CoolGameClient.ServerClient;

class Program
{
    private static ClientWebSocket _clientWebSocket = new ClientWebSocket();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Game Client...");

        // Request and print all active offer bundles and events.
        await FetchAndPrintOffersAndEvents();

        // Connect to WebSocket server
        await _clientWebSocket.ConnectAsync(new Uri($"ws://localhost:5284/ws?segment={new Random().Next(1, 3)}"), CancellationToken.None);

        // Start listening for messages
        await ReceiveMessagesAsync();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static async Task FetchAndPrintOffersAndEvents()
    {
        IServerClient serverClient = new ServerClient();
        
        Console.WriteLine("Available offers: ");
        foreach (var offer in await serverClient.GetOffers())
        {
            Console.WriteLine(offer);
        }
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Available events: ");
        foreach (var @event in await serverClient.GetEvents())
        {
            Console.WriteLine(@event);
        }
        Console.WriteLine("------------------------------------");

        
    }

    private static async Task ReceiveMessagesAsync()
    {
        while (_clientWebSocket.State == WebSocketState.Open)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result;
            do
            {
                result = await _clientWebSocket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    Console.WriteLine($"Received message: {message}");
                }
            } while (!result.EndOfMessage);
        }
    }
}
