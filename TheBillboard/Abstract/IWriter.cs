using System.Data;
public interface IWriter
{
    Task<bool> WriteAsync(string query, IEnumerable<(string, object)> parameters);
}