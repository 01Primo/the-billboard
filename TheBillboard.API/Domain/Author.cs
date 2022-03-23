using Microsoft.AspNetCore.Mvc;

namespace TheBillboard.Domain
{
    //todo creare costruttore con firma
    public record Author (string Name = "", string Surname = "", int? Id = default)
    {
        public override string ToString() => Name + " " + Surname;
    }
}