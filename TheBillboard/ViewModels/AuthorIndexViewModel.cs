using TheBillboard.MVC.Models;

namespace TheBillboard.MVC.ViewModels
{
    public record AuthorIndexViewModel (Author Author, bool IsDeletable);    
}
