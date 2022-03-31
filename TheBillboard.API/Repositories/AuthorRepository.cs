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

    //TODO: find out why "messages" is in the JSON response and handle it
    public async Task<IEnumerable<Author>> GetAll() => _context.Author;

    //TODO: find out why "messages" is in the JSON response and handle it
    public async Task<Author?> GetById(int id) => _context.Author.Find(id) ?? null;
}
