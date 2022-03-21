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

            var parametersTuple = new List<(string Name, object Value)>
            {
                (@"Name", author.Name),
                (@"Surname", author.Surname)
            };
            return _writer.CreateAsync(query, parametersTuple);
        }

        public async Task<bool> Delete(int id)
        {
            const string query = $"DELETE FROM [dbo].[Author] WHERE Id = @Id";
            var parametersTuple = new List<(string Name, object Value)>
            {
                (@"Id", id)
            };
            return await _writer.DeleteAsync(query, parametersTuple);
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
