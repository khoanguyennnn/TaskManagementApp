namespace TaskManagementApp.Dtos.Task
{
    public class CreateTaskDto
    {
        public required string Name { get; set; }
        public string Description { get; set; }
    }
}
