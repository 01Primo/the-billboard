using Dapper;
using System.Data;
using TheBillboard.Abstract;
using TheBillboard.Models;

namespace TheBillboard.Gateways
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

        public IAsyncEnumerable<Author> GetAll()
        {
            const string query = "select * from Author";
            return _reader.QueryAsync(query, Map);
        }

        public async Task<Author>? GetById(int id)
        {
            const string query = $"select * from Author where id = @Id";
            var parametersTuple = new List<(string Name, object Value)>
            {
                (@"Id", id)
            };
            var result = await _reader.QueryAsync(query, Map, parametersTuple).ToListAsync();
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
