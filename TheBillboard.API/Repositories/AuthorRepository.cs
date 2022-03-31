using TheBillboard.API.Abstract;
using TheBillboard.API.Data;
using TheBillboard.API.Domain;

namespace TheBillboard.API.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BillboardDbContext _context;

    public AuthorRepository(BillboardDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Author>> GetAll()
    {
        var authors = _context.Author.Select(_ => _); // Select all
        return (Task<IEnumerable<Author>>)authors;
    }

    public Task<Author?> GetById(int id)
    {
        var author = _context.Author.Where(author => author.Id == id);
        return (Task<Author?>)author;
    }
}
