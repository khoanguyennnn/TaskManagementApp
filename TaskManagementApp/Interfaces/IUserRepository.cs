using TaskManagementApp.Models;

namespace TaskManagementApp.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<bool> UserExists(int id);
    }
}
