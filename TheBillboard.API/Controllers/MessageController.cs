namespace TheBillboard.API.Controllers;

using Abstract;
using Dtos;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MessageDto message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _messageRepository.Create(message);
            
            return Created($"{this.Request.Scheme}://{this.Request.Host}{this.Request.Path}/{created.Id}", created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MessageDto message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updated = await _messageRepository.Update(message);
            return Ok(updated);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var success = await _messageRepository.Delete(id);
            return success ? Ok() : Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}