using CleanArquitecture.Domain.Interfaces;
using CleanArquitecture.Infrastructure.Data;
using CleanArquitecture.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbContextDependencies(this IServiceCollection services, string cnx)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(cnx);
            });

            return services;
        }
    }
};

