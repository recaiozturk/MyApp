using MyApp.Core.Entities;

namespace MyApp.Data.Repositories
{
    public interface ILogRepository
    {
        Task<Log?> GetByIdAsync(long id);
        Task<IEnumerable<Log>> GetAllAsync(int pageNumber = 1, int pageSize = 50);
        Task<IEnumerable<Log>> GetByLevelAsync(string level, int pageNumber = 1, int pageSize = 50);
        Task<int> GetTotalCountAsync();
    }
}


