namespace TheBillboard.API.Data;

using Domain;
using Microsoft.EntityFrameworkCore;

public class BillboardDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    public BillboardDbContext(DbContextOptions options) : base(options)
    {
    }

}