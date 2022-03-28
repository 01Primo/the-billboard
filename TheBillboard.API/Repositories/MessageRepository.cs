namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;

public class MessageRepository : IMessageRepository
{
    private readonly List<Message> _messages = new()
    {
        new()
        {
            Author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Email = "john.dow.mail.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            Id = 1,
            Title = "Hello",
            Body = "Hello World!",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            AuthorId = 1
        },
        new()
        {
            Author = new Author()
            {
                Id = 2,
                Name = "Jane",
                Surname = "Doe",
                Email = "jane.doe.mail.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            Id = 2,
            Title = "Hi",
            Body = "Hi World!",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            AuthorId = 2
        }
    };

    private readonly IReader _reader;
    private readonly IWriter _writer;

    public MessageRepository(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }
    public Task<IEnumerable<Message>> GetAll()
    {
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
                                "FROM Message M JOIN Author A                           ON A.Id = M.AuthorId";

        return _reader.QueryAsync<Message>(query);
    }

    public async Task<Message?> GetById(int id)
    {
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

        return await _reader.GetByIdAsync<Message>(query, id);
    }

    public async Task<MessageDto?> Create(MessageDto message)
    {
        const string query = "insert into [Message] (Title, Body, CreatedAt, AuthorId)" +
                             " output inserted.Id" +
                             " values (@Title, @Body, @CreatedAt, @AuthorId)";

        var messageEntity = new Message(null, message.Title, message.Body, message.AuthorId, DateTime.Now, null);
        var newId = await _writer.CreateAsync(query, messageEntity);
        return message with { Id = newId };
    }

    public async Task<MessageDto?> Update(int id, MessageDto message)
    {
        const string query = @"UPDATE Message
                      SET Title=@Title,Body=@Body,UpdatedAt=@UpdatedAt,AuthorId=@AuthorId
                      WHERE (Id=@Id)";

        var messageEntity = new Message(id, message.Title, message.Body, message.AuthorId, null, DateTime.Now);
        var rowsAffected = await _writer.UpdateAsync(query, messageEntity);
        var updatedMessage = message with { Id = id };

        return rowsAffected > 0 ? updatedMessage : null;
    }

    public async Task<bool> Delete(int id)
    {
        const string query = @"DELETE FROM Message
                      WHERE (Id=@Id)";

        var rowsAffected = await _writer.DeleteByIdAsync(query, id);
        return rowsAffected > 0;
    }
}