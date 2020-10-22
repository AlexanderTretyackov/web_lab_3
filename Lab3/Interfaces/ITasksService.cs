using System.Collections.Generic;
using System.Threading.Tasks;
using Lab3.Data.Entities;

namespace Lab3.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<UserTask>> GetUserTasks(string userId);
        Task<UserTask> AddUserTask(string userId, UserTask userTask);
        Task<bool> DeleteUserTask(string userId, string userTaskId);
    }
}