using CleanArquitecture.Api.Extensions;
using CleanArquitecture.Api.Middlewares;
using CleanArquitecture.Application;
using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Infrastructure;
using CleanArquitecture.Infrastructure.Configurations;
using CleanArquitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NetNinja.Mediator;

namespace CleanArquitecture.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            GlobalConfiguration globalConfiguration = builder.Configuration.GetSection("Settings").Get<GlobalConfiguration>()!;
            
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton(globalConfiguration);
            builder.Services.AddDbContextDependencies(globalConfiguration.Azure.Sql.LocalConnectionString!);
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddSwaggerHere();
            builder.Services.AddNetNinjaMediator(
                autoRegisterValidators: true,
                autoRegisterValidationBehavior: false,
                autoRegisterPipelineBehaviors: false,
                autoRegisterHandlers: true,
                typeof(CreateUserHandler).Assembly
            );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseCorsHere();
                app.UseSwaggerHere();
            }
            
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.MapControllers();
            
            /*
             * Generate firs the migrations before apply with this command in domain location
             dotnet ef migrations add Users --context ApplicationDbContext --project CleanArquitecture.Infrastructure --output-dir Migrations --startup-project CleanArquitecture.Api
             */
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any()) dbContext.Database.Migrate();
            }
            
            app.Run();
        }
    }
}