using CleanArquitecture.Domain.Entities;

namespace CleanArquitecture.Domain.Builders
{
    public class UserSessionBuilder
    {
        private readonly UserSession _userSession;

        private UserSessionBuilder()
        {
            _userSession = new UserSession(
                Guid.Empty,
                string.Empty,
                DateTime.MinValue,
                false
            );
        }

        public static UserSessionBuilder CreateNewSession() => new UserSessionBuilder();

        public UserSessionBuilder SetUserId(Guid userId)
        {
            _userSession.UserId = userId;
            return this;
        }

        public UserSessionBuilder SetRefreshToken(string refreshToken)
        {
            _userSession.RefreshToken = refreshToken;
            return this;
        }

        public UserSessionBuilder SetRefreshTokenExpirationDate(DateTime expirationDate)
        {
            _userSession.RefreshTokenExpirationDate = expirationDate;
            return this;
        }

        public UserSessionBuilder SetUsed(bool used)
        {
            _userSession.Used = used;
            return this;
        }

        public UserSession Build() => _userSession;
    }
};

