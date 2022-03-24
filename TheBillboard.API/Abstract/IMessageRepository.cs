using TheBillboard.API.Domain;

namespace TheBillboard.API.Abstract
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAll();
        Message? GetBtId(int id);
    }
}
