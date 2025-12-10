using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CleanArquitecture.Application.Commands.Auth;
using CleanArquitecture.Application.Commands.Auth.Login;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Domain.Interfaces;
using CleanArquitecture.Infrastructure.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace CleanArquitecture.Application.Services.Auth
{
    public class TokenService :ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly ISessionService _sessionService;

        public TokenService(
            ISessionService sessionService,
            GlobalConfiguration globalConfiguration, IUserRepository userRepository
            )
        {
            _sessionService = sessionService;
            _globalConfiguration = globalConfiguration;
            _userRepository = userRepository;
        }

        public string GenerateToken(IDictionary<string, string> properties)
        {
            var keyByteArray = Encoding.UTF8.GetBytes(_globalConfiguration.Token.Key);

            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var expiration = DateTime.UtcNow.AddSeconds(_globalConfiguration.Token.Expiration);

            var claims = new List<Claim>();

            foreach (var property in properties)
            {
                claims.Add(new Claim(property.Key, property.Value));
            }

            var token = new JwtSecurityToken(
                issuer: _globalConfiguration.Token.Issuer,
                audience: _globalConfiguration.Token.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        
        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            var session = await _sessionService.GetLastCustomerSessionAsync(refreshToken, null);

            if (session == null) throw new NotFoundException("CustomerSession" , refreshToken); 
                
            var user = await _userRepository.GetByIdAsync(session.UserId);

            if (user == null)
            {
                throw new NotFoundException("User" , session.UserId);  
            }
        
            if(_sessionService.CheckIfRefreshTokenExpired(session))throw new UnauthorizedAccessException();
        
            var accessToken = GenerateToken(new Dictionary<string, string>()
            {
                { "id", user.Id.ToString() },
                { "email", user.Email },
                { "phone", user.Phone },
                { "firstName", user.FirstName },
                { "lastName", user.LastName },
                { "documentNumber", user.DocumentNumber }
            });
            
            var authResponse = LoginResponse.GetAuthResponse
                .SetUserId(user.Id)
                .SetToken(accessToken)
                .SetRefreshToken(refreshToken)
                .SetAccessToken(accessToken)
                .SetMd5(GetMd5(accessToken))
                .SetExpiration(DateTime.UtcNow.AddSeconds(_globalConfiguration.Token.Expiration))
                .SetLifetime(_globalConfiguration.Token.Expiration)
                .Build();
        
            return authResponse;
        }
        
        public string GetMd5(string @string)
        {
            var bytes = Encoding.UTF8.GetBytes(@string);

            using var md5 = MD5.Create();

            var hashBytes = md5.ComputeHash(bytes);

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
};

