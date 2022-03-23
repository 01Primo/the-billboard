using Microsoft.AspNetCore.Mvc;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Models;
using TheBillboard.MVC.ViewModels;

namespace TheBillboard.MVC.Controllers;

public class MessagesController : Controller
{
    private readonly IMessageGateway _messageGateway;
    private readonly IAuthorGateway _authorGateway;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageGateway messageGateway, ILogger<MessagesController> logger, IAuthorGateway authorGateway)
    {
        _logger = logger;
        _messageGateway = messageGateway;
        _authorGateway = authorGateway;
    }

    public async Task<IActionResult> Index()
    {
        var messages = await  _messageGateway.GetAllAsync();
        var authors = await _authorGateway.GetAllAsync();

        var messagesWithAuthor = messages.Select(message => MatchAuthorToMessage(message, authors));

        var createViewModel = new MessageCreationViewModel(new Message(), authors);
        var indexModel = new MessagesIndexViewModel(createViewModel, messagesWithAuthor);
        return View(indexModel);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? id)
    {
        var message = id is not null ? await _messageGateway.GetByIdAsync((int)id)! : new Message();

        if (message is null)
            return View("Error");
        else
        {
            var viewModel = new MessageCreationViewModel(message, await _authorGateway.GetAllAsync());
            return View(viewModel);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Message message)
    {
        if (!ModelState.IsValid)
            return View(new MessageCreationViewModel(message, await _authorGateway.GetAllAsync()));

        if (message.Id == default)
            await _messageGateway.CreateAsync(message);
        else
            await _messageGateway.UpdateAsync(message);

        _logger.LogInformation($"Message received: {message.Title}");
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Detail(int id)
    {
        var message = await _messageGateway.GetByIdAsync(id)!;
        if (message is null)
            return View("Error");

        var author = await _authorGateway.GetById(message.AuthorId)!;
        return View(new MessageWithAuthor(message, author));
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _messageGateway. DeleteAsync(id);
        return RedirectToAction("Index");
    }

    private MessageWithAuthor MatchAuthorToMessage(Message message, IEnumerable<Author> authors)
        => new MessageWithAuthor(message, authors.First(a => a.Id == message.AuthorId));
}