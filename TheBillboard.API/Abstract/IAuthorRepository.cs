namespace TheBillboard.API.Abstract;

using Dtos;

public interface IAuthorRepository
{
    Task<IEnumerable<AuthorDto>> GetAll();
    Task<AuthorDto?> GetById(int id);
    Task<AuthorDto> Create(AuthorDto authorDto);
    Task<AuthorDto> Update(AuthorDto authorDto);
    Task Delete(int id);
}
