using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffManagement.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [MaxLength(50)]
        public string? Priority { get; set; } 

        [MaxLength(50)]
        public string? Status { get; set; }

        public byte[]? AttachmentData { get; set; }
        public string? AttachmentType { get; set; }
        public string? AttachmentName { get; set; }

        [NotMapped]
        public IFormFile? ClientFile { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

  
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }



    }
}
