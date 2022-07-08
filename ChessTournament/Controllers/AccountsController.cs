using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChessTournament.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service)
        {
            _service = service;       
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterDto dto)
        {
            await _service.RegisterAsync(dto);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]LoginDto dto)
        {
            var token = await _service.GetTokenAsync(dto);

            return Ok(token);
        }
    }
}
