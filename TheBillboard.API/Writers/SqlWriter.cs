namespace TheBillboard.API.Writers;

using System.Data.SqlClient;
using Abstract;
using Dapper;
using Microsoft.Extensions.Options;
using Options;
using Domain;

public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.DefaultDatabase;
    }

    public async Task<int?> WriteAndReturnIdAsync<TEntity>(string query, TEntity objectToBindToQuery)
    {
        await using var connection = new SqlConnection(_connectionString);
        var insertedId = await connection.ExecuteScalarAsync(query,
                                                         objectToBindToQuery,
                                                         commandTimeout: 10) as int?;
        return insertedId;
    }
    public async Task<bool> WriteAsync<TEntity>(string query, TEntity objectToBindToQuery)
    {
        await using var connection = new SqlConnection(_connectionString);
        var affectedRows = await connection.ExecuteAsync(query,
                                                         objectToBindToQuery,
                                                         commandTimeout: 10);
        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync<TEntity>(string query, TEntity objectToBindToQuery) => await WriteAsync(query, objectToBindToQuery);
    public async Task<bool> DeleteAsync<TEntity>(string query, TEntity objectToBindToQuery)
    {
        await using var connection = new SqlConnection(_connectionString);
        var affectedRows = await connection.ExecuteAsync(query,
                                                         objectToBindToQuery,
                                                         commandTimeout: 10);
        return affectedRows > 0;
    }
}