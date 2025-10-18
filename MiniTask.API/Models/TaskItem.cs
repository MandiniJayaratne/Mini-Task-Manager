using System;
using System.ComponentModel.DataAnnotations;

namespace MiniTask.API.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; } = "To Do"; 

        public DateTime? DueDate { get; set; }
    }
}

