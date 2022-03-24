namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;

public class MessageRepository : IMessageRepository
{
    private readonly IReader _reader;

    public MessageRepository(IReader reader)
    {
        _reader = reader;
    }
    public Task<IEnumerable<Message>> GetAll()
    {
        const string query = "SELECT M.Id," +
                                " M.Title" +
                                ", M.Body" +
                                ", M.AuthorId" +
                                ", A.Name" +
                                ", A.Surname" +
                                ", A.Mail as Email" +
                                ", M.CreatedAt as MessageCreatedAt" +
                                ", M.UpdatedAt as MessageUpdatedAt" +
                                ", A.CreatedAt as AuthorCreatedAt" +
                                " " +
                                "FROM Message M JOIN Author A                           ON A.Id = M.AuthorId";

        return _reader.QueryAsync<Message>(query);
    }

    public async Task<Message?> GetById(int id)
    {
        const string query = "SELECT M.Id," +
                                " M.Title" +
                                ", M.Body" +
                                ", M.AuthorId" +
                                ", A.Name" +
                                ", A.Surname" +
                                ", A.Mail as Email" +
                                ", M.CreatedAt as MessageCreatedAt" +
                                ", M.UpdatedAt as MessageUpdatedAt" +
                                ", A.CreatedAt as AuthorCreatedAt" +
                                " " + 
                                "FROM Message M JOIN Author A                           ON A.Id = M.AuthorId" +
                                " " +
                                "WHERE M.Id=@Id";

        return await _reader.GetByIdAsync<Message>(query, id);
    }
}