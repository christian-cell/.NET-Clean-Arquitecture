using CleanArquitecture.Domain.Entities;

namespace CleanArquitecture.Domain.Interfaces
{
    public interface ISessionRepository
    {
        Task<List<UserSession>> SearchAsync(string? refresToken, Guid? userId);
        Task<bool> UpdateAsync(Guid sessionId, UserSession session);
        Task AddAsync(UserSession session);
        Task<bool> SaveAsync();
    }
};

