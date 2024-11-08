using TaskManagementApp.Helpers;
using TaskManagementApp.Models;
using TaskModel = TaskManagementApp.Models.Task;

namespace TaskManagementApp.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllAsync(QueryObject query, User user);
        //Task<List<TaskModel>> GetAllByUserIdAsync(QueryObject query, int userId);
        Task<TaskModel?> GetByIdAsync(int id, User user);
        Task<TaskModel> CreateAsync(TaskModel taskModel);
        Task<TaskModel?> UpdateAsync(int id, TaskModel taskModel);
        Task<TaskModel?> UpdateStatusAsync(int id);
        Task<TaskModel?> DeleteAsync(int id);
    }
}
