using CleanArquitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArquitecture.Infrastructure.Data.Configurations
{
    public class UserSessionConfiguration : BaseEntityConfiguration<UserSession>
    {
        protected override void ConfigureDomainEntity(EntityTypeBuilder<UserSession> builder)
        {
            builder.HasKey(us => us.Id); 

            builder.Property(us => us.RefreshToken).IsRequired(); 
            builder.Property(us => us.RefreshTokenExpirationDate)
                .IsRequired(); 

            builder.HasOne(us => us.User) 
                .WithMany(c => c.UserSessions)
                .HasForeignKey(us => us.UserId); 

            builder.ToTable("UserSessions", "core");
        }
    }
};

