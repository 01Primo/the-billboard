namespace TheBillboard.MVC.Abstract;

public interface IStudentGateway
{
    IEnumerable<string> GetStudents();
}