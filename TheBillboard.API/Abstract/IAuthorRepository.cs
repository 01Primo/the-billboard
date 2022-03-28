using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Abstract;

public interface IAuthorRepository
{
    Task<IEnumerable<AuthorDtoWithDate>> GetAll();
    Task<AuthorDtoWithDate?> GetById(int id);
    Task<AuthorDto> Create(AuthorDto author);
    Task<AuthorDto> Update(AuthorDto author);
    Task<bool> Delete(int id);
}