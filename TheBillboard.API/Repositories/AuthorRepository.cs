using TheBillboard.API.Abstract;
using TheBillboard.API.Domain;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public AuthorRepository(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public Task<IEnumerable<Author>> GetAll()
    {
        const string query = @"SELECT Id, Name, Surname, Mail, CreatedAt, UpdatedAt
                               FROM Author";

        return _reader.QueryAsync<Author>(query);
    }

    public Task<Author?> GetById(int id)
    {
        const string query = $@"SELECT Id, Name, Surname, Mail, CreatedAt, UpdatedAt
                       FROM Author
                       WHERE Id=@Id";

        return _reader.GetByIdAsync<Author>(query, id);
    }

    public async Task<AuthorDto?> Create(AuthorDto author)
    {
        const string query = "insert into Author(Name, Surname, Mail, CreatedAt)" +
                             " output inserted.Id" +
                             " values (@Name, @Surname, @Email, @CreatedAt)";

        var authorEntity = new Author(null, author.Name, author.Surname, author.Email, DateTime.Now, null);
        var newId = await _writer.CreateAsync(query, authorEntity);
        return author with { Id = newId };
    }

    public async Task<AuthorDto?> Update(int id, AuthorDto author)
    {
        const string query = "update Author" +
                             " set Name=@Name,Surname=@Surname,Mail=@Email,UpdatedAt=@UpdatedAt" +
                             " where (Id=@Id)";

        var authorEntity = new Author(id, author.Name, author.Surname, author.Email, null, DateTime.Now);
        var rowsAffected = await _writer.UpdateAsync(query, authorEntity);
        var updatedAuthor = author with { Id = id };

        return rowsAffected > 0 ? updatedAuthor : null;
    }

    public async Task<bool> Delete(int id)
    {
        const string query = "delete from [Author]" +
                             " where (Id=@Id)";

        var rowsAffected = await _writer.DeleteByIdAsync(query, id);
        return rowsAffected > 0;
    }
}
