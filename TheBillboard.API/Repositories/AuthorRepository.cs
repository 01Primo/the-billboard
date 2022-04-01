using Microsoft.EntityFrameworkCore;
using TheBillboard.API.Abstract;
using TheBillboard.API.Data;
using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BillboardDbContext _context;

    public AuthorRepository(BillboardDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author?> GetById(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task<AuthorDto?> Create(AuthorDto author)
    {
        var authorEntity = new Author
        {
            Name = author.Name,
            Surname = author.Surname,
            Email = author.Email,
        };
        _context.Authors.Add(authorEntity);
        await _context.SaveChangesAsync();

        return author with { Id = authorEntity.Id };
    }

    public async Task<AuthorDto?> Update(int id, AuthorDto author)
    {
        var authorEntity = new Author(author.Name, author.Surname, id, author.Email, null, DateTime.Now);
        _context.Authors.Update(authorEntity);
        await _context.SaveChangesAsync();

        return author with { Id = id };
    }

    public async Task<bool> Delete(int id)
    {
        _context.Authors.Remove(new Author() { Id = id });
        await _context.SaveChangesAsync();
        return true;
    }
}
