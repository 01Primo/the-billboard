using System.ComponentModel.DataAnnotations;

namespace TheBillboard.MVC.Options;

public class ConnectionStringOptions
{
    [Required]
    public string DefaultDatabase { get; set; } = null!;    
    public string PostgreDatabase { get; set; } = null!;
}