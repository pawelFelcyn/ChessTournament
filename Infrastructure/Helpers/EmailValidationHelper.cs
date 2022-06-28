using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class EmailValidationHelper : IEmailValidationHelper
    {
        private readonly ChessTournamentDbContext _dbContext;

        public EmailValidationHelper(ChessTournamentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsTaken(string value)
        {
            return _dbContext.Users.Any(u => u.Email == value);
        }
    }
}
