namespace TheBillboard.API.Repositories;

using Abstract;
using Domain;
using Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBillboard.API.Data;

public class MessageRepository : IMessageRepository
{
    //private readonly TheBillboardDbContext _context;

    //public MessageRepository(TheBillboardDbContext context)
    //{
    //    _context = context;
    //}
    //public async Task<IEnumerable<MessageDtoWithDateAndAuthor>> GetAll()
    //{
    //    const string query = @"SELECT M.Id
    //                            , M.Title
    //                            , M.Body
    //                            , M.AuthorId 
    //                            , A.Name
    //                            , A.Surname
    //                            , A.Mail as Email
    //                            , M.CreatedAt as MessageCreatedAt
    //                            , M.UpdatedAt as MessageUpdatedAt
    //                            , A.CreatedAt as AuthorCreatedAt
    //                            , A.UpdatedAt as AuthorUpdatedAt
    //                             FROM Message M JOIN Author A ON A.Id = M.AuthorId";

    //    var messageList = await _reader.QueryAsync<Message>(query);
    //    var messageDtoList = new List<MessageDtoWithDateAndAuthor>();
    //    foreach (var message in messageList)
    //    {
    //        messageDtoList.Add(
    //            new MessageDtoWithDateAndAuthor()
    //            {
    //                Title = message.Title,
    //                Body = message.Body,
    //                AuthorId = message.AuthorId,
    //                Id = message.Id,
    //                CreatedAt = message.CreatedAt,
    //                UpdatedAt = message.UpdatedAt,
    //                Author = new()
    //                {
    //                    Id = message.AuthorId,
    //                    Name = message.Author.Name,
    //                    Surname = message.Author.Surname,
    //                    Mail = message.Author.Mail,
    //                    CreatedAt = message.Author.CreatedAt,
    //                    UpdatedAt = message.Author.UpdatedAt
    //                }
    //            }
    //        );
    //    }
    //    return messageDtoList;
    //}

    //public async Task<MessageDtoWithDateAndAuthor?> GetById(int id)
    //{
    //    const string query = @"SELECT M.Id
    //                            , M.Title
    //                            , M.Body
    //                            , M.AuthorId 
    //                            , A.Name
    //                            , A.Surname
    //                            , A.Mail as Email
    //                            , M.CreatedAt as MessageCreatedAt
    //                            , M.UpdatedAt as MessageUpdatedAt
    //                            , A.CreatedAt as AuthorCreatedAt
    //                            , A.UpdatedAt as AuthorUpdatedAt
    //                             FROM Message M JOIN Author A ON A.Id = M.AuthorId                               
    //                             WHERE M.Id=@Id";

    //    var newMessage = await _reader.GetByIdAsync<Message>(query, id);
    //    return new MessageDtoWithDateAndAuthor()
    //    {
    //        Title = newMessage.Title,
    //        Body = newMessage.Body,
    //        AuthorId = newMessage.AuthorId,
    //        Id = id,
    //        CreatedAt = newMessage.CreatedAt,
    //        UpdatedAt = newMessage.UpdatedAt,
    //        Author = new()
    //        {
    //            Id = newMessage.AuthorId,
    //            Name = newMessage.Author.Name,
    //            Surname = newMessage.Author.Surname,
    //            Mail = newMessage.Author.Mail,
    //            CreatedAt = newMessage.Author.CreatedAt,
    //            UpdatedAt = newMessage.Author.UpdatedAt
    //        }
    //    };
    //}

    //public async Task<MessageDto> Create(MessageDto message)
    //{

    //    const string query = @"INSERT INTO Message
    //                        (Title, Body, CreatedAt, UpdatedAt, AuthorId)
    //                        OUTPUT INSERTED.Id
    //                        VALUES (@Title, @Body, @CreatedAt, @UpdatedAt, @AuthorId)";

    //    var newMessage = new Message(default, message.Title, message.Body, message.AuthorId, default, DateTime.Now, DateTime.Now);
    //    var newId = await _writer.WriteAndReturnIdAsync<Message>(query, newMessage);

    //    return new MessageDto()
    //    {
    //        Title = newMessage.Title,
    //        Body = newMessage.Body,
    //        AuthorId = newMessage.AuthorId,
    //        Id = newId
    //    };

    //}
    //public async Task<MessageDto> Update(MessageDto message)
    //{
    //    var updatedID = message.Id;
    //    const string query = @"UPDATE Message
    //                        SET Title = @Title, Body = @Body, UpdatedAt = @UpdatedAt, AuthorId = @AuthorId
    //                        WHERE Id = @Id";

    //    var newMessage = new Message(updatedID, message.Title, message.Body, message.AuthorId, default, default, DateTime.Now);
    //    var result = await _writer.UpdateAsync<Message>(query, newMessage);

    //    return new MessageDto()
    //    {
    //        Title = newMessage.Title,
    //        Body = newMessage.Body,
    //        AuthorId = newMessage.AuthorId,
    //        Id = updatedID,
    //    };
    //}

    //public async Task<bool> Delete(int id)
    //{
    //    const string query = @"DELETE FROM Message
    //                        WHERE Id = @Id";

    //    var newMessage = new Message(id, default, default, default, default, default, default);
    //    return await _writer.DeleteAsync<Message>(query, newMessage);
    //}
    public Task<MessageDto> Create(MessageDto message)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MessageDtoWithDateAndAuthor>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<MessageDtoWithDateAndAuthor?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<MessageDto> Update(MessageDto message)
    {
        throw new NotImplementedException();
    }
}