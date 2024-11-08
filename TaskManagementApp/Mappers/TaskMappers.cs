using TaskManagementApp.Dtos.Task;
using TaskManagementApp.Models;

namespace TaskManagementApp.Mappers
{
    public static class TaskMappers
    {
        public static TaskDto ToTaskDto(this Models.Task taskModel)
        {
            return new TaskDto
            {
                Id = taskModel.Id,
                Name = taskModel.Name,
                Description = taskModel.Description,
                CreatedDate = taskModel.CreatedDate,
                UpdatedDate = taskModel.UpdatedDate,
                EndDate = taskModel.EndDate,
                IsCompleted = taskModel.IsCompleted,
                UserId = taskModel.UserId,
            };
        }

        public static Models.Task ToTaskFromCreate(this CreateTaskDto taskDto, User user)
        {
            return new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                EndDate = taskDto.EndDate,
                UserId = user.Id
            };
        }

        public static Models.Task ToTaskFromUpdate(this UpdateTaskRequestDto taskDto)
        {
            return new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                EndDate = taskDto.EndDate,
            };
        }

        public static Models.Task ToTaskFromUpdateStatus(this UpdateTaskStatusDto taskDto)
        {
            return new Models.Task
            {
                IsCompleted = taskDto.IsCompleted
            };
        }
    }
}
