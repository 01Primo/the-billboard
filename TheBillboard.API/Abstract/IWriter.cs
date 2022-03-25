namespace TheBillboard.API.Abstract;

public interface IWriter
{
    Task<int> WriteAsync(string query, object parameters);
    Task<int?> CreateAsync(string query, object parameters);
}