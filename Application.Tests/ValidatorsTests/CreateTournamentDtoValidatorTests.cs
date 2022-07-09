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
    public class CreateTournamentDtoValidatorTests : IClassFixture<CreateTournamentDtoValidator>
    {
        private readonly CreateTournamentDtoValidator _validator;

        public CreateTournamentDtoValidatorTests(CreateTournamentDtoValidator validator)
        {
            _validator = validator;
        }

        public static IEnumerable<object[]> InvalidPostalCodes()
        {
            yield return new object[] { "" };
            yield return new object[] { "ab-cde" };
            yield return new object[] { "000-22" };
            yield return new object[] { "xd" };
        }

        public static IEnumerable<object[]> ValidDatesFrom()
        {
            yield return new object[] { DateTime.UtcNow.AddDays(1) };
            yield return new object[] { DateTime.UtcNow.AddDays(2) };
            yield return new object[] { DateTime.UtcNow.AddDays(7) };
            yield return new object[] { DateTime.UtcNow.AddDays(250) };
        }

        public static IEnumerable<object[]> InvalidDatesFrom()
        {
            yield return new object[] { DateTime.UtcNow.AddDays(-1) };
            yield return new object[] { DateTime.UtcNow.AddDays(-2) };
            yield return new object[] { DateTime.UtcNow.AddDays(-7) };
            yield return new object[] { DateTime.UtcNow.AddDays(-250) };
            yield return new object[] { DateTime.UtcNow };
        }

        public static IEnumerable<object[]> ValidPrices()
        {
            yield return new object[] { null };
            yield return new object[] { 0M };
            yield return new object[] { 2.99M };
        }

        [Fact]
        public void Validate_ForValidName_ShouldNotReturnAnyValidationErrorsForName()
        {
            var model = new CreateTournamentDto() { Name = "somevalid name" };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Name);
        }

        [Theory]
        [InlineData("muuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuch tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooong naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee")]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_ForInvalidName_ShouldReturnValidationErrorForName(string name)
        {
            var model = new CreateTournamentDto() { Name = name };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Name);
        }

        [Theory]
        [InlineData("some valid city")]
        [InlineData(null)]
        public void Validate_ForValidCity_ShouldNotReturnAnyValidationErrorForCity(string city)
        {
            var model = new CreateTournamentDto() { City = city };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.City);
        }

        [Fact]
        public void Validate_ForInvalidCity_ShouldReturnValidationErrorForCity()
        {
            var model = new CreateTournamentDto() { City = "tooooooooooooooooooo looooooooooooooooooooooooooooooooong ciiiiiiiiiiiiiiityyyyyyyyyyyy" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.City);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("89-329")]
        [InlineData("00-123")]
        public void Validate_ForValidPostalCode_ShouldNotReturnAnyValidationErrorForPostalCode(string postalCode)
        {
            var model = new CreateTournamentDto() { PostalCode = postalCode };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.PostalCode);
        }

        [Theory]
        [MemberData(nameof(InvalidPostalCodes))]
        public void Validate_ForInvalidPostalCode_ShouldReturnValidationErrorForPostalCode(string postalCode)
        {
            var model = new CreateTournamentDto() { PostalCode = postalCode };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.PostalCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("some valid street")]
        public void Validate_ForValidStreet_ShouldNotReturnAnyValidationErrorForStreet(string street)
        {
            var model = new CreateTournamentDto { Street = street };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Street);
        }

        [Fact]
        public void Validate_ForInvalidStreet_ShouldReturnValidationErrorForStreet()
        {
            var model = new CreateTournamentDto() { Street = "Tooooooooooooooooo looooooooooooooooooooong streeeeeeeeeeeettttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.Street);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("1A")]
        [InlineData("abc")]
        public void Validate_ForValidBuildingNumber_ShouldNotReturnAnyValidationErrorForBuildingNumber(string buildingNumber)
        {
            var model = new CreateTournamentDto() { BuildingNumber = buildingNumber };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.BuildingNumber);
        }

        [Fact]
        public void Validate_ForInvalidBuildingNumber_ShouldReturnValidationErrorForBuildingNumber()
        {
            var model = new CreateTournamentDto() { BuildingNumber = "too loong nuumber" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.BuildingNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("1A")]
        [InlineData("abc")]
        public void Validate_ForValidLocalNumber_ShouldNotReturnAnyValidationErrorForBuildingNumber(string localNumber)
        {
            var model = new CreateTournamentDto() { LocalNumber = localNumber };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.LocalNumber);
        }

        [Fact]
        public void Validate_ForInvalidLocalNumber_ShouldReturnValidationErrorForBuildingNumber()
        {
            var model = new CreateTournamentDto() { LocalNumber = "too loong nuumber" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.LocalNumber);
        }

        [Theory]
        [MemberData(nameof(ValidDatesFrom))]
        public void Validate_ForValidDateFrom_ShouldNotReturnAnyValidationErrorForDateFrom(DateTime dateFrom)
        {
            var model = new CreateTournamentDto() { DateFrom = dateFrom };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.DateFrom);
        }

        [Theory]
        [MemberData(nameof(InvalidDatesFrom))]
        public void Validate_ForInvalidDateFrom_ShouldReturnValidationErrorForDateFrom(DateTime dateFrom)
        {
            var model = new CreateTournamentDto() { DateFrom = dateFrom };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.DateFrom);
        }

        [Fact]
        public void ValidateForValidDateTo_ShouldNotReturnAnyValidationErrorForDateTo()
        {
            var model = new CreateTournamentDto
            {
                DateFrom = DateTime.UtcNow,
                DateTo = DateTime.UtcNow.AddDays(1)
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.DateTo);
        }

        [Fact]
        public void Validate_ForInvalidDateTo_ShouldReturnValidationErrorForDateTo()
        {
            var model = new CreateTournamentDto
            {
                DateFrom = DateTime.UtcNow,
                DateTo = DateTime.UtcNow.AddDays(-1)
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.DateTo);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(2)]
        [InlineData(5)]
        public void Validate_ForValidMaxPlayers_ShouldNotReturnAnyValidationErrorForMaxPlayers(int? maxPlayers)
        {
            var model = new CreateTournamentDto() { MaxPlayers = maxPlayers };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.MaxPlayers);
        }
       
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-3)]
        public void Validate_ForInalidMaxPlayers_ShouldReturnValidationErrorForMaxPlayers(int? maxPlayers)
        {
            var model = new CreateTournamentDto() { MaxPlayers = maxPlayers };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.MaxPlayers);
        }

        [Theory]
        [MemberData(nameof(ValidPrices))]
        public void Validate_ForValidPrice_ShouldNotReturnAnyValidationErrorForPrice(decimal? price)
        {
            var model = new CreateTournamentDto() { CostPerPlayer = price };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.CostPerPlayer);
        }

        [Fact]
        public void Validate_ForInvalidPrice_ShouldReturnValidationErrorForPrice()
        {
            var model = new CreateTournamentDto() { CostPerPlayer = -1M };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.CostPerPlayer);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("some valid desc")]
        public void Validate_ForValidDescription_ShouldNotReturnValidationErrorForDescriptipon(string desc)
        {
            var model = new CreateTournamentDto() { Description = desc };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.Description);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(30)]
        public void Validate_ForValidNumberOfRounds_ShouldNotReturnAnyValidationErrorForNumberOfRounds(int numberOfRounds)
        {
            var model = new CreateTournamentDto() { NumberOfRounds = numberOfRounds };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(m => m.NumberOfRounds);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-30)]
        [InlineData(0)]
        public void Validate_ForInvalidNumberOfRounds_ShouldReturnValidationErrorForNumberOfRounds(int numberOfRounds)
        {
            var model = new CreateTournamentDto() { NumberOfRounds = numberOfRounds };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(m => m.NumberOfRounds);
        }
    }
}
