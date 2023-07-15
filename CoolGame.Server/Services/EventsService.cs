using CoolGame.Server.DataAccess.Entities;
using CoolGame.Server.DataAccess.Repositories.Interfaces;
using CoolGame.Server.Services.Interfaces;
using CoolGame.Server.Websockets;

namespace CoolGame.Server.Services;

public class EventsService : IEventsService
{
    private IRepository<Event> _eventsRepository;
    private IWebsocketManager _websocketManager;

    public EventsService(IRepository<Event> eventsRepository, IWebsocketManager websocketManager)
    {
        _eventsRepository = eventsRepository;
        _websocketManager = websocketManager;
    }

    public async Task<Guid> AddEvent(Event @event)
    {
        _eventsRepository.Add(@event);
        await (_eventsRepository).SaveChangesAsync();

        await _websocketManager.SendMessageToSegmentAsync(@event, new Random().Next(1, 3));

        return @event.Id;
    }

    public async Task<List<Event>> GetEvents()
    {
        return await _eventsRepository.ListAsync();
    }
}