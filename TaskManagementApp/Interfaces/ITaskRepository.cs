using TaskManagementApp.Helpers;
using TaskManagementApp.Models;
using TaskModel = TaskManagementApp.Models.Task;

namespace TaskManagementApp.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllAsync(QueryObject query);
        //Task<List<TaskModel>> GetAllByUserIdAsync(QueryObject query, int userId);
        Task<TaskModel?> GetByIdAsync(int id);
        Task<TaskModel> CreateAsync(TaskModel taskModel);
        Task<TaskModel?> UpdateAsync(int id, TaskModel taskModel);
        Task<TaskModel?> DeleteAsync(int id);
    }
}
