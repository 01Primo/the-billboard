namespace TheBillboard.API.Data;

using Domain;
using Microsoft.EntityFrameworkCore;

public class BillboardDbContext : DbContext
{
    public DbSet<Author> Author { get; set; } = null!;
    public DbSet<Message> Message { get; set; } = null!;

    public BillboardDbContext(DbContextOptions options) : base(options)
    {
    }

}