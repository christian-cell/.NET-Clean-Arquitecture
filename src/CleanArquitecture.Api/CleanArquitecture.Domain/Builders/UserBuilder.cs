using CleanArquitecture.Domain.Entities;

namespace CleanArquitecture.Domain.Builders
{
    public class UserBuilder
    {
        /*new builder*/
        private readonly User _user = new User("", "", "", "", "", "", "", false, Guid.NewGuid());

        public UserBuilder SetFirstName(string firstName) { _user.FirstName = firstName; return this; }
        public UserBuilder SetId(Guid id) { _user.Id = id; return this; }
        public UserBuilder SetLastName(string lastName) { _user.LastName = lastName; return this; }
        public UserBuilder SetDocumentNumber(string doc) { _user.DocumentNumber = doc; return this; }
        public UserBuilder SetEmail(string email) { _user.Email = email; return this; }
        public UserBuilder SetPhone(string phone) { _user.Phone = phone; return this; }
        public UserBuilder SetSalt(string salt) { _user.Salt = salt; return this; }
        public UserBuilder SetPasswordHash(string hash) { _user.PasswordHash = hash; return this; }
        public UserBuilder SetIsActive(bool active) { _user.Active = active; return this; }

        public User Build() => _user;

        public static UserBuilder CreateNewUser() => new UserBuilder();
    }
};