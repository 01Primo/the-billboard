using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.MVC.Options;

namespace TheBillboard.MVC.Writers;

public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter(IOptions<ConnectionStringOptions> options) => _connectionString = options.Value.DefaultDatabase;

    private async Task<bool> WriteAsync(string query, DynamicParameters parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(query, parameters);
        return true;
    }
    public async Task<bool> CreateAsync(string query, DynamicParameters parameters) => await WriteAsync(query, parameters);

    public async Task<bool> DeleteAsync(string query, DynamicParameters parameters) => await WriteAsync(query, parameters);

    public async Task<bool> UpdateAsync(string query, DynamicParameters parameters) => await WriteAsync(query, parameters);    
}