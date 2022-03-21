using Microsoft.AspNetCore.Mvc;

namespace TheBillboard.Models
{
    public record Author (string Name = "", string Surname = "", int? Id = default)
    {
        public override string ToString() => Name + " " + Surname;
    }
}