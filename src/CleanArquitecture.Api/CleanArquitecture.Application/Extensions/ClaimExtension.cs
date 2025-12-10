using CleanArquitecture.Application.Abstractions.Enums;
using Microsoft.AspNetCore.Http;

namespace CleanArquitecture.Application.Extensions
{
    public static class ClaimExtension
    {
        extension(string type)
        {
            public string GetValueFromUserClaims<T>(HttpContext context, UserClaim customerClaim)
            {
                return context.User.Claims.FirstOrDefault()?.Value!;
            }
        }
    }
};