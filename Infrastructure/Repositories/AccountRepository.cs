using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ChessTournamentDbContext _dbContext;

        public AccountRepository(ChessTournamentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserCredentailsInfo> GetByEmailAsync(string email)
        {
            var credInfo =  (from u in _dbContext.Users
                             select new UserCredentailsInfo()
                             {
                                 Id = u.Id,
                                 Email = u.Email,
                                 Role = u.RoleName,
                                 PasswordHash = u.PasswordHash
                             }).FirstOrDefault(u => u.Email == email);

            if (credInfo == null)
            {
                throw new InvalidEmailException();
            }

            return credInfo;
        }
    }
}
