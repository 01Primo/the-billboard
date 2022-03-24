namespace TheBillboard.MVC.Models
{
    public record MessageCreationViewModel(Message Message, IEnumerable<Author> Authors);
}
