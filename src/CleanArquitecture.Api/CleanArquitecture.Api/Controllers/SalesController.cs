using CleanArquitecture.Api.ActionFilters;
using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = AppConstants.AuthScheme)]
    public class SalesController: BaseController
    {
        private IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [EndpointRateLimitFilterAttribute(5, 5)]
        public async Task<IActionResult> CreateSale(CreateUserCommand createUserCommand)
        {
            await _mediator.Send(createUserCommand);
            return Ok();
        }
    }
};

