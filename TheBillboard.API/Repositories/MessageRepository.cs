namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;

public class MessageRepository : IMessageRepository
{
    /*private readonly List<Message> _messages = new()
    {
        new()
        {
            Author = new Author()
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Mail = "john.dow.mail.com",
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
                Mail = "jane.doe.mail.com",
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
    };*/

    private readonly IReader _reader;
    private readonly IWriter _writer;

    public MessageRepository(IReader reader, IWriter writer)
    {
        _reader = reader;
        _writer = writer;
    }
    public Task<IEnumerable<Message>> GetAll()
    {
        const string query = @"SELECT M.Id
                                , M.Title
                                , M.Body
                                , M.AuthorId 
                                , A.Name
                                , A.Surname
                                , A.Mail as Email
                                , M.CreatedAt as MessageCreatedAt
                                , M.UpdatedAt as MessageUpdatedAt
                                , A.CreatedAt as AuthorCreatedAt
                                , A.UpdatedAt as AuthorUpdatedAt
                                 FROM Message M JOIN Author A ON A.Id = M.AuthorId";

        return _reader.QueryAsync<Message>(query);
    }

    public async Task<Message?> GetById(int id)
    {
        const string query = @"SELECT M.Id
                                , M.Title
                                , M.Body
                                , M.AuthorId 
                                , A.Name
                                , A.Surname
                                , A.Mail as Email
                                , M.CreatedAt as MessageCreatedAt
                                , M.UpdatedAt as MessageUpdatedAt
                                , A.CreatedAt as AuthorCreatedAt
                                , A.UpdatedAt as AuthorUpdatedAt
                                 FROM Message M JOIN Author A ON A.Id = M.AuthorId                               
                                 WHERE M.Id=@Id";

        //"SELECT M.Id," +
        //                    " M.Title" +
        //                    ", M.Body" +
        //                    ", M.AuthorId" +
        //                    ", A.Name" +
        //                    ", A.Surname" +
        //                    ", A.Mail as Email" +
        //                    ", M.CreatedAt as MessageCreatedAt" +
        //                    ", M.UpdatedAt as MessageUpdatedAt" +
        //                    ", A.CreatedAt as AuthorCreatedAt" +
        //                    " " +
        //                    "FROM Message M JOIN Author A                           ON A.Id = M.AuthorId" +
        //                    " " +
        //                    "WHERE M.Id=@Id";

        return await _reader.GetByIdAsync<Message>(query, id);
    }

    public async Task<MessageDto> Create(MessageDto message)
    {

        const string query = @"INSERT INTO Message
                            (Title, Body, CreatedAt, UpdatedAt, AuthorId)
                            OUTPUT INSERTED.Id
                            VALUES (@Title, @Body, @CreatedAt, @UpdatedAt, @AuthorId)";

        var newMessage = new Message(default, message.Title, message.Body, message.AuthorId, default, DateTime.Now, DateTime.Now);
        var newId = await _writer.WriteAndReturnIdAsync<Message>(query, newMessage);

        return new MessageDto()
        {
            Title = newMessage.Title,
            Body = newMessage.Body,
            AuthorId = newMessage.AuthorId,
            Id = newId
        };

    }
    public async Task<MessageDto> Update(MessageDto message)
    {
        var updatedID = message.Id;
        const string query = @"UPDATE Message
                            SET Title = @Title, Body = @Body, UpdatedAt = @UpdatedAt, AuthorId = @AuthorId
                            WHERE Id = @Id";

        var newMessage = new Message(updatedID, message.Title, message.Body, message.AuthorId, default, default, DateTime.Now);
        var result = await _writer.UpdateAsync<Message>(query, newMessage);

        return new MessageDto()
        {
            Title = newMessage.Title,
            Body = newMessage.Body,
            AuthorId = newMessage.AuthorId,
            Id = updatedID,
        };
    }

    public async Task<bool> Delete(int id)
    {
        const string query = @"DELETE FROM Message
                            WHERE Id = @Id";

        var newMessage = new Message(id, default, default, default, default, default, default);
        return await _writer.DeleteAsync<Message>(query, newMessage);
    }
}