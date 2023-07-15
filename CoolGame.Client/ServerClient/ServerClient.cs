using Newtonsoft.Json;

namespace CoolGameClient.ServerClient;

public class ServerClient : IServerClient
{
    private const string ApiUrl = "http://localhost:5284/api";
    static readonly HttpClient Client = new HttpClient();
    
    public async Task<List<Event>> GetEvents()
    {
        return await GetDataFromServer<List<Event>>("/events");
    }

    public async Task<List<Offer>> GetOffers()
    {
        return await GetDataFromServer<List<Offer>>("/offers");
    }

    private async Task<T> GetDataFromServer<T>(string relativeUrl)
    {
        try
        {
            HttpResponseMessage response = await Client.GetAsync(ApiUrl + relativeUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message: {0} ", e.Message);

            throw;
        }
    } 
}