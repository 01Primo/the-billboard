namespace TheBillboard.MVC.Models
{
    public record MessageCreationViewModel(Message Message, IAsyncEnumerable<Author> Authors);
}
