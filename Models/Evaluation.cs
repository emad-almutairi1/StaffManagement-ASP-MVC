using System.ComponentModel.DataAnnotations;

namespace StaffManagement.Models
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        public DateTime EvaluationDate { get; set; }

        [Range(1, 10)]
        public int Score { get; set; } 

        [MaxLength(1000)]
        public string? Comments { get; set; }
    }
}
