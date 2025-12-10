namespace CleanArquitecture.Application.Commands.Auth.Login
{
    public class LoginResponseBuilder
    {
        public LoginResponse LoginResponse = new LoginResponse(
            new Guid(),
            "",
            "",
            "",
            "",
            new DateTime(),
            0,
            ""
        ) { };

        public LoginResponse Build()
        {
            return LoginResponse;
        }
    }

    public class LoginResponseUserIdBuilder<TSelf> : LoginResponseBuilder where TSelf : LoginResponseUserIdBuilder<TSelf>
    {
        public TSelf SetUserId(Guid userId)
        {
            LoginResponse.UserId = userId;
            return (TSelf)this;
        }
    }

    public class LoginResponseTokenBuilder<TSelf> : LoginResponseUserIdBuilder<LoginResponseTokenBuilder<TSelf>>
        where TSelf : LoginResponseTokenBuilder<TSelf>
    {
        public TSelf SetToken(string token)
        {
            LoginResponse.Token = token;
            return (TSelf)this;
        }
    }

    public class
        LoginResponseRefreshTokenBuilder<TSelf> : LoginResponseTokenBuilder<LoginResponseRefreshTokenBuilder<TSelf>>
        where TSelf : LoginResponseRefreshTokenBuilder<TSelf>
    {
        public TSelf SetRefreshToken(string refreshToken)
        {
            LoginResponse.RefreshToken = refreshToken;
            return (TSelf)this;
        }
    }

    public class
        LoginResponseAccessTokenBuilder<TSelf> : LoginResponseRefreshTokenBuilder<LoginResponseAccessTokenBuilder<TSelf>>
        where TSelf : LoginResponseAccessTokenBuilder<TSelf>
    {
        public TSelf SetAccessToken(string accessToken)
        {
            LoginResponse.AccessToken = accessToken;
            return (TSelf)this;
        }
    }

    public class LoginResponseMd5Builder<TSelf> : LoginResponseAccessTokenBuilder<LoginResponseMd5Builder<TSelf>>
        where TSelf : LoginResponseMd5Builder<TSelf>
    {
        public TSelf SetMd5(string md5)
        {
            LoginResponse.Md5 = md5;
            return (TSelf)this;
        }
    }

    public class LoginResponseExpirationBuilder<TSelf> : LoginResponseMd5Builder<LoginResponseExpirationBuilder<TSelf>>
        where TSelf : LoginResponseExpirationBuilder<TSelf>
    {
        public TSelf SetExpiration(DateTime expiration)
        {
            LoginResponse.Expiration = expiration;
            return (TSelf)this;
        }
    }

    public class LoginResponseLifetimeBuilder<TSelf> : LoginResponseExpirationBuilder<LoginResponseLifetimeBuilder<TSelf>>
        where TSelf : LoginResponseLifetimeBuilder<TSelf>
    {
        public TSelf SetLifetime(int lifetime)
        {
            LoginResponse.Lifetime = lifetime;
            return (TSelf)this;
        }
    }

    public class LoginResponseEmailAddressBuilder<TSelf> : LoginResponseLifetimeBuilder<LoginResponseEmailAddressBuilder<TSelf>>
        where TSelf : LoginResponseEmailAddressBuilder<TSelf>
    {
        public TSelf SetEmail(string email)
        {
            LoginResponse.Email = email;
            return (TSelf)this;
        }
    }
};