using Application.Dtos;
using Application.Validation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.ValidatorsTests
{
    public class LoginDtoValidatorTests : IClassFixture<LoginDtoValidator>
    {
        private readonly LoginDtoValidator _validator;

        public LoginDtoValidatorTests(LoginDtoValidator validator)
        {
            _validator = validator;
        }

        [Fact]
        public void Validate_ForValidModel_ReturnsProperValidationResult()
        {
            var model = new LoginDto("email", "password");

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_ForInvalidEmail_ReturnsProperValidationResult(string email)
        {
            var model = new LoginDto(email, "password");

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(l => l.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_ForInvalidPassword_ReturnsProperValidationResult(string password)
        {
            var model = new LoginDto("email", password);

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(l => l.Password);
        }
    }
}
