using MyApp.Core.DTOs;
using MyApp.Core.Interfaces;
using MyApp.Data.Repositories;

namespace MyApp.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<LogDto>> GetAllLogsAsync(int pageNumber = 1, int pageSize = 50)
        {
            var logs = await _logRepository.GetAllAsync(pageNumber, pageSize);
            return logs.Select(log => new LogDto
            {
                Id = log.Id,
                Application = log.Application,
                Logged = log.Logged,
                Level = log.Level,
                Message = log.Message,
                Logger = log.Logger,
                Callsite = log.Callsite,
                Exception = log.Exception,
                Properties = log.Properties
            });
        }

        public async Task<LogDto?> GetLogByIdAsync(long id)
        {
            var log = await _logRepository.GetByIdAsync(id);
            if (log == null)
                return null;

            return new LogDto
            {
                Id = log.Id,
                Application = log.Application,
                Logged = log.Logged,
                Level = log.Level,
                Message = log.Message,
                Logger = log.Logger,
                Callsite = log.Callsite,
                Exception = log.Exception,
                Properties = log.Properties
            };
        }

        public async Task<IEnumerable<LogDto>> GetLogsByLevelAsync(string level, int pageNumber = 1, int pageSize = 50)
        {
            var logs = await _logRepository.GetByLevelAsync(level, pageNumber, pageSize);
            return logs.Select(log => new LogDto
            {
                Id = log.Id,
                Application = log.Application,
                Logged = log.Logged,
                Level = log.Level,
                Message = log.Message,
                Logger = log.Logger,
                Callsite = log.Callsite,
                Exception = log.Exception,
                Properties = log.Properties
            });
        }

        public async Task<int> GetTotalLogCountAsync()
        {
            return await _logRepository.GetTotalCountAsync();
        }
    }
}


