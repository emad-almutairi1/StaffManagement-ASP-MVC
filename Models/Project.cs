using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffManagement.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(100)]
        public string? Status { get; set; }

        public byte[]? AttachmentData { get; set; }
        public string? AttachmentType { get; set; }
        public string? AttachmentName { get; set; }

        [NotMapped]
        public IFormFile? ClientFile { get; set; } 
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

  
        public ICollection<TaskItem> TaskItems { get; set; }

    }
}
