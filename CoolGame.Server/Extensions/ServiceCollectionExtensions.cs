using CoolGame.Server.DataAccess;
using CoolGame.Server.DataAccess.Entities;
using CoolGame.Server.DataAccess.Repositories;
using CoolGame.Server.DataAccess.Repositories.Interfaces;
using CoolGame.Server.Services;
using CoolGame.Server.Services.Interfaces;
using CoolGame.Server.Websockets;
using Microsoft.EntityFrameworkCore;

namespace CoolGame.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CoolGameDbContext>(options => 
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<IWebsocketManager, WebsocketManager>();
        services.AddScoped<IEventsService, EventsService>();
        services.AddScoped<IOffersService, OffersService>();
        services.AddScoped<IRepository<Offer>, Repository<Offer>>();
        services.AddScoped<IRepository<Event>, Repository<Event>>();
    }
}