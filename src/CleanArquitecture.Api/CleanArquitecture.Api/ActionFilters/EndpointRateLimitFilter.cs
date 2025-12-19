using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArquitecture.Api.ActionFilters
{
    public class EndpointRateLimitFilter: Attribute, IAsyncActionFilter
    {
        private readonly int _delayBetweenRequests;
        private readonly int _maxFailedAttempts;
        private readonly ILogger<EndpointRateLimitFilter> _logger;
        private readonly IMemoryCache _memoryCache;

        public EndpointRateLimitFilter(int delayBetweenRequests, int maxFailedAttempts,ILogger<EndpointRateLimitFilter> logger, IMemoryCache memoryCache)
        {
            _delayBetweenRequests = delayBetweenRequests;
            _maxFailedAttempts = maxFailedAttempts;
            _logger = logger;
            _memoryCache = memoryCache;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (ShouldRateLimit(context, out var configuration, out var name, out var ip, out var lastKey, out var countKey))
            {
                if (HandleRateLimitExceeded(context, configuration, countKey))
                {
                    return;
                }

                if (HandleFrequentRequests(context,  lastKey, countKey))
                {
                    return;
                }

                ResetRateLimit(context, lastKey);
            }

            await next();
        }

        private bool ShouldRateLimit(ActionExecutingContext context, out EndpointRateLimitFilterAttribute configuration , out string name, out string ip, out string lastKey, out string countKey)
        {
            configuration = null;
            name = null;
            ip = null;
            lastKey = null;
            countKey = null;

            if (context.ActionArguments.TryGetValue("command", out var obj) && obj is CreateUserCommand)
            {
                configuration = context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<EndpointRateLimitFilterAttribute>()!;

                if (configuration != null)
                {
                    name = context.ActionDescriptor.DisplayName!;
                    ip = context.HttpContext.GetIpAddress();

                    lastKey = $"{ip}_{name}_last";
                    countKey = $"{ip}_{name}";

                    return true;
                }
            }

            return false;
        }

        private bool HandleFrequentRequests(ActionExecutingContext context, string lastKey, string countKey)
        {
            _memoryCache.TryGetValue(lastKey, out DateTime? last);

            if (last.HasValue)
            {
                var seconds = DateTime.UtcNow.Subtract(last.Value).TotalSeconds;

                if (seconds < _delayBetweenRequests)
                {
                    _memoryCache.Set(lastKey, DateTime.UtcNow);
                    _memoryCache.TryGetValue(countKey, out int count);
                    _memoryCache.Set(countKey, count + 1);

                    var ip = context.HttpContext.GetIpAddress();
                    
                    throw new RateLimitException(
                        $"The requests are too frequent from {ip}. Please do not try again for {_delayBetweenRequests - (int)seconds} seconds or your IP address will be permanently blocked.",
                        _delayBetweenRequests - (int)seconds , _maxFailedAttempts , count
                    );
                    return true;
                }
            }

            return false;
        }

        private bool HandleRateLimitExceeded(ActionExecutingContext context, EndpointRateLimitFilterAttribute configuration, string countKey)
        {
            _memoryCache.TryGetValue(countKey, out int count);

            if (count > _maxFailedAttempts)
            {
                _memoryCache.Set(countKey, count + 1);
                
                throw new RateLimitException($"You have exceeded the maximum number of failed attempts. You have been blocked 💀" , 0.00 , 0 , count);
            }

            return false;
        }

        private void ResetRateLimit(ActionExecutingContext context, string lastKey)
        {
            _memoryCache.Set(lastKey, DateTime.UtcNow);
        }
    }
    
    public static class HttpContextExtensions
    {
        public static string GetIpAddress(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString()!;
        }
    }
};

