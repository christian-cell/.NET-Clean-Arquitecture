using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.Api.ActionFilters
{
    public class NewScoreRateLimitFilterAttribute : TypeFilterAttribute
    {
        public NewScoreRateLimitFilterAttribute(int delayBetweenRequests, int maxFailedAttempts) : base(
            typeof(EndpointRateLimitFilter))
        {
            Arguments = new object[]{ delayBetweenRequests, maxFailedAttempts };
        }
    }
};

