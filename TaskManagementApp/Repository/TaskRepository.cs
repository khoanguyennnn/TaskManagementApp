using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Threading.Tasks;
using TaskManagementApp.Data;
using TaskManagementApp.Helpers;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Mappers;
using TaskManagementApp.Models;

namespace TaskManagementApp.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;
        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Models.Task> CreateAsync(Models.Task taskModel)
        {
            await _context.Tasks.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public async Task<Models.Task?> DeleteAsync(int id)
        {
            var taskModel = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            
            if(taskModel == null)
            {
                return null;
            }
            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();
            return taskModel;
        }

        public async Task<List<Models.Task>> GetAllAsync(QueryObject query, User user)
        {
            var tasks =  _context.Tasks.AsQueryable();
            tasks = tasks.Where(s => s.UserId == user.Id);
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                tasks = tasks.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.IsCompleted))
            {
                bool isComplete = false;
                if (query.IsCompleted == "true")
                {
                    isComplete = true;
                }
                tasks = tasks.Where(s => s.IsCompleted.Equals(isComplete));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy)
                {
                    case var name when string.Equals(name, "Name", StringComparison.InvariantCultureIgnoreCase):
                        tasks = query.IsDecsending ? tasks.OrderByDescending(s => s.Name) : tasks.OrderBy(s => s.Name);
                        break;
                    case var name when string.Equals(name, "updatedDate", StringComparison.InvariantCultureIgnoreCase):
                        tasks = query.IsDecsending ? tasks.OrderByDescending(s => s.UpdatedDate) : tasks.OrderBy(s => s.UpdatedDate);
                        break;
                    case var name when string.Equals(name, "isCompleted", StringComparison.InvariantCultureIgnoreCase):
                        tasks = query.IsDecsending ? tasks.OrderByDescending(s => s.IsCompleted) : tasks.OrderBy(s => s.IsCompleted);
                        break;
                    case var name when string.Equals(name, "createdDate", StringComparison.InvariantCultureIgnoreCase):
                        tasks = query.IsDecsending ? tasks.OrderByDescending(s => s.CreatedDate) : tasks.OrderBy(s => s.CreatedDate);
                        break;
                    case var name when string.Equals(name, "endDate", StringComparison.InvariantCultureIgnoreCase):
                        tasks = query.IsDecsending ? tasks.OrderByDescending(s => s.EndDate) : tasks.OrderBy(s => s.EndDate);
                        break;
                }
            }
            return await tasks.ToListAsync();
        }

        //public async Task<List<Models.Task>> GetAllByUserIdAsync(QueryObject query, int userId)
        //{
        //    var tasks = _context.Tasks.AsQueryable();
        //    tasks = tasks.Where(s => s.UserId.Equals(userId));
        //    if (!string.IsNullOrWhiteSpace(query.Name))
        //    {
        //        tasks = tasks.Where(s => s.Name.Contains(query.Name));
        //    }
        //    if (!string.IsNullOrWhiteSpace(query.IsCompleted))
        //    {
        //        bool isComplete = false;
        //        if (query.IsCompleted == "true")
        //        {
        //            isComplete = true;
        //        }
        //        tasks = tasks.Where(s => s.IsCompleted.Equals(isComplete));
        //    }
//            if (!string.IsNullOrWhiteSpace(query.SortBy))
//            {
//                switch (query.SortBy)
//                {
//                    case var name when string.Equals(name, "Name", StringComparison.InvariantCultureIgnoreCase) :
//                        tasks = query.IsDecsending? tasks.OrderByDescending(s => s.Name) : tasks.OrderBy(s => s.Name);
//                        break;
//                    case var name when string.Equals(name, "updatedDate", StringComparison.InvariantCultureIgnoreCase) :
//                        tasks = query.IsDecsending? tasks.OrderByDescending(s => s.UpdatedDate) : tasks.OrderBy(s => s.UpdatedDate);
//                        break;
//                    case var name when string.Equals(name, "isCompleted", StringComparison.InvariantCultureIgnoreCase) :
//                        tasks = query.IsDecsending? tasks.OrderByDescending(s => s.IsCompleted) : tasks.OrderBy(s => s.IsCompleted);
//                        break;
//                    case var name when string.Equals(name, "createdDate", StringComparison.InvariantCultureIgnoreCase) :
//                        tasks = query.IsDecsending? tasks.OrderByDescending(s => s.CreatedDate) : tasks.OrderBy(s => s.CreatedDate);
//                        break;
//                    case var name when string.Equals(name, "endDate", StringComparison.InvariantCultureIgnoreCase) :
//                        tasks = query.IsDecsending? tasks.OrderByDescending(s => s.EndDate) : tasks.OrderBy(s => s.EndDate);
//                        break;
//                }
//}

//    return await tasks.ToListAsync();
//}

        public async Task<Models.Task?> GetByIdAsync(int id, User user)
        {
            var taskModel = await _context.Tasks.AsQueryable().Where(s => s.Id == id).FirstOrDefaultAsync(x => x.UserId == user.Id);
            return taskModel;
        }

        public async Task<Models.Task?> UpdateAsync(int id, Models.Task taskModel)
        {
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null) {
                return null;
            }

            existingTask.Name = taskModel.Name;
            existingTask.Description = taskModel.Description;
            existingTask.EndDate = DateTime.Parse(taskModel.EndDate.ToString());
            existingTask.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<Models.Task?> UpdateStatusAsync(int id)
        {
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null)
            {
                return null;
            }

            if (existingTask.IsCompleted)
            {
                existingTask.IsCompleted = false;
            } else
            {
                existingTask.IsCompleted = true;
            }

            await _context.SaveChangesAsync();
            return existingTask;
        }
    }
}
