namespace TheBillboard.API.Controllers;

using Abstract;
using Bogus;
using Domain;
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
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] MessageDto message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _messageRepository.Create(message);
            var url = $"{Request.Scheme}://{Request.Host}{Request.Path}";

            return created is not null 
                   ? Created($"{url}/{created.Id}", created)
                   : BadRequest();
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var success = await _messageRepository.Delete(id);

            return success
                ? Ok(id)
                : NotFound();
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromBody] MessageDto message, int id)
    {
        try
        {
            var updated = await _messageRepository.Update(id, message);

            return updated is not null
                ? Ok(updated)
                : NotFound();
        }
        catch (Exception e)
        {
            return Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}