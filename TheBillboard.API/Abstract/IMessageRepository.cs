namespace TheBillboard.API.Abstract;

using Domain;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAll();
    Task<Message?> GetById(int id);
}