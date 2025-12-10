using CleanArquitecture.Application.Commands.Auth.Login;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Auth.Token
{
    public class RefreshTokenCommand: IRequest<LoginResponse>
    {
        public string? RefreshToken { get; set; }
    }
};

