using System.Data;
public interface IWriter
{
    Task<bool> CreateAsync(string query, IEnumerable<(string, object)> parameters);
    Task<bool> DeleteAsync(string query, IEnumerable<(string, object)> parameters);
    Task<bool> UpdateAsync(string query, IEnumerable<(string, object)> parameters);
}