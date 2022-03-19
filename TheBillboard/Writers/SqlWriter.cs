using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TheBillboard.Options;

namespace TheBillboard.Writers;

public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter(IOptions<ConnectionStringOptions> options)
    {
        _connectionString = options.Value.DefaultDatabase;
    }

    public async Task<bool> WriteAsync(string query, IEnumerable<(string, object)> parameters)
    {

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(query, connection);


        foreach (var item in parameters)
        {

            var newParameter = new SqlParameter(item.Item1, typeMap[item.Item2.GetType()]);
            newParameter.Value = item.Item2;
            if (item.Item2 is String)
                newParameter.Size = (item.Item2 as String).Length;

            command.Parameters.Add(newParameter);
        }
        await connection.OpenAsync();
        try
        {
            await command.PrepareAsync();
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


        return true;
    }
    static Dictionary<Type, SqlDbType> typeMap = new()
    {
        [typeof(int)] = SqlDbType.Int,
        [typeof(string)] = SqlDbType.NVarChar,
        [typeof(DateTime)] = SqlDbType.DateTime
    };
}

