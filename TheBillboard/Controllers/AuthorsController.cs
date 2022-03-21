using Microsoft.AspNetCore.Mvc;
using TheBillboard.Abstract;
using TheBillboard.Models;

namespace TheBillboard.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorGateway _authorGateway;

        public AuthorsController(IAuthorGateway authorGateway)
        {
            _authorGateway = authorGateway;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _authorGateway.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _authorGateway.Create(author);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id is not null)
            {
                return View(await _authorGateway.GetById((int)id)!);
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorGateway.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
