using System.Linq.Expressions;
using CleanArquitecture.Domain.Entities;

namespace CleanArquitecture.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<bool> UpdateAsync(Guid id, User user, string userName = "System");
        Task<bool> SaveAsync();
        Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate);
        Task<int> CountAsync();
    }
};

