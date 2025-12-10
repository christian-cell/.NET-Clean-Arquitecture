using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Auth.Login
{
    public class LoginCommand: IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    
        public LoginCommand(string email, string password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
};