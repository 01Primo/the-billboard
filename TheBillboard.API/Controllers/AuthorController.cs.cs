using Microsoft.AspNetCore.Mvc;
using TheBillboard.API.Abstract;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ILogger<AuthorController> _logger;
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository, ILogger<AuthorController> logger)
    {
        _authorRepository = authorRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _authorRepository.GetAll());
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var author = await _authorRepository.GetById(id);

            return author is not null
                ? Ok(author)
                : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting author by id");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AuthorDto author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _authorRepository.Create(author);
            var url = $"{Request.Scheme}://{Request.Host}{Request.Path}";

            return created is not null 
                   ? Created($"{url}/{created.Id}", created)
                   : BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while creating author");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var success = await _authorRepository.Delete(id);

            return success
                ? Ok(id)
                : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting author");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromBody] AuthorDto author, int id)
    {
        try
        {
            var updated = await _authorRepository.Update(id, author);

            return updated is not null
                ? Ok(updated)
                : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating author");
            return Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
