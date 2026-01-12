using InternshipEx.Modules.Auth.Application.UseCases.LoginUser;
using InternshipEx.Modules.Auth.Application.UseCases.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternshipEx.Modules.Auth.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid request data.");
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid request data.");
            }
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest("Invalid username or password");
        }
    }
}
