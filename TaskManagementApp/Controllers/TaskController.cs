using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Dtos.Task;
using TaskManagementApp.Extensions;
using TaskManagementApp.Helpers;
using TaskManagementApp.Interfaces;
using TaskManagementApp.Mappers;
using TaskManagementApp.Models;
using TaskModel = TaskManagementApp.Models.Task;

namespace TaskManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly UserManager<User> _userManager;
        public TaskController(ITaskRepository taskRepo, UserManager<User> userManager)
        {
            _taskRepo = taskRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTasks([FromQuery] QueryObject query)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var tasks = await _taskRepo.GetAllAsync(query, user);
            var taskDto = tasks.Select(s => s.ToTaskDto());
            return Ok(taskDto);
        }

        //[HttpGet("user/{userId:int}")]
        //public async Task<IActionResult> GetAllTasksByUserId([FromRoute] int userId,[FromQuery] QueryObject query)
        //{
        //    var tasks = await _taskRepo.GetAllByUserIdAsync(query, userId);
        //    var taskDto = tasks.Select(s => s.ToTaskDto());
        //    return Ok(taskDto);
        //}

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetTask([FromRoute] int id)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var task = await _taskRepo.GetByIdAsync(id, user);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            return Ok(task.ToTaskDto());
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateTask( CreateTaskDto taskDto)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var taskModel = taskDto.ToTaskFromCreate(user);
            await _taskRepo.CreateAsync(taskModel);
            return CreatedAtAction(nameof(GetTask), new { id = taskModel.Id }, taskModel.ToTaskDto());
        }

        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] UpdateTaskRequestDto updateDto)
        {
            var tasks = await _taskRepo.UpdateAsync(id, updateDto.ToTaskFromUpdate());

            if(tasks == null)
            {
                return NotFound("Task not found");
            }

            return Ok(tasks.ToTaskDto());
        }

        [HttpPost]
        [Route("finish/{id:int}")]
        [Authorize]
        public async Task<IActionResult> FinishTask([FromRoute] int id)
        {
            var task = await _taskRepo.UpdateStatusAsync(id);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            return Ok(task.ToTaskDto());
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize]
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
