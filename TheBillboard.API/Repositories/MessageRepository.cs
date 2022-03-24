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
    
    public MessageRepository(IReader reader)
    {
        _reader = reader;
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

    public MessageDto Create(MessageDto message)
    {
        var lastId = _messages.Max(m => m.Id);
        var newId = (lastId ?? 0) + 1;
        
        var newMessage = new Message(newId, message.Title, message.Body, message.AuthorId, DateTime.Now, default);
        _messages.Add(newMessage);
        
        return new MessageDto()
        {
            Title = newMessage.Title,
            Body = newMessage.Body,
            AuthorId = newMessage.AuthorId,
            Id = newId
        };
    }
}