using CoolGame.Server.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CoolGame.Server.Extensions;

public static class ServiceProviderExtensions
{
    public static void InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<CoolGameDbContext>();
            context.Database.EnsureCreated();
        }
    }

}