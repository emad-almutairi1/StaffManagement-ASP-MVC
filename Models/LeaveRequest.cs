using System.ComponentModel.DataAnnotations;

namespace StaffManagement.Models
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Reason { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }
    }
}
