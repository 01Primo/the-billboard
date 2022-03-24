namespace TheBillboard.API.Abstract;

using Domain;
using Dtos;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAll();
    Task<Message?> GetById(int id);
    MessageDto Create(MessageDto message);
}