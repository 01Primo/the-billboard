namespace TheBillboard.API.Abstract;

public interface IWriter
{
    Task<bool> WriteAsync(string query, object objectToBindToQuery);
    Task<bool> UpdateAsync(string query, object objectToBindToQuery);
    Task<bool> DeleteAsync(string query, object objectToBindToQuery);
    Task<int?> WriteAndReturnIdAsync(string query, object objectToBindToQuery);
}