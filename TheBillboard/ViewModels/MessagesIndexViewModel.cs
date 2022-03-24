using TheBillboard.MVC.Models;

namespace TheBillboard.MVC.ViewModels
{
    public record MessagesIndexViewModel(MessageCreationViewModel MessageCreationViewModel, IEnumerable<MessageWithAuthor> Messages);
}