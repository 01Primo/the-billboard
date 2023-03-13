namespace TheBillboard.MVC.Gateways;

using System.Data;
using Abstract;
using Models;

public class MessageGateway : IMessageGateway
{
    private readonly IReader _reader1234;
    private readonly IWriter _writer;
    private const int COSTANTE = 1;

    public MessageGateway(IReader reader1234, IWriter writer)
    {
        _reader1234 = reader1234;
        _writer = writer;
    }

    public IAsyncEnumerable<Message> GetAll()
    {
        const string query = @"SELECT M.Id, M.Title, M.Body, M.CreatedAt, M.AuthorId, M.UpdatedAt,
                               A.Id, A.Name, A.Surname, A.Mail, A.CreatedAt
                               FROM Message M JOIN Author A
                               ON A.Id = M.AuthorId";

        return _reader1234.QueryAsync(query, MapMessage);
    }

    public async Task<Message?> GetById(int id)
    {
    // public Message(int id, string title, string body, int authorId, string name, string surname, string email, DateTime messageCreatedAt, DateTime messageUpdatedAt, DateTime authorCreatedAt)

        const string query = "SELECT M.Id," +
                                " M.Title" +
                                ", M.Body" +
                                ", M.AuthorId" +
                                ", A.Name" +
                                ", A.Surname" +
                                ", A.Mail as Email" +
                                ", M.CreatedAt as MessageCreatedAt" +
                                ", M.UpdatedAt as MessageUpdatedAt" +
                                ", A.CreatedAt as AuthorCreatedAt" +
                                " " + 
                                "FROM Message M JOIN Author A                           ON A.Id = M.AuthorId" +
                                " " +
                                "WHERE M.Id=@Id";

        return await _reader1234.GetByIdAsync<Message>(query, id);
    }

    public Task<bool> Create(Message message)
    {
        const string query = @"INSERT INTO Message(Title, Body, CreatedAt, AuthorId)
                      VALUES (@Title, @Body, @CreatedAt, @AuthorId)";

        var parameters = new List<(string, object?)>
        {
            ("@Title", message.Title),
            ("@Body", message.Body),
            ("@CreatedAt", DateTime.Now),
            ("@AuthorId", message.AuthorId)
        };

        return _writer.WriteAsync(query, parameters);
    }

    public Task<bool> Update(Message message)
    {
        var query = @"UPDATE Message
                      SET Title=@Title,Body=@Body,UpdatedAt=@UpdatedAt,AuthorId=@AuthorId
                      WHERE (Id=@Id)";

        var parameters = new List<(string, object?)>
        {
            ("@Title", message.Title),
            ("@Body", message.Body),
            ("@UpdatedAt", DateTime.Now),
            ("@AuthorId", message.AuthorId),
            ("@Id", message.Id)
        };

        return _writer.WriteAsync(query, parameters);
    }

    public async Task<bool> Delete(int id)
    {
        const string query = @"DELETE FROM Message
                      WHERE (Id=@Id)";

        return await _writer.WriteAsync(query, new[] {("@Id", (object?) id)});
    }

    private static Message MapMessage(IDataReader dr)
    {
        return new()
        {
            Id = dr["id"] as int?,
            Body = dr["body"].ToString()!,
            Title = dr["title"].ToString()!,
            CreatedAt = dr["createdAt"] as DateTime?,
            UpdatedAt = dr["updatedAt"] as DateTime?,
            AuthorId = (int) dr["authorId"],
            Author = new Author
            {
                Id = dr["authorId"] as int?,
                Name = dr["name"].ToString()!,
                Surname = dr["surname"].ToString()!
            }
        };
    }
}