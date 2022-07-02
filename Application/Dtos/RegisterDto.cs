using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record RegisterDto(string FirstName, string LastName, string Email, string RoleName, string? Club, string? City, DateTime? Birthdate, string Password, string ConfirmPassword);
}
