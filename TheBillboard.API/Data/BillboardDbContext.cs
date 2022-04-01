using Microsoft.EntityFrameworkCore;
using TheBillboard.API.Domain;

namespace TheBillboard.API.Data;

public class BillboardDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;

    public BillboardDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var messages = modelBuilder.Entity<Message>();
        messages.Property(m => m.CreatedAt).IsRequired();
        messages
            .HasOne(m => m.Author)
            .WithMany(a => a.Messages)
            .HasForeignKey(m => m.AuthorId);

        var authors = modelBuilder.Entity<Author>();
        authors.Property(a => a.CreatedAt).IsRequired();
        authors.HasMany(a => a.Messages);
    }
}
