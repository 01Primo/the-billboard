namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;
using TheBillboard.API.Data;

public class MessageRepository : IMessageRepository
{

    private readonly BillboardDbContext _context;

    public MessageRepository(BillboardDbContext context)
    {
        _context = context;
    }
    
        //TODO: assess whether Author is needed, if yes add it to "message" before returning
    public async Task<IEnumerable<Message>> GetAll()
    {
        var messages = _context
            .Message
            .Join(_context.Author,
                  message => message.AuthorId,
                  author => author.Id,
                  (message, author) => new { Message = message, Author = author })
            .Select(messageAndAuthor => messageAndAuthor.Message)
            .AsEnumerable();

        return messages;
    }

    //TODO: assess whether Author is needed, if yes add it to "message" before returning
    public async Task<Message?> GetById(int id)
    {
        var message = _context.Message.Find(id);
        return message ?? null;
    }

    //TODO: currently automatically assigns Id on DB, probably need to retrieve it and set it as Id of returned MessageDto
    public MessageDto Create(MessageDto message)
    {
        var newMessage = new Message(message.Title, message.Body, DateTime.Now, DateTime.Now)
        {
            AuthorId = message.AuthorId
        };

        _context.Add(newMessage);
        _context.SaveChanges();

        return new MessageDto()
        {
            Title = message.Title,
            Body = message.Body,
            AuthorId = message.AuthorId,
            Id = message.Id
        };
    }
}