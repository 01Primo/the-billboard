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

        public async IAsyncEnumerable<TEntity> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector, IEnumerable<(string, object)> parameters = default!)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(query, connection);

            if (parameters is not null)            
                foreach (var item in parameters)
                    command.Parameters.AddWithValue(item.Item1, item.Item2);
            
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

        public async Task<IEnumerable<TEntity>> QueryWithDapper<TEntity>(string query, DynamicParameters? parameters = null)
        {
            await using var connection = new SqlConnection(_connectionString);
            IEnumerable<TEntity>? result = default;
            try
            {
                result = parameters is null ?
            await connection.QueryAsync<TEntity>(query) :
            await connection.QueryAsync<TEntity>(query, parameters);
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
            }            
            return result!;
        }
    }
}
