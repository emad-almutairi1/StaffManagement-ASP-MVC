
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StaffManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public DateTime? HireDate { get; set; }

       [DataType(DataType.Date)]
       [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }

        [MaxLength(200)]
        public string? Department { get; set; }

        public decimal? Salary { get; set; }
        public string? UserId { get; set; }

      
        public byte[]? ImageData { get; set; }
        public string? ImageType { get; set; }

        [NotMapped]
        public IFormFile? ClientFile { get; set; }


        public IdentityUser? User { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; }
        public ICollection<Project> Projects { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
