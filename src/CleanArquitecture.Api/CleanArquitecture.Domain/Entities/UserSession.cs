using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArquitecture.Domain.Entities
{
    public class UserSession : EntityBase
    {
        public Guid UserId { get; set; }
        
        [MaxLength(500)]
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpirationDate { get; set; }
        public bool Used { get; set; }
        
        [NotMapped]
        public virtual User? User { get; set; } // Nullable para permitir creación por EF

        // Constructor parameterless requerido por EF Core
        protected UserSession() { }

        public UserSession(Guid userId, string refreshToken, DateTime refreshTokenExpirationDate,
            bool used)
        {
            UserId = userId;
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
            RefreshTokenExpirationDate = refreshTokenExpirationDate;
            Used = used;
        }

        public override String ToString()
        {
            return
                $"{nameof(UserId)} : {UserId}, {nameof(User.Email)} , {User!.Email}, {nameof(RefreshToken)} : {RefreshToken}";
        }
    }
};