using Microsoft.AspNetCore.Mvc;
using TheBillboard.Abstract;
using TheBillboard.Models;
using TheBillboard.ViewModels;

namespace TheBillboard.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorGateway _authorGateway;
        private readonly IMessageGateway _messageGateway;

        public AuthorsController(IAuthorGateway authorGateway, IMessageGateway messageGateway)
        {
            _authorGateway = authorGateway;
            _messageGateway = messageGateway;
        }

        public async Task<IActionResult> Index()
        {
            var authors = _authorGateway.GetAll();
            var messages = _messageGateway.GetAll();
            var authorIndexViewModelList = new List<AuthorIndexViewModel>();
            await foreach(var author in authors)           
            {
                var isDeletable = !(await messages.AnyAsync(t => t.AuthorId == author.Id));
                authorIndexViewModelList.Add(new AuthorIndexViewModel(author, isDeletable));
            }

            return View(authorIndexViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
                return View();
            

            await _authorGateway.Create(author);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id is not null)
                return View(await _authorGateway.GetById((int)id)!);
            

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorGateway.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
