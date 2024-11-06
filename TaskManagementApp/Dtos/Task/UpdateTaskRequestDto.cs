namespace TaskManagementApp.Dtos.Task
{
    public class UpdateTaskRequestDto
    {
        public required string Name { get; set; }
        public string Description { get; set; }
    }
}
