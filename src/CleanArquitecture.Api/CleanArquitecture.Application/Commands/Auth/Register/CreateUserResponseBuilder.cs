namespace CleanArquitecture.Application.Commands.Auth.Register
{
    public class CreateUserResponseBuilder
    {
        public CreateUserResponse CreateUserResponse = new CreateUserResponse(
            "",
            "",
            "",
            "",
            "",
            "",
            "", 
            false, 
            0 , 
            new Guid()
        );

        public CreateUserResponse Build()
        {
            return CreateUserResponse;
        }
    }
    
    public class CreateUserResponseFirstNameBuilder<TSelf> : CreateUserResponseBuilder
        where TSelf : CreateUserResponseFirstNameBuilder<TSelf>
    {
        public TSelf SetFirstName(string firstName)
        {
            CreateUserResponse.FirstName = firstName;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseLastNameBuilder<TSelf> : CreateUserResponseFirstNameBuilder<CreateUserResponseLastNameBuilder<TSelf>>
        where TSelf : CreateUserResponseLastNameBuilder<TSelf>
    {
        public TSelf SetLastName(string lastName)
        {
            CreateUserResponse.LastName = lastName;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseDocumentNumberBuilder<TSelf> : CreateUserResponseLastNameBuilder<CreateUserResponseDocumentNumberBuilder<TSelf>>
        where TSelf : CreateUserResponseDocumentNumberBuilder<TSelf>
    {
        public TSelf SetDocumentNumber(string documentNumber)
        {
            CreateUserResponse.DocumentNumber = documentNumber;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseEmailBuilder<TSelf> : CreateUserResponseDocumentNumberBuilder<CreateUserResponseEmailBuilder<TSelf>>
        where TSelf : CreateUserResponseEmailBuilder<TSelf>
    {
        public TSelf SetEmail(string email)
        {
            CreateUserResponse.Email = email;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponsePhoneBuilder<TSelf> : CreateUserResponseEmailBuilder<CreateUserResponsePhoneBuilder<TSelf>>
        where TSelf : CreateUserResponsePhoneBuilder<TSelf>
    {
        public TSelf SetPhone(string phone)
        {
            CreateUserResponse.Phone = phone;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseSaltBuilder<TSelf> : CreateUserResponsePhoneBuilder<CreateUserResponseSaltBuilder<TSelf>>
        where TSelf : CreateUserResponseSaltBuilder<TSelf>
    {
        public TSelf SetSalt(string salt)
        {
            CreateUserResponse.Salt = salt;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponsePasswordHashBuilder<TSelf> : CreateUserResponseSaltBuilder<CreateUserResponsePasswordHashBuilder<TSelf>>
        where TSelf : CreateUserResponsePasswordHashBuilder<TSelf>
    {
        public TSelf SetPasswordHash(string passwordHash)
        {
            CreateUserResponse.PasswordHash = passwordHash;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseActiveBuilder<TSelf> : CreateUserResponsePasswordHashBuilder<CreateUserResponseActiveBuilder<TSelf>>
        where TSelf : CreateUserResponseActiveBuilder<TSelf>
    {
        public TSelf SetActive(bool active)
        {
            CreateUserResponse.Active = active;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseStatusBuilder<TSelf> : CreateUserResponseActiveBuilder<CreateUserResponseStatusBuilder<TSelf>>
        where TSelf : CreateUserResponseStatusBuilder<TSelf>
    {
        public TSelf SetStatus(int status)
        {
            CreateUserResponse.Status = status;
            return (TSelf)this;
        }
    }
    
    public class CreateUserResponseIdBuilder<TSelf> : CreateUserResponseStatusBuilder<CreateUserResponseIdBuilder<TSelf>>
        where TSelf : CreateUserResponseIdBuilder<TSelf>
    {
        public TSelf SetId(Guid id)
        {
            CreateUserResponse.Id = id;
            return (TSelf)this;
        }
    }
};

