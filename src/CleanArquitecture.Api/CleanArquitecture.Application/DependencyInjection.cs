using CleanArquitecture.Application.Behaviors;
using CleanArquitecture.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using Mm.Game.Application.Abstractions.Auth;
using NetNinja.Mediator.Abstractions;

namespace CleanArquitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogginBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
};