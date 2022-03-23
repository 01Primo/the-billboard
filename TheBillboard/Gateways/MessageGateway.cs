using Npgsql;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Models;
using System.Linq;
using Dapper;

namespace TheBillboard.MVC.Gateways;

public class MessageGateway : IMessageGateway
{
    private readonly IReader _reader;
    private readonly IWriter _writer;

    public MessageGateway(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }

    public async Task<IEnumerable<Message>> GetAll()
    {       
        const string query = "select M.Id, M.Title, M.Body, M.CreatedAt, M.UpdatedAt, M.authorId, A.Name, A.Surname from Message M join Author A on A.Id = M.AuthorId";
        return await _reader.QueryWithDapper<Message>(query);
    }

    public async Task<Message>? GetById(int id)
    {
        const string query = $"select * from Message M join Author A on A.Id = M.AuthorId where M.Id = @Id";
        var parametersTuple = new List<(string Name, object Value)>
        {
            (@"Id", id)
        };
        var message = await _reader.QueryAsync(query, Map, parametersTuple).ToListAsync();
        return message.First();
    }

    public Task<bool> Create(Message message)
    {
        const string query = $"INSERT INTO [dbo].[Message] ([Title],[Body],[CreatedAt],[UpdatedAt],[AuthorId])" +
            $"VALUES(@Title, @Body, @CreatedAt, @UpdatedAt, @AuthorId)";

        var parameters = new DynamicParameters();
        parameters.Add("Title", message.Title, DbType.String);
        parameters.Add("Body", message.Body, DbType.String);
        parameters.Add("CreatedAt", DateTime.Now, DbType.DateTime);
        parameters.Add("UpdatedAt", DateTime.Now, DbType.DateTime);
        parameters.Add("AuthorId", message.AuthorId, DbType.Int32);

        return _writer.CreateAsync(query, parameters);
    }

    public async Task<bool> Delete(int id)
    {
        const string query = $"DELETE FROM [dbo].[Message] WHERE Id = @Id";
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);

        return await _writer.DeleteAsync(query, parameters);
    }

    public async Task<bool> Update(Message message)
    {
        const string query = $"UPDATE [dbo].[Message] SET [Title] = @Title,[Body] = @body,[UpdatedAt] = @UpdatedAt WHERE Id = @Id";        
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", message.Id!, DbType.Int32);
        parameters.Add("Title", message.Title, DbType.String);
        parameters.Add("Body", message.Body, DbType.String);
        parameters.Add("UpdatedAt", DateTime.Now, DbType.DateTime);

        return await _writer.UpdateAsync(query, parameters);
    }
    private Message Map(IDataReader dr)
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