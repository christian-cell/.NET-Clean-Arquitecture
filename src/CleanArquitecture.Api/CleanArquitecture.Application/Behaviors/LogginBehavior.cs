using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application.Behaviors
{
    public class LogginBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LogginBehavior<TRequest, TResponse>> _logger;

        public LogginBehavior(ILogger<LogginBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
        {
            var requestName = typeof(TRequest).Name;
            
            _logger.LogInformation($"Handling {requestName} before validator behavior");

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();
                
                _logger.LogInformation(
                    "Handled {RequestName} in {ElapsedMilliseconds}ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                return response;

            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(
                    ex,
                    "Error handling {RequestName} after {ElapsedMilliseconds}ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
};

