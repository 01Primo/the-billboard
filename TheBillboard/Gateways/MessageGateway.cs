using Npgsql;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.Abstract;
using TheBillboard.Models;

namespace TheBillboard.Gateways;

public class MessageGateway : IMessageGateway
{
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public MessageGateway(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public Task<IEnumerable<Message>> GetAll()
    {
        const string query = "select * from Message M join Author A on A.Id = M.AuthorId";
        return _reader.QueryAsync(query, Map);
    }

    public async Task<Message>? GetById(int id)
    {
        const string query = $"select * from Message M join Author A on A.Id = M.AuthorId where M.Id = @Id";
        var parametersTuple = new List<(string Name, object Value)>
        {
            (@"Id", id)
        };
        var message = await _reader.QueryAsync(query, Map, parametersTuple);
        return message.ToList().First();
    }

    public Task<bool> Create(Message message)
    {
        const string query = $"INSERT INTO [dbo].[Message] ([Title],[Body],[CreatedAt],[UpdatedAt],[AuthorId])" +
            $"VALUES(@Title, @Body, @CreatedAt, @UpdatedAt, @AuthorId)";

        var parametersTuple = new List<(string Name, object Value)>
        {
            (@"Title", message.Title),
            (@"Body", message.Body),
            (@"CreatedAt", DateTime.Now),
            (@"UpdatedAt", DateTime.Now),
            (@"AuthorId", message.AuthorId)
        };

        return _writer.WriteAsync(query, parametersTuple);
    }

    public async Task<bool> Delete(int id)
    {
        const string query = $"DELETE FROM [dbo].[Message] WHERE Id = @Id";
        var parametersTuple = new List<(string Name, object Value)>
        {
            (@"Id", id)
        };
        return await _writer.DeleteAsync(query, parametersTuple);
    }

    public async Task<bool> Update(Message message)
    {
        const string query = $"UPDATE [dbo].[Message] SET [Title] = @Title,[Body] = @body,[UpdatedAt] = @UpdatedAt WHERE Id = @Id";
        var parametersTuple = new List<(string Name, object Value)>
        {
            (@"Id", message.Id!),
            (@"Title", message.Title),
            (@"Body", message.Body),
            (@"UpdatedAt", DateTime.Now),
        };
        return await _writer.UpdateAsync(query, parametersTuple);
    }
    Message Map(IDataReader dr)
    {
        return new Message
        {
            Id = dr["id"] as int?,
            Body = dr["body"].ToString()!,
            Title = dr["title"].ToString()!,
            CreatedAt = dr["createdAt"] as DateTime?,
            UpdatedAt = dr["updatedAt"] as DateTime?,
            AuthorId = (int)dr["authorId"],
            Author = new Author
            {
                Id = dr["authorId"] as int?,
                Name = dr["name"].ToString()!,
                Surname = dr["surname"].ToString()!,
            }
        };
    }
}