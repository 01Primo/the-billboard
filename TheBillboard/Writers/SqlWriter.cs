using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.Options;

namespace TheBillboard.Writers;

public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter(IOptions<ConnectionStringOptions> options) => _connectionString = options.Value.DefaultDatabase;

    private async Task<bool> WriteAsync(string query, IEnumerable<(string, object)> parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(query, connection);

        foreach (var item in parameters)
        {
            var newParameter = new SqlParameter(item.Item1, typeMap[item.Item2.GetType()]);
            newParameter.Value = item.Item2;
            if (item.Item2 is String)
                newParameter.Size = (item.Item2 as String)!.Length;

            command.Parameters.Add(newParameter);
        }

        await connection.OpenAsync();
        await command.PrepareAsync();
        await command.ExecuteNonQueryAsync();

        return true;
    }
    public async Task<bool> CreateAsync(string query, IEnumerable<(string, object)> parameters) => await WriteAsync(query, parameters);

    public async Task<bool> DeleteAsync(string query, IEnumerable<(string, object)> parameters) => await WriteAsync(query, parameters);

    public async Task<bool> UpdateAsync(string query, IEnumerable<(string, object)> parameters) => await WriteAsync(query, parameters);

    private static Dictionary<Type, SqlDbType> typeMap = new()
    {
        [typeof(int)] = SqlDbType.Int,
        [typeof(string)] = SqlDbType.NVarChar,
        [typeof(DateTime)] = SqlDbType.DateTime
    };

    //Query without PrepareAsync method

    //private async Task<bool> WriteAsync(string query, IEnumerable<(string, object)> parameters)
    //{
    //    await using var connection = new SqlConnection(_connectionString);
    //    await using var command = new SqlCommand(query, connection);

    //    foreach (var item in parameters)
    //        command.Parameters.AddWithValue(item.Item1, item.Item2);

    //    await connection.OpenAsync();   
    //    await command.ExecuteNonQueryAsync();
    //    return true;
    //}
}