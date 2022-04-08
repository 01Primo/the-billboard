namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;
using TheBillboard.API.Data;

public class MessageRepository : IMessageRepository
{
    private readonly BillboardDbContext _context;

    public MessageRepository(BillboardDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Message>> GetAll()
    {
        return await _context.Messages.ToListAsync();
    }

    public async Task<Message?> GetById(int id)
    {
        return await _context.Messages.Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MessageDto?> Create(MessageDto message)
    {
        var messageEntity = new Message
        {
            Title = message.Title,
            Body = message.Body,
            AuthorId = message.AuthorId,
        };

        _context.Messages.Add(messageEntity);
        await _context.SaveChangesAsync();

        return message with { Id = messageEntity.Id };
    }

    public async Task<bool> Delete(int id)
    {
        _context.Messages.Remove(new Message() { Id = id });
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MessageDto?> Update(int id, MessageDto message)
    {
        var messageEntity = new Message
        {
            Id = id,
            Title = message.Title,
            Body = message.Body,
            AuthorId = message.AuthorId,
            UpdatedAt = DateTime.Now
        };
        _context.Messages.Update(messageEntity);
        await _context.SaveChangesAsync();

        return message with { Id = id };
    }
}