namespace TheBillboard.API.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain;

public class TheBillboardDbContext : DbContext
{
    public DbSet<Author> Author { get; set; } = null!;
    public DbSet<Message> Message { get; set; } = null!;

    public TheBillboardDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var author = modelBuilder.Entity<Author>();
        author.HasKey(a => a.Id);
        author.Property(a => a.Name).HasMaxLength(50).IsRequired();
        author.Property(a => a.Surname).HasMaxLength(50).IsRequired();
        author.Property(a => a.Mail).HasMaxLength(50).IsRequired();
        author.Property(a => a.CreatedAt).IsRequired();
        author.Property(a => a.CreatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        author.Property(a => a.UpdatedAt).IsRequired();

        var message = modelBuilder.Entity<Message>();
        message.HasKey(m => m.Id);
        message.Property(m => m.Title).HasMaxLength(50).IsRequired();
        message.Property(m => m.Body).HasMaxLength(256).IsRequired();
        message.Property(m => m.AuthorId).IsRequired();
        message.Property(m => m.CreatedAt).IsRequired();
        message.Property(m => m.CreatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        message.Property(m => m.UpdatedAt).IsRequired();

        message
            .HasOne(m => m.Author)
            .WithMany(a => a.Messages)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(m => m.AuthorId);
    }
}