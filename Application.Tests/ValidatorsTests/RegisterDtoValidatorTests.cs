using Application.Dtos;
using Application.Tests.TestData;
using Application.Validation;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.ValidatorsTests
{
    public class RegisterDtoValidatorTests
    {
        private readonly RegisterDtoValidator _validator;

        public RegisterDtoValidatorTests()
        {
            _validator = new RegisterDtoValidator(OkEmailValidationHelper());
        }

        public static IEnumerable<object[]> InvalidDates()
        {
            yield return new object[] { DateTime.UtcNow.Date };
            yield return new object[] { DateTime.UtcNow.Date.AddDays(5) };
            yield return new object[] { DateTime.UtcNow.Date.AddDays(20) };
        }

        public static IEnumerable<object[]> InvalidPasswords()
        {
            yield return new object[] { "InvalidPassword123" };
            yield return new object[] { "!InvalidPassword" };
            yield return new object[] { "!invalidpassword123" };
            yield return new object[] { "!INVALIDPASSWORD123" };
            yield return new object[] { "!sH2" };
            yield return new object[] { "!!!!!PPPPPdfdffdd21893791283792187391298391287398217392712" };
        }

        [Fact]
        public void Validate_ForValidModel_ShouldReturnSuccess()
        {
            var model = GetValidRegisterDto();
            
            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [ClassData(typeof(InvalidFirstNameRegisterDto))]
        public void Validate_ForNotValidFirstName_ReturnsProperValidationResult(RegisterDto model)
        {
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.FirstName);
        }

        [Theory]
        [ClassData(typeof(InvalidLastNameRegisterDto))]
        public void Validate_ForNotValidLastName_ShouldReturnProperValidationResult(RegisterDto model)
        {
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.LastName);
        }

        [Theory]
        [ClassData(typeof(InvalidEmailRegisterDto))]
        public void Validate_ForInvalidButNotTakenEmail_ShouldReturnProperValidationRetult(RegisterDto model)
        {
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Email);
        }

        [Fact]
        public void Validate_ForValidButTakenEmail_ShouldReturnAProperValidationResult()
        {
            var model = GetValidRegisterDto();
            var validator = new RegisterDtoValidator(NotOkEmailValidationHelper());

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.Email);
        }

        [Fact]
        public void Validate_ForInvalidRoleName_ShouldReturnProperValidationResult()
        {
            var model = GetValidRegisterDto() with { RoleName = "Bad role" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.RoleName);
        }

        [Fact]
        public void Validate_ForTooLongClub_ReturnsProperValidationResult()
        {
            var model = GetValidRegisterDto() with { Club = "Too loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong cluuuuuuuuub" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.Club);
        }

        [Fact]
        public void Validate_ForTooLongCity_ShouldReturnProperValidationResult()
        {
            var model = GetValidRegisterDto() with { City = "Tooooooo looooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong cityyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.City); 
        }

        [Theory]
        [MemberData(nameof(InvalidDates))]
        public void Validate_ForBirthdateNotErlierThanNow_ShouldReturnProperValidationResult(DateTime date)
        {
            var model = GetValidRegisterDto() with { Birthdate = date };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.Birthdate);
        }

        [Theory]
        [MemberData(nameof(InvalidPasswords))]
        public void Validate_ForInvalidPassword_ShouldReturnProperValidationResult(string password)
        {
            var model = GetValidRegisterDto() with { Password = password, ConfirmPassword = password };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.Password);
        }

        [Fact]
        public void Validate_ForBadlyConfirmedPassword_SHouldReturnProperValidationResult()
        {
            var model = GetValidRegisterDto() with { ConfirmPassword = "Bad" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(r => r.ConfirmPassword);
        }

        public static RegisterDto GetValidRegisterDto()
        {
            return new RegisterDto("Sherlock", "Holmes", "valid@email.com", "Arbiter", null, null, null, "!ValidPassword123", "!ValidPassword123");
        }

        private IEmailValidationHelper OkEmailValidationHelper()
        {
            var mock = new Mock<IEmailValidationHelper>();
            mock.Setup(m => m.IsTaken(It.IsAny<string>())).Returns(false);
            return mock.Object;
        }

        private IEmailValidationHelper NotOkEmailValidationHelper()
        {
            var mock = new Mock<IEmailValidationHelper>();
            mock.Setup(m => m.IsTaken(It.IsAny<string>())).Returns(true);
            return mock.Object;
        }
    }
}
