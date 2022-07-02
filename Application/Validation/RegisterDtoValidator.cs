using Application.Dtos;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        private readonly IEmailValidationHelper _emailValidationHelper;
        private readonly string[] _allowedRoles = new[] { "Arbiter", "Player" };

        public RegisterDtoValidator(IEmailValidationHelper emailValidationHelper)
        {
            _emailValidationHelper = emailValidationHelper;

            RuleForFirstName();
            RuleForLastName();
            RuleForEmail();
            RuleForRoleName();
            RuleForClub();
            RuleForCity();
            RuleForBirthDate();
            RuleForPassword();
            RuleForConfirmPassword();
        }

        private void RuleForFirstName()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("First name must have a value")
                .MaximumLength(20)
                .WithMessage("First name bust be shorter or equal to 20 characters");
        }

        private void RuleForLastName()
        {
            RuleFor(r => r.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Last name must have a value")
                .MaximumLength(20)
                .WithMessage("Last name bust be shorter or equal to 20 characters");
        }

        private void RuleForEmail()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Emai must have a value")
                .EmailAddress()
                .WithMessage("This is not a valid email")
                .Must(e => !_emailValidationHelper.IsTaken(e))
                .WithMessage("That email is already taken");
        }

        private void RuleForRoleName()
        {
            RuleFor(r => r.RoleName)
                .Must(r => _allowedRoles.Contains(r))
                .WithMessage($"Role name must be in [{string.Join(',', _allowedRoles)}]");
        }

        private void RuleForClub()
        {
            RuleFor(r => r.Club)
                .MaximumLength(50)
                .WithMessage("Club must be shorter or equal to 50");
        }

        private void RuleForCity()
        {
            RuleFor(r => r.City)
                .MaximumLength(50)
                .WithMessage("City must be shorter or equal to 50");
        }

        private void RuleForBirthDate()
        {
            RuleFor(r => r.Birthdate)
                .Must(d => d is null || d.Value.Date < DateTime.UtcNow.Date)
                .WithMessage("Bihdate must be erlier than now");
        }

        private void RuleForPassword()
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$");
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password must not be empty")
                .Must(p => p is null || regex.Match(p).Success)
                .WithMessage("Password must be between 8 and 30 characters long, must contain uppercase and lowercase letters, at least one digit and at least one special character");
        }

        private void RuleForConfirmPassword()
        {
            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password)
                .WithMessage("Confirm password must be equal to password");
        }
    }
}
