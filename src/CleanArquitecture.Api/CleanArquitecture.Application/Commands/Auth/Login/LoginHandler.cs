using CleanArquitecture.Application.Services.Auth;
using CleanArquitecture.Infrastructure.Configurations;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Auth.Login
{
    public class LoginHandler: IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly GlobalConfiguration _globalConfiguration;
            
        public LoginHandler(
            ISessionService sessionService,
            ITokenService tokenService,
            GlobalConfiguration globalConfiguration, 
            IUserService userService)
        {
            _globalConfiguration = globalConfiguration;
            _userService = userService;
            _tokenService = tokenService;
            _sessionService = sessionService;
        }
        
        public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var userDto = await _userService.CheckVerifiedUserAsync(command.Email, command.Password);
            
            var accessToken = _tokenService.GenerateToken(new Dictionary<string, string>()
            {
                { "email", userDto.Email },
                { "phone", userDto.Phone },
                { "firstName", userDto.FirstName },
                { "lastName", userDto.LastName },
                { "documentNumber", userDto.DocumentNumber }
            });
            
            if(string.IsNullOrEmpty(userDto.Salt)) throw new ArgumentException("Salt can't be null or empty");
            if(string.IsNullOrEmpty(userDto.PasswordHash)) throw new ArgumentException("PasswordHash can't be null or empty");
            
            string refreshToken;
            
            var lastSession = await _sessionService.GetLastCustomerSessionAsync(null, userDto.Id);

            if (lastSession == null)
            {
                refreshToken = _tokenService.GenerateRefreshToken();
                
                await _sessionService.InsertNewSessionAsync(userDto.Id, refreshToken);
            }
            else
            {
                if ( _sessionService.CheckIfRefreshTokenExpired(lastSession) )
                {
                    refreshToken = _tokenService.GenerateRefreshToken();
                
                    await _sessionService.InsertNewSessionAsync(userDto.Id, refreshToken);

                    lastSession.Used = true;
                
                    await _sessionService.UpdateSessionAsync(lastSession);
                }
                else
                {
                    refreshToken = lastSession.RefreshToken;
                }
            }

            var authResponse = LoginResponse.GetAuthResponse
                .SetUserId(userDto.Id)
                .SetToken(accessToken)
                .SetRefreshToken(refreshToken)
                .SetAccessToken(accessToken)
                .SetMd5(_tokenService.GetMd5(accessToken))
                .SetExpiration(DateTime.UtcNow.AddSeconds(_globalConfiguration.Token.Expiration))
                .SetLifetime(_globalConfiguration.Token.Expiration)
                .SetEmail(userDto.Email)
                .Build();
            
            return authResponse;
        }
    }
};

