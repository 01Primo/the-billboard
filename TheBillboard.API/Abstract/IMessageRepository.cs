namespace TheBillboard.API.Abstract;

using Domain;
using Dtos;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAll();
    Task<Message?> GetById(int id);
    Task<MessageDto?> Create(MessageDto message);
    Task<bool> Delete(int id);
    Task<MessageDto?> Update(int id, MessageDto message);
}