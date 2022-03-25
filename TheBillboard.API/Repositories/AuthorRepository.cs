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

    public async Task<IEnumerable<Author>> GetAll()
    {
        const string query = @"SELECT Id, Name, Surname, Mail, CreatedAt, UpdatedAt
                               FROM Author";

        return await _reader.QueryAsync<Author>(query);
    }

    public async Task<Author?> GetById(int id)
    {
        var query = $@"SELECT Id, Name, Surname, Mail, CreatedAt, UpdatedAt
                       FROM Author
                       WHERE Id=@Id";

        return await _reader.GetByIdAsync<Author>(query, id);
    }

    public async Task<AuthorDto> Create(AuthorDto author)
    {
        const string query = @"INSERT INTO Author
                            (Name, Surname, Mail, CreatedAt, UpdatedAt)
                            OUTPUT INSERTED.Id
                            VALUES (@Name, @Surname, @Mail, @CreatedAt, @UpdatedAt)";

        var newAuthor = new Author(author.Name, author.Surname, default, author.Mail, DateTime.Now, DateTime.Now);
        var newId = await _writer.WriteAndReturnIdAsync<Author>(query, newAuthor);

        return new AuthorDto()
        {
            Name = newAuthor.Name,
            Surname = newAuthor.Surname,
            Mail = newAuthor.Mail,
            Id = newId
        };
    }

    public async Task<AuthorDto> Update(AuthorDto author)
    {
        var updatedID = author.Id;
        const string query = @"UPDATE Author
                            SET Name = @Name, Surname = @Surname, Mail = @Mail, UpdatedAt = @UpdatedAt
                            WHERE Id = @Id";

        var newAuthor = new Author(author.Name, author.Surname, updatedID, author.Mail, default, DateTime.Now);
        var result = await _writer.UpdateAsync<Author>(query, newAuthor);

        return new AuthorDto()
        {
            Name = newAuthor.Name,
            Surname = newAuthor.Surname,
            Id = updatedID,
            Mail = newAuthor.Mail            
        };
    }

    public async Task<bool> Delete(int id)
    {
        const string query = @"DELETE FROM Author
                            WHERE Id = @Id";

        var newAuthor = new Author(default, default, id, default, default, default);
        return await _writer.DeleteAsync<Author>(query, newAuthor);
    }
}
