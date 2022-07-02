using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserCredentailsInfo> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
