using Microsoft.AspNetCore.Mvc;

namespace CleanArquitecture.Api.ActionFilters
{
    public class EndpointRateLimitFilterAttribute : TypeFilterAttribute
    {
        public EndpointRateLimitFilterAttribute(int delayBetweenRequests, int maxFailedAttempts) : base(
            typeof(EndpointRateLimitFilter))
        {
            Arguments = new object[]{ delayBetweenRequests, maxFailedAttempts };
        }
    }
};

