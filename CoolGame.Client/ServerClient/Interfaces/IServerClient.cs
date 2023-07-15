namespace CoolGameClient.ServerClient
{
    public interface IServerClient
    {
        public Task<List<Event>> GetEvents();
        public Task<List<Offer>> GetOffers();
    }
}