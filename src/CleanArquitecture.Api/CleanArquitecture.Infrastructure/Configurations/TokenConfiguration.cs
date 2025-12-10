namespace CleanArquitecture.Infrastructure.Configurations
{
    public class TokenConfiguration
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Expiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
            
        public TokenConfiguration(string key, string audience, string issuer, int expiration,
            int refreshTokenExpiration)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Audience = audience ?? throw new ArgumentNullException(nameof(audience));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Expiration = expiration;
            RefreshTokenExpiration = refreshTokenExpiration;
        }
    }
};

