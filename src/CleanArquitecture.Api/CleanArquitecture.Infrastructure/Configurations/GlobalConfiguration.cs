namespace CleanArquitecture.Infrastructure.Configurations;

public class GlobalConfiguration
{
    public AzureConfiguration Azure { get; set; } 
    public TokenConfiguration Token { get; set; }
    public UseLimitsConfiguration UseLimits { get; set; }
        
    public GlobalConfiguration(AzureConfiguration azure, TokenConfiguration token, UseLimitsConfiguration useLimits)
    {
        Azure = azure ?? throw new ArgumentNullException(nameof(azure));
        Token = token ?? throw new ArgumentNullException(nameof(token));
        UseLimits = useLimits ?? throw new ArgumentNullException(nameof(useLimits));
    }
}