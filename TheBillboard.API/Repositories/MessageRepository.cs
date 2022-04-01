namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


public class MessageRepository : IMessageRepository
{
    private readonly TheBillboardDbContext _context;

    public MessageRepository(TheBillboardDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MessageDtoWithDateAndAuthor>> GetAll()
    {
        var messages = await _context.Message.Include(a => a.Author).ToListAsync();
        var result = messages.Select(message =>
        new MessageDtoWithDateAndAuthor()
        {
            Title = message.Title,
            Body = message.Body,
            AuthorId = message.AuthorId,
            Id = message.Id,
            CreatedAt = message.CreatedAt,
            UpdatedAt = message.UpdatedAt,
            Author = new()
            {
                Id = message.AuthorId,
                Name = message.Author.Name,
                Surname = message.Author.Surname,
                Mail = message.Author.Mail,
                CreatedAt = message.Author.CreatedAt,
                UpdatedAt = message.Author.UpdatedAt
            }
        }
        );
        return result;
    }

    public async Task<MessageDtoWithDateAndAuthor?> GetById(int id)
    {
        var newMessage = await _context.Message.Include(a => a.Author).SingleOrDefaultAsync(m => m.Id == id);
        return new MessageDtoWithDateAndAuthor()
        {
            Title = newMessage.Title,
            Body = newMessage.Body,
            AuthorId = newMessage.AuthorId,
            Id = id,
            CreatedAt = newMessage.CreatedAt,
            UpdatedAt = newMessage.UpdatedAt,
            Author = new()
            {
                Id = newMessage.AuthorId,
                Name = newMessage.Author.Name,
                Surname = newMessage.Author.Surname,
                Mail = newMessage.Author.Mail,
                CreatedAt = newMessage.Author.CreatedAt,
                UpdatedAt = newMessage.Author.UpdatedAt
            }
        };
    }

    public async Task<MessageDto> Create(MessageDto message)
    {
        var newMessage = new Message(default, message.Title, message.Body, message.AuthorId, DateTime.Now, DateTime.Now);
        var dbEntry = await _context.AddAsync(newMessage);

        await _context.SaveChangesAsync();

        return new MessageDto()
        {
            Title = newMessage.Title,
            Body = newMessage.Body,
            AuthorId = newMessage.AuthorId,
            Id = dbEntry.Entity.Id
        };
    }
    public async Task<MessageDto> Update(MessageDto message)
    {
        var  newMessage = new  Message(message.Id, message.Title, message.Body, message.AuthorId, UpdatedAt: DateTime.Now);
        _context.Entry(newMessage).Property(m => m.Title).IsModified = true;
        _context.Entry(newMessage).Property(m => m.Body).IsModified = true;
        _context.Entry(newMessage).Property(m => m.AuthorId).IsModified = true;
        _context.Entry(newMessage).Property(m => m.UpdatedAt).IsModified = true;
        await _context.SaveChangesAsync();

        return new MessageDto()
        {
            Title = newMessage.Title,
            Body = newMessage.Body,
            AuthorId = newMessage.AuthorId,
            Id = message.Id,
        };
    }

    public async Task<bool> Delete(int id)
    {
        var newMessage = new Message(id);
        _context.Message.Remove(newMessage);
        await _context.SaveChangesAsync();
        return true;
    }
}