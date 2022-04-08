using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Abstract;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAll();
    Task<Author?> GetById(int id);
    Task<AuthorDto?> Create(AuthorDto author);
    Task<bool> Delete(int id);
    Task<AuthorDto?> Update(int id, AuthorDto author);
}
