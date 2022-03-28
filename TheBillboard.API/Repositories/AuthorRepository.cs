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

    public async Task<IEnumerable<AuthorDtoWithDate>> GetAll()
    {
        const string query = @"SELECT Id, Name, Surname, Mail, CreatedAt, UpdatedAt
                               FROM Author";

        var authorList = await _reader.QueryAsync<Author>(query);
        var authorDtoList = new List<AuthorDtoWithDate>();
        foreach (var author in authorList)
        {
            authorDtoList.Add(
                new AuthorDtoWithDate()
                {
                    Name = author.Name,
                    Surname = author.Surname,
                    Mail = author.Mail,
                    Id = author.Id,
                    CreatedAt = (DateTime)author.CreatedAt!,
                    UpdatedAt = (DateTime)author.UpdatedAt!
                }
            );
        }
        return authorDtoList;
    }

    public async Task<AuthorDtoWithDate?> GetById(int id)
    {
        var query = $@"SELECT Id, Name, Surname, Mail, CreatedAt, UpdatedAt
                       FROM Author
                       WHERE Id=@Id";

        var newAuthor = await _reader.GetByIdAsync<Author>(query, id);
        if (newAuthor is null) return null;
        return new AuthorDtoWithDate()
        {
            Name = newAuthor.Name,
            Surname = newAuthor.Surname,
            Mail = newAuthor.Mail,
            Id = newAuthor.Id,
            CreatedAt = (DateTime)newAuthor.CreatedAt!,
            UpdatedAt = (DateTime)newAuthor.UpdatedAt!
        };
    }

    public async Task<AuthorDto> Create(AuthorDto author)
    {
        const string query = @"INSERT INTO Author
                            (Name, Surname, Mail, CreatedAt, UpdatedAt)
                            OUTPUT INSERTED.Id
                            VALUES (@Name, @Surname, @Mail, @CreatedAt, @UpdatedAt)";

        var newAuthor = new Author(default, author.Name, author.Surname, author.Mail, DateTime.Now, DateTime.Now);
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

        var newAuthor = new Author(updatedID, author.Name, author.Surname, author.Mail, default, DateTime.Now);
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

        var newAuthor = new Author(id, default, default, default, default, default);
        return await _writer.DeleteAsync<Author>(query, newAuthor);
    }
}
