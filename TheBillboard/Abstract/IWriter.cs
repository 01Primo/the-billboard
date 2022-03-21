using Dapper;
using System.Data;
public interface IWriter
{
    Task<bool> CreateAsync(string query, DynamicParameters parameters);
    Task<bool> DeleteAsync(string query, DynamicParameters parameters);
    Task<bool> UpdateAsync(string query, DynamicParameters parameters);
}