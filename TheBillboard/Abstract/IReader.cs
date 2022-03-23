using Dapper;
using System.Data;

namespace TheBillboard.MVC.Abstract;

public interface IReader
{
    IAsyncEnumerable<TEntity> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector, IEnumerable<(string, object)> parameters = default!);
    Task<IEnumerable<TEntity>> QueryWithDapper<TEntity>(string query, DynamicParameters? parameters = null);
}