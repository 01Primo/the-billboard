namespace TheBillboard.API.Abstract;

using Dtos;

public interface IMessageRepository
{
    Task<IEnumerable<MessageDtoWithDateAndAuthor>> GetAll();
    Task<MessageDtoWithDateAndAuthor?> GetById(int id);
    Task<MessageDto> Create(MessageDto message);
    Task<MessageDto> Update(MessageDto message);
    Task<bool> Delete(int id);
}