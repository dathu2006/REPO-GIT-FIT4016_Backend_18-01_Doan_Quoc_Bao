using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models
{
    [Table("students")] // Map to table 'students'
    public class Student
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("school_id")]
        public int SchoolId { get; set; }

        [Required]
        [Column("full_name")]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        [Column("student_id")]
        [StringLength(20, MinimumLength = 5)]
        public string StudentId { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        [RegularExpression(@"^[0-9]{10,11}$")] // Validate 10-11 digits
        public string? Phone { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        [ForeignKey("SchoolId")]
        public School School { get; set; }
    }
}