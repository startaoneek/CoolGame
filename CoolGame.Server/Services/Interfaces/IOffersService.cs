using CoolGame.Server.DataAccess.Entities;

namespace CoolGame.Server.Services.Interfaces;

public interface IOffersService
{
    Task<Guid> AddOffer(Offer offer);
    Task<List<Offer>> GetOffers();
}