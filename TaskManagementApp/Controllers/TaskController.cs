using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Dtos.Task;
using TaskManagementApp.Helpers;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Mappers;
using TaskModel = TaskManagementApp.Models.Task;

namespace TaskManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IUserRepository _userRepo;
        public TaskController(ITaskRepository taskRepo, IUserRepository userRepo)
        {
            _taskRepo = taskRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks([FromQuery] QueryObject query)
        {
            var tasks = await _taskRepo.GetAllAsync(query);
            var taskDto = tasks.Select(s => s.ToTaskDto());
            return Ok(taskDto);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetAllTasksByUserId([FromRoute] int userId,[FromQuery] QueryObject query)
        {
            var tasks = await _taskRepo.GetAllByUserIdAsync(query, userId);
            var taskDto = tasks.Select(s => s.ToTaskDto());
            return Ok(taskDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTask([FromRoute] int id, [FromQuery] QueryObject query)
        {
            var task = await _taskRepo.GetByIdAsync(id, query);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            return Ok(task.ToTaskDto());
        }

        [HttpPost("create/{userId:int}")]
        public async Task<IActionResult> CreateTask([FromRoute] int userId, CreateTaskDto taskDto)
        {
            if(!await _userRepo.UserExists(userId))
            {
                return BadRequest("User does not exist");
            }
            var taskModel = taskDto.ToTaskFromCreate(userId);
            await _taskRepo.CreateAsync(taskModel);
            return CreatedAtAction(nameof(GetTask), new { id = taskModel.Id }, taskModel.ToTaskDto());
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] UpdateTaskRequestDto updateDto)
        {
            var tasks = await _taskRepo.UpdateAsync(id, updateDto.ToTaskFromUpdate());

            if(tasks == null)
            {
                return NotFound("Task not found");
            }

            return Ok(tasks.ToTaskDto());
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            var taskModel = await _taskRepo.DeleteAsync(id);

            if(taskModel == null)
            {
                return NotFound("Task does not exist");
            }

            return Ok("Delete successfully");
        }
    }
}
