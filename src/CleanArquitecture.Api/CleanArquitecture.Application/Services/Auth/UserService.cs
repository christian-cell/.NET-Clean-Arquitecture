using System.Security;
using CleanArquitecture.Application.Abstractions.Enums;
using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Application.Dtos;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Application.Extensions;
using CleanArquitecture.Domain.Builders;
using CleanArquitecture.Domain.Entities;
using CleanArquitecture.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mm.Game.Application.Abstractions.Auth;

namespace CleanArquitecture.Application.Services.Auth
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserService> _logger;
        private readonly ICryptographyService _cryptographyService;

        public UserService(
            ICryptographyService cryptographyService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<UserService> logger, IUserRepository userRepository
            )
        {
            _cryptographyService = cryptographyService;
            _logger = logger;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CheckUserExists(string email)
        {
        
            var customer = await _userRepository.GetByEmailAsync(email);

            if (customer == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }

        public async Task<Guid> CreateUser( CreateUserCommand command )
        {
            var user = UserBuilder.CreateNewUser()
                .SetFirstName(command.FirstName)
                .SetDocumentNumber(command.DocumentNumber)
                .SetEmail(command.Email)
                .SetPhone(command.Phone)
                .SetSalt(command.Salt!)
                .SetPasswordHash(command.PasswordHash!)
                .SetIsActive(true)
                .Build();
        
            var result = await _userRepository.AddAsync(user);
         
            bool ok = await _userRepository.SaveAsync().ConfigureAwait(false);
            
            if (!ok)
            {
                _logger.LogError("Something went wrong creating a new Customer");
 
                throw new Exception("Something went wrong creating a new Customer");
            }

            return result.Id;
        }
        
        public async Task<UserDto> CheckVerifiedUserAsync(string email, string password)
        {
            var user = await SearchUserByEmail(email);
            
            if (!user.Active) throw new SecurityException($"The user {email} has not been activated");
            
            var verified = _cryptographyService.ValidatePasswordAndHash(password, user.Salt, user.PasswordHash);

            if (!verified) throw new SecurityException($"Not valid credentials");

            return new UserDto(
                
                user.FirstName,
                user.LastName,
                user.DocumentNumber,
                user.Email,
                user.Phone,
                user.Active
            )
            {
                Id = user.Id,
                Salt = user.Salt,
                PasswordHash = user.PasswordHash
            };
        }
        
        public async Task<User> SearchUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            
            if (user is null) throw new NotFoundException("User","email",email);

            return user;
        }

        public async Task<User> SearchUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null) throw new NotFoundException("User", "id", id);
            return user;
        }

        public string GetUser()
        {

            return ClaimExtension.GetValueFromUserClaims<string>("user", _httpContextAccessor.HttpContext, UserClaim.Id);
        }

        
    }
};

