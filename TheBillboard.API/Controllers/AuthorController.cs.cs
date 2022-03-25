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
    public IActionResult Create([FromBody] AuthorDto author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var created = _authorRepository.Create(author);

            return Ok(created);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] AuthorDto author)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updated = _authorRepository.Update(author);
            return Ok(updated);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updated = _authorRepository.Delete(id);
            return Ok(updated);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
