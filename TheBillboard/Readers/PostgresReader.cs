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
    public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string query, DynamicParameters? parameters = null)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        IEnumerable<TEntity>? result = default;
        return parameters is null ?
               await connection.QueryAsync<TEntity>(query) :
               await connection.QueryAsync<TEntity>(query, parameters);
    }
}