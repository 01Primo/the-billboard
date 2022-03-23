using System.Data;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Npgsql;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Models;
using TheBillboard.MVC.Options;

namespace TheBillboard.MVC.Readers;

public class PostgresReader : IReader
{
    private readonly string _connectionString;

    public PostgresReader(IOptions<ConnectionStringOptions> options) => _connectionString = options.Value.PostgreDatabase;

    public async IAsyncEnumerable<TEntity> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector, IEnumerable<(string, object)> parameters = default!)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await using var command = new NpgsqlCommand(query, connection);

        await connection.OpenAsync();
        await using var dr = command.ExecuteReader();
        while (await dr.ReadAsync())
        {
            var message = selector(dr);
            yield return message;
        }

        await connection.CloseAsync();
        await connection.DisposeAsync();
    }

    public Task<IEnumerable<TEntity>> QueryWithDapper<TEntity>(string query, DynamicParameters? parameters = null)
    {
        throw new NotImplementedException();
    }
}