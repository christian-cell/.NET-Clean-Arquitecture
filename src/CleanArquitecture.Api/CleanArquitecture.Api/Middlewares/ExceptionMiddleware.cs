using System.Net;
using System.Security;
using System.Text.Json;
using CleanArquitecture.Api.Exceptions;
using CleanArquitecture.Application.Exceptions;

namespace CleanArquitecture.Api.Middlewares
{
    public class ExceptionMiddleware
    {

        private const string ContentType = "application/json";
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var errorMessage = string.Empty;

            try
            {
                await _next(httpContext);
            }
            catch (NotAllowedException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(NotAllowedException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.Forbidden,
                    Type = $"Not allowed",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            catch (ExpiredTokenException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(SecurityException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.Unauthorized,
                    Type = $"Token exception",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            catch (SecurityException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(SecurityException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.Unauthorized,
                    Type = $"Security exception",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            catch (ConfigurationException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(ConfigurationException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Type = $"Configuration error",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            catch (NotFoundException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(NotFoundException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Type = $"Resource not found",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            catch (AlreadyExistsException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(AlreadyExistsException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.Conflict,
                    Type = $"Resource already exists",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            catch (ValidationException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(ValidationException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Type = $"Command or Query validation exception",
                    Message = exception.Message,
                    Errors = exception.Errors.Select(error => new Exceptions.ApiException.ValidationError()
                    {
                        Property = error.PropertyName,
                        Code = error.ErrorCode,
                        Message = error.ErrorMessage
                    })
                }));
            }
            catch (RateLimitException exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(RateLimitException)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.TooManyRequests,
                    Type = $"Too many requests",
                    Message = exception.Message,
                    Attributes = new Dictionary<string, object>
                    {
                        { "RemainingSeconds", exception.RemainingSeconds },
                        { "MaxFailedAttempts", exception.MaxFailureAttemps },
                        { "Attempts", exception.Attempts }
                    }
                }));
            }
            catch (Exception exception)
            {
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                errorMessage = exception.Message;

                _logger.LogInformation($"🔴 {nameof(Exception)}: {errorMessage}");

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ApiException()
                {
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Type = $"Unknown exception",
                    Message = exception.Message,
                    Errors = null
                }));
            }
            finally
            {
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _logger.LogError($"🔴 {errorMessage}");
                }
            }

        }
    }
};