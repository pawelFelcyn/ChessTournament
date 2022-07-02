using Application.Dtos;
using ChessTournament.Test.Helpers;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ChessTournament.Test.ControllersTests
{
    public class AccountsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly WebApplicationFactory<Program> _factory;
        private ChessTournamentDbContext _context;

        public AccountsControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextService = services.FirstOrDefault(s => s.ServiceType == typeof(ChessTournamentDbContext));
                    services.Remove(dbContextService);

                    var dbContextOptionBuilder = new DbContextOptionsBuilder();
                    dbContextOptionBuilder.UseInMemoryDatabase("ChessTournamentDb");
                    _context = new ChessTournamentDbContext(dbContextOptionBuilder.Options);
                    services.AddSingleton(_context);
                });
            });
        }

        [Fact]
        public async Task Register_ForIvalidBody_ReturnsBadRequestSatusCode()
        {
            var client = _factory.CreateClient();
            var model = new RegisterDto(null, null, null, null, null, null, null, null, null);

            var response = await client.PostAsync("/api/Accounts/register", model.ToJsonContent());

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Register_ForValidBody_RegistersNewUser()
        {
            var client = _factory.CreateClient();
            var model = new RegisterDto("Name", "Name", "email@email.com", "Arbiter", null, null, null, "!Password123", "!Password123");

            var response = await client.PostAsync("/api/Accounts/register", model.ToJsonContent());

            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
            var user = _context.Users.FirstOrDefault(u => u.Email == "email@email.com");
            user.Should().NotBeNull();
            user.FirstName.Should().Be("Name");
            user.LastName.Should().Be("Name");
            user.Email.Should().Be("email@email.com");
            user.RoleName.Should().Be("Arbiter");
            user.City.Should().BeNull();
            user.Club.Should().BeNull();
            user.Birthdate.Should().BeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
