using Application.Authentication;
using Application.Dtos;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IPasswordHasher<UserCredentailsInfo> _credHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(IAccountRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher, 
            IPasswordHasher<UserCredentailsInfo> credHasher, AuthenticationSettings authenticationSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _credHasher = credHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> GetTokenAsync(LoginDto dto)
        {
            var credInfo = await _repository.GetByEmailAsync(dto.Email);

            var verificationResult = _credHasher.VerifyHashedPassword(credInfo, credInfo.PasswordHash, dto.Password);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, credInfo.Id.ToString()),
                new Claim(ClaimTypes.Role, credInfo.Role),
                new Claim("Email", credInfo.Email)
            };

            var token = new JwtSecurityToken
                (_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
