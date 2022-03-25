using Microsoft.AspNetCore.Mvc;
using TheBillboard.API.Abstract;
using TheBillboard.API.Dtos;

namespace TheBillboard.API.Controllers;

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
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }
}
