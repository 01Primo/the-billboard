using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Options;
using Dapper;

namespace TheBillboard.MVC.Readers
{
    public class SqlReader : IReader
    {
        private readonly string _connectionString;
        public SqlReader(IOptions<ConnectionStringOptions> options) => _connectionString = options.Value.DefaultDatabase;
        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, DynamicParameters? parameters = null)
        {
            await using var connection = new SqlConnection(_connectionString);
            IEnumerable<TEntity>? result = default;
            return parameters is null ?
                   await connection.QueryAsync<TEntity>(query) :
                   await connection.QueryAsync<TEntity>(query, parameters);
        }
    }
}