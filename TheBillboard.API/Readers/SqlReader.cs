namespace TheBillboard.API.Readers;

using System.Data;
using System.Data.SqlClient;
using Abstract;
using Dapper;
using Microsoft.Extensions.Options;
using Options;

public class SqlReader : IReader
{
    private readonly string _connectionString;

    public SqlReader(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.DefaultDatabase;
    }

    public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query)
    {
        await using var conn = new SqlConnection {ConnectionString = _connectionString};
        return await conn.QueryAsync<TEntity>(query, commandType: CommandType.Text, commandTimeout: 10);
    }

    public async Task<TEntity?> GetByIdAsync<TEntity>(string query, int id)
    {
        await using var conn = new SqlConnection {ConnectionString = _connectionString};
        IEnumerable<TEntity?> result = await conn.QueryAsync<TEntity>(query, param: new { Id = id }, commandType: CommandType.Text, commandTimeout: 10);
        
        return result.FirstOrDefault();
    }
}