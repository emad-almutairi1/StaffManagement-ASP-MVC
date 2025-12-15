using System.ComponentModel.DataAnnotations;

namespace StaffManagement.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string? Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public bool IsRead { get; set; } = false; 
    }
}
