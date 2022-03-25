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
                             " values (@Name, @Surname, @Mail, @CreatedAt)";

        var parameters = new 
        {
            Name = author.Name,
            Surname = author.Surname,
            Mail = author.Email,
            CreatedAt = DateTime.Now
        };
        var newId = await _writer.CreateAsync(query, parameters);
        var newAuthor = author with { Id = newId };
        return newAuthor;
    }

    public async Task<bool> Delete(int id)
    {
        const string query = "delete from [Author]" + 
                             " where (Id=@Id)";
        

        var rowsAffected = await _writer.WriteAsync(query, new{Id = id});
        return rowsAffected > 0;
    }

    public async Task<AuthorDto?> Update(int id, AuthorDto author)
    {
        const string query = "update Author" +
                             " set Name=@Name,Surname=@Surname,Mail=@Mail,UpdatedAt=@UpdatedAt" +
                             " where (Id=@Id)";

        var parameters = new
        {
            Name = author.Name,
            Surname = author.Surname,
            Mail = author.Email,
            UpdatedAt = DateTime.Now,
            Id = id
        };

        var rowsAffected = await _writer.WriteAsync(query, parameters);
        var updatedMessage = author with { Id = id };

        return rowsAffected > 0 ? updatedMessage : null;
    }
}
