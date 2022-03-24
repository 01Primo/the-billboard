namespace TheBillboard.API.Controllers;

using Abstract;
using Bogus;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _messageRepository.GetAll());
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var message = await _messageRepository.GetById(id);

            return message is not null
                ? Ok(message)
                : NotFound();    
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}