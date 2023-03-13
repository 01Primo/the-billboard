namespace TheBillboard.MVC.Gateways;

using System.Data;
using Abstract;
using Models;

public class FakeGateway 
{
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public FakeGateway(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public IAsyncEnumerable<Author> GetAll()
    {
        const string query = @"SELECT Id, Name, Surname, Mail, CreatedAt
                               FROM Author";

        return _reader.QueryAsync(query, MapAuthor);
    }

    public Task<Author?> GetById(int id)
    {
        var query = $@"SELECT Id, Name, Surname, Mail, CreatedAt
                       FROM Author
                       WHERE Id={id}";

        return _reader.SingleQueryAsync(query, MapAuthor);
    }

    public Task<bool> Create(Author author)
    {
        var query = @"INSERT INTO Author(Name, Surname, Mail, CreatedAt)
                      VALUES (@Name, @Surname, @Email, @CreatedAt)";

        var parameters = new List<(string, object?)>
        {
            ("@Name", author.Name),
            ("@Surname", author.Surname),
            ("@Email", author.Email),
            ("@CreatedAt", DateTime.Now)
        };

        return _writer.WriteAsync(query, parameters);
    }

    public Task<bool> Delete(int id)
    {
        var query = @"DELETE FROM ""Author""
                      WHERE (Id=@Id)";

        return _writer.WriteAsync(query, new[] { ("@Id", (object?)id) });
    }

    private Author MapAuthor(IDataReader dr)
    {
        return new Author
        {
            Id = dr["Id"] as int?,
            Name = dr["Name"].ToString()!,
            Surname = dr["Surname"].ToString()!,
            Email = dr["Mail"].ToString(),
            CreatedAt = dr["createdAt"] as DateTime?
        };
    }
}