using System.Text.Json.Serialization;

namespace CleanArquitecture.Application.Commands.Auth.Register
{
    public class CreateUserResponse
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("salt")]
        public string Salt { get; set; }

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        public CreateUserResponse(string firstName, string lastName, string documentNumber, string email, string phone,
            string salt, string passwordHash, bool active, int status, Guid id)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DocumentNumber = documentNumber ?? throw new ArgumentNullException(nameof(documentNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Active = active;
            Status = status;
            Id = id;
        }
        
        public class Builder: CreateUserResponseIdBuilder<Builder>;

        public static Builder GetCreateUserResponse => new Builder();
    }
};

