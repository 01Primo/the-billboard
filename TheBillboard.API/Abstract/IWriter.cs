using TheBillboard.API.Domain;

namespace TheBillboard.API.Abstract;

public interface IWriter
{
    Task<int> CreateAsync<TEntity>(string query, TEntity entity)
        where TEntity : EntityBase;
    Task<int> UpdateAsync<TEntity>(string query, TEntity entity)
        where TEntity : EntityBase;
    Task<int> DeleteByIdAsync(string query, int id);
}