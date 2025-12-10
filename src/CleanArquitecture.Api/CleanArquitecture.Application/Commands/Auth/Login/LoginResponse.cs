namespace CleanArquitecture.Application.Commands.Auth.Login
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string? Md5 { get; set; }
        public DateTime Expiration { get; set; }
        public int Lifetime { get; set; }
        public string Email { get; set; }
        
        public LoginResponse(Guid userId, string token, string? refreshToken, string accessToken, string? md5,
            DateTime expiration, int lifetime, string email)
        {
            UserId = userId;
            Token = token ?? throw new ArgumentNullException(nameof(token));
            RefreshToken = refreshToken;
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            Md5 = md5;
            Expiration = expiration;
            Lifetime = lifetime;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public class Builder: LoginResponseEmailAddressBuilder<Builder>;

        public static Builder GetAuthResponse => new Builder();

        public override string ToString()
        {
            return $"{nameof(UserId)} : {UserId} , {nameof(Token)}: {Token} , {nameof(RefreshToken)}: {RefreshToken} ,  {nameof(AccessToken)}: {AccessToken} , {nameof(Md5)}: {Md5}";
        }
    }
};