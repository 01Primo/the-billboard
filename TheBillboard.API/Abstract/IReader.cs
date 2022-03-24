namespace TheBillboard.API.Abstract;

using System.Data;

public interface IReader
{
    public Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query);
    public Task<TEntity?> GetByIdAsync<TEntity>(string query, int id);
}