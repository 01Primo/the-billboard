namespace TheBillboard.API.Controllers;

using Abstract;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMessageRepository _messageRepository;

    public MessageController(ILogger<MessageController> logger, IMessageRepository messageRepository)
    {
        _logger = logger;
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
            _logger.LogError(e, "Error getting message by id");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
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
            if (e.InnerException is SqlException { Number: 547 })
            {
                return BadRequest(new { Error = "Cannot create message with non existing author" });
            }
            _logger.LogError(e, "Error creating message");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
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
            _logger.LogError(e, "Error deleting message");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
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
            _logger.LogError(e, "Error updating message");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}