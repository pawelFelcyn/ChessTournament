using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(IAccountRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public Task<string> GetTokenAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.DateOfAppending = DateTime.UtcNow;
            await _repository.AddAsync(user);
        }
    }
}
