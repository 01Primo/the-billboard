using System.Data;
public interface IWriter
{
    Task<bool> WriteAsync(string query, IEnumerable<(string, object)> parameters);
    Task<bool> DeleteAsync(string query, IEnumerable<(string, object)> parameters);
    Task<bool> UpdateAsync(string query, IEnumerable<(string, object)> parameters);
}