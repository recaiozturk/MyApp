using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Core.DTOs;
using MyApp.Core.Interfaces;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")] // Sadece SuperAdmin erişebilir
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetLogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 50;

            var logs = await _logService.GetAllLogsAsync(pageNumber, pageSize);
            var totalCount = await _logService.GetTotalLogCountAsync();

            return Ok(new
            {
                data = logs,
                totalCount = totalCount,
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogDto>> GetLog(long id)
        {
            var log = await _logService.GetLogByIdAsync(id);
            if (log == null)
                return NotFound();

            return Ok(log);
        }

        [HttpGet("level/{level}")]
        public async Task<ActionResult<object>> GetLogsByLevel(
            string level, 
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 50)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 50;

            var logs = await _logService.GetLogsByLevelAsync(level, pageNumber, pageSize);
            var totalCount = await _logService.GetTotalLogCountAsync();

            return Ok(new
            {
                data = logs,
                level = level,
                pageNumber = pageNumber,
                pageSize = pageSize
            });
        }
    }
}


