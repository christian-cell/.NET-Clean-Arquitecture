namespace CleanArquitecture.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string? Prefix { get; set; }
        public string Phone { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public bool Active { get; set; }
 
        public UserDto(string firstName, string lastName, string documentNumber, string email, string phone,
            bool active)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DocumentNumber = documentNumber ?? throw new ArgumentNullException(nameof(documentNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Active = active;
        }
    }
};

