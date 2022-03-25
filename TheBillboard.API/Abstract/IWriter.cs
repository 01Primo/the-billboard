namespace TheBillboard.API.Abstract;

using Domain;
public interface IWriter
{
    Task<bool> WriteAsync<TEntity>(string query, TEntity objectToBindToQuery);
    Task<bool> UpdateAsync<TEntity>(string query, TEntity objectToBindToQuery);
    Task<bool> DeleteAsync<TEntity>(string query, TEntity objectToBindToQuery);
    Task<int?> WriteAndReturnIdAsync<TEntity>(string query, TEntity objectToBindToQuery);
}