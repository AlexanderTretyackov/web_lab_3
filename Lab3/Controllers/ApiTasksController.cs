using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lab3.Data.Api;
using Lab3.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ApiTasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public ApiTasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpPost("[action]")]
        public async Task<bool> DeleteTask([FromBody] string userTaskId)
        {
            return await _tasksService.DeleteUserTask(UserId, userTaskId);
        }
        
        [HttpPost("[action]")]
        public async Task<bool> ToggleCompleteTask([FromBody] string userTaskId)
        {
            return await _tasksService.ToggleCompleteTask(UserId, userTaskId);
        }
        
        [HttpGet("[action]")]
        public async Task<IEnumerable<string>> GetCommentsUserTask(string userTaskId)
        {
            return await _tasksService.GetCommentsUserTask(UserId, userTaskId);
        }
    }
}