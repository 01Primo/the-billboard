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

    public Task<IEnumerable<Message>> GetAll()
    {
        var messages = _context
            .Message
            .Join(_context.Author,
                  message => message.AuthorId,
                  author => author.Id,
                  (message, author) => new { Message = message, Author = author })
            .Select(messageAndAuthor => messageAndAuthor.Message);

        return (Task<IEnumerable<Message>>)messages;
    }

    public async Task<Message?> GetById(int id)
    {
        //TODO: make asynchronous
        var messages = _context
            .Message
            .Join(_context.Author,
                  message => message.AuthorId,
                  author => author.Id,
                  (message, author) => new { Message = message, Author = author })
            .Where(messageAndAuthor => messageAndAuthor.Message.AuthorId == id)
            .Select(messageAndAuthor => messageAndAuthor.Message);

        return (Message?)messages;
    }

    public MessageDto Create(MessageDto message)
    {
        var lastId = _context
            .Message
            .Select(c => c.Id)
            .Max();

        if (lastId is null) lastId = 0;
        var newId = lastId++;

        _context.Add(new Message(message.Title, message.Body, DateTime.Now, DateTime.Now));
        _context.SaveChangesAsync();

        return new MessageDto()
        {
            Title = message.Title,
            Body = message.Body,
            AuthorId = message.AuthorId,
            Id = newId
        };
    }
}