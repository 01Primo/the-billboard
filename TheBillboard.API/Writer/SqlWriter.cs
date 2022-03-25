namespace TheBillboard.API.Writers;

using Dapper;
using System.Data.SqlClient;
using Abstract;
using Microsoft.Extensions.Options;
using Options;

public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.DefaultDatabase;
    }

    public async Task<int?> CreateAsync(string query, object parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteScalarAsync<int>(query, parameters);
    }

    public async Task<int> WriteAsync(string query, object parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(query, parameters);
    }
}