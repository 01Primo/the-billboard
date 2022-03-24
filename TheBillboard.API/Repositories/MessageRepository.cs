using TheBillboard.API.Abstract;
using TheBillboard.API.Domain;

namespace TheBillboard.API.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public IEnumerable<Message> GetAll()
        {
            return new Message[] {
            new()
            {
                Id = 1,
                Title = "Hello!",
                Body = "Hello World",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AuthorId = 1,
                Author = new Author()
                {
                    Id = 1,
                    Name = "Jhon",
                    Surname = "Doe"
                }
            },
            new()
            {
                Id = 2,
                Title = "Ciao!",
                Body = "Ciao mondo",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AuthorId = 2,
                Author = new Author()
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Doe"
                }
            }
            };
        }

        public Message? GetBtId(int id)
        {
            return new Message()
            {
                Id = 1,
                Title = "Hello!",
                Body = "Hello World",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AuthorId = 1,
                Author = new Author()
                {
                    Id = 1,
                    Name = "Jhon",
                    Surname = "Doe"
                }
            };
        }
    }
}
