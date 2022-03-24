using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using TheBillboard.MVC.Options;

namespace TheBillboard.MVC.Writers;

public class PostgresWriter : IWriter
{
    private readonly string _connectionString;

    public PostgresWriter(IOptions<ConnectionStringOptions> options) => _connectionString = options.Value.PostgreDatabase;
    private async Task<bool> WriteAsync(string query, DynamicParameters parameters)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(query, parameters);
        return true;
    }
    public async Task<bool> CreateAsync(string query, DynamicParameters parameters) => await WriteAsync(query, parameters);
    public async Task<bool> DeleteAsync(string query, DynamicParameters parameters) => await WriteAsync(query, parameters);
    public async Task<bool> UpdateAsync(string query, DynamicParameters parameters) => await WriteAsync(query, parameters);
}