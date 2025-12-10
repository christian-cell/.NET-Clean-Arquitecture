namespace CleanArquitecture.Infrastructure.Configurations
{
    public class UseLimitsConfiguration
    {
        public int DelayBetweenRequests { get; set; }
        public int MaxFailedAttempts { get; set; }
        public int UserNumberLimit  { get; set; }
    }
};

