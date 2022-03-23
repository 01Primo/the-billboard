using Dapper;
using System.Data;

namespace TheBillboard.MVC.Abstract;

public interface IReader
{
    Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, DynamicParameters? parameters = null);
}