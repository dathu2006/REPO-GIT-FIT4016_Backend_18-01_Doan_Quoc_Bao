using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;   // <-- Đã sửa thành SchoolManagement
using SchoolManagement.Models; // <-- Đã sửa thành SchoolManagement
using SchoolManagement.DTOs;   // <-- Đã sửa thành SchoolManagement

namespace SchoolManagement.Controllers // <-- Đã sửa
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public SchoolsController(SchoolDbContext context)
        {
            _context = context;
        }

        // ... (Phần code logic bên dưới giữ nguyên)
        
        // 1. Lấy danh sách tất cả trường
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchools()
        {
            var schools = await _context.Schools
                .Select(s => new SchoolDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Principal = s.Principal,
                    Address = s.Address
                })
                .ToListAsync();

            return Ok(schools);
        }

        // 2. Lấy chi tiết 1 trường
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null) return NotFound($"Không tìm thấy trường ID = {id}");

            return Ok(new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Principal = school.Principal,
                Address = school.Address
            });
        }

        // 3. Thêm trường mới
        [HttpPost]
        public async Task<ActionResult<SchoolDto>> CreateSchool(CreateSchoolDto createDto)
        {
            var school = new School
            {
                Name = createDto.Name,
                Principal = createDto.Principal,
                Address = createDto.Address,
                CreatedAt = DateTime.Now
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Principal = school.Principal,
                Address = school.Address
            });
        }

        // 4. Cập nhật
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, CreateSchoolDto updateDto)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null) return NotFound();

            school.Name = updateDto.Name;
            school.Principal = updateDto.Principal;
            school.Address = updateDto.Address;
            school.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. Xóa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null) return NotFound();

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}