﻿namespace TaskManagementApp.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
