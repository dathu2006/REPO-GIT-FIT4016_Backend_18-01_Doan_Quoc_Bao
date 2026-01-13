using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.DTOs;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentsController(SchoolDbContext context)
        {
            _context = context;
        }

        // --- 2. READ: Get List with Pagination ---
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page < 1) page = 1;

                var query = _context.Students
                    .Include(s => s.School) // Join to get School info
                    .OrderByDescending(s => s.CreatedAt);

                var totalItems = await query.CountAsync();
                
                var students = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(s => new 
                    {
                        s.Id,
                        s.FullName,
                        s.StudentId,
                        s.Email,
                        s.Phone,
                        SchoolName = s.School.Name // Display School Name
                    })
                    .ToListAsync();

                return Ok(new
                {
                    Success = true,
                    Data = students,
                    Pagination = new { CurrentPage = page, TotalItems = totalItems, PageSize = pageSize }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An internal error occurred.", Error = ex.Message });
            }
        }

        // --- 1. CREATE: Create new student ---
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentFormDto request)
        {
            // 1. Validate Model State (Data Annotations)
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Success = false, Message = "Validation failed.", Errors = ModelState });
            }

            try
            {
                // 2. Validate Foreign Key (School exists)
                var schoolExists = await _context.Schools.AnyAsync(s => s.Id == request.SchoolId);
                if (!schoolExists)
                {
                    return BadRequest(new { Success = false, Message = "The selected School does not exist." });
                }

                // 3. Validate Uniqueness (StudentId and Email)
                if (await _context.Students.AnyAsync(s => s.StudentId == request.StudentId))
                {
                    return BadRequest(new { Success = false, Message = "Student ID already exists." });
                }
                if (await _context.Students.AnyAsync(s => s.Email == request.Email))
                {
                    return BadRequest(new { Success = false, Message = "Email already exists." });
                }

                // 4. Map DTO to Entity
                var newStudent = new Student
                {
                    SchoolId = request.SchoolId,
                    FullName = request.FullName,
                    StudentId = request.StudentId,
                    Email = request.Email,
                    Phone = request.Phone
                };

                // 5. Save to DB
                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAll), new { id = newStudent.Id }, new { Success = true, Message = "Student created successfully.", Data = newStudent });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Failed to create student.", Error = ex.Message });
            }
        }

        // --- 3. UPDATE: Update existing student ---
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentFormDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Success = false, Message = "Validation failed.", Errors = ModelState });
            }

            try
            {
                var existingStudent = await _context.Students.FindAsync(id);
                if (existingStudent == null)
                {
                    return NotFound(new { Success = false, Message = "Student not found." });
                }

                // Validate School existence
                if (!await _context.Schools.AnyAsync(s => s.Id == request.SchoolId))
                {
                    return BadRequest(new { Success = false, Message = "The selected School does not exist." });
                }

                // Validate Uniqueness (Exclude current student)
                if (await _context.Students.AnyAsync(s => s.StudentId == request.StudentId && s.Id != id))
                {
                    return BadRequest(new { Success = false, Message = "Student ID is already taken by another student." });
                }
                if (await _context.Students.AnyAsync(s => s.Email == request.Email && s.Id != id))
                {
                    return BadRequest(new { Success = false, Message = "Email is already taken by another student." });
                }

                // Update fields
                existingStudent.FullName = request.FullName;
                existingStudent.SchoolId = request.SchoolId;
                existingStudent.StudentId = request.StudentId;
                existingStudent.Email = request.Email;
                existingStudent.Phone = request.Phone;

                // Save changes (UpdatedAt will be handled by DbContext)
                _context.Students.Update(existingStudent);
                await _context.SaveChangesAsync();

                return Ok(new { Success = true, Message = "Student updated successfully.", Data = existingStudent });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Failed to update student.", Error = ex.Message });
            }
        }

        // --- 4. DELETE: Remove student ---
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound(new { Success = false, Message = "Student not found." });
                }

                // EF Core handles deletion
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok(new { Success = true, Message = "Student deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Failed to delete student.", Error = ex.Message });
            }
        }
    }
}