using CleanArquitecture.Domain.Entities;
using CleanArquitecture.Domain.Interfaces;
using CleanArquitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArquitecture.Infrastructure.Repositories
{
    public class SessionRepository: ISessionRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<UserSession>> SearchAsync(string? refresToken, Guid? userId)
        {
            var query = _context.Set<UserSession>().Where(g => !g.Deleted);

            if (!string.IsNullOrEmpty(refresToken))
            {
                query = query.Where(g => g.RefreshToken == refresToken);
            }
            
            if (userId.HasValue && userId.Value != Guid.Empty)
            {
                query = query.Where(g => g.UserId  == userId);
            }

            return await query
                .OrderByDescending(g => g.CreatedOn)
                .ToListAsync();
        }
        
        public async Task<bool> UpdateAsync(Guid sessionId, UserSession session)
        {
            var sessionDb = await _context.Set<UserSession>().FindAsync(sessionId).ConfigureAwait(false);

            if (sessionDb is null)
                return false;

            // Mantener datos de creación y borrado
            session.ModifiedOn = DateTime.UtcNow;
            session.CreatedByUser = sessionDb.CreatedByUser;
            session.CreatedOn = sessionDb.CreatedOn;
            session.DeletedByUser = sessionDb.DeletedByUser;
            session.DeletedOn = sessionDb.DeletedOn;

            _context.Entry(sessionDb).CurrentValues.SetValues(session);

            return true;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task AddAsync(UserSession session)
        {
            await _context.Set<UserSession>().AddAsync(session).ConfigureAwait(false);
        }
    }
};

