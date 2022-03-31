namespace TheBillboard.API.Repositories;

using TheBillboard.API.Abstract;
using TheBillboard.API.Data;
using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;
using Microsoft.EntityFrameworkCore;

public class AuthorRepository : IAuthorRepository
{
    private readonly TheBillboardDbContext _context;

    public AuthorRepository(TheBillboardDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorDtoWithDate>> GetAll()
    {
        var authors = await _context.Author.ToListAsync();
        var result = authors.Select(
            author => new AuthorDtoWithDate()
            {
                Name = author.Name,
                Surname = author.Surname,
                Mail = author.Mail,
                Id = author.Id,
                CreatedAt = (DateTime)author.CreatedAt!,
                UpdatedAt = (DateTime)author.UpdatedAt!
            });
        return result;
    }

    public async Task<AuthorDtoWithDate?> GetById(int id)
    {
        var newAuthor = await _context.Author.SingleOrDefaultAsync(a => a.Id == id);
        if (newAuthor is null) return null;
        return new AuthorDtoWithDate()
        {
            Name = newAuthor.Name,
            Surname = newAuthor.Surname,
            Mail = newAuthor.Mail,
            Id = newAuthor.Id,
            CreatedAt = (DateTime)newAuthor.CreatedAt!,
            UpdatedAt = (DateTime)newAuthor.UpdatedAt!
        };
    }

    public async Task<AuthorDto> Create(AuthorDto author)
    {
        var newAuthor = new Author(default, author.Name, author.Surname, author.Mail, DateTime.Now, DateTime.Now);
        var dbEntry = await _context.AddAsync(newAuthor);

        await _context.SaveChangesAsync();

        return new AuthorDto()
        {
            Name = newAuthor.Name,
            Surname = newAuthor.Surname,
            Mail = newAuthor.Mail,
            Id = dbEntry.Entity.Id
        };
    }

    public async Task<AuthorDto> Update(AuthorDto author)
    {
        var newAuthor = new Author(author.Id, author.Name, author.Surname, author.Mail, UpdatedAt: DateTime.Now);
        await _context.AddAsync(newAuthor);
        await _context.SaveChangesAsync();        
        return new AuthorDto()
        {
            Name = newAuthor.Name,
            Surname = newAuthor.Surname,
            Id = newAuthor.Id,
            Mail = newAuthor.Mail
        };
    }

    public async Task<bool> Delete(int id)
    {
        var NewId = await _context.Author.SingleOrDefaultAsync(a => a.Id == id);
        var newAuthor = new Author(id);
        _context.Author.Remove(newAuthor);
        _context.SaveChanges();

        return true;
    }
}
