using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.DTOs
{
    // Dùng để trả dữ liệu về cho người dùng xem
    public class SchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Principal { get; set; }
        public string Address { get; set; }
    }

    // Dùng để nhận dữ liệu khi người dùng muốn Thêm mới hoặc Sửa
    public class CreateSchoolDto
    {
        [Required(ErrorMessage = "Tên trường không được để trống")]
        public string Name { get; set; }

        [Required]
        public string Principal { get; set; }

        public string Address { get; set; } = null;
    }
}