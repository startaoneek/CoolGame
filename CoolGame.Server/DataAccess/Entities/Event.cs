using CoolGame.Server.DataAccess.Entities.Interfaces;

namespace CoolGame.Server.DataAccess.Entities;

public class Event: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int EventType { get; set; }
}