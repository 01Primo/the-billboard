using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheBillboard.Abstract;
using TheBillboard.Gateways;
using TheBillboard.Models;

namespace TheBillboard.Controllers;

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