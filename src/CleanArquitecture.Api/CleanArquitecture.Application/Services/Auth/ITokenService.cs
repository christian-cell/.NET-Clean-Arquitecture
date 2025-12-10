using CleanArquitecture.Application.Commands.Auth;
using CleanArquitecture.Application.Commands.Auth.Login;

namespace CleanArquitecture.Application.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(IDictionary<string, string> properties);
        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
        string GetMd5(string @string);
        string GenerateRefreshToken();
    }
};

