namespace CleanArquitecture.Api.Extensions
{
    public static class CorsExtension
    {
        public static void AddCorsHere(this IServiceCollection services)
        {
            services.AddCors((options) =>
            {
                options.AddPolicy("DevCors", corsBuilder =>
                {
                    corsBuilder
                        .WithOrigins("http://localhost:4202", "http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
        }

        public static void UseCorsHere(this WebApplication application)
        {
            application.UseCors("DevCors");
        }
    }
};