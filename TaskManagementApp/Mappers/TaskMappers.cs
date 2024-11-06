using TaskManagementApp.Dtos.Task;

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
                UserId = taskModel.UserId
            };
        }

        public static Models.Task ToTaskFromCreate(this CreateTaskDto taskDto, int userId)
        {
            return new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                UserId = userId
            };
        }

        public static Models.Task ToTaskFromUpdate(this UpdateTaskRequestDto taskDto)
        {
            return new Models.Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description
            };
        }
    }
}
