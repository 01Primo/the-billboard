namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;
using TheBillboard.API.Data;
using Microsoft.EntityFrameworkCore;

public class MessageRepository : IMessageRepository
{

    private readonly BillboardDbContext _context;

    public MessageRepository(BillboardDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MessageDto>> GetAll()
    {
        var result = _context.Message.Select(msg => new MessageDto(msg));
        return await result.ToListAsync();
    }

    public async Task<MessageDto?> GetById(int id)
    {
        var message = await _context.Message.FindAsync(id);
        return message is not null ? new MessageDto(message) : null;
    }

    public async Task<MessageDto> Create(MessageDto msgDto)
    {
        var newMessage = new Message(msgDto);
        newMessage.Id = null;

        var result = await _context.AddAsync(newMessage);
        await _context.SaveChangesAsync();

        return new MessageDto(result.Entity);
    }

    public async Task<MessageDto> Update(MessageDto msgDto)
    {
        var newMessage = new Message(msgDto);

        var result = _context.Update(newMessage);
        await _context.SaveChangesAsync();

        return new MessageDto(result.Entity);
    }

    public async Task Delete(int id)
    {
        //var newMessage = new Message();
        //await _context.Message.Remove(newMessage);
        //await _context.SaveChangesAsync();

        return;
    }
}