using TheBillboard.Models;

namespace TheBillboard.Abstract;

public interface IMessageGateway
{
    IAsyncEnumerable<Message> GetAll();
    Task<Message>? GetById(int id);
    Task<bool> Create(Message message);
    Task<bool> Update(Message message);
    Task<bool>Delete(int id);
}