using TheBillboard.API.Abstract;
using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly IReader _reader;

    public AuthorRepository(IReader reader)
    {
        _reader = reader;
    }

    public Task<IEnumerable<Author>> GetAll()
    {
        const string query = @"SELECT Id, Name, Surname, Mail, CreatedAt
                               FROM Author";

        return _reader.QueryAsync<Author>(query);
    }

    public Task<Author?> GetById(int id)
    {
        var query = $@"SELECT Id, Name, Surname, Mail, CreatedAt
                       FROM Author
                       WHERE Id=@Id";

        return _reader.GetByIdAsync<Author>(query, id);
    }

    public AuthorDto Create(AuthorDto author)
    {
        //TODO
        return new AuthorDto();
}
    public AuthorDto Update(AuthorDto author)
    {
        //TODO
        return new AuthorDto();
    }

    public bool Delete(int id)
    {
        //TODO
        return true;
    }
}
