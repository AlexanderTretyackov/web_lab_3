using System.Security.Claims;
using System.Threading.Tasks;
using Lab3.Data.Entities;
using Lab3.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;

        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }
        
        public async Task<IActionResult> GetAll()
        {
            var userTasks = await _tasksService.GetUserTasks(UserId);
            return View("Tasks", userTasks);
        }
        
        public async Task<IActionResult> DeleteTask(string userTaskId)
        {
            await _tasksService.DeleteUserTask(UserId, userTaskId);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> AddTask()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddTask(UserTask userTask)
        {
            await _tasksService.AddUserTask(UserId, userTask);
            return RedirectToAction(nameof(GetAll));
        }
    }
}