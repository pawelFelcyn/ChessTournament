using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleForEmail();
            RuleForPassword();
        }

        private void RuleForEmail()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .WithMessage("Email must not be empty");
        }

        private void RuleForPassword()
        {
            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("Password must not be empty");
        }
    }
}
