namespace TaskManagementApp.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;
        public string? IsCompleted { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
    }
}
