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

    public async Task<IEnumerable<Message>> GetAllAsync()
    {       
        const string query = "SELECT M.Id, M.Title, M.Body, M.CreatedAt, M.UpdatedAt, M.AuthorId, A.Name, A.Surname FROM Message M join Author A on A.Id = M.AuthorId";
        return await _reader.QueryAsync<Message>(query);
    }

    public async Task<Message>? GetByIdAsync(int id)
    {
        const string query = $"SELECT M.Id, M.Title, M.Body, M.CreatedAt, M.UpdatedAt, M.AuthorId, A.Name, A.Surname FROM Message M join Author A on A.Id = M.AuthorId WHERE M.Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);

        var message = await _reader.QueryAsync<Message>(query, parameters);
        return message.First();
    }

    public Task<bool> CreateAsync(Message message)
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

    public async Task<bool>  DeleteAsync(int id)
    {
        const string query = $"DELETE FROM [dbo].[Message] WHERE Id = @Id";
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);

        return await _writer.DeleteAsync(query, parameters);
    }

    public async Task<bool> UpdateAsync(Message message)
    {
        const string query = $"UPDATE [dbo].[Message] SET [Title] = @Title,[Body] = @body,[UpdatedAt] = @UpdatedAt WHERE Id = @Id";        
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", message.Id!, DbType.Int32);
        parameters.Add("Title", message.Title, DbType.String);
        parameters.Add("Body", message.Body, DbType.String);
        parameters.Add("UpdatedAt", DateTime.Now, DbType.DateTime);

        return await _writer.UpdateAsync(query, parameters);
    }
}