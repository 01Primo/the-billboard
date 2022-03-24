using System.ComponentModel.DataAnnotations;

namespace TheBillboard.MVC.Options;

public class AppOptions
{
    [Required, MinLength(5)]
    public IEnumerable<string> Students { get; set; } = null!;
}