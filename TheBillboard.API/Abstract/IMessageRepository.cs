namespace TheBillboard.API.Abstract;

using Domain;
using Dtos;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAll();
    Task<Message?> GetById(int id);
    Task<MessageDto> Create(MessageDto message);
    Task<MessageDto> Update(MessageDto message);
    Task<bool> Delete(int id);
}