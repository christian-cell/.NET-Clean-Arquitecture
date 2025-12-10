using CleanArquitecture.Application.Commands.Auth.Login;
using CleanArquitecture.Application.Services.Auth;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Auth.Token
{
    public class RefreshTokenHandler: IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(
            ITokenService tokenService
        )
        {
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            return await _tokenService.RefreshTokenAsync(command.RefreshToken!);
        }
    }
};

