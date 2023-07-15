using CoolGame.Server.DataAccess.Entities.Interfaces;

namespace CoolGame.Server.DataAccess.Entities;

public class Offer: IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int OfferType { get; set; }
}