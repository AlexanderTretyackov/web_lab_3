using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab3.Data;
using Lab3.Data.Api;
using Lab3.Data.Entities;
using Lab3.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public TasksService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<UserTask> GetUserTask(string userId, string userTaskId)
        {
            var userTask = await _applicationDbContext.UsersTasks.FindAsync(userTaskId);
            return userTask.IdentityUserId == userId ? userTask : null;
        }
        
        public async Task<IEnumerable<UserTask>> GetUserTasks(string userId)
        {
            return _applicationDbContext.UsersTasks.Where(t => t.IdentityUserId == userId);
        }

        public async Task<UserTask> AddUserTask(string userId, UserTask userTask)
        {
            userTask.IdentityUserId = userId;
            await _applicationDbContext.UsersTasks.AddAsync(userTask);
            await _applicationDbContext.SaveChangesAsync();
            return userTask;
        }

        public async Task<bool> DeleteUserTask(string userId, string userTaskId)
        {
            var userTask = await _applicationDbContext.UsersTasks.FindAsync(userTaskId);
            if (userTask.IdentityUserId != userId)
                return false;
            _applicationDbContext.UsersTasks.Remove(userTask);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleCompleteTask(string userId, string userTaskId)
        {
            var userTask = await _applicationDbContext.UsersTasks.FindAsync(userTaskId);
            if (userTask.IdentityUserId != userId)
                return false;
            userTask.IsCompleted = !userTask.IsCompleted;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetCommentsUserTask(string userId, string userTaskId)
        {
            var userTask = await _applicationDbContext.UsersTasks.FindAsync(userTaskId);
            if (userTask.IdentityUserId != userId)
                return null;
            
            return Enumerable
                .Range(0, 10)
                .Select(x => $"Комментарий {x.ToString()}/{userTask.Id}/{userTask.Name}");
        }
    }
}