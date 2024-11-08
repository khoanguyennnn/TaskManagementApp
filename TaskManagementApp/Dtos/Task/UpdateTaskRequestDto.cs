namespace TaskManagementApp.Dtos.Task
{
    public class UpdateTaskRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Parse("0001-01-01 00:00:00.0000000");

    }
}
