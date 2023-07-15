using Microsoft.EntityFrameworkCore;
using CoolGame.Server.DataAccess.Entities;

namespace CoolGame.Server.DataAccess;

public class CoolGameDbContext : DbContext
{
    public CoolGameDbContext(DbContextOptions<CoolGameDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Event> Events { get; set; }
}