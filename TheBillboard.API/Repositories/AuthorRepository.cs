namespace TheBillboard.API.Repositories;

using Abstract;
using Data;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;

public class AuthorRepository : IAuthorRepository
{
    private readonly BillboardDbContext _context;

    public AuthorRepository(BillboardDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorDto>> GetAll()
    {
        var result = _context.Author.Select(author => new AuthorDto(author));
        return await result.ToListAsync();
    }

    public async Task<AuthorDto?> GetById(int id)
    {
        var author = await _context.Author.FindAsync(id);
        return author is not null ? new AuthorDto(author) : null;
    }

    public async Task<AuthorDto> Create(AuthorDto authorDto)
    {
        var newAuthor = new Author(authorDto);
        newAuthor.Id = null;

        var result = await _context.AddAsync(newAuthor);
        await _context.SaveChangesAsync();

        return new AuthorDto(result.Entity);
    }

    public async Task<AuthorDto> Update(AuthorDto authorDto)
    {
        var newAuthor = new Author(authorDto);

        var result = _context.Update(newAuthor);
        await _context.SaveChangesAsync();

        return new AuthorDto(result.Entity);
    }

    public async Task Delete(int id)
    {
        //TODO
        return;
    }
}