using TheBillboard.MVC.Models;

namespace TheBillboard.MVC.Abstract
{
    public interface IAuthorGateway
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author>? GetById(int id);
        Task<bool> Create(Author author);
        Task<bool> Delete(int id);
    }
}