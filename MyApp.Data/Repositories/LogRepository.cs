using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Data;

namespace MyApp.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly MyAppDbContext _context;

        public LogRepository(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<Log?> GetByIdAsync(long id)
        {
            return await _context.Logs.FindAsync(id);
        }

        public async Task<IEnumerable<Log>> GetAllAsync(int pageNumber = 1, int pageSize = 50)
        {
            return await _context.Logs
                .OrderByDescending(l => l.Logged)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetByLevelAsync(string level, int pageNumber = 1, int pageSize = 50)
        {
            return await _context.Logs
                .Where(l => l.Level == level)
                .OrderByDescending(l => l.Logged)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Logs.CountAsync();
        }
    }
}


