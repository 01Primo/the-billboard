using Microsoft.AspNetCore.Mvc;

namespace TheBillboard.MVC.Models
{
    public record Author
    {
        public Author(
            string name = "",
            string surname = "",
            int? id = default)
        {
            Name = name;
            Surname = surname;
            Id = id;
        }

        public string Name { get; init; }
        public string Surname { get; init; }
        public int? Id { get; init; }

        public override string ToString() => Name + " " + Surname;
    }
}