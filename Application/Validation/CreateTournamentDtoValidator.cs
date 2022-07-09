using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class CreateTournamentDtoValidator : AbstractValidator<CreateTournamentDto>, IValidator<CreateTournamentDto>
    {
        public CreateTournamentDtoValidator()
        {
            RuleForName();
            RuleForCity();
            RuleForPostalCode();
            RuleForStreet();
            RuleForBuildingNumber();
            RuleForLocalNumber();
            RuleForDateFrom();
            RuleForDateTo();
            RuleForMaxPlayers();
            RuleForCostPerPlayer();
            RuleForDescription();
            RuleForNumberOfRounds();
        }

        private void RuleForName()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name of the tournament must not be empty")
                .MaximumLength(100)
                .WithMessage("Maximum length of the name is 100 characters");
        }

        private void RuleForCity()
        {
            RuleFor(c => c.City)
                .MaximumLength(50)
                .WithMessage("Maximum length of the city is 50 characters");
        }

        private void RuleForPostalCode()
        {
            var postalCodeRegex = new Regex("^[0-9]{2}-[0-9]{3}$");

            RuleFor(c => c.PostalCode)
                .Must(p => p is null || postalCodeRegex.Match(p).Success)
                .WithMessage("Postal code must be in format xx-xxx ('x' is a digit)");
        }

        private void RuleForStreet()
        {
            RuleFor(c => c.Street)
                .MaximumLength(50)
                .WithMessage("Maximum length of the street is 50 characters");
        }

        private void RuleForBuildingNumber()
        {
            RuleFor(c => c.BuildingNumber)
                .MaximumLength(10)
                .WithMessage("Maximum length of the building number is 10 characters");
        }

        private void RuleForLocalNumber()
        {
            RuleFor(c => c.LocalNumber)
                .MaximumLength(10)
                .WithMessage("Maximum length of the local number is 10 characters");
        }
        
        private void RuleForDateFrom()
        {
            RuleFor(c => c.DateFrom)
                .Must(d => d.Date > DateTime.UtcNow.Date)
                .WithMessage("Starting date must be later than now");
        }

        private void RuleForDateTo()
        {
            RuleFor(c => new { c.DateFrom, c.DateTo })
                .Custom((value, context) =>
                {
                    if (value.DateFrom >= value.DateTo)
                    {
                        context.AddFailure("DateTo", "Ending date must be later than starting date");
                    }
                });
        }

        private void RuleForMaxPlayers()
        {
            RuleFor(c => c.MaxPlayers)
                .GreaterThan(1)
                .WithMessage("Tournament must allow number of players greather than 1");
        }

        private void RuleForCostPerPlayer()
        {
            RuleFor(c => c.CostPerPlayer)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be a positive number");
        }

        private void RuleForDescription()
        {
            RuleFor(c => c.Description)
                .MaximumLength(500)
                .WithMessage("Maximum length of the description is 500 characters");
        }

        private void RuleForNumberOfRounds()
        {
            RuleFor(c => c.NumberOfRounds)
                .GreaterThan(0)
                .WithMessage("Number of rounds must be greather than 0");
        }
    }
}
