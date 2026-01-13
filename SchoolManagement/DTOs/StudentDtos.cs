using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.DTOs
{
    // DTO for Create and Update
    public class StudentFormDto
    {
        [Required(ErrorMessage = "School ID is required.")]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full Name must be between 2 and 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Student ID is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Student ID must be between 5 and 20 characters.")]
        public string StudentId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Phone number must be 10 or 11 digits.")]
        public string? Phone { get; set; }
    }
}