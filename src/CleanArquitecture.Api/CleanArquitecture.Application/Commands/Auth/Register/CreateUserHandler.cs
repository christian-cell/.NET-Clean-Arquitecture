using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Application.Services.Auth;
using Mm.Game.Application.Abstractions.Auth;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Auth.Register
{
    public class CreateUserHandler: IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private IUserService _userService;
        private ICryptographyService _cryptographyService;

        public CreateUserHandler( IUserService userService, ICryptographyService cryptographyService)
        {
            _userService = userService;
            _cryptographyService = cryptographyService;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userService.CheckUserExists(command.Email);
             
            if (userExists)throw new AlreadyExistsException("User" , "Email", command.Email);
            
            string salt = _cryptographyService.GenerateSalt();

            command.Salt = salt;
            command.PasswordHash = _cryptographyService.CreateHash(command.Password!, salt);
            command.IsActive = true;

            var userId = await _userService.CreateUser(command);

            var registerResponse = CreateUserResponse.GetCreateUserResponse
                .SetFirstName(command.FirstName)
                .SetLastName(command.LastName)
                .SetDocumentNumber(command.DocumentNumber)
                .SetEmail(command.Email)
                .SetPhone(command.Phone)
                .SetSalt(salt)
                .SetPasswordHash(command.PasswordHash)
                .SetActive(true)
                .SetStatus(1)
                .SetId(userId)
                .Build();

            return registerResponse;
        }
    }
};

