using TaskManagementApp.Dtos.Task;

namespace TaskManagementApp.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public required string Email { get; set; }

        //Password
        //Task
        public List<TaskDto> Tasks { get; set; }
    }
}
