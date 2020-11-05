using System.Collections.Generic;
using System.Threading.Tasks;
using Lab3.Data.Api;
using Lab3.Data.Entities;

namespace Lab3.Interfaces
{
    public interface ITasksService
    {
        Task<UserTask> GetUserTask(string userId, string userTaskId);
        Task<IEnumerable<UserTask>> GetUserTasks(string userId);
        Task<UserTask> AddUserTask(string userId, UserTask userTask);
        Task<bool> DeleteUserTask(string userId, string userTaskId);
        Task<bool> ToggleCompleteTask(string userId, string userTaskId);
        Task<IEnumerable<string>> GetCommentsUserTask(string userId, string userTaskId);
    }
}