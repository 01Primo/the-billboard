namespace TheBillboard.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Abstract;
using Dtos;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
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
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorDto author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = await _authorRepository.Create(author);

            return Ok(created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AuthorDto author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updated = await _authorRepository.Update(author);
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
            var success = await _authorRepository.Delete(id);
            return success ? Ok() : Problem(statusCode: StatusCodes.Status500InternalServerError);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
