using System.Data;

namespace TheBillboard.Abstract;

public interface IReader
{
    IAsyncEnumerable<TEntity> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector, IEnumerable<(string, object)> parameters = default!);
}