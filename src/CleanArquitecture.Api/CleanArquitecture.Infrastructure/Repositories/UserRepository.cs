using System.Linq.Expressions;
using CleanArquitecture.Domain.Entities;
using CleanArquitecture.Domain.Interfaces;
using CleanArquitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Set<User>().AnyAsync(u => u.Email == email);
        }

        public async Task<User> AddAsync(User user)
        {
            var entry = await _context.Set<User>().AddAsync(user);
            return entry.Entity;
        }

        public async Task<bool> UpdateAsync(Guid id, User user, string userName = "System")
        {
            var userDb = await _context.Set<User>().FindAsync(id);
            if (userDb is null) return false;

            user.ModifiedOn = DateTime.UtcNow;
            user.ModifiedByUser = userName;
            user.CreatedByUser = userDb.CreatedByUser;
            user.CreatedOn = userDb.CreatedOn;
            user.DeletedByUser = userDb.DeletedByUser;
            user.DeletedOn = userDb.DeletedOn;

            _context.Entry(userDb).CurrentValues.SetValues(user);
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<User>().CountAsync();
        }
    
    }
};

