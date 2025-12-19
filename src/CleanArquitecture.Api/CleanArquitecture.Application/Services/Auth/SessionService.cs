using CleanArquitecture.Domain.Builders;
using CleanArquitecture.Domain.Entities;
using CleanArquitecture.Domain.Interfaces;
using CleanArquitecture.Infrastructure.Configurations;

namespace CleanArquitecture.Application.Services.Auth
{
    public class SessionService :ISessionService
    { 
        private readonly ISessionRepository _sessionRepository;
        private GlobalConfiguration _globalConfiguration;
        
        public SessionService(
            GlobalConfiguration globalConfiguration, ISessionRepository sessionRepository
            )
        {
            _globalConfiguration = globalConfiguration;
            _sessionRepository = sessionRepository;
        }
        
        public async Task UpdateSessionAsync(UserSession session)
        {
            var ok = await _sessionRepository.UpdateAsync(session.Id, session).ConfigureAwait(false);

            if (!ok) throw new Exception("Something went wrong updating Session");
            
            await _sessionRepository.SaveAsync().ConfigureAwait(false);
        }

        
        public bool CheckIfRefreshTokenExpired(UserSession session)
        {
            return DateTime.UtcNow > session.RefreshTokenExpirationDate || session.Used;
        }
        
        public async Task<UserSession?> GetLastCustomerSessionAsync(string? refreshToken, Guid? userId)
        {
            var sessions = await _sessionRepository.SearchAsync(refreshToken, userId);

            return sessions.FirstOrDefault();
        }
        
        public async Task<bool> InsertNewSessionAsync(Guid userId, string refreshToken)
        {
            var userSession = UserSessionBuilder.CreateNewSession()
                .SetUserId(userId)
                .SetRefreshToken(refreshToken)
                .SetRefreshTokenExpirationDate(DateTime.UtcNow.AddSeconds(_globalConfiguration.Token.RefreshTokenExpiration))
                .SetUsed(false)
                .Build();

            await _sessionRepository.AddAsync(userSession);

            if (!await _sessionRepository.SaveAsync()) throw new Exception("Failed to save new session");
           
            return true;
        }
    }
};