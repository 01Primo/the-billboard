namespace TheBillboard.API.Abstract;

using Dtos;

public interface IMessageRepository
{
    Task<IEnumerable<MessageDto>> GetAll();
    Task<MessageDto?> GetById(int id);
    Task<MessageDto> Create(MessageDto message);
    Task<MessageDto> Update(MessageDto msgDto);
    Task Delete(int id);
}