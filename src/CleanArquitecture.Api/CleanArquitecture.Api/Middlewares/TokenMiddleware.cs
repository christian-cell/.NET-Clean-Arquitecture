using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using CleanArquitecture.Application.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace CleanArquitecture.Api.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                
                var endpoint = httpContext.GetEndpoint();

                var displayName = endpoint?.DisplayName;

                var path = httpContext.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;

                var excludedPaths = new[]
                {
                    "/Auth/register",
                    "/Auth/facebook",
                    "/Auth/google",
                    "/Auth/authasync",
                    "/auth/register",
                    "/auth/facebook",
                    "/auth/google",
                    "/auth/authasync",
                    "/api/Auth/register",
                    "/api/Auth/facebook",
                    "/api/Auth/google",
                    "/api/Auth/authasync",
                    "/api/auth/register",
                    "/api/auth/facebook",
                    "/api/auth/google",
                    "/api/auth/authasync"
                };
                
                if (!excludedPaths.Any(e => path.Contains(e)))
                {
                    CheckUserSession();
                }

                await _next(httpContext);
            }
            catch (SecurityTokenExpiredException)
            {
                
                    // httpContext.Response.Clear();
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    httpContext.Response.ContentType = "application/json";

                    var jsonMessage = JsonSerializer.Serialize(new { error = "AccessToken has expired" });
                    await httpContext.Response.WriteAsync(jsonMessage);
                
            }
        }

        private void CheckUserSession()
        {
            var authHeader = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader))
            {
                var parts = authHeader.Split(' ');

                if (parts.Length < 2)
                {
                    throw new Exception("Malformed authorization header");
                }

                var token = parts[1];
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken == null)
                {
                    throw new Exception("jwtToken is null");
                }

                if (DateTime.UtcNow > jwtToken.ValidTo)
                {
                    throw new ExpiredTokenException("AccessTokenExpired");
                }
            }
        }
    }
};

