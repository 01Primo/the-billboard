namespace TheBillboard.Models
{
    public record MessageCreationViewModel(Message Message, IAsyncEnumerable<Author> Authors);
}
