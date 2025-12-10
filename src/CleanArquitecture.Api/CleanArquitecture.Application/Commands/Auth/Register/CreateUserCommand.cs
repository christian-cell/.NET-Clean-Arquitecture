using System.Text.Json.Serialization;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Commands.Auth.Register
{
    public class CreateUserCommand: IRequest<CreateUserResponse>
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("prefix")]
        public string? Prefix { get; set; }

        [JsonPropertyName("salt")]
        public string? Salt { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("passwordConfirm")]
        public string? PasswordConfirm { get; set; }

        [JsonPropertyName("passwordHash")]
        public string? PasswordHash { get; set; }

        [JsonPropertyName("active")]
        public bool IsActive { get; set; }

        // Constructor parameterless para que System.Text.Json pueda instanciar la clase desde JSON
        public CreateUserCommand() { }

        // Constructor existente que sigue siendo usable en el código interno
        public CreateUserCommand(Guid id, string firstName, string lastName, string documentNumber, string email, string prefix,
            string phone, string password, string passwordConfirm, bool isActive, string salt, string passwordHash)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DocumentNumber = documentNumber ?? throw new ArgumentNullException(nameof(documentNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            PasswordConfirm = passwordConfirm ?? throw new ArgumentNullException(nameof(passwordConfirm));
            IsActive = isActive;
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }
    }
};

/*
 * Sample
 {
  "firstName": "christian",
  "lastName": "garcia",
  "documentNumber": "234234234r",
  "email": "cgarcia@",
  "phone": "string",
  "password": "1234",
  "passwordConfirm": "123"
}
 */

