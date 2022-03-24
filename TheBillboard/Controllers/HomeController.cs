using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Gateways;
using TheBillboard.MVC.Models;

namespace TheBillboard.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStudentGateway _studentGateway;
    
    public HomeController(IStudentGateway studentGateway, ILogger<HomeController> logger)
    {
        _logger = logger;
        _studentGateway = studentGateway;
    }

    public IActionResult Index() => View();
    
    public IActionResult Students() => View("Students", _studentGateway.GetStudents());

    public IActionResult About() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
}