using CoolGame.Server.DataAccess.Entities;

namespace CoolGame.Server.Services.Interfaces;

public interface IEventsService
{
    Task<Guid> AddEvent(Event @event);
    Task<List<Event>> GetEvents();
}