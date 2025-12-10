using CleanArquitecture.Application.Commands.Auth.Login;
using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Application.Commands.Auth.Token;
using Microsoft.AspNetCore.Mvc;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterUserAsync")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPost("AuthAsync")]
        public async Task<IActionResult> AuthAsync([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
    
            return new OkObjectResult(response);
        }
        
        [HttpPost("RefreshTokenAsync")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return new OkObjectResult(response);
        }
    }
};

