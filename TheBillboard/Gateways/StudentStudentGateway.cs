using Microsoft.Extensions.Options;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Options;

namespace TheBillboard.MVC.Gateways;

public class StudentStudentGateway : IStudentGateway
{
    private readonly ILogger<StudentStudentGateway> _logger;
    private readonly AppOptions _options;

    public StudentStudentGateway(IOptions<AppOptions> options, ILogger<StudentStudentGateway> logger)
    {
        _logger = logger;
        _options = options.Value;
    }
    
    public IEnumerable<string> GetStudents()
    {
        _logger.LogInformation("GetStudents Called!");
        return _options.Students;
    }
}