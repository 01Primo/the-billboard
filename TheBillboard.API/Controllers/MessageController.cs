using Microsoft.AspNetCore.Mvc;
using TheBillboard.API.Abstract;
using TheBillboard.Domain;

namespace TheBillboard.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        [HttpGet]
        public IEnumerable<Message> GetAll()
        {
            return _messageRepository.GetAll();
        }

        [HttpGet("{id:int}")]
        public Message GetById(int id)
        {
            return _messageRepository.GetBtId(id);
        }
    }
}
