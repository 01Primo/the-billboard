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

        public async IAsyncEnumerable<TEntity> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector, IEnumerable<(string, object)> parameters = default!)
        {

            await using var connection = new SqlConnection(_connectionString);

            await using var command = new SqlCommand(query, connection);

            if (parameters is not null)
            {
                foreach (var item in parameters)
                    command.Parameters.AddWithValue(item.Item1, item.Item2);
            }
            await connection.OpenAsync();
            await using var dr = command.ExecuteReader();
            while (await dr.ReadAsync())
            {
                var queryResult = selector(dr);
                yield return queryResult;
            }

            await connection.CloseAsync();
            await connection.DisposeAsync();
        }
    }
}
