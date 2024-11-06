using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Models;

namespace TaskManagementApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) { 
            _context = context;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(c => c.Tasks).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(c => c.Tasks).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(i => i.Id == id);
        }
    }
}
