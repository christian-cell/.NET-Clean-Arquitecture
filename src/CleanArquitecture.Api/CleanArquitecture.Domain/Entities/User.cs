using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArquitecture.Domain.Entities
{
    public class User: EntityBase
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(12)]
        public string DocumentNumber { get; set; }
        [MaxLength(60)]
        public string Email { get; set; }
        [MaxLength(14)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Salt { get; set; }
        [MaxLength(100)]
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        
        [NotMapped]
        public virtual ICollection<UserSession> UserSessions { get; set; }
        
        // Constructor parameterless requerido por EF Core
        protected User() { }
    
        public User(string firstName, string lastName, string documentNumber, string email, string phone, string salt,
            string passwordHash, bool active, Guid id)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            DocumentNumber = documentNumber ?? throw new ArgumentNullException(nameof(documentNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Active = active;
        }

        public override string ToString()
        {
            return $"{nameof(FirstName)} : {FirstName}, {nameof(LastName)} : {LastName}, {nameof(DocumentNumber)} : {DocumentNumber}, {nameof(Email)} : {Email},  {nameof(Phone)} : {Phone}, {nameof(Salt)} : {Salt}, {nameof(PasswordHash)} : {PasswordHash}, {nameof(Active)} : {Active} ";
        }
    }
};

