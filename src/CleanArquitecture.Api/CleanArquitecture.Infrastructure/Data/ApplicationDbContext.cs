using CleanArquitecture.Domain.Entities;
using CleanArquitecture.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
        }
    
    }
};

