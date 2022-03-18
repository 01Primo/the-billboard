using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.Abstract;
using TheBillboard.Options;

namespace TheBillboard.Readers
{
    public class SqlReader : IReader
    {
        private readonly string _connectionString;

        public SqlReader(IOptions<ConnectionStringOptions> options)
        {
            _connectionString = options.Value.DefaultDatabase;
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector)
        {
            var queryResults = new HashSet<TEntity>();

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(query, connection);

            await connection.OpenAsync();
            await using var dr = command.ExecuteReader();
            while (await dr.ReadAsync())
            {
                var queryResult = selector(dr);
                queryResults.Add(queryResult);
            }

            await connection.CloseAsync();
            await connection.DisposeAsync();

            return queryResults;
        }
    }
}
