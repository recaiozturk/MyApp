using MyApp.Core.DTOs;

namespace MyApp.Core.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<LogDto>> GetAllLogsAsync(int pageNumber = 1, int pageSize = 50);
        Task<LogDto?> GetLogByIdAsync(long id);
        Task<IEnumerable<LogDto>> GetLogsByLevelAsync(string level, int pageNumber = 1, int pageSize = 50);
        Task<int> GetTotalLogCountAsync();
    }
}


