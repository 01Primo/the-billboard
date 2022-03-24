using Dapper;
using System.Data;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Models;

namespace TheBillboard.MVC.Gateways
{
    public class AuthorGateway : IAuthorGateway
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;

        public AuthorGateway(IReader reader, IWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            const string query = "SELECT * FROM Author";
            return await _reader.QueryAsync<Author>(query);
        }

        public async Task<Author>? GetById(int id)
        {
            const string query = $"SELECT * FROM Author WHERE id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            var result = await _reader.QueryAsync<Author>(query, parameters);
            return result.First();
        }

        public Task<bool> Create(Author author)
        {
            const string query = @"INSERT INTO [dbo].[Author] ([Name],[Surname]) VALUES (@Name, @Surname)";
         
            var parameters = new DynamicParameters();
            parameters.Add("Name", author.Name, DbType.String);
            parameters.Add("Surname", author.Surname, DbType.String);

            return _writer.CreateAsync(query, parameters);
        }

        public async Task<bool> Delete(int id)
        {
            const string query = $"DELETE FROM [dbo].[Author] WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            return await _writer.DeleteAsync(query, parameters);
        }

        private Author Map(IDataReader dr)
        {
            return new Author
            {
                Id = dr["Id"] as int?,
                Name = dr["name"].ToString()!,
                Surname = dr["surname"].ToString()!
            };
        }
    }
}
