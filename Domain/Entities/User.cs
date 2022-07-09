using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public string? Club { get; set; }
        public string? City { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime DateOfAppending { get; set; }
        public string? PasswordHash { get; set; }

        public virtual List<Tournament>? Tournaments { get; set; }
        public virtual List<Tournament>? CreatedTournaments { get; set; }
    }
}
