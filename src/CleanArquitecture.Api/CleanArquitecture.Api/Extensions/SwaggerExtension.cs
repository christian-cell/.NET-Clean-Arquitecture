using Microsoft.OpenApi;

namespace CleanArquitecture.Api.Extensions
{
    public static class SwaggerExtension
    {
        extension(IServiceCollection services)
        {
            public void AddSwaggerHere()
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArquitecture", Version = "v1" });
                
                });
            }
        }
        
        public static void UseSwaggerHere(this WebApplication application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArquitecture");
                config.OAuthScopeSeparator(" ");
            });
        }
    }
};

