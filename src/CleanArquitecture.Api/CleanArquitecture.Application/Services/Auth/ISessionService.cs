
using CleanArquitecture.Domain.Entities;

namespace CleanArquitecture.Application.Services.Auth
{
    public interface ISessionService
    {
        Task UpdateSessionAsync(UserSession session);
        bool CheckIfRefreshTokenExpired(UserSession session);
        Task<UserSession?> GetLastCustomerSessionAsync(string? refreshToken, Guid? customerId);
        Task<bool> InsertNewSessionAsync(Guid userId, string refreshToken);
    
    }
};

