namespace CleanArquitecture.Application.Exceptions
{
    public class RateLimitException : Exception
    {
        public double? RemainingSeconds { get; }
        public int? MaxFailureAttemps { get; }
        public int? Attempts { get; }
        public RateLimitException(string message , double? seconds , int maxFailureAttemps , int attempts) : base(message)
        {
            RemainingSeconds = seconds;
            MaxFailureAttemps = maxFailureAttemps;
            Attempts = attempts;
        }   
    }
};