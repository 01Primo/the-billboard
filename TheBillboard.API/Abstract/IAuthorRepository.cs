using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Abstract;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAll();
    Task<Author?> GetById(int id);
    AuthorDto Create(AuthorDto author);
    AuthorDto Update(AuthorDto author);
    bool Delete(int id);
}
