using CoolGame.Server.DataAccess.Entities;
using CoolGame.Server.DataAccess.Repositories.Interfaces;
using CoolGame.Server.Services.Interfaces;
using CoolGame.Server.Websockets;

namespace CoolGame.Server.Services;

public class OffersService : IOffersService
{
    private IRepository<Offer> _offersRepository;
    private IWebsocketManager _websocketManager;

    public OffersService(IRepository<Offer> offersRepository, IWebsocketManager websocketManager)
    {
        _offersRepository = offersRepository;
        _websocketManager = websocketManager;
    }

    public async Task<Guid> AddOffer(Offer offer)
    {
        _offersRepository.Add(offer);
        await _offersRepository.SaveChangesAsync();

        await _websocketManager.SendMessageToSegmentAsync(offer, new Random().Next(1, 3));

        return offer.Id;
    }

    public async Task<List<Offer>> GetOffers()
    {
        return await _offersRepository.ListAsync();
    }
}